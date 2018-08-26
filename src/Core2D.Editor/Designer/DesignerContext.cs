﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Immutable;
using System.Linq;
using Core2D.Containers;
using Core2D.Data;
using Core2D.Editor.Recent;
using Core2D.Interfaces;
using Core2D.Path;
using Core2D.Path.Segments;
using Core2D.Renderer;
using Core2D.Shapes;
using Core2D.Style;

namespace Core2D.Editor.Designer
{
    /// <summary>
    /// Design time DataContext base class.
    /// </summary>
    public class DesignerContext
    {
        /// <summary>
        /// The design time <see cref="ProjectEditor"/>.
        /// </summary>
        public static ProjectEditor Editor { get; set; }

        /// <summary>
        /// The design time <see cref="IMatrixObject"/> template.
        /// </summary>
        public static IMatrixObject Transform { get; set; }

        /// <summary>
        /// The design time <see cref="IPageContainer"/> template.
        /// </summary>
        public static IPageContainer Template { get; set; }

        /// <summary>
        /// The design time <see cref="IPageContainer"/> page.
        /// </summary>
        public static IPageContainer Page { get; set; }

        /// <summary>
        /// The design time <see cref="IDocumentContainer"/>.
        /// </summary>
        public static IDocumentContainer Document { get; set; }

        /// <summary>
        /// The design time <see cref="ILayerContainer"/>.
        /// </summary>
        public static ILayerContainer Layer { get; set; }

        /// <summary>
        /// The design time <see cref="IOptions"/>.
        /// </summary>
        public static IOptions Options { get; set; }

        /// <summary>
        /// The design time <see cref="IProjectContainer"/>.
        /// </summary>
        public static IProjectContainer Project { get; set; }

        /// <summary>
        /// The design time <see cref="ILibrary{ShapeStyle}"/>.
        /// </summary>
        public static ILibrary<IShapeStyle> CurrentStyleLibrary { get; set; }

        /// <summary>
        /// The design time <see cref="ILibrary{IGroupShape}"/>.
        /// </summary>
        public static ILibrary<IGroupShape> CurrentGroupLibrary { get; set; }

        /// <summary>
        /// The design time <see cref="IShapeState"/>.
        /// </summary>
        public static IShapeState State { get; set; }

        /// <summary>
        /// The design time <see cref="Data.IDatabase"/>.
        /// </summary>
        public static IDatabase Database { get; set; }

        /// <summary>
        /// The design time <see cref="Data.IContext"/>.
        /// </summary>
        public static IContext Data { get; set; }

        /// <summary>
        /// The design time <see cref="Data.IRecord"/>.
        /// </summary>
        public static IRecord Record { get; set; }

        /// <summary>
        /// The design time <see cref="Style.ArgbColor"/>.
        /// </summary>
        public static IArgbColor ArgbColor { get; set; }

        /// <summary>
        /// The design time <see cref="Style.IArrowStyle"/>.
        /// </summary>
        public static IArrowStyle ArrowStyle { get; set; }

        /// <summary>
        /// The design time <see cref="Style.IFontStyle"/>.
        /// </summary>
        public static IFontStyle FontStyle { get; set; }

        /// <summary>
        /// The design time <see cref="Style.ILineFixedLength"/>.
        /// </summary>
        public static ILineFixedLength LineFixedLength { get; set; }

        /// <summary>
        /// The design time <see cref="Style.ILineStyle"/>.
        /// </summary>
        public static ILineStyle LineStyle { get; set; }

        /// <summary>
        /// The design time <see cref="Style.IShapeStyle"/>.
        /// </summary>
        public static IShapeStyle Style { get; set; }

        /// <summary>
        /// The design time <see cref="Style.ITextStyle"/>.
        /// </summary>
        public static ITextStyle TextStyle { get; set; }

        /// <summary>
        /// The design time <see cref="IArcShape"/>.
        /// </summary>
        public static IArcShape Arc { get; set; }

