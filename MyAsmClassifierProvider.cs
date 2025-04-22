using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace MASMSyntaxHighlighting
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("C/C++")]
    internal class MyAsmClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null;

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            return buffer.Properties.GetOrCreateSingletonProperty(() =>
                new AsmClassifier(ClassificationRegistry));
        }
    }
}