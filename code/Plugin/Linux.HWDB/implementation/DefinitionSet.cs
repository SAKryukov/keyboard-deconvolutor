namespace SA.Plugin;

static class DefinitionSet {
    
    internal const string dialogFilter = "Linux key remapping files (*.hwdb)|*.hwdb";
    
    internal const string prolog = "evdev:atkbd:dmi:bvn*";
    internal static string FormatKey(byte value, string name) =>
        $" KEYBOARD_KEY_{value:x2}={name.ToLower()}";
    internal static string FormatMissingKeyCodeOriginal(Semantic.ScanCode scanCode) =>
        $"# Original scan code {scanCode} not found";
    internal static string FormatMissingKeyCodeReplacement(Semantic.ScanCode scanCode) =>
        $"# Replacement scan code {scanCode} not found";

} //class DefinitionSet
