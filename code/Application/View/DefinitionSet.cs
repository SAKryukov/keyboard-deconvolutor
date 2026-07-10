namespace SA;

static class DefinitionSet {

    internal static class Help {
        internal const string directory = "Resources";
        internal const string file = "help.html";
    } //Help

    //SA??? modify when published:
    internal static string sourceCode = "https://github.com/SAKryukov/keyboard-deconvolutor";

    internal static class DialogData {
        internal static string defaultSuffix = ".scan-code-mapping";
        internal static string filter = $"Scan Code Mapping Files|*{defaultSuffix}";
        internal static string openFileDialogTitle = "Load Scan Code Map";
        internal static string saveFileDialogTitle = "Store Scan Code Map";
    } //class DialogData
    
} //class DefinitionSet
