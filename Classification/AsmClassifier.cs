using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MASMSyntaxHighlighting
{
    internal class AsmClassifier : IClassifier
    {
        private readonly IClassificationType _commentType;
        private readonly IClassificationType _instructionType;
        private readonly IClassificationType _directiveType;
        private readonly IClassificationType _registerType;
        private readonly IClassificationType _symbolType;
        private readonly IClassificationType _operatorType;
        private readonly IClassificationType _runtimeOperatorType;

        private static readonly HashSet<string> Instructions = new HashSet<string>
        {
            "loopnew","loopnzw","loopew","loopne","loopnz","loopzw","cmpsb","cmpsw","lodsb","lodsw","loope","loopw","loopz","movsb","movsw","pushf","scasb","scasw","stosb","stosw","xlatb","call","cmps","idiv","imul","into","iret","jcxz","jnae","jnbe","jnge","jnle","lahf","lods","loop","popf","push","retf","retn","sahf","scas","stos","test","wait","xchg","xlat","aaa","aad","aam","aas","adc","add","cbw","clc","cld","cli","cmc","cmp","cwd","daa","das","dec","div","esc","hlt","inc","int","jae","jbe","jge","jle","jmp","jna","jnb","jnc","jne","jng","jnl","jno","jnp","jns","jnz","jpe","jpo","lds","lea","les","mov","mul","neg","nop","out","pop","rcl","rcr","ret","rol","ror","sal","sar","sbb","stc","std","sti","sub","in","ja","jb","jc","je","jg","jl","jo","jp","js","jz","rep", "repe", "repne", "movsd","movsq","stosd","stosq","lodsd","lodsq","scasd","scasq","cmpsd","cmpsq","xadd","cdq","cqo", "cwde","cdqe"
        };

        private static readonly HashSet<string> Directives = new HashSet<string>
        {
            "option avxencoding", "option language", ".listmacroall", ".nolistmacro", ".allocstack", "pushcontext", ".savexmm128", ".endprolog", "includelib", ".listmacro", "popcontext", ".pushframe", ".continue", "externdef", ".fardata?", ".nolistif", ".setframe", ".untilcxz", ".errdifi", ".erridni", ".errndef", ".fardata", ".listall", ".pushreg", ".safeseh", ".savereg", ".startup", "subtitle", "comment", ".dosseg", "elseif2", ".elseif2", ".errdef", ".errdif", ".erridn", "include", ".lfcond", ".listif", ".nocref", ".nolist", ".repeat", "segment", ".sfcond", "sizestr", "textequ", ".tfcond", "typedef", "xmmword", "ymmword", ".alpha", "assume", ".break", "catstr", ".const", ".data?", "dosseg", ".elseif", ".endif", ".errnb", ".errnz", "extern", "ifdifi", "ifidni", "ifndef", "invoke", "mmword", ".model", "option", "public", ".radix", "real10", "record", "sdword", "sqword", ".stack", "struct", "substr", "subttl", ".until", ".while", ".xcref", ".xlist", ".386p", ".486p", ".586p", ".686p", "alias", "align", ".code", ".cref", ".data", "dword", ".else", ".endw", ".err2", ".errb", ".erre", ".exit", "exitm", "extrn", "fword", "group", "ifdef", "ifdif", "ifidn", "instr", "label", ".lall", ".list", "local", ".macro", "oword", "proto", "purge", "qword", "real4", "real8", ".sall", "sbyte", "struc", "sword", "tbyte", "title", "union", ".xall", ".386", ".387", ".486", ".586", ".686", "byte", "comm", "echo", ".endm", "endp", "ends", ".err", "even", ".forc", ".fpo", "goto", "ifnb", "irpc", ".k3d", ".mmx", "name", "%out", "page", "proc", "rept", ".seq", "word", ".xmm", "end", "equ", "for", ".if", "if2", "ifb", "ife", "irp", "org", "db", "dd", "df", "dq", "dt", "dw", "=", "req", "vararg", ".extern",".public",".align",".include"
        };

        private static readonly HashSet<string> Registers = new HashSet<string>
        {
            "r10d", "r10w", "r11d", "r11w", "r12d", "r12w", "r13d", "r13w", "r14d", "r14w", "r15d", "r15w", "rax", "eax", "rbx", "ebx", "rcx", "ecx", "rdx", "edx", "rsi", "esi", "rdi", "edi", "rbp", "ebp", "rsp", "esp", "r8d", "r8w", "r9d", "r9w", "r10", "r10b", "r11", "r11b", "r12", "r12b", "r13", "r13b", "r14", "r14b", "r15", "r15b", "cr0", "cr2", "cr3", "dr0", "dr1", "dr2", "dr3", "dr6", "dr7", "eax", "ebp", "ebx", "ecx", "edi", "edx", "esi", "esp", "tr3", "tr4", "tr5", "tr6", "tr7", "ax", "ah", "bx", "bh", "cx", "ch", "dx", "dh", "si", "sil", "di", "dil", "bp", "bpl", "sp", "spl", "r8", "r8b", "r9", "r9b", "ah", "al", "ax", "bh", "bl", "bp", "bx", "ch", "cl", "cs", "cx", "dh", "di", "dl", "ds", "dx", "es", "fs", "gs", "si", "sp", "ss", "st"
        };

        private static readonly HashSet<string> Symbols = new HashSet<string>
        {
            "@interface", "@codesize", "@datasize", "@fardata?", "@filename", "@wordsize", "@environ", "@fardata", "@filecur", "@sizestr", "@version", "@catstr", "@curseg", "@substr", "@instr", "@model", "@stack", "@code", "@data", "@date", "@line", "@time", "@cpu", "@@", "@b", "@f", "$", "?"
        };

        private static readonly HashSet<string> Operators = new HashSet<string>
        {
            "sectionrel", "highword", "imagerel", "lengthof", "lroffset", "lowword", "high32", "length", "offset", "opattr", "sizeof", "low32", ".type", "short", "width", "addr", "high", "mask", "size", "this", "type", "& &", "abs", "and", "dup", "low", "mod", "not", "ptr", "seg", "shl", "shr", "xor", "[]", "<>", "\"\"", "''", ";;", "eq", "ge", "gt", "le", "lt", "ne", "or", "+", "-", "*", "/", ":", ".", "!", ";", "%"
        };

        private static readonly HashSet<string> RuntimeOperators = new HashSet<string>
        {
            "overflow?", "parity?", "carry?", "sign?", "zero?", "==", "!=", ">=", "<=", "||", "&&", ">", "<", "&"
        };

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        public AsmClassifier(IClassificationTypeRegistryService registry)
        {
            _commentType = registry.GetClassificationType("masmComment");
            _instructionType = registry.GetClassificationType("masmInstruction");
            _directiveType = registry.GetClassificationType("masmDirective");
            _registerType = registry.GetClassificationType("masmRegister");
            _symbolType = registry.GetClassificationType("masmSymbol");
            _operatorType = registry.GetClassificationType("masmOperator");
            _runtimeOperatorType = registry.GetClassificationType("masmRuntimeOperator");
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var result = new List<ClassificationSpan>();
            string text = span.GetText();
            int start = span.Start.Position;

            // Comments
            int commentIndex = text.IndexOf(';');
            if (commentIndex >= 0)
            {
                var commentSpan = new SnapshotSpan(span.Snapshot, new Span(start + commentIndex, text.Length - commentIndex));
                result.Add(new ClassificationSpan(commentSpan, _commentType));
                text = text.Substring(0, commentIndex);
            }

            var matches = Regex.Matches(text, @"[\.@]?\b[\.\w\d%]+?\b[\?]?|(?:&(?:[\w\d_]+?)&)|(?:\[(?:.+?)])|(?:<(?:.+?)>)|(?:\""(?:.+?)\"")|(?:'(?:.+?)')|(?:@@:)|[\.\+\-/\*%]|(?:!(?!=))|(?::)|(?:==|!=|>=|<=|<|>|\|\||&&??)|\$|\?|@@"); // almost working

            for (int i = 0; i < matches.Count; i++)
            {
                string word = matches[i].Value;
                int wordStart = matches[i].Index;
                int wordEnd = matches[i].Value.Length - 1;

                // Try two-word directive match first
                if (i + 1 < matches.Count)
                {
                    string combined = word + " " + matches[i + 1].Value;
                    if (Directives.Contains(combined))
                    {
                        int comboStart = matches[i].Index;
                        int comboLength = matches[i].Length + 1 + matches[i + 1].Length;

                        var directiveSpan = new SnapshotSpan(span.Snapshot, new Span(start + comboStart, comboLength));
                        result.Add(new ClassificationSpan(directiveSpan, _directiveType));

                        i++;

                        continue;
                    }
                }

                // Check for single-word directive
                if (Directives.Contains(word.ToLowerInvariant()))
                {
                    var directiveSpan = new SnapshotSpan(span.Snapshot, new Span(start + wordStart, word.Length));
                    result.Add(new ClassificationSpan(directiveSpan, _directiveType));
                }
                // Check for keyword
                else if (Instructions.Contains(word.ToLowerInvariant()))
                {
                    var instructionSpan = new SnapshotSpan(span.Snapshot, new Span(start + wordStart, word.Length));
                    result.Add(new ClassificationSpan(instructionSpan, _instructionType));
                }
                // Check for register
                else if (Registers.Contains(word.ToLowerInvariant()))
                {
                    var registerSpan = new SnapshotSpan(span.Snapshot, new Span(start + wordStart, word.Length));
                    result.Add(new ClassificationSpan(registerSpan, _registerType));
                }
                // Check for symbol
                else if (Symbols.Contains(word.ToLowerInvariant()))
                {
                    var symbolSpan = new SnapshotSpan(span.Snapshot, new Span(start + wordStart, word.Length));
                    result.Add(new ClassificationSpan(symbolSpan, _symbolType));
                }
                // Check for operator
                else if (Operators.Contains(word.ToLowerInvariant()))
                {
                    var operatorSpan = new SnapshotSpan(span.Snapshot, new Span(start + wordStart, word.Length));
                    result.Add(new ClassificationSpan(operatorSpan, _operatorType));
                }
                // Check for runtime operator
                else if (RuntimeOperators.Contains(word.ToLowerInvariant()))
                {
                    var runtimeOperatorSpan = new SnapshotSpan(span.Snapshot, new Span(start + wordStart, word.Length));
                    result.Add(new ClassificationSpan(runtimeOperatorSpan, _runtimeOperatorType));
                }
                else if (
                    (word.StartsWith("&") && word.EndsWith("&") && word.Length > 2)      // & & operator
                    || (word.StartsWith("[") && word.EndsWith("]") && word.Length > 2)   // [] operator
                    || (word.StartsWith("<") && word.EndsWith(">") && word.Length > 2)   // <> operator
                    || (word.StartsWith("\"") && word.EndsWith("\"") && word.Length > 2)   // "" operator
                    || (word.StartsWith("'") && word.EndsWith("'") && word.Length > 2)   // '' operator
                    || (word.StartsWith("@@") && word.Length > 2)   // '' operator
                    )
                {
                    var operatorSpan = new SnapshotSpan(span.Snapshot, new Span(start + wordStart, 1));
                    result.Add(new ClassificationSpan(operatorSpan, _operatorType));

                    operatorSpan = new SnapshotSpan(span.Snapshot, new Span(start + wordStart + word.Length - 1, 1));
                    result.Add(new ClassificationSpan(operatorSpan, _operatorType));
                }
            }

            return result;
        }
    }
}