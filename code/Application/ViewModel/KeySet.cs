namespace SA.ViewModel;
using ScanCode = Semantic.ScanCode;
using Enum = System.Enum;
using KeyDictionary = System.Collections.Generic.Dictionary<Semantic.ScanCode, string>;
using KeyIndexDictionary = System.Collections.Generic.Dictionary<Semantic.ScanCode, int>;
using KeyVirtualCodeScanCodeDictionary = System.Collections.Generic.Dictionary<System.Windows.Forms.Keys, Semantic.ScanCode>;
using ItemsSource = System.Collections.Generic.List<ItemsSourceItem>;

public class ItemsSourceItem {
    public ScanCode ScanCode { get; set; }
    public string Name { get; set; }
} //ItemsSourceItem

static class KeySetView {

    internal static bool KeyFound(ScanCode scanCode) => keyDictionary.ContainsKey(scanCode);
    internal static ItemsSource ItemsSource { get { return itemsSource; }}
    internal static string KeyName(ScanCode scanCode) => keyDictionary[scanCode];
    internal static ScanCode ScanCodeFromVirtualKey(System.Windows.Forms.Keys virtualKey) =>
        keyVirtualCodeScanCodeDictionary.TryGetValue(virtualKey, out ScanCode value)
            ? value
            : null;
    internal static ScanCode FromIndex(int index) => 
        ItemsSource[index].ScanCode;
    internal static int FromScanCode(ScanCode scanCode) =>
        keyIndexDictionary[scanCode];

    static KeySetView() {
        int order = 0;
        Build(ref order, DefinitionSet.ExtensionNone);
        Build(ref order, DefinitionSet.ExtensionSequenceOfTwo);
        Build(ref order, DefinitionSet.ExtensionSequenceOfThree);
    } //KeySetView

    static void Build(ref int order, ushort extension) {
        for (ushort index = 1; index <= 0xFF; ++index) {
            ushort packedScanCode = (ushort)(index | extension); 
            ushort mappedVirtualKey = (ushort)Model.WindowsAPI.MapVirtualKeyA(packedScanCode, Model.MapVirtualKeyType.MAPVK_VSC_TO_VK_EX);
            if (!Enum.IsDefined(typeof(System.Windows.Forms.Keys), (System.Windows.Forms.Keys)mappedVirtualKey)) continue;
            if (mappedVirtualKey < 1) continue;
            if (keyDictionary.ContainsKey(new ScanCode(packedScanCode))) continue; 
            ScanCode correctedScanCode = new(packedScanCode);
            string keyName = ((System.Windows.Forms.Keys)mappedVirtualKey).ToString();
            string specialName = DefinitionSet.KeyNameFromScanCode(new ScanCode(packedScanCode));
            string correctedKeyName = specialName ?? DefinitionSet.KeyNameFromSystemWindowsFormsKey(keyName);
            correctedKeyName = DefinitionSet.ExtendKeyName(correctedScanCode, correctedKeyName);
            keyDictionary.TryAdd(correctedScanCode, correctedKeyName);
            keyIndexDictionary.Add(correctedScanCode, order);
            keyVirtualCodeScanCodeDictionary.TryAdd((System.Windows.Forms.Keys)mappedVirtualKey, correctedScanCode);
            ++order;
            itemsSource.Add(new ItemsSourceItem() { ScanCode = correctedScanCode, Name = correctedKeyName });
        } //loop
    } //Build

    static readonly KeyDictionary keyDictionary = [];
    static readonly KeyIndexDictionary keyIndexDictionary = [];
    static readonly ItemsSource itemsSource = [];
    static readonly KeyVirtualCodeScanCodeDictionary keyVirtualCodeScanCodeDictionary = [];

} //class KeySetView
