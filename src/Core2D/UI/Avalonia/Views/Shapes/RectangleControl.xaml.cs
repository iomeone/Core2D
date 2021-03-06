﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Core2D.UI.Avalonia.Views.Shapes
{
    /// <summary>
    /// Interaction logic for <see cref="RectangleControl"/> xaml.
    /// </summary>
    public class RectangleControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleControl"/> class.
        /// </summary>
        public RectangleControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the Xaml components.
        /// </summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
