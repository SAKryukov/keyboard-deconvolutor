namespace SA.Semantic.Windows;

public static class DefinitionSet {

    internal const string Title = "Scan Code Mapping to Windows Registry";
    
    internal static string Usage(string applicationName) =>
        $"Usage:\n\n{applicationName} <scan code mapping file name>";
    
    internal static string Usage(string applicationName, string missingFile) =>
        $"File {Semantic.DefinitionSet.Admin.OpenQuote}{missingFile}{Semantic.DefinitionSet.Admin.CloseQuote} not found\n\n" +
        "Usage:\n\n" +
        $"{applicationName} <scan code mapping file name>";

    internal static readonly string RegistryWarning =
        "Warning!" +
        "\n\nYou are about to write absolute binary Scan Code Mappings directly " +
        "into the HKEY_LOCAL_MACHINE system configuration registry hive." +
        $"\n\nKey: {Semantic.DefinitionSet.Admin.OpenQuote}{Semantic.DefinitionSet.Registry.Key}{Semantic.DefinitionSet.Registry.WriteValue}{Semantic.DefinitionSet.Admin.CloseQuote}" +
        $"\nKey: {Semantic.DefinitionSet.Admin.OpenQuote}{Semantic.DefinitionSet.Registry.WriteValue}{Semantic.DefinitionSet.Registry.WriteValue}{Semantic.DefinitionSet.Admin.CloseQuote}" +
        "\n\nA corrupted payload or bad mapping can render your user login pass " +
        "permanently non-functional at the next Windows boot cycle." +
        "\n\nTo learn about proper precautions, please see the product Help." +
        "\n\nAre you absolutely sure you want to commit these changes?";

    internal static string RegistryEntryCreated {
        get {
            string openQuote = Semantic.DefinitionSet.Admin.OpenQuote;
            string closeQuote = Semantic.DefinitionSet.Admin.CloseQuote;
            return
                $"Registry entry created:\n\n" +
                $"Key: {openQuote}{Semantic.DefinitionSet.Registry.Key}{closeQuote},\nValue: {openQuote}{Semantic.DefinitionSet.Registry.WriteValue}{closeQuote}." +
                "\n\nThe scan code mappings will be effective after the next user logon.";
        } //get RegistryEntryCreated
    } //RegistryEntryCreated

} //DefinitionSet
