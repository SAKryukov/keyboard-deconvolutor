namespace SA.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using PointList = System.Collections.Generic.List<System.Windows.Point>;

public partial class Arrow : System.Windows.Controls.Control {

    public Arrow() {
        Focusable = true;
        FocusVisualStyle = null;
    } //Arrow

    internal Arrow Alternative { get; set; }
    
    internal bool IsChecked { get { return isChecked; } set { isChecked = value; MarkState(); } }

    protected override void OnKeyDown(KeyEventArgs e) {
        base.OnKeyDown(e);
        if (e.Key != Key.Space && e.Key != Key.Return) return;
        isChecked = !isChecked;
        MarkState();
    } //OnKeyDown

    protected override void OnMouseDown(MouseButtonEventArgs e) {
        base.OnMouseDown(e);
        Focus();
        isChecked = !isChecked;
        MarkState();
    } //OnMouseDown

    protected override void OnRenderSizeChanged(SizeChangedInfo info) {
        base.OnRenderSizeChanged(info);
        MarkState();
    } //OnRenderSizeChanged

    protected override void OnGotFocus(RoutedEventArgs e) {
        base.OnGotFocus(e);
        InvalidateVisual();
    } //OnGotFocus
    protected override void OnLostFocus(RoutedEventArgs e) {
        base.OnLostFocus(e);
        InvalidateVisual();
    } //OnLostFocus

    void MarkState() {
        InvalidateVisual();
        if (Alternative == null) return;
        Alternative.isChecked = !isChecked;
        Alternative.InvalidateVisual();
    } //MarkState

    protected override void OnRender(DrawingContext drawingContext) {
        base.OnRender(drawingContext);
        PointList points = [];
        double h0 = 0, h1 = ActualWidth/3, hC = ActualWidth/2, h3 = ActualWidth*2/3, h4 = ActualWidth;
        double v0 = ActualHeight, v1 = ActualHeight - ActualWidth, v2 = 0;
        Point start = new(hC, v0);
        points.Add(new(h0, v1));
        points.Add(new(h1, v1));
        points.Add(new(h1, v2));
        points.Add(new(h3, v2));
        points.Add(new(h3, v1));
        points.Add(new(h4, v1));
        StreamGeometry geometry = new();
        using (StreamGeometryContext ctx = geometry.Open()) {
            ctx.BeginFigure(start, isFilled: true, isClosed: true);
            foreach (var point in points)
                ctx.LineTo(point, isStroked: true, isSmoothJoin: true);
        } //using 
        Brush brush = isChecked ? Brushes.LightGreen : Brushes.Transparent;
        Pen pen = isChecked ? penChecked : penUnChecked;
        pen.Thickness = IsFocused ? 3 : 1;
        drawingContext.DrawGeometry(brush, pen, geometry);
    } //OnRender

    bool isChecked = false;
    readonly Pen penChecked = new() { Brush = Brushes.Black };
    readonly Pen penUnChecked = new() { Brush = Brushes.Gray };

} //Arrow
