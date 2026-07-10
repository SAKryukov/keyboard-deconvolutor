namespace SA.Controls;
using ScanCode = Semantic.ScanCode;
using Canvas = System.Windows.Controls.Canvas;
using Rectangle = System.Windows.Shapes.Rectangle;
using TextBlock = System.Windows.Controls.TextBlock;
using Brush = System.Windows.Media.Brush;

public partial class Keyboard : System.Windows.Controls.UserControl {

    public class KeyboardClickEventArgs : System.EventArgs {
        internal KeyboardClickEventArgs(
                ScanCode scanCode, bool isHardware = false,
                bool isValid = true, string keyName = null) {
            ScanCode = scanCode;
            IsHardware = isHardware;
            IsValid = isValid;
            if (!isValid)
                KeyName = keyName;
        } //KeyboardClickEventArgs
        internal ScanCode ScanCode { get; init; }
        internal bool IsHardware { get; init; }
        internal bool IsValid { get; init; }
        internal string KeyName { get; init; }
    } //KeyboardClickEventArgs

    internal event System.EventHandler<KeyboardClickEventArgs> KeyClick;

    internal void InvokeKeyClick(KeyboardClickEventArgs eventArgs) {
        KeyClick?.Invoke(null, eventArgs);
    } //InvokeKeyClick

    public Keyboard() {
        InitializeComponent();
        Traverse();
    } //Keyboard

    static void HighlightKey(Canvas canvas, Brush brush) {
        if (canvas.Children.Count < 1) return;
        if (canvas.Children[0] is not Rectangle rectangle) return;
        rectangle.Fill = brush;
    } //HighlightKey

    void Traverse() {
        foreach (var child in this.keyboard.Children) {
            if (child is not Canvas canvas) continue;
            ScanCode scanCode = DefinitionSet.ParseTag(canvas.Tag.ToString());
            canvas.Tag = scanCode;
            if (canvas.Children[0] is not Rectangle rectangle) continue;
            rectangle.Fill = DefinitionSet.ColorKey;
            if (DefinitionSet.KeyboardLabels.TryGetValue(scanCode.Value, out string[] label)) {
                TextBlock textBlock = new() { Text = DefinitionSet.ArrangeLabel(label), IsHitTestVisible = false };
                canvas.Children.Add(textBlock);
                foreach (var labelLevel in label) System.Diagnostics.Debug.WriteLine(labelLevel);
            } //if
            canvas.MouseLeftButtonDown += (sender, EventArgs) => {
                if (sender is not Canvas clicked) return;
                HighlightKey(clicked, DefinitionSet.ColorHighlightedKey);
            }; //canvas.MouseLeftButtonDown
            canvas.MouseLeftButtonUp += (sender, EventArgs) => {
                if (sender is not Canvas clicked) return;
                HighlightKey(clicked, DefinitionSet.ColorKey);
                if (clicked.Tag is not ScanCode scanCode) return;
                KeyClick?.Invoke(null, new KeyboardClickEventArgs(scanCode));
            }; //canvas.MouseLeftButtonUp
            canvas.MouseLeave += (sender, EventArgs) => {
                if (sender is not Canvas clicked) return;
                HighlightKey(clicked, DefinitionSet.ColorKey);
            }; //canvas.MouseLeave
            canvas.MouseEnter += (sender, EventArgs) => {
                if (sender is not Canvas clicked) return;
                HighlightKey(clicked, DefinitionSet.ColorHoverKey);
            }; //canvas.MouseLeave
        } //loop
    } //Traverse

} //Keyboard

