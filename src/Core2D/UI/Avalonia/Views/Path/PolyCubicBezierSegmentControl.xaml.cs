﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Core2D.UI.Avalonia.Views.Path
{
    /// <summary>
    /// Interaction logic for <see cref="PolyCubicBezierSegmentControl"/> xaml.
    /// </summary>
    public class PolyCubicBezierSegmentControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolyCubicBezierSegmentControl"/> class.
        /// </summary>
        public PolyCubicBezierSegmentControl()
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
