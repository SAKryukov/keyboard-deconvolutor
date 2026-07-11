namespace SA;

static class EntryPoint {

    static string HelpFormat(string applicationName) =>
        $"Usage: {applicationName} <mapping file name> <registry file name>";

    static void Main(string[] args) {
        Semantic.AdminUtilitySet.ToRegistryFileEntryPoint(HelpFormat, args);
    } //Main

} //class EntryPoint
