using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace MMASMSyntaxHighlighting.Classification
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "masmComment")]
    [Name("masmComment")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class AsmCommentFormat : ClassificationFormatDefinition
    {
        public AsmCommentFormat()
        {
            DisplayName = "MASM Comment";
            ForegroundColor = Colors.Orange;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "masmInstruction")]
    [Name("masmInstruction")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class AsmInstructionFormat : ClassificationFormatDefinition
    {
        public AsmInstructionFormat()
        {
            DisplayName = "MASM Instruction";
            ForegroundColor = Colors.Red;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "masmDirective")]
    [Name("masmDirective")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class AsmDirectiveFormat : ClassificationFormatDefinition
    {
        public AsmDirectiveFormat()
        {
            DisplayName = "MASM Directive";
            ForegroundColor = Colors.Pink;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "masmRegister")]
    [Name("masmRegister")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class AsmRegisterFormat : ClassificationFormatDefinition
    {
        public AsmRegisterFormat()
        {
            DisplayName = "MASM Register";
            ForegroundColor = Colors.Beige;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "masmSymbol")]
    [Name("masmSymbol")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class AsmSymbolFormat : ClassificationFormatDefinition
    {
        public AsmSymbolFormat()
        {
            DisplayName = "MASM Symbol";
            ForegroundColor = Colors.MediumSeaGreen;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "masmOperator")]
    [Name("masmOperator")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class AsmOperatorFormat : ClassificationFormatDefinition
    {
        public AsmOperatorFormat()
        {
            DisplayName = "MASM Operator";
            ForegroundColor = Colors.Crimson;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "masmOperator")]
    [Name("masmRuntimeOperator")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class AsmRuntimeOperatorFormat : ClassificationFormatDefinition
    {
        public AsmRuntimeOperatorFormat()
        {
            DisplayName = "MASM Runtime Operator";
            ForegroundColor = Colors.YellowGreen;
        }
    }
}