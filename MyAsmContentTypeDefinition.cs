using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace MASMSyntaxHighlighting
{
    internal static class ContentTypes
    {
        [Export(typeof(ContentTypeDefinition))]
        [Name("asm")]
        [BaseDefinition("code")]
        public static ContentTypeDefinition AsmContentType = null;

        [Export(typeof(FileExtensionToContentTypeDefinition))]
        [ContentType("asm")]
        [FileExtension(".asm")]
        public static FileExtensionToContentTypeDefinition AsmFileExtension = null;
    }
}