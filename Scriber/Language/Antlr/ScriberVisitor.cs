//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Scriber.g4 by ANTLR 4.7.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Scriber.Language.Antlr {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="ScriberParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public interface IScriberVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.root"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRoot([NotNull] ScriberParser.RootContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLine([NotNull] ScriberParser.LineContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.lineContent"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLineContent([NotNull] ScriberParser.LineContentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.environment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnvironment([NotNull] ScriberParser.EnvironmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.inlineEnvironment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInlineEnvironment([NotNull] ScriberParser.InlineEnvironmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.multilineEnvironment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultilineEnvironment([NotNull] ScriberParser.MultilineEnvironmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.arguments"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArguments([NotNull] ScriberParser.ArgumentsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.argument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgument([NotNull] ScriberParser.ArgumentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.jsonObject"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJsonObject([NotNull] ScriberParser.JsonObjectContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.keyValuePair"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitKeyValuePair([NotNull] ScriberParser.KeyValuePairContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.key"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitKey([NotNull] ScriberParser.KeyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitValue([NotNull] ScriberParser.ValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.quotation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitQuotation([NotNull] ScriberParser.QuotationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifier([NotNull] ScriberParser.IdentifierContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.quoated"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitQuoated([NotNull] ScriberParser.QuoatedContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.text"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitText([NotNull] ScriberParser.TextContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ScriberParser.standard"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStandard([NotNull] ScriberParser.StandardContext context);
}
} // namespace Scriber.Language.Antlr
