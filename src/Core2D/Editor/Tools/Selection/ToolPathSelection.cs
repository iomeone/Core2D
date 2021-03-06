﻿using Core2D.Containers;
using Core2D.Shapes;
using Core2D.Style;

namespace Core2D.Editor.Tools.Selection
{
    /// <summary>
    /// Helper class for <see cref="IPathShape"/> shape selection.
    /// </summary>
    public class ToolPathSelection
    {
        private readonly ILayerContainer _layer;
        private readonly IPathShape _path;
        private readonly IShapeStyle _style;
        private readonly IBaseShape _point;

        /// <summary>
        /// Initialize new instance of <see cref="ToolPathSelection"/> class.
        /// </summary>
        /// <param name="layer">The selection shapes layer.</param>
        /// <param name="shape">The selected shape.</param>
        /// <param name="style">The selection shapes style.</param>
        /// <param name="point">The selection point shape.</param>
        public ToolPathSelection(ILayerContainer layer, IPathShape shape, IShapeStyle style, IBaseShape point)
        {
            _layer = layer;
            _path = shape;
            _style = style;
            _point = point;
        }
    }
}
