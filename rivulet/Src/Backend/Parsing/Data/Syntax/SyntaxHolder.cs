using System.Text.RegularExpressions;

namespace rivulet.Backend.Parsing.Data.Syntax;

public class SyntaxHolder {

    public readonly List<ASyntaxComponent> SyntaxComponents = new List<ASyntaxComponent>();

    protected String CachedRegExpStr = "";

    protected bool Modifiable = false;

    public Regex GetRegex() {
        return new Regex(CachedRegExpStr);
        /*
         * new Regex(
            GetSyntaxComponents()
                .Select(regex => $"({regex})")
                .Aggregate((aggregated, next) => aggregated + next)
        );
         */
    }

    public List<ASyntaxComponent> GetSyntaxComponents() {
        return SyntaxComponents;
    }

    protected internal void AddSyntaxComponent(ASyntaxComponent component) {
        if (!IsModifiable()) return;
        SyntaxComponents.Add(component);
        CachedRegExpStr += $"({component.GetRegExp()})";
    }

    public void RecomputeRegEx() {
        if (!IsModifiable()) return;
        CachedRegExpStr = GetSyntaxComponents()
            .Select(regex => $"({regex})")
            .Aggregate((aggregated, next) => aggregated + next);
    }
    
    public void Lock() {
        Modifiable = true;
    }

    public bool IsModifiable() {
        return Modifiable;
    }

    public class Builder {
        private SyntaxHolder _modifiableSyntaxHolder = new SyntaxHolder();

        public Builder Add(ASyntaxComponent component) {
            _modifiableSyntaxHolder.AddSyntaxComponent(component);
            return this;
        }
        
        public SyntaxHolder Build() {
            _modifiableSyntaxHolder.Lock();
            return _modifiableSyntaxHolder;
        }
    }
}