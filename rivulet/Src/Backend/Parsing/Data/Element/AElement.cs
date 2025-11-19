using System.Text.RegularExpressions;
using rivulet.Backend.Parsing.Data.Syntax;

namespace rivulet.Backend.Parsing.Data.Element;

public abstract class AElement {
    protected readonly SyntaxHolder SyntaxHolder;

    protected AElement(SyntaxHolder syntaxHolder) {
        this.SyntaxHolder = syntaxHolder;
    }

    public Regex GetRegex() {
        return SyntaxHolder.GetRegex();
    }
}