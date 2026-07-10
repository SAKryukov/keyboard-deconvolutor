namespace SA;
using System;
using System.Windows;
using Application = System.Windows.Application;
using Path = System.IO.Path;
using AdvancedApplicationBase = Agnostic.UI.AdvancedApplicationBase;

public partial class About : Window {

    public About() {
        InitializeComponent();
        description.Text = application.AssemblyDescription;
        buttonClose.Click += (_, _) => Hide();
        buttonDetailedHelp.Click += (_, _) => ShowDetailedHelp();
        buttonSourceCode.Click += (_, _) => ShowSourceCode();
    } //About
    
    internal void ShowAbout(string title) {
        Owner = Application.Current.MainWindow;
        Title = title;
        ShowDialog();
    } //Show

    protected override void OnClosing(System.ComponentModel.CancelEventArgs eventArgs) {
        base.OnClosing(eventArgs);
        Hide();
        eventArgs.Cancel = true;
    } //OnClosing

    static void ShowIri(string uri) {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo {
            FileName = uri,
            UseShellExecute = true
        });
    } //ShowIri

    void ShowDetailedHelp() {
        string location = application.ExecutableDirectory;
        location = Path.Join(location, DefinitionSet.Help.directory, DefinitionSet.Help.file);
        ShowIri(location);
    } //ShowDetailedHelp

    static void ShowSourceCode() => ShowIri(DefinitionSet.sourceCode);

    protected override void OnContentRendered(EventArgs eventArgs) {
        base.OnContentRendered(eventArgs);
        buttonClose.Focus();
    } //OnContentRendered

    readonly AdvancedApplicationBase application = (AdvancedApplicationBase)Application.Current;
    /*
    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
        base.OnRenderSizeChanged(sizeInfo);
        Title = $"{ActualWidth}x{ActualHeight}";
    }
    */

} //class WindowMain
