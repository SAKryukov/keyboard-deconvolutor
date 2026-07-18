[
    assembly:SA.Agnostic.PluginManifest(
        typeof(SA.Semantic.IPlugin),
        typeof(SA.Plugin.Implementation))
]

namespace SA.Plugin;
using IPlugin = Semantic.IPlugin;
using StreamWriter = System.IO.StreamWriter;

class Implementation : IPlugin {

    string IPlugin.DialogFilter =>
        DefinitionSet.dialogFilter;

    void IPlugin.Export(Semantic.DataModel model, string filename) {
        using StreamWriter writer = new(filename, false);
        writer.WriteLine(DefinitionSet.prolog);
        foreach (var pair in model.Replacements) {
            bool foundOriginal = DictionarySource.keys.TryGetValue(pair.Original.Value, out DictionarySource.Key originalKey);
            bool foundReplacement = DictionarySource.keys.TryGetValue(pair.Replacement.Value, out DictionarySource.Key replacementKey);
            if (!(foundOriginal && foundReplacement)) {
                if (!foundOriginal)
                    writer.WriteLine(DefinitionSet.FormatMissingKeyCodeOriginal(pair.Original));
                if (!foundReplacement)
                    writer.WriteLine(DefinitionSet.FormatMissingKeyCodeReplacement(pair.Replacement));
                continue;
            } //if not found
            writer.WriteLine(DefinitionSet.FormatKey(originalKey.Id, replacementKey.Name));
        } //loop
    } //IPlugin.Export
    
} //class Implementation
