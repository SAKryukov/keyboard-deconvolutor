namespace SA;
using System.Windows.Input;
using PlacementMode = System.Windows.Controls.Primitives.PlacementMode;
using EditingCommands = System.Windows.Documents.EditingCommands;

public partial class WindowMain {

    void AddCommandBindings() {

        CommandBindings.Add(new CommandBinding(
            ApplicationCommands.Open,
            (_, _) => Load(),
            (_, eventArgs) => { eventArgs.CanExecute = true; }));

        CommandBindings.Add(new CommandBinding(
            ApplicationCommands.Save,
            (_, _) => Store(),
            (_, eventArgs) => { eventArgs.CanExecute = HasData; }));

        CommandBindings.Add(new CommandBinding(
            ApplicationCommands.SaveAs,
            (_, _) => StoreAs(),
            (_, eventArgs) => { eventArgs.CanExecute = HasData; }));

        CommandBindings.Add(new CommandBinding(
            ApplicationCommands.Help,
            (_, _) => about.ShowAbout(Title),
            (_, eventArgs) => { eventArgs.CanExecute = true; }));

        CommandBindings.Add(new CommandBinding(
            ApplicationCommands.Close,
            (_, _) => { applicationCloser.Placement = PlacementMode.RelativePoint; Close(); },
            (_, eventArgs) => { eventArgs.CanExecute = true; }));

        CommandBindings.Add(new CommandBinding(            
            ApplicationCommands.New,
            (_, _) => AddToMap(),
            (_, eventArgs) => { eventArgs.CanExecute = comboBoxOriginalSelector.SelectedIndex != comboBoxReplacementSelector.SelectedIndex; }));

        CommandBindings.Add(new CommandBinding(
            EditingCommands.Delete,
            (_, _) => RemoveFromMap(),
            (_, eventArgs) => { eventArgs.CanExecute = HasData; }));

    } //AddCommandBindings

    static bool HasData { get { return ViewModel.MappingView.Count > 0; } }

} //class WindowMain
