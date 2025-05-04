using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace MASMSyntaxHighlighting.Classification
{
    internal static class ClassificationTypes
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("masmComment")]
        internal static ClassificationTypeDefinition AsmCommentType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("masmInstruction")]
        internal static ClassificationTypeDefinition AsmInstructionType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("masmDirective")]
        internal static ClassificationTypeDefinition AsmDirectiveType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("masmRegister")]
        internal static ClassificationTypeDefinition AsmRegisterType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("masmSymbol")]
        internal static ClassificationTypeDefinition AsmSymbolType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("masmOperator")]
        internal static ClassificationTypeDefinition AsmOperatorType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("masmRuntimeOperator")]
        internal static ClassificationTypeDefinition AsmRuntimeOperatorType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("masmProcedure")]
        internal static ClassificationTypeDefinition AsmProcedureType = null;
    }
}