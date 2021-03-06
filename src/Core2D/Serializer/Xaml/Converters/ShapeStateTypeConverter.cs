﻿using System;
using System.ComponentModel;
using System.Globalization;
using Core2D.Renderer;

namespace Core2D.Serializer.Xaml.Converters
{
    /// <summary>
    /// Defines <see cref="ShapeState"/> type converter.
    /// </summary>
    internal class ShapeStateTypeConverter : TypeConverter
    {
        /// <inheritdoc/>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <inheritdoc/>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        /// <inheritdoc/>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return ShapeState.Parse((string)value);
        }

        /// <inheritdoc/>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return value is ShapeState state ? state.ToString() : throw new NotSupportedException();
        }
    }
}
