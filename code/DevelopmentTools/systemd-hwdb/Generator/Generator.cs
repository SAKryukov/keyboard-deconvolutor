namespace SA;
using Console = System.Console;

static class Generator {
    
    internal static void Generate() {
        LinuxApi.InvokeGenerator(GeneratorAction);
    } //Generate

    // compare: /usr/include/linux/input-event-codes.h

    static void GeneratorAction(int activeFileDescriptor) {
        Console.WriteLine(DefinitionSet.Format.prolog);
        Generate(activeFileDescriptor, 0);
        Generate(activeFileDescriptor, 0xE000);
        Generate(activeFileDescriptor, 0xE100);
        Console.WriteLine(DefinitionSet.Format.epilog);
    } //GeneratorAction

    static void Generate(int activeFileDescriptor, ushort prefix) {
        for (ushort index = 1; index < 0xFF; ++index) {
            ushort scanCode = (ushort)(index | prefix);
            uint key = LinuxApi.ScancodeToKeyCode(activeFileDescriptor, scanCode);
            if (key == 0) continue;
            bool isExtended = (scanCode & 0x80) > 0;
            scanCode = (ushort)(scanCode & (0xFFFF ^ 0x80));
            if (isExtended) scanCode |= 0xE000; 
            string name = LinuxApi.EventCodeGetName(key);
            Console.WriteLine(DefinitionSet.Format.Entry(scanCode, key, name));
        } //loop
    } //Generate

} //class Generator
