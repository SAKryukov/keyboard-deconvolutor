[
    assembly:SA.Agnostic.PluginManifest(
        typeof(SA.Semantic.IPlugin),
        typeof(SA.Plugin.Implementation))
]

namespace SA.Plugin;
using IPlugin = Semantic.IPlugin;

class Implementation : IPlugin {

    string IPlugin.DialogFilter =>
        DefinitionSet.dialogFilter;

    void IPlugin.Export(Semantic.DataModel model, string filename) {
        Semantic.RegisteredModel.ModelToRegistryFile(model, filename);
    } //IPlugin.Export
    
} //class Implementation
