﻿using System;
using System.IO;
using Core2D.Containers;
using Core2D.Interfaces;
using Core2D.Renderer;
using SkiaSharp;

namespace Core2D.FileWriter.SkiaSharpPng
{
    /// <summary>
    /// SkiaSharp png <see cref="IProjectExporter"/> implementation.
    /// </summary>
    public sealed class PngSkiaSharpExporter : IProjectExporter
    {
        private readonly IShapeRenderer _renderer;
        private readonly IContainerPresenter _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PngSkiaSharpExporter"/> class.
        /// </summary>
        /// <param name="renderer">The shape renderer.</param>
        /// <param name="presenter">The container presenter.</param>
        public PngSkiaSharpExporter(IShapeRenderer renderer, IContainerPresenter presenter)
        {
            _renderer = renderer;
            _presenter = presenter;
        }

        /// <inheritdoc/>
        public void Save(Stream stream, IPageContainer container)
        {
            var info = new SKImageInfo((int)container.Width, (int)container.Height);
            using var bitmap = new SKBitmap(info);
            using (var canvas = new SKCanvas(bitmap))
            {
                _presenter.Render(canvas, _renderer, container, 0, 0);
            }
            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            data.SaveTo(stream);
        }

        /// <inheritdoc/>
        public void Save(Stream stream, IDocumentContainer document)
        {
            throw new NotSupportedException("Saving documents as png drawing is not supported.");
        }

        /// <inheritdoc/>
        public void Save(Stream stream, IProjectContainer project)
        {
            throw new NotSupportedException("Saving projects as png drawing is not supported.");
        }
    }
}
