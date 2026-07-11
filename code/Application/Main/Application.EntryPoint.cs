namespace SA {
    using System;
    using System.Windows;

    static class EntryPoint {
        [STAThread]
        static void Main() {
            Application app = new SA.Agnostic.UI.AdvancedApplication<WindowMain>() {};
            app.Run();
        } //MainClass
    } //class EntryPoint

}
