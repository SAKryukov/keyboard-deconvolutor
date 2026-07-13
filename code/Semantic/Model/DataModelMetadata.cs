namespace SA.Semantic;
using System.Runtime.Serialization;
using Version = System.Version;
using AssemblyWrapper = Agnostic.AssemblyWrapper;

[DataContract(Name=DefinitionSet.DataContract.topName, Namespace=DefinitionSet.DataContract.namespaceUri)]
public class DataModelMetadata {

    [DataContract(Name=DefinitionSet.DataContract.topName, Namespace=DefinitionSet.DataContract.namespaceUri)]
    internal class PersistentVersion {
        internal PersistentVersion(Version version) {
            Major = version.Major;
            Minor = version.Minor;
            Build = version.Build;
            Revision = version.Revision;
        } //PersistentVersion
        [DataMember(Order = 1)]
        internal int Major; // Do not rename (binary serialization)
        [DataMember(Order = 2)]
        internal int Minor; // Do not rename (binary serialization)
        [DataMember(Order = 3)]
        internal int Build; // Do not rename (binary serialization)
        [DataMember(Order = 4)]
        internal int Revision; // Do not rename (binary serialization)
    } //PersistentVersion

    internal DataModelMetadata() {
        ModelVersion = new(DefinitionSet.DataContract.modelVersion);
        AssemblyWrapper assemblyWrapper = new();
        SoftwareVersion = new(assemblyWrapper.AssemblyVersion);
    } //DataModelMetadata

    [DataMember]
    internal PersistentVersion ModelVersion { get; set; }
    [DataMember]
    internal PersistentVersion SoftwareVersion { get; set; }

} //class DataModelMetadata
