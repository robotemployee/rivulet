using rivulet.Backend.Parsing.Data.Syntax;

namespace rivulet.Backend.Parsing.Data.Element;

public abstract class ANumericElement : AElement {
    protected ANumericElement(SyntaxHolder syntaxHolder) : base(syntaxHolder) {
        
    }
}