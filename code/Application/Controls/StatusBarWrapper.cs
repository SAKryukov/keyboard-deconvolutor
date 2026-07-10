namespace SA.Controls;
using TextBlock = System.Windows.Controls.TextBlock;
using Visibility = System.Windows.Visibility;

internal  partial class StatusBarWrapper {

    internal StatusBarWrapper(TextBlock issueIndicator, TextBlock mappingDescripion) {
        this.issueIndicator = issueIndicator;
        this.mappingDescripion = mappingDescripion;
    } //StatusBarWrapper

    internal void HideIssueIndicator() {
        issueIndicator.Visibility = Visibility.Collapsed;
        mappingDescripion.Visibility = Visibility.Visible;
    } //HideIssueIndicator

    internal string Issue {
        get { return issueIndicator.Text; }
        set { SetText(issueIndicator, value); Show(Mode.Issue); }
    } //Issue

    internal string Mapping {
        get { return mappingDescripion.Text; }
        set { SetText(mappingDescripion, value); Show(Mode.Mapping); }
    } //Issue

    enum Mode { Issue, Mapping, }
    void Show(Mode mode) {
        issueIndicator.Visibility = mode == Mode.Issue
            ? Visibility.Visible : Visibility.Collapsed;
        mappingDescripion.Visibility = mode == Mode.Mapping
            ? Visibility.Visible : Visibility.Collapsed;
    } //Show
    void SetText(TextBlock textBlock, string text) {
        textBlock.Text = text;
        textBlock.ToolTip = text;
    } //SetText

    readonly TextBlock issueIndicator, mappingDescripion;

} //StatusBarWrapper
