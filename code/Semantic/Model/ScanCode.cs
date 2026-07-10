namespace SA.Semantic;
using System.Runtime.Serialization;
using ScanCodeValue = ushort;

[DataContract(Name=DefinitionSet.DataContract.itemMame, Namespace=DefinitionSet.DataContract.namespaceUri)]
public class ScanCode(ScanCodeValue value) {

    public const ScanCodeValue InvalidValue = 0;
    
    public ScanCode() : this(InvalidValue) { }
    [DataMember(Name = DefinitionSet.DataContract.scanCodeName)]
    public ScanCodeValue Value { get; init; } = value;
    
    public static bool operator ==(ScanCode left, ScanCode right) {
        if (Equals(left, null) && Equals(right, null)) return true;
        if (Equals(left, null) || Equals(right, null)) return false;
        return right.Value == left.Value;
    } //==
    public static bool operator !=(ScanCode left, ScanCode right) => !(left == right);
    public override bool Equals(object @object) {
        if (Equals(@object, null)) return false;
        if (@object is ScanCode value)
            return Value == value.Value;
        else
            return false;
    } //Equals
    
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString("X4").ToUpper(); //SA???

    [DataContract(Name=DefinitionSet.DataContract.itemMame, Namespace=DefinitionSet.DataContract.namespaceUri)]
    public class MappingElement {
        // names should match column binding in WindowMain XAML
        [DataMember]
        public ScanCode Original { get; set; }
        [DataMember]
        public ScanCode Replacement { get; set; }
    } //MappingElement

} //class ScanCode

