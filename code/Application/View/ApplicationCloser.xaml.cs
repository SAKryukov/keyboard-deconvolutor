namespace SA;
using EventArgs = System.EventArgs;
using EventHandler = System.EventHandler;
using Key = System.Windows.Input.Key;
using Keyboard = System.Windows.Input.Keyboard;
using IInputElement = System.Windows.IInputElement;
using PlacementMode = System.Windows.Controls.Primitives.PlacementMode;
using Popup = System.Windows.Controls.Primitives.Popup;

public partial class ApplicationCloser : Popup {

    internal EventHandler QuitWithoutSavingClick;
    internal EventHandler SaveMappingClick;
    internal EventHandler SaveMappingAndQuit;

    public ApplicationCloser() {
        InitializeComponent(); 
        AllowsTransparency = true;        
        StaysOpen = false;
        SetEventHandler();
        Placement = PlacementMode.MousePoint;
        safestSelectedIndex = listBox.SelectedIndex;
        Closed += (_, _) => Placement = PlacementMode.MousePoint;
    } //ApplicationCloser

    internal double FontSize {
        set {
            textBlock.FontSize = value;
            listBox.FontSize = value;
        } //set FontSize
    } //FontSize

    void EventHander() {
        EventArgs eventArgs = new();
        switch (listBox.SelectedIndex) {
            case 0: QuitWithoutSavingClick?.Invoke(this, eventArgs); break;
            case 1: SaveMappingClick?.Invoke(this, eventArgs); break;
            case 2: SaveMappingAndQuit?.Invoke(this, eventArgs); break;
        }
        IsOpen = false;           
    } //EventHander
    void SetEventHandler() {
        listBox.MouseDoubleClick += (sender, eventArgs) => EventHander();
        listBox.KeyDown += (sender, eventArgs) => {
            if (eventArgs.Key == Key.Enter) EventHander();
            if (eventArgs.Key == Key.Escape) IsOpen = false;
        }; //listBox.KeyDown
    } //SetEventHandler

    protected override void OnOpened(EventArgs e) {
        base.OnOpened(e);
        listBox.SelectedIndex = safestSelectedIndex;
        Keyboard.Focus((IInputElement)listBox.SelectedItem);
    } //OnOpened

    int safestSelectedIndex;
   
} //ApplicationCloser
