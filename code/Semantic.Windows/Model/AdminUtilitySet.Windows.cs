namespace SA.Semantic;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxImage = System.Windows.MessageBoxImage;
using MessageBoxResult = System.Windows.MessageBoxResult;
using LocalDefinitionSet = Windows.DefinitionSet;
using File = System.IO.File;
using Path = System.IO.Path;

public static class AdminUtilitySetWindows {

    public static void ToRegistryEntryPoint(string[] args) {
        string inputFile = FilenameOrHelp(args);
        if (inputFile == null) return;
        MessageBoxResult decision = MessageBox.Show(
            LocalDefinitionSet.RegistryWarning,
            LocalDefinitionSet.Title,
            MessageBoxButton.YesNo,
            MessageBoxImage.Exclamation,
            MessageBoxResult.No);
        if (decision != MessageBoxResult.Yes) { Utility.ShowHelpInBrowser(); return; }
        //Core(inputFile);
        MessageBox.Show(
            LocalDefinitionSet.RegistryEntryCreated,
            LocalDefinitionSet.Title,
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    } //ToRegistryEntryPoint

    static void Core(string inputFile) {
        DataModel model = Agnostic.Persistence<DataModel>.Load(inputFile);
        RegistryStorage.SetData(model);
    } //Core

    static void Usage(string notExistingFilename = null) {
        string message = notExistingFilename == null
            ? LocalDefinitionSet.Usage(AdminUtilitySet.ApplicationName)
            : LocalDefinitionSet.Usage(AdminUtilitySet.ApplicationName, notExistingFilename);
        MessageBox.Show(message, LocalDefinitionSet.Title, MessageBoxButton.CancelTryContinue, MessageBoxImage.Stop);
    } //Usage

    static string FilenameOrHelp(string[] args) {
        if (args.Length != 1) {
            Usage();
            return null;
        } else {
            if (!File.Exists(args[0])) {
                Usage(args[0]);
                return null;               
            } //if
            return args[0];
        } //if
    } //FilenameOrHelp

} //AdminUtilitySetWindows
