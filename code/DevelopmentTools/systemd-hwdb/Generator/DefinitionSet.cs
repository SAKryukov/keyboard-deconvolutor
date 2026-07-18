namespace SA;

static class DefinitionSet {

    internal static class Format {       
        internal const string prolog = "    static readonly KeyNameDictionary keys = new() {";
        internal const string epilog = "    }; //keys";
        internal static string Entry(ushort keyCode, uint linuxKey, string name) =>
            $"        [0x{keyCode:X4}] = new Key(0x{linuxKey:X2}, \"{name}\"),";
    } //class Format

} //class DefinitionSet

