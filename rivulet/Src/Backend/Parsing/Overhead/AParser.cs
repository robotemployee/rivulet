using System.Text;
using System.Text.RegularExpressions;
using rivulet.Backend.Parsing.Data.Element;

namespace rivulet.Backend.Parsing.Overhead;

public abstract class AParser {
    public virtual SortedList<TextPosition, AElement> GetElementsFromRaw(String rawString) {
        var elements = new SortedList<TextPosition, AElement>();
        var registeredElements = GetRegisteredElements();

        if (!registeredElements.Any()) return elements;

        var lookup = new Dictionary<string, AElement>();
        StringBuilder bigRegexStr = new StringBuilder();

        int groupIndex = 0;
        foreach (AElement registeredElement in registeredElements) {
            String groupName = $"group{groupIndex++}";
            lookup[groupName] = registeredElement;

            if (bigRegexStr.Length > 0) bigRegexStr.Append('|');
            bigRegexStr.Append($"(?<{groupName}>({registeredElement.GetRegex()}))");
        }

        Regex bigRegex = new Regex(bigRegexStr.ToString());

        foreach (Match match in bigRegex.Matches(rawString)) {
            string? winner = match.Groups.Values.FirstOrDefault(g => g.Success && lookup.ContainsKey(g.Name))?.Name;

            if (winner == null) continue;
            
            TextPosition position = TextPosition.FromStringIndex(rawString, match.Index);
            AElement foundElement = lookup[winner];
            elements.TryAdd(position, foundElement);
        }

        return elements;
    }

    public virtual SortedList<TextPosition, AElement> GetElementsFromPath(String filePath) {
        if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);

        String raw = File.ReadAllText(filePath);
        return GetElementsFromRaw(raw);
    }

    public abstract HashSet<AElement> GetRegisteredElements();
}

public readonly record struct TextPosition(int Line, int Column) {
    public static TextPosition FromStringIndex(String rawString, int index) {

        if (index < 0 || index > rawString.Length) throw new ArgumentOutOfRangeException(nameof(index));

        int line = 1;
        int lastNewlineIndex = 0;

        for (int i = 0; i < index; i++) {
            if (rawString[i] == '\n') {
                line++;
                lastNewlineIndex = i;
            }
        }

        int column = index - lastNewlineIndex;

        return new TextPosition(line, column);
    }
}