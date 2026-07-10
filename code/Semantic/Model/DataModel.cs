namespace SA.Semantic;
using System.Runtime.Serialization;
using DataSource = System.Collections.Generic.IList<ScanCode.MappingElement>;
using ScanCodeSet = System.Collections.Generic.HashSet<ScanCode>;
using MappintElementList = System.Collections.Generic.List<ScanCode.MappingElement>;

[DataContract(Name=DefinitionSet.DataContract.topName, Namespace=DefinitionSet.DataContract.namespaceUri)]
public class DataModel {

    [DataMember(Name = DefinitionSet.DataContract.collectionName)]
    ScanCode.MappingElement[] replacements;
    public ScanCode.MappingElement[] Replacements { get { return replacements; } set { replacements = value; } }

    public int Populate(DataSource source) {
        ScanCodeSet scanCodeSet = [];
        MappintElementList mappintElementList = [];
        for (int index = 0; index < source.Count; ++index) {
            var value = source[index];
            if (scanCodeSet.Contains(value.Original)) continue;
            scanCodeSet.Add(value.Original);
            mappintElementList.Add(value);
        } //loop
        replacements = [.. mappintElementList];
        return source.Count - mappintElementList.Count; //number of removed duplicates
    } //Populate

} //class DataModel
