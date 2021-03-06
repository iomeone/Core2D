﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Core2D.Attributes;

namespace Core2D.Path
{
    /// <summary>
    /// Path geometry.
    /// </summary>
    public class PathGeometry : ObservableObject, IPathGeometry
    {
        private ImmutableArray<IPathFigure> _figures;
        private FillRule _fillRule;

        /// <inheritdoc/>
        [Content]
        public ImmutableArray<IPathFigure> Figures
        {
            get => _figures;
            set => Update(ref _figures, value);
        }

        /// <inheritdoc/>
        public FillRule FillRule
        {
            get => _fillRule;
            set => Update(ref _fillRule, value);
        }

        /// <inheritdoc/>
        public override object Copy(IDictionary<object, object> shared)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a string representation of figures collection.
        /// </summary>
        /// <param name="figures">The figures collection.</param>
        /// <returns>A string representation of figures collection.</returns>
        public string ToString(ImmutableArray<IPathFigure> figures)
        {
            if (figures.Length == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            for (int i = 0; i < figures.Length; i++)
            {
                sb.Append(figures[i]);
                if (i != figures.Length - 1)
                {
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            string figuresString = string.Empty;

            if (Figures.Length > 0)
            {
                figuresString = ToString(Figures);
            }

            if (FillRule == FillRule.Nonzero)
            {
                return "F1" + figuresString;
            }

            return figuresString;
        }

        /// <summary>
        /// Check whether the <see cref="Figures"/> property has changed from its default value.
        /// </summary>
        /// <returns>Returns true if the property has changed; otherwise, returns false.</returns>
        public virtual bool ShouldSerializeFigures() => true;

        /// <summary>
        /// Check whether the <see cref="FillRule"/> property has changed from its default value.
        /// </summary>
        /// <returns>Returns true if the property has changed; otherwise, returns false.</returns>
        public virtual bool ShouldSerializeFillRule() => _fillRule != default;
    }
}