        /// <summary>
        /// The design time <see cref="ICubicBezierShape"/>.
        /// </summary>
        public static ICubicBezierShape CubicBezier { get; set; }

        /// <summary>
        /// The design time <see cref="IEllipseShape"/>.
        /// </summary>
        public static IEllipseShape Ellipse { get; set; }

        /// <summary>
        /// The design time <see cref="IGroupShape"/>.
        /// </summary>
        public static IGroupShape Group { get; set; }

        /// <summary>
        /// The design time <see cref=IImageShape"/>.
        /// </summary>
        public static IImageShape Image { get; set; }

        /// <summary>
        /// The design time <see cref="ILineShape"/>.
        /// </summary>
        public static ILineShape Line { get; set; }

        /// <summary>
        /// The design time <see cref="IPathShape"/>.
        /// </summary>
        public static IPathShape Path { get; set; }

        /// <summary>
        /// The design time <see cref="IPointShape"/>.
        /// </summary>
        public static IPointShape Point { get; set; }

        /// <summary>
        /// The design time <see cref="IQuadraticBezierShape"/>.
        /// </summary>
        public static IQuadraticBezierShape QuadraticBezier { get; set; }

        /// <summary>
        /// The design time <see cref="IRectangleShape"/>.
        /// </summary>
        public static IRectangleShape Rectangle { get; set; }

        /// <summary>
        /// The design time <see cref="ITextShape"/>.
        /// </summary>
        public static ITextShape Text { get; set; }

        /// <summary>
        /// The design time <see cref="Path.Segments.ArcSegment"/>.
        /// </summary>
        public static ArcSegment ArcSegment { get; set; }

        /// <summary>
        /// The design time <see cref="Path.Segments.CubicBezierSegment"/>.
        /// </summary>
        public static CubicBezierSegment CubicBezierSegment { get; set; }

        /// <summary>
        /// The design time <see cref="Path.Segments.LineSegment"/>.
        /// </summary>
        public static LineSegment LineSegment { get; set; }

        /// <summary>
        /// The design time <see cref="IPathFigure"/>.
        /// </summary>
        public static IPathFigure PathFigure { get; set; }

        /// <summary>
        /// The design time <see cref="IPathGeometry"/>.
        /// </summary>
        public static IPathGeometry PathGeometry { get; set; }

        /// <summary>
        /// The design time <see cref="IPathSize"/>.
        /// </summary>
        public static IPathSize PathSize { get; set; }

        /// <summary>
        /// The design time <see cref="Path.Segments.PolyCubicBezierSegment"/>.
        /// </summary>
        public static PolyCubicBezierSegment PolyCubicBezierSegment { get; set; }

        /// <summary>
        /// The design time <see cref="Path.Segments.PolyLineSegment"/>.
        /// </summary>
        public static PolyLineSegment PolyLineSegment { get; set; }

        /// <summary>
        /// The design time <see cref="Path.Segments.PolyQuadraticBezierSegment"/>.
        /// </summary>
        public static PolyQuadraticBezierSegment PolyQuadraticBezierSegment { get; set; }

        /// <summary>
        /// The design time <see cref="Path.Segments.QuadraticBezierSegment"/>.
        /// </summary>
        public static QuadraticBezierSegment QuadraticBezierSegment { get; set; }

