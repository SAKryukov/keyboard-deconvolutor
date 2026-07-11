namespace SA;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

public partial class WindowMain {

    void SetupDialogs() {
        openFileDialog.Title = DefinitionSet.DialogData.openFileDialogTitle;
        saveFileDialog.Title = DefinitionSet.DialogData.saveFileDialogTitle;
        openFileDialog.Filter = DefinitionSet.DialogData.filter;
        openFileDialog.Filter = openFileDialog.Filter;
        openFileDialog.DefaultExt = DefinitionSet.DialogData.defaultSuffix;
        saveFileDialog.DefaultExt = openFileDialog.DefaultExt;
    } //SetupDialogs

    void Load() {
        if (openFileDialog.ShowDialog(this) != true) return;
        currentFileName = openFileDialog.FileName;
        Load(currentFileName);
        isModified = false;
    } //Load
    bool Store() {
        if (currentFileName == null)
            return StoreAs();
        else
            Store(currentFileName);
        isModified = false;
        return true;
    } //Store
    bool StoreAs() {
        if (saveFileDialog.ShowDialog(this) != true) return false;
        currentFileName = saveFileDialog.FileName;
        Store(currentFileName);
        isModified = false;
        return true;
    } //StoreAs

    static void Store(string filename) {
        Semantic.DataModel model = new();
        model.Populate(ViewModel.MappingView.ItemsSource);
        Agnostic.Persistence<Semantic.DataModel>.Store(model, filename);
    } //Store
    static void Load(string filename) {
        ViewModel.MappingView.Clear();
        Semantic.DataModel model = Agnostic.Persistence<Semantic.DataModel>.Load(filename);
        LoadFromModel(model);
    } //Load
    static void LoadFromModel(Semantic.DataModel model) {
        int count = model.Replacements.Length;
        for (int index = 0; index < count; ++index) {
            Semantic.ScanCode original = model.Replacements[index].Original;
            if (!ViewModel.KeySetView.KeyFound(original)) continue;
            Semantic.ScanCode replacement = model.Replacements[index].Replacement;
            if (!ViewModel.KeySetView.KeyFound(replacement)) continue;
            ViewModel.MappingView.Add(original, replacement); 
        } //loop
    } //LoadFromModel
    void LoadFromRegistry() {
        var model = Semantic.RegistredModel.GetData();
        if (model == null) return;
        LoadFromModel(model);
        isModified = false;
    } //LoadFromRegistry
    
    readonly OpenFileDialog openFileDialog = new();
    readonly SaveFileDialog saveFileDialog = new();
    string currentFileName = null;

} //class WindowMain
