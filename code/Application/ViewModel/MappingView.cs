namespace SA.ViewModel;
using SA.Semantic;
using IList = System.Collections.Generic.IList<Semantic.ScanCode.MappingElement>;
using RecordCollection = System.Collections.ObjectModel.ObservableCollection<Semantic.ScanCode.MappingElement>;

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

    internal static void Add(ScanCode original, ScanCode replacement) {
        recordCollection.Add(new ScanCode.MappingElement() { Original = original, Replacement = replacement });
    } //Add
    
    internal static void Remove(int index) {
        if (index <0 || index > recordCollection.Count - 1) return;
        recordCollection.RemoveAt(index);
    } //Remove
    
    internal static void Clear() {
        recordCollection.Clear();
    } //Clear
    
    readonly static RecordCollection recordCollection = [];   

} //class MappingView