        /// <summary>
        /// Initializes static designer context.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static void InitializeContext(IServiceProvider serviceProvider)
        {
            // Editor

            Editor = serviceProvider.GetService<ProjectEditor>();

            // Recent Projects

            Editor.RecentProjects = Editor.RecentProjects.Add(RecentFile.Create("Test1", "Test1.project"));
            Editor.RecentProjects = Editor.RecentProjects.Add(RecentFile.Create("Test2", "Test2.project"));

            // New Project

            Editor.OnNewProject();

            // Transform

            Transform = MatrixObject.Identity;

            // Data

            var db = Core2D.Data.Database.Create("Db");
            var fields = new string[] { "Column0", "Column1" };
            var columns = ImmutableArray.CreateRange(fields.Select(c => Column.Create(db, c)));
            db.Columns = columns;
            var values = Enumerable.Repeat("<empty>", db.Columns.Length).Select(c => Value.Create(c));
            var record = Core2D.Data.Record.Create(
                db,
                ImmutableArray.CreateRange(values));
            db.Records = db.Records.Add(record);
            db.CurrentRecord = record;

            Database = db;
            Data = Core2D.Data.Context.Create(record);
            Record = record;

            // Project

            var containerFactory = serviceProvider.GetService<IContainerFactory>();
            var shapeFactory = serviceProvider.GetService<IShapeFactory>();

            Project = containerFactory.GetProject();

            Template = PageContainer.CreateTemplate();

            Page = PageContainer.CreatePage();
            var layer = Page.Layers.FirstOrDefault();
            layer.Shapes = layer.Shapes.Add(LineShape.Create(0, 0, null, null));
            Page.CurrentLayer = layer;
            Page.CurrentShape = layer.Shapes.FirstOrDefault();
            Page.Template = Template;

            Document = DocumentContainer.Create();
            Layer = LayerContainer.Create();
            Options = Containers.Options.Create();

            CurrentStyleLibrary = Project.CurrentStyleLibrary;
            CurrentGroupLibrary = Project.CurrentGroupLibrary;

            // State

            State = ShapeState.Create();

            // Style

            ArgbColor = Core2D.Style.ArgbColor.Create(128, 255, 0, 0);
            ArrowStyle = Core2D.Style.ArrowStyle.Create();
            FontStyle = Core2D.Style.FontStyle.Create();
            LineFixedLength = Core2D.Style.LineFixedLength.Create();
            LineStyle = Core2D.Style.LineStyle.Create();
            Style = Core2D.Style.ShapeStyle.Create("Default");
            TextStyle = Core2D.Style.TextStyle.Create();

            // Shapes

            Arc = ArcShape.Create(0, 0, Style, null);
            CubicBezier = CubicBezierShape.Create(0, 0, Style, null);
            Ellipse = EllipseShape.Create(0, 0, Style, null);
            Group = GroupShape.Create(Constants.DefaulGroupName);
            Image = ImageShape.Create(0, 0, Style, null, "key");
            Line = LineShape.Create(0, 0, Style, null);
            Path = PathShape.Create(Style, null);
            Point = PointShape.Create();
            QuadraticBezier = QuadraticBezierShape.Create(0, 0, Style, null);
            Rectangle = RectangleShape.Create(0, 0, Style, null);
            Text = TextShape.Create(0, 0, Style, null, "Text");

            // Path

            ArcSegment = ArcSegment.Create(PointShape.Create(), Core2D.Path.PathSize.Create(), 180, true, SweepDirection.Clockwise, true, true);
            CubicBezierSegment = CubicBezierSegment.Create(PointShape.Create(), PointShape.Create(), PointShape.Create(), true, true);
            LineSegment = LineSegment.Create(PointShape.Create(), true, true);
            PathFigure = Core2D.Path.PathFigure.Create(PointShape.Create(), false, true);
            PathGeometry = Core2D.Path.PathGeometry.Create(ImmutableArray.Create<IPathFigure>(), FillRule.EvenOdd);
            PathSize = Core2D.Path.PathSize.Create();
            PolyCubicBezierSegment = PolyCubicBezierSegment.Create(ImmutableArray.Create<IPointShape>(), true, true);
            PolyLineSegment = PolyLineSegment.Create(ImmutableArray.Create<IPointShape>(), true, true);
            PolyQuadraticBezierSegment = PolyQuadraticBezierSegment.Create(ImmutableArray.Create<IPointShape>(), true, true);
            QuadraticBezierSegment = QuadraticBezierSegment.Create(PointShape.Create(), PointShape.Create(), true, true);
        }
    }
}
