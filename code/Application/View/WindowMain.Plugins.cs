namespace SA;
using Application = System.Windows.Application;
using AdvancedApplicationBase = Agnostic.UI.AdvancedApplicationBase;
using PluginLoader = Agnostic.PluginLoader<Semantic.IPlugin>;
using PluginLoaderList = System.Collections.Generic.List<Agnostic.PluginLoader<Semantic.IPlugin>>;
using Directory = System.IO.Directory;
using StringList = System.Collections.Generic.List<string>;

public partial class WindowMain {

    void LoadPlugins() {
        string[] files = Directory.GetFiles(application.ExecutableDirectory, DefinitionSet.pluginAssemblyFilePattern);
        foreach (string file in files) {
            PluginLoader loader = new(file);
            plugins.Add(loader);
        } //loop
        StringList list = [];
        foreach(var plugin in plugins)
            list.Add(plugin.Instance.DialogFilter);
        exportDialog.Filter = string.Join(DefinitionSet.DialogData.filterSeparator, list);
    } //LoadPlugins

    readonly PluginLoaderList plugins = [];
    readonly AdvancedApplicationBase application = (AdvancedApplicationBase)Application.Current;

} //class WindowMain
