namespace SA;

static class EntryPoint {

    static string HelpFormat(string applicationName) =>
        $"Usage: {applicationName} <mapping file name>";

    static void Main(string[] args) {
        Semantic.AdminUtilitySetWindows.ToRegistryEntryPoint(HelpFormat, args);
    } //Main
    
} //class EntryPoint
