using System.Collections.Immutable;
using System.Linq;
using Fmk.RoslynCop.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Fmk.RoslynCop.Diagnostics.Design {

    /// <summary>
    /// V�rifie que les m�thodes de DAL ont un test unitaire.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DalMethodTestAnalyser : DiagnosticAnalyzer {

        public const string DiagnosticId = "FRC1104";

        private static readonly string Title = "Les impl�mentations de service ne doivent pas �tre d�cor�s.";
        private static readonly string MessageFormat = "La m�thode {1} du service {0} ne doit pas �tre d�cor�e avec l'attribut {2}.";
        private static readonly string Description = "Les d�corations doivent �tre port�es par le contrat de service.";
        private const string Category = "Design";

        private static DiagnosticDescriptor Rule = DiagnosticRuleUtils.CreateRule(DiagnosticId, Title, MessageFormat, Category, Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context) {
            /* Analyse pour les d�claration de fields. */
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.ClassDeclaration);
        }

        private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context) {
            var classNode = context.Node as ClassDeclarationSyntax;
            if (classNode == null) {
                return;
            }

            var classSymbol = context.SemanticModel.GetDeclaredSymbol(classNode);
            if (classSymbol == null) {
                return;
            }
            if (!classSymbol.IsServiceImplementation()) {
                return;
            }
            var serviceName = classNode.GetClassName();
            var methods = classNode.Members.OfType<MethodDeclarationSyntax>();
            foreach (var methodNode in methods) {
                var methodSymbol = context.SemanticModel.GetDeclaredSymbol(methodNode);
                var attrs = methodNode.AttributeLists.SelectMany(x => x.Attributes);
                foreach (var attr in attrs) {
                    var attrSymbol = context.SemanticModel.GetSymbolInfo(attr);
                    var attrType = attrSymbol.Symbol.ContainingType;
                    if (attrType.IsServiceImplementationAttribute()) {
                        continue;
                    }
                    var attrName = attrSymbol.Symbol.ContainingType.Name;
                    var diagnostic = Diagnostic.Create(Rule, attr.GetLocation(), serviceName, methodSymbol.Name, attrName);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
