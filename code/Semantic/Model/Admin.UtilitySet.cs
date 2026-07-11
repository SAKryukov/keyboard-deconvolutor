namespace SA.Semantic;
using File = System.IO.File;
using Path = System.IO.Path;
using Console = System.Console;

public static class AdminUtilitySet {

    public delegate string HelpFormat(string applicationName);
    
    public static void ToRegistryFileEntryPoint(HelpFormat format, string[] args) {
        (string inputFile, string outputFile) = TwoFilenamesOrHelp(format, args);
        if (inputFile == null) return;
        DataModel model = Agnostic.Persistence<DataModel>.Load(inputFile);
        RegisteredModel.ModelToRegistryFile(model, outputFile);
        Console.WriteLine(DefinitionSet.Admin.RegistryFileCreated(outputFile));
    } //ToRegistryFileEntryPoint

    public static string FilenameOrHelp(HelpFormat format, string[] args) {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        if (args.Length != 1) {
            System.Console.WriteLine(format(ApplicationName));
            return null;
        } else {
            if (!File.Exists(args[0])) {
                Console.WriteLine(DefinitionSet.Admin.FileNotFound(args[0]));
                Console.WriteLine(format(ApplicationName));
                return null;               
            } //if
            return args[0];
        }
    } //FilenameOrHelp
    
    public static (string inputFile, string outputFile) TwoFilenamesOrHelp(HelpFormat format, string[] args) {
        System.Console.OutputEncoding = System.Text.Encoding.UTF8;
        if (args.Length != 2) {
            System.Console.WriteLine(format(ApplicationName));
            return (null, null);
        } else
           if (!File.Exists(args[0])) {                
                Console.WriteLine(DefinitionSet.Admin.FileNotFound(args[0]));
                Console.WriteLine(format(ApplicationName));
                return (null, null);
            } //if
           return (args[0], args[1]);
    } //FilenameOrHelp

    static string ApplicationName {
        get {
            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            return Path.GetFileNameWithoutExtension(location);
        } //get ApplicationName
    } //ApplicationName

} //AdminUtilitySet
