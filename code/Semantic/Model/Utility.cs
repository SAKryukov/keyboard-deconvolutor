namespace SA.Semantic;
using Path = System.IO.Path;
using AssemblyWrapper = Agnostic.AssemblyWrapper;

public static class Utility {

    public static void ShowHelpInBrowser(AssemblyWrapper assemblyWrapper) {
        string fileName = Path.Join(assemblyWrapper.AssemblyDirectory, DefinitionSet.Utility.helpLocation[0], DefinitionSet.Utility.helpLocation[1]);
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo {
            FileName = fileName,
            UseShellExecute = true
        });
    } //ShowHelpInBrowser

} //Utility
