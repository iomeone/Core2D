﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core2D.Editor.Input;
using Core2D.Editor.Tools.Path.Settings;
using Core2D.Editor.Tools.Selection;
using Core2D.Interfaces;
using Core2D.Path;
using Core2D.Path.Segments;
using Core2D.Shapes;
using static System.Math;

namespace Core2D.Editor.Tools.Path
{
    /// <summary>
    /// Arc path tool.
    /// </summary>
    public class PathToolArc : ObservableObject, IPathTool
    {
        public enum State { Start, End }
        private readonly IServiceProvider _serviceProvider;
        private PathToolSettingsArc _settings;
        private State _currentState = State.Start;
        private LineShape _arc = new LineShape();
        private ToolLineSelection _selection;
        private const double _defaultRotationAngle = 0.0;
        private const bool _defaultIsLargeArc = false;
        private const SweepDirection _defaultSweepDirection = SweepDirection.Clockwise;

        /// <inheritdoc/>
        public string Title => "Arc";

        /// <summary>
        /// Gets or sets the path tool settings.
        /// </summary>
        public PathToolSettingsArc Settings
        {
            get => _settings;
            set => Update(ref _settings, value);
        }

        /// <summary>
        /// Initialize new instance of <see cref="PathToolArc"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public PathToolArc(IServiceProvider serviceProvider) : base()
        {
            _serviceProvider = serviceProvider;
            _settings = new PathToolSettingsArc();
        }

        /// <inheritdoc/>
        public override object Copy(IDictionary<object, object> shared)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void LeftDown(InputArgs args)
        {
            var factory = _serviceProvider.GetService<IFactory>();
            var editor = _serviceProvider.GetService<IProjectEditor>();
            var pathTool = _serviceProvider.GetService<ToolPath>();
            (double sx, double sy) = editor.TryToSnap(args);
            switch (_currentState)
            {
                case State.Start:
                    {
                        _arc.Start = editor.TryToGetConnectionPoint(sx, sy) ?? factory.CreatePointShape(sx, sy, editor.Project.Options.PointShape);
                        if (!pathTool.IsInitialized)
                        {
                            pathTool.InitializeWorkingPath(_arc.Start);
                        }
                        else
                        {
                            _arc.Start = pathTool.GetLastPathPoint();
                        }

                        _arc.End = factory.CreatePointShape(sx, sy, editor.Project.Options.PointShape);
                        pathTool.GeometryContext.ArcTo(
                            _arc.End,
                            factory.CreatePathSize(
                                Abs(_arc.Start.X - _arc.End.X),
                                Abs(_arc.Start.Y - _arc.End.Y)),
                            _defaultRotationAngle,
                            _defaultIsLargeArc,
                            _defaultSweepDirection,
                            editor.Project.Options.DefaultIsStroked,
                            editor.Project.Options.DefaultIsSmoothJoin);
                        editor.Project.CurrentContainer.WorkingLayer.Invalidate();
                        ToStateEnd();
                        Move(null);
                        _currentState = State.End;
                        editor.IsToolIdle = false;
                    }
                    break;
                case State.End:
                    {
                        _arc.End.X = sx;
                        _arc.End.Y = sy;
                        if (editor.Project.Options.TryToConnect)
                        {
                            var end = editor.TryToGetConnectionPoint(sx, sy);
                            if (end != null)
                            {
                                _arc.End = end;
                            }
                        }
                        _arc.Start = _arc.End;
                        _arc.End = factory.CreatePointShape(sx, sy, editor.Project.Options.PointShape);
                        pathTool.GeometryContext.ArcTo(
                            _arc.End,
                            factory.CreatePathSize(
                                Abs(_arc.Start.X - _arc.End.X),
                                Abs(_arc.Start.Y - _arc.End.Y)),
                            _defaultRotationAngle,
                            _defaultIsLargeArc,
                            _defaultSweepDirection,
                            editor.Project.Options.DefaultIsStroked,
                            editor.Project.Options.DefaultIsSmoothJoin);
                        editor.Project.CurrentContainer.WorkingLayer.Invalidate();
                        Move(null);
                        _currentState = State.End;
                    }
                    break;
            }
        }

        /// <inheritdoc/>
        public void LeftUp(InputArgs args)
        {
        }

        /// <inheritdoc/>
        public void RightDown(InputArgs args)
        {
            switch (_currentState)
            {
                case State.Start:
                    break;
                case State.End:
                    Reset();
                    break;
            }
        }

        /// <inheritdoc/>
        public void RightUp(InputArgs args)
        {
        }

        /// <inheritdoc/>
        public void Move(InputArgs args)
        {
            var editor = _serviceProvider.GetService<IProjectEditor>();
            var pathTool = _serviceProvider.GetService<ToolPath>();
            (double sx, double sy) = editor.TryToSnap(args);
            switch (_currentState)
            {
                case State.Start:
                    {
                        if (editor.Project.Options.TryToConnect)
                        {
                            editor.TryToHoverShape(sx, sy);
                        }
                    }
                    break;
                case State.End:
                    {
                        if (editor.Project.Options.TryToConnect)
                        {
                            editor.TryToHoverShape(sx, sy);
                        }
                        _arc.End.X = sx;
                        _arc.End.Y = sy;
                        var figure = pathTool.Geometry.Figures.LastOrDefault();
                        var arc = figure.Segments.LastOrDefault() as ArcSegment;
                        arc.Point = _arc.End;
                        arc.Size.Width = Abs(_arc.Start.X - _arc.End.X);
                        arc.Size.Height = Abs(_arc.Start.Y - _arc.End.Y);
                        editor.Project.CurrentContainer.WorkingLayer.Invalidate();
                        Move(null);
                    }
                    break;
            }
        }

        /// <summary>
        /// Transfer tool state to <see cref="State.End"/>.
        /// </summary>
        public void ToStateEnd()
        {
            var editor = _serviceProvider.GetService<IProjectEditor>();
            _selection?.Reset();
            _selection = new ToolLineSelection(
                _serviceProvider,
                editor.Project.CurrentContainer.HelperLayer,
                _arc,
                editor.Project.Options.HelperStyle,
                editor.Project.Options.PointShape);

            _selection.ToStateEnd();
        }

        /// <inheritdoc/>
        public void Move(IBaseShape shape)
        {
            if (_selection != null)
            {
                _selection.Move();
            }
        }

        /// <inheritdoc/>
        public void Finalize(IBaseShape shape)
        {
        }

        /// <inheritdoc/>
        public void Reset()
        {
            var editor = _serviceProvider.GetService<IProjectEditor>();
            var pathTool = _serviceProvider.GetService<ToolPath>();

            switch (_currentState)
            {
                case State.Start:
                    break;
                case State.End:
                    {
                        pathTool.RemoveLastSegment<ArcSegment>();
                        editor.Project.CurrentContainer.WorkingLayer.Shapes = editor.Project.CurrentContainer.WorkingLayer.Shapes.Remove(pathTool.Path);
                        if (pathTool.Path.Geometry.Figures.LastOrDefault().Segments.Length > 0)
                        {
                            Finalize(null);
                            editor.Project.AddShape(editor.Project.CurrentContainer.CurrentLayer, pathTool.Path);
                        }
                        else
                        {
                            editor.Project.CurrentContainer.WorkingLayer.Invalidate();
                        }
                    }
                    break;
            }

            _currentState = State.Start;
            editor.IsToolIdle = true;

            if (_selection != null)
            {
                _selection.Reset();
                _selection = null;
            }
        }
    }
}
