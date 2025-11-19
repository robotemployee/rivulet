using System.Text.RegularExpressions;

namespace rivulet.Backend.Parsing.Data.Syntax;

public abstract class ASyntaxComponent {
    public abstract bool Matches(String str);

    public abstract Regex GetRegExp();
}