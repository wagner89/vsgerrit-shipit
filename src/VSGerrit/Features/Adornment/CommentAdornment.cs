using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace VSGerrit.Features.Adornment
{
    internal sealed class CommentAdornment
    {
        private const string AdornmentName = "CommentAdornment";
        
        private readonly IAdornmentLayer _layer;

        private readonly IWpfTextView _view;

        private Brush _brush;

        private Pen _pen;

        public CommentAdornment(IWpfTextView view)
        {
            if (view == null) throw new ArgumentNullException(nameof(view));

            SetupBrushes();

            _layer = view.GetAdornmentLayer(AdornmentName);
            _view = view;
            _view.LayoutChanged += OnLayoutChanged;
        }

        private void SetupBrushes()
        {
            _brush = new SolidColorBrush(Color.FromArgb(0x20, 0x00, 0x00, 0xff));
            _brush.Freeze();

            var penBrush = new SolidColorBrush(Colors.Gainsboro);
            penBrush.Freeze();
            _pen = new Pen(penBrush, 0.5);
            _pen.Freeze();
        }

        internal void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            Dictionary<string, IEnumerable<int>> highlighDictionarty = new Dictionary<string, IEnumerable<int>>();
            //TODO: populate dictionary from simpleton
            highlighDictionarty["CommentAdornment.cs"] = new List<int> { 1, 3, 7, 15 };

            var currentFilename = GetFilenameFromTextBuffer(_view.TextBuffer);

            if (!highlighDictionarty.ContainsKey(currentFilename))
                return;

            var linesToHighlight = highlighDictionarty[currentFilename];
            for (var currentLineIndex = 1; currentLineIndex < _view.TextBuffer.CurrentSnapshot.LineCount; currentLineIndex++)
            {
                var line = _view.TextBuffer.CurrentSnapshot.Lines.ElementAt(currentLineIndex - 1);

                if (linesToHighlight.Contains(currentLineIndex))
                {
                    CreateVisuals(line);
                }
            }
        }

        private string GetFilenameFromTextBuffer(ITextBuffer textBuffer)
        {
            ITextDocument textDocument;
            var filename = string.Empty;
            var success = textBuffer.Properties.TryGetProperty(typeof(ITextDocument), out textDocument);
            if (success)
            {
                filename = new Uri(textDocument.FilePath).Segments.Last();
            }
            return filename;
        }

        private void CreateVisuals(ITextSnapshotLine line)
        {
            if (line == null)
                return;

            var textViewLines = _view.TextViewLines;
            var spanToHighlight = Span.FromBounds(line.Start.Position,
                                                  line.End.Position == decimal.Zero ? 10 : line.End.Position);

            var snapshotSpan = new SnapshotSpan(_view.TextSnapshot, spanToHighlight);
            SetBoundary(textViewLines, snapshotSpan);
            AddComment(textViewLines, snapshotSpan, line);
        }

        private void AddComment(IWpfTextViewLineCollection textViewLines, SnapshotSpan span, ITextSnapshotLine line)
        {
            Geometry g = textViewLines.GetMarkerGeometry(span);
            var commentPopup = new CommentPopup.CommentPopup();
            Canvas.SetTop(commentPopup, g.Bounds.Bottom);
            Canvas.SetLeft(commentPopup, g.Bounds.Left);
            _layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, line.Extent, null, commentPopup, null);
        }

        private void SetBoundary(IWpfTextViewLineCollection textViewLines, SnapshotSpan span)
        {
            Geometry g = textViewLines.GetMarkerGeometry(span);
            if (g != null)
            {
                GeometryDrawing drawing = new GeometryDrawing(_brush, _pen, g);
                drawing.Freeze();

                DrawingImage drawingImage = new DrawingImage(drawing);
                drawingImage.Freeze();

                Image image = new Image();
                image.Source = drawingImage;

                //Align the image with the top of the bounds of the text geometry
                Canvas.SetLeft(image, g.Bounds.Left);
                Canvas.SetTop(image, g.Bounds.Top);

                _layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, span, null, image, null);
            }
        }
    }
}