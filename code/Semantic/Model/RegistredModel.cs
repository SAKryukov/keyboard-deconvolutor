namespace SA.Semantic;
using MemoryStream = System.IO.MemoryStream;
using SeekOrigin = System.IO.SeekOrigin;
using BitConverter = System.BitConverter;
using SizeMismatchException = System.IO.InvalidDataException;
using StringList = System.Collections.Generic.List<string>;

public static class RegisteredModel {

    public static void ModelToRegistryFile(DataModel model, string filename) {
        System.IO.File.WriteAllText(filename, ModelToRegistryFileString(model));
    } //ModelToRegistryFile

    static string ModelToRegistryFileString(DataModel model) {
        if (model == null) return null;
        if (model.Replacements == null) return null;
        if (model.Replacements.Length < 0) return null;
        byte[] data = ModelToData(model);
        StringList list = [];
        foreach (byte dataElement in data)
            list.Add(DefinitionSet.RegistryFile.FormatByte(dataElement));
        string dataLine = string.Join(DefinitionSet.RegistryFile.byteSeparator, list);
        return DefinitionSet.RegistryFile.Template(
            DefinitionSet.Registry.Key,
            DefinitionSet.Registry.WriteValue,
            dataLine);
    } //ModelToRegistryFileString

    public static DataModel DataToModel(byte[] data) {
        DataModel model = new();
        if (data == null) return null;
        byte[] replacementArray = new byte[ScanCode.DataSize];
        byte[] originalArray = new byte[ScanCode.DataSize];
        using MemoryStream stream = new(data);
        stream.Seek(DefinitionSet.Registry.dataOffset, SeekOrigin.Begin);
        byte[] countBuffer = new byte[DefinitionSet.Registry.sizeOfsize];
        stream.Read(countBuffer, 0, DefinitionSet.Registry.sizeOfsize);
        int count = BitConverter.ToInt32(countBuffer, 0) - 1;
        int size = (count + 1) * ScanCode.MappingElement.DataSize + sizeof(int) + DefinitionSet.Registry.dataOffset;
        if (size != data.Length) throw new SizeMismatchException(DefinitionSet.Registry.sizeMismatchException);
        model.Replacements = new ScanCode.MappingElement[count];
        for (int index = 0; index < count; ++index) {
            stream.Read(replacementArray, 0, ScanCode.DataSize);
            stream.Read(originalArray, 0, ScanCode.DataSize);
            ScanCode replacement = new(BitConverter.ToUInt16(replacementArray, 0));
            ScanCode original = new(BitConverter.ToUInt16(originalArray, 0));
            model.Replacements[index] = new ScanCode.MappingElement() { Original = original, Replacement = replacement };
        } //loop
        return model;        
    } //DataToModel

    public static byte[] ModelToData(DataModel model) {
        int dataSize = (model.Replacements.Length + 1) * ScanCode.MappingElement.DataSize + sizeof (int) + DefinitionSet.Registry.dataOffset;
        byte[] data = new byte[dataSize];
        using MemoryStream stream = new(data);
        stream.Seek(DefinitionSet.Registry.dataOffset, SeekOrigin.Begin);
        byte[] sizeBuffer = BitConverter.GetBytes(model.Replacements.Length + 1);
        stream.Write(sizeBuffer, 0, sizeBuffer.Length);
        for (int index = 0; index < model.Replacements.Length; ++index) {
            ScanCode.MappingElement map = model.Replacements[index];
            ScanCode replacement = map.Replacement;
            ScanCode original = map.Original;
            stream.Write(BitConverter.GetBytes(replacement.Value), 0, ScanCode.DataSize);
            stream.Write(BitConverter.GetBytes(original.Value), 0, ScanCode.DataSize);
        } //loop
        return data;
    } //ModelToData

} //class RegisteredModel
