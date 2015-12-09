using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using VSGerrit.Common.Services;
using VSGerrit.Features.Adornment.CommentPopup;

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
            var currentFilename = GetFilenameFromTextBuffer(_view.TextBuffer);

            var change = ChangeCommentService.Instance.GetCommentsForCurrentChange();

            if (!change.CommentedFiles.Any(file => file.Contains(currentFilename)))
                return;

            foreach (var comment in change.GetCommentsForFile(currentFilename))
            {
                var line = _view.TextBuffer.CurrentSnapshot.Lines.ElementAt(comment.LineNumber - 1);
                CreateVisuals(line, comment.CommentText);
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

        private void CreateVisuals(ITextSnapshotLine line, string comment)
        {
            if (line == null)
                return;

            var textViewLines = _view.TextViewLines;
            var spanToHighlight = Span.FromBounds(line.Start.Position,
                                                  line.End.Position == decimal.Zero ? 10 : line.End.Position);

            var snapshotSpan = new SnapshotSpan(_view.TextSnapshot, spanToHighlight);
            SetBoundary(textViewLines, snapshotSpan);
            AddComment(textViewLines, snapshotSpan, line, comment);
        }

        private void AddComment(IWpfTextViewLineCollection textViewLines, SnapshotSpan span, ITextSnapshotLine line, string comment)
        {
            var g = textViewLines.GetMarkerGeometry(span);
            if (g == null) return;

            var commentPopup = new CommentPopup.CommentPopup(new CommentPopupViewModel(comment));
            Canvas.SetTop(commentPopup, g.Bounds.Top - 2);
            Canvas.SetLeft(commentPopup, g.Bounds.Right + 5);
            _layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, line.Extent, null, commentPopup, null);
        }

        private void SetBoundary(IWpfTextViewLineCollection textViewLines, SnapshotSpan span)
        {
            var g = textViewLines.GetMarkerGeometry(span);
            if (g == null) return;

            var drawing = new GeometryDrawing(_brush, _pen, g);
            drawing.Freeze();

            var drawingImage = new DrawingImage(drawing);
            drawingImage.Freeze();

            var image = new Image { Source = drawingImage };

            //Align the image with the top of the bounds of the text geometry
            Canvas.SetLeft(image, g.Bounds.Left);
            Canvas.SetTop(image, g.Bounds.Top);

            _layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, span, null, image, null);
        }
    }
}