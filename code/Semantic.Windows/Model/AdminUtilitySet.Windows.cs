namespace SA.Semantic;
using Console = System.Console;

public static class AdminUtilitySetWindows {

    public static void ToRegistryEntryPoint(AdminUtilitySet.HelpFormat format, string[] args) {
        string inputFile = AdminUtilitySet.FilenameOrHelp(format, args);
        if (inputFile == null) return;
        DataModel model = Agnostic.Persistence<DataModel>.Load(inputFile);
        RegistryStorage.SetData(model);
        Console.WriteLine(DefinitionSet.Admin.RegistryEntryCreated);
    } //ToRegistryEntryPoint

} //AdminUtilitySetWindows
