﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Core2D.Attributes;
using Core2D.Shapes;

namespace Core2D.Containers
{
    /// <summary>
    /// Layer container.
    /// </summary>
    public class LayerContainer : ObservableObject, ILayerContainer
    {
        /// <inheritdoc/>
        public event InvalidateLayerEventHandler InvalidateLayer;

        private bool _isVisible = true;
        private ImmutableArray<IBaseShape> _shapes;

        /// <inheritdoc/>
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                Update(ref _isVisible, value);
                Invalidate();
            }
        }

        /// <inheritdoc/>
        [Content]
        public ImmutableArray<IBaseShape> Shapes
        {
            get => _shapes;
            set => Update(ref _shapes, value);
        }

        /// <inheritdoc/>
        public void Invalidate() => InvalidateLayer?.Invoke(this, new InvalidateLayerEventArgs());

        /// <inheritdoc/>
        public override object Copy(IDictionary<object, object> shared)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check whether the <see cref="IsVisible"/> property has changed from its default value.
        /// </summary>
        /// <returns>Returns true if the property has changed; otherwise, returns false.</returns>
        public virtual bool ShouldSerializeIsVisible() => _isVisible != default;

        /// <summary>
        /// Check whether the <see cref="Shapes"/> property has changed from its default value.
        /// </summary>
        /// <returns>Returns true if the property has changed; otherwise, returns false.</returns>
        public virtual bool ShouldSerializeShapes() => true;
    }
}
