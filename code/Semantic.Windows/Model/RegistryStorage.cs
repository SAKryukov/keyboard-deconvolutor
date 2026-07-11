namespace SA.Semantic;
using Registry = Microsoft.Win32.Registry;

public static class RegistryStorage {

    public static DataModel GetData() {
        byte[] data = (byte[])Registry.GetValue(DefinitionSet.Registry.Key, DefinitionSet.Registry.ReadValue, null);
        DataModel model = RegisteredModel.DataToModel(data);
        return model;
    } //GetData

    public static void SetData(DataModel model) {
        if (model == null) return;
        if (model.Replacements == null) return;
        if (model.Replacements.Length < 0) return;
        byte[] data = RegisteredModel.ModelToData(model);
        Registry.SetValue(DefinitionSet.Registry.Key, DefinitionSet.Registry.WriteValue, data);
    } //SetData

} //class RegistryStorage
