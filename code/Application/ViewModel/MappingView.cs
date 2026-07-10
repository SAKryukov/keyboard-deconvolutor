namespace SA.ViewModel;
using SA.Semantic;
using IList = System.Collections.Generic.IList<Semantic.ScanCode.MappingElement>;
using RecordCollection = System.Collections.ObjectModel.ObservableCollection<Semantic.ScanCode.MappingElement>;
using OriginalScanCodeSet = System.Collections.Generic.HashSet<Semantic.ScanCode>;

static class MappingView {

    internal static int Count { get => recordCollection.Count; }
    internal static IList ItemsSource { get { return recordCollection; } }

    internal static string MappingLegend(int index) {
        if (index < 0 || index > recordCollection.Count - 1) return null;
        ScanCode.MappingElement element = recordCollection[index];
        string original = KeySetView.KeyName(element.Original);
        string replacement = KeySetView.KeyName(element.Replacement);
        return DefinitionSet.FormatMappingLegend(original, replacement);
    } //MappingLegend

    internal static bool Add(ScanCode original, ScanCode replacement) {
        bool result = originalScanCodeSet.Contains(original);
        originalScanCodeSet.Add(original);
        recordCollection.Add(new ScanCode.MappingElement() { Original = original, Replacement = replacement });
        return result;
    } //Add
    
    internal static void Remove(int index) {
        if (index <0 || index > recordCollection.Count - 1) return;
        originalScanCodeSet.Remove(recordCollection[index].Original);
        recordCollection.RemoveAt(index);
    } //Remove
    
    internal static void Clear() {
        originalScanCodeSet.Clear();
        recordCollection.Clear();
    } //Clear
    
    readonly static RecordCollection recordCollection = [];   
    readonly static OriginalScanCodeSet originalScanCodeSet = [];

} //class MappingView

