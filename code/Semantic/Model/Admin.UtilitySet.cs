namespace SA.Semantic;
using File = System.IO.File;
using Path = System.IO.Path;
using Console = System.Console;

public static class AdminUtilitySet {

    public delegate string HelpFormat(string applicationName);
    
    public static string FilenameOrHelp(HelpFormat format, string[] args) {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        if (args.Length != 1) {
            Console.WriteLine(format(ApplicationName));
            return null;
        } else {
            if (!File.Exists(args[0])) {
                Console.WriteLine(DefinitionSet.Admin.FileNotFound(args[0]));
                Console.WriteLine(format(ApplicationName));
                return null;               
            } //if
            return args[0];
        } //if
    } //FilenameOrHelp
    
    public static string ApplicationName {
        get {
            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            return Path.GetFileNameWithoutExtension(location);
        } //get ApplicationName
    } //ApplicationName

} //AdminUtilitySet
