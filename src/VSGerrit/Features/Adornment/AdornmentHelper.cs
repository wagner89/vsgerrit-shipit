using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;

namespace VSGerrit.Features.Adornment
{
    public class AdornmentHelper
    {
        /// <summary>
        /// This will get the text of the ITextView line as it appears in the actual user editable
        /// document.
        /// jared parson: https://gist.github.com/4320643
        /// </summary>
        public static bool TryGetText(IWpfTextView textView, ITextViewLine textViewLine, out string text)
        {
            var extent = textViewLine.Extent;
            var bufferGraph = textView.BufferGraph;
            try
            {
                var collection = bufferGraph.MapDownToSnapshot(extent, SpanTrackingMode.EdgeInclusive, textView.TextSnapshot);
                var span = new SnapshotSpan(collection[0].Start, collection[collection.Count - 1].End);
                text = span.GetText();
                return true;
            }
            catch
            {
                text = null;
                return false;
            }
        }
    }
}