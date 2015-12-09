using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace VSGerrit.Features.Adornment
{
    internal sealed class CommentAdornment
    {
        private readonly IAdornmentLayer _layer;

        private readonly IWpfTextView _view;

        private readonly Brush _brush;

        private readonly Pen _pen;

        public CommentAdornment(IWpfTextView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            _layer = view.GetAdornmentLayer("CommentAdornment");

            _view = view;
            _view.LayoutChanged += OnLayoutChanged;

            // Create the _pen and _brush to color the box behind the a's
            _brush = new SolidColorBrush(Color.FromArgb(0x20, 0x00, 0x00, 0xff));
            _brush.Freeze();

            var penBrush = new SolidColorBrush(Colors.Gainsboro);
            penBrush.Freeze();
            _pen = new Pen(penBrush, 0.5);
            _pen.Freeze();
        }

        internal void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            foreach (ITextViewLine line in e.NewOrReformattedLines)
            {
                CreateVisuals(line);
            }
        }

        private void CreateVisuals(ITextViewLine line)
        {
            var textViewLines = _view.TextViewLines;

            // Loop through each character, and place a box around any 'a'
            for (int charIndex = line.Start; charIndex < line.End; charIndex++)
            {
                if (_view.TextSnapshot[charIndex] == 'w')
                {
                    var span = new SnapshotSpan(_view.TextSnapshot, Span.FromBounds(charIndex, charIndex + 1));
                    var geometry = textViewLines.GetMarkerGeometry(span);
                    if (geometry == null) continue;
                    var drawing = new GeometryDrawing(_brush, _pen, geometry);
                    drawing.Freeze();

                    var drawingImage = new DrawingImage(drawing);
                    drawingImage.Freeze();

                    var image = new Image
                    {
                        Source = drawingImage,
                    };

                    // Align the image with the top of the bounds of the text geometry
                    Canvas.SetLeft(image, geometry.Bounds.Left);
                    Canvas.SetTop(image, geometry.Bounds.Top);

                    _layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, span, null, image, null);
                }
            }
        }
    }
}