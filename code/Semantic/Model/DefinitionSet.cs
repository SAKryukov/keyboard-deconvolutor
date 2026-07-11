namespace SA.Semantic;

public static class DefinitionSet {

    internal static class DataContract {
        internal const string topName = "ScanCodeMapping";
        internal const string itemMame = "Map";
        internal const string namespaceUri = "http://www.SAKryukov.org/Schema/KeyboardDeconvolutor";
        internal const string collectionName = "Replacements";
        internal const string scanCodeName = "ScanCode";
    } //class DataContract
    
    public static class Registry {
        public const string Key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Keyboard Layout";
        public const string ReadValue = "Scancode Map";
        #if DEBUG
        public const string WriteValue = "Test.Scancode.Map";
        #else
        public const string WriteValueValue = ReadValue;
        #endif
        internal const int dataOffset = 8; 
        internal const int sizeOfsize = 4; 
        internal const string sizeMismatchException =
            "Corrupted data in the system Registry, " +
            $"structure size mismatch; key: \"{Key}\", value: \"{ReadValue}\"";
    } //class Registry

    internal static class RegistryFile {
        internal static string Template(string key, string valueName, string data) =>
            $"Windows Registry Editor Version 5.00\n\n[{key}]\n\"{valueName}\"=hex:{data}";
        internal static string FormatByte(byte value) => value.ToString("X2");
        internal const string byteSeparator = ",";
    } //class RegistryFile


} //DefinitionSet