namespace SA;
using ScanCode = Semantic.ScanCode;
using System.Windows;
using PlacementMode = System.Windows.Controls.Primitives.PlacementMode;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Key = System.Windows.Input.Key;
using System.Windows.Controls;
using System;

public partial class WindowMain : Window {

    public WindowMain() {
        InitializeComponent();
        map.ItemsSource = ViewModel.MappingView.ItemsSource;
        comboBoxOriginalSelector.ItemsSource = ViewModel.KeySetView.ItemsSource;
        comboBoxReplacementSelector.ItemsSource = ViewModel.KeySetView.ItemsSource;
        arrowOriginal.Alternative = arrowReplacement;
        arrowReplacement.Alternative = arrowOriginal;
        arrowOriginal.IsChecked = true;
        keyboard.KeyClick += (_, eventArgs) => {
            if (eventArgs.IsHardware && checkBoxHardware.IsChecked != true) return;
            var selector = arrowOriginal.IsChecked
                ? comboBoxOriginalSelector
                : comboBoxReplacementSelector;
            if (selector.IsKeyboardFocusWithin && eventArgs.IsHardware) return;
            if (eventArgs.IsValid)
                selector.SelectedIndex = ViewModel.KeySetView.FromScanCode(eventArgs.ScanCode);            
            //SA??? else? show something
        }; //keyboard.KeyClick
        applicationCloser.FontSize = FontSize;
        SetupDialogs();
        menuStart.MouseDown += (_, _) => {
            ContextMenu.Placement = PlacementMode.Bottom;
            ContextMenu.PlacementTarget = menuStart;
            ContextMenu.IsOpen = true;
        }; //menuStart.MouseDown
        FixMenu();
        AddCommandBindings();
        statusBarWrapper = new(issue, status);
        map.SelectionChanged += (_, eventArgs) => {
            statusBarWrapper.Mapping = ViewModel.MappingView.MappingLegend(map.SelectedIndex);
        }; //gridMap.SelectionChanged
        LoadFromRegistry();
    } //WindowMain

    protected override void OnSourceInitialized(EventArgs eventArgs) {
        base.OnSourceInitialized(eventArgs);
        rawInput = new(this);
        rawInput.KeyClick += (_, eventArgs) =>
            keyboard.InvokeKeyClick(eventArgs);
    } //OnSourceInitialized

    protected override void OnContentRendered(EventArgs e) {
        base.OnContentRendered(e);
        checkBoxHardware.Focus();
        buttonAdd.Width = buttonRemove.ActualWidth;
    } //OnContentRendered

    void AddToMap() {
            ScanCode original = ViewModel.KeySetView.FromIndex(comboBoxOriginalSelector.SelectedIndex);
            ScanCode replacement = ViewModel.KeySetView.FromIndex(comboBoxReplacementSelector.SelectedIndex);
            bool redundant = ViewModel.MappingView.Add(original, replacement);
            map.SelectedIndex = ViewModel.MappingView.Count - 1;
            map.ScrollIntoView(map.SelectedItem);
            map.Focus();
            isModified = true;
            if (!redundant) return;
            statusBarWrapper.Issue = DefinitionSet.StatusBarDuplicateIssue(original);
    } //AddToMap
    void RemoveFromMap() {
            bool wasFocused = map.IsKeyboardFocusWithin;
            int index = map.SelectedIndex;
            ViewModel.MappingView.Remove(index);
            if (ViewModel.MappingView.Count < 1) return;
            if (index < 1) index = 0;
            if (index > ViewModel.MappingView.Count - 1) index = ViewModel.MappingView.Count - 1;
            map.SelectedIndex = index;
            if (wasFocused) map.Focus();    
            isModified = true;
    } //RemoveFromMap

    void LeavePopupState() {
        comboBoxOriginalSelector.IsDropDownOpen = false;
        comboBoxReplacementSelector.IsDropDownOpen = false;
    } //LeavePopupState

    protected override void OnPreviewKeyDown(KeyEventArgs eventArgs) {
        base.OnPreviewKeyDown(eventArgs);
        if (eventArgs.Key == Key.System && eventArgs.SystemKey == Key.F4) {
            LeavePopupState();
            applicationCloser.Placement = PlacementMode.RelativePoint;
        } //if
    } //OnPreviewKeyDown

    protected override void OnClosing(System.ComponentModel.CancelEventArgs eventArgs) {
        base.OnClosing(eventArgs);
        if (!isModified) return;
        if (isDecidedToClose) return;
        bool byKeyboard = applicationCloser.Placement == PlacementMode.RelativePoint;
        applicationCloser.HorizontalOffset = byKeyboard ? this.ActualWidth + this.Left : 0;
        applicationCloser.VerticalOffset = byKeyboard ? this.Top : 0;
        applicationCloser.IsOpen = true;
        applicationCloser.QuitWithoutSavingClick += (_, _) => CloseSelf();
        applicationCloser.SaveMappingClick += (_, _) => Store();
        applicationCloser.SaveMappingAndQuit += (_, _) => CloseSelf(true);
        eventArgs.Cancel = true;
    } //OnClosing

    protected override void OnPreviewMouseMove(MouseEventArgs eventArgs) {
        base.OnPreviewMouseMove(eventArgs);
        if (!comboBoxOriginalSelector.IsDropDownOpen && !comboBoxReplacementSelector.IsDropDownOpen) return;
        if (eventArgs.GetPosition(this).Y >= 0) return;
        LeavePopupState();
    } //OnPreviewMouseMove
    void FixMenu() {
        this.ContextMenu.PreviewMouseMove += (sender, eventArgs) => {        
            if (eventArgs.GetPosition(this).Y >= 0) return;
            ContextMenu.IsOpen = false;
        }; //ContextMenu.PreviewMouseMove
    } //FixMenu

    void CloseSelf(bool store = false) {
        if (store) Store();
        isDecidedToClose = true;
        Close();
    } //CloseSelf

    Model.RawInput rawInput;
    bool isDecidedToClose = false;
    bool isModified = false;
    readonly About about = new();
    readonly ApplicationCloser applicationCloser = new();
    readonly Controls.StatusBarWrapper statusBarWrapper;

} //class WindowMain
