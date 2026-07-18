namespace SA;

static class DefinitionSet {

    internal const string sourceCode = "https://github.com/SAKryukov/keyboard-deconvolutor";
    internal const string pluginAssemblyFilePattern = "Plugin*.dll";

    internal static string StatusBarDuplicateIssue(Semantic.ScanCode scanCode) =>
        $"Original scan code {scanCode} is already remapped. Please remove items with identical Original scan codes, otherwise, only the first occurrence will be saved.";

    internal static class DialogData {
        internal const string defaultSuffix = ".scan-code-mapping";
        internal const string filter = $"Scan Code Mapping Files|*{defaultSuffix}";
        internal const string openFileDialogTitle = "Load Scan Code Map";
        internal const string saveFileDialogTitle = "Store Scan Code Map";
        internal const string exportDialogTitle = "Export Scan Code Map";
        internal const string filterSeparator = "|";
    } //class DialogData
    
} //class DefinitionSet
