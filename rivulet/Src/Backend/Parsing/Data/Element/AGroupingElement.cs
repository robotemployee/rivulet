using rivulet.Backend.Parsing.Data.Syntax;

namespace rivulet.Backend.Parsing.Data.Element;

public abstract class AGroupingElement : AElement {
    protected AGroupingElement(SyntaxHolder syntaxHolder) : base(syntaxHolder) {
        
    }
}