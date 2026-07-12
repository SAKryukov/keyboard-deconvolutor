namespace SA.Semantic;
using Path = System.IO.Path;

public static class Utility {

    public static void ShowHelpInBrowser() {
        string location = System.Reflection.Assembly.GetEntryAssembly().Location;
        string fileName = Path.Join(Path.GetDirectoryName(location), DefinitionSet.Utility.helpLocation[0], DefinitionSet.Utility.helpLocation[1]);
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo {
            FileName = fileName,
            UseShellExecute = true
        });
    } //ShowHelpInBrowser

} //Utility
