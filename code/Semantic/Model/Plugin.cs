namespace SA.Semantic;

public interface IPlugin : Agnostic.IRecognizable {
    
    string DialogFilter { get; }
    void Export(DataModel model, string filename);

} //interface IPlugin

