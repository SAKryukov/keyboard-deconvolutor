namespace SA.ViewModel;
using KeyNameDictionary = System.Collections.Generic.Dictionary<ushort, string>;
using System.Linq;
using Regex = System.Text.RegularExpressions.Regex;

static partial class DefinitionSet {

    internal static string KeyNameFromScanCode(Semantic.ScanCode scancode) {
        if (!specialKeyNames.TryGetValue(scancode.Value, out string result)) return null;
        return result;
    } //KeyNameFromScanCode

    internal static string KeyNameFromSystemWindowsFormsKey(string name) => KeyNameDigitOrF(FromCamelCase(name));

    internal static string ExtendKeyName(Semantic.ScanCode scanCode, string name) =>
        (scanCode.Value & ExtensionSequenceOfTwo) > 0
            ? $"{name} (extended)"
            : name;

    internal static string FormatMappingLegend(string original, string replacement) => 
       $"Mapping: replace {original} with {replacement}";

    static string FromCamelCase(string input, string delimiter = " ") {
        if (string.IsNullOrEmpty(input)) return input;
        if (input.Length < 3) return input;
        return string.Concat(input.Select((c, i) =>
            i > 0 && (char.IsUpper(c) || (char.IsDigit(input[i]) && !char.IsDigit(input[i - 1])))
            ? delimiter + c
            : c.ToString())).TrimStart();
    } //FromCamelCase

    readonly static Regex regexKeyF = regexKeyFInitializer();
    [System.Text.RegularExpressions.GeneratedRegex("F[0-9 ]+")]
    private static partial Regex regexKeyFInitializer();

    static string KeyNameDigitOrF(string name) { //two special cased D1-D9, D0 and F10-F24
        if (regexKeyF.IsMatch(name, 0)) {
            return name.Replace(" ", "");
        } else if ((name.Length == 2) && (name[0] == 'D') && char.IsDigit(name[1]))
            return name[1].ToString();
        else
            return name;
    } //KeyNameDigitOrF

    internal const ushort ExtensionNone = 0x0000;
    internal const ushort ExtensionSequenceOfTwo = 0xE000;
    internal const ushort ExtensionSequenceOfThree = 0xE100;

    const string windowsLogo = "\u229E Super, Meta, OS, System, Command, Windows Logo";
    const string leftWindowsLogo = $"L {windowsLogo}";
    const string rightWindowsLogo = $"R {windowsLogo}";

    // special key names requires because of case typo in System.Windows.Forms.Keys uses as name source
    // and other peculiarities:
    static readonly KeyNameDictionary specialKeyNames = new() {
        [0x001C] = "Enter (Return)",
        [0x003A] = "Caps Lock (Capital)",
        [0x0049] = "Page Up (Prior)",
        [0x0051] = "Page Down (Next)",
        [0x0054] = "Print Screen (Snapshot)",
        [0x0027] = "OEM Semicolon (Oem1)",
        [0x0035] = "OEM Question (Oem2)",
        [0x0029] = "OEM Tilde (Oem3)",
        [0x001A] = "OEM Open Brackets (Oem4)",
        [0x002B] = "OEM Pipe (Oem5)",
        [0x001B] = "OEM Close Brackets (Oem6)",
        [0x0028] = "OEM Quotes (Oem7)",
        [0x0056] = "OEM Backslash (Oem102)",
        [0xE05B] = leftWindowsLogo,
        [0xE05C] = rightWindowsLogo,
        [0xE05D] = $"{char.ConvertFromUtf32(0x1f5b9)} Context Menu (Applications)",
        [0x000C] = "OEM Minus",
        [0x000D] = "OEM Plus",
        [0x0033] = "OEM Comma",
        [0x0034] = "OEM Period",
    }; //specialKeyNames

} //class DefinitionSet
