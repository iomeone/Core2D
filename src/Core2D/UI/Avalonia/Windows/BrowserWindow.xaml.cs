﻿using Avalonia;
using Avalonia.Markup.Xaml;
using Dock.Avalonia.Controls;

namespace Core2D.UI.Avalonia.Windows
{
    /// <summary>
    /// Interaction logic for <see cref="BrowserWindow"/> xaml.
    /// </summary>
    public class BrowserWindow : MetroWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserWindow"/> class.
        /// </summary>
        public BrowserWindow()
        {
            InitializeComponent();
            this.AttachDevTools();
            App.Selector.EnableThemes(this);
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
