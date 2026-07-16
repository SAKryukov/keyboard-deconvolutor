namespace  SA;
using System;
using System.Runtime.InteropServices;
using IOException = System.IO.IOException;
using Directory = System.IO.Directory;

internal static class LinuxApi {

    // native:

    const string LibEvdevName = "libevdev.so.2";
    const string LibCName = "libc";

    [DllImport(LibCName, EntryPoint = "open", SetLastError = true)]
    static extern int Open(string pathname, int flags);

    [DllImport(LibCName, EntryPoint = "close", SetLastError = true)]
    static extern void Close(int fd);

    const int O_RDONLY = 0;

    [DllImport(LibCName, EntryPoint = "ioctl", SetLastError = true)]
    static extern int IoctlKeymap(int fd, uint request, ref InputKeymapEntry ke);

    [DllImport(LibCName, EntryPoint = "ioctl", SetLastError = true)]
    static extern int IoctlName(int fd, uint request, [Out] byte[] nameBuffer);

    // Universal macro command IDs for 64-bit Linux architectures
    const uint EVIOCGKEYCODE_V2 = 0x80284504;
    const uint EVIOCGNAME_256   = 0x81004506; // Reads up to 256 bytes of the device name string

    [StructLayout(LayoutKind.Sequential)]
    struct InputKeymapEntry {
        internal byte Flags;     // Set to 0 to look up strictly by physical scan code value
        internal byte Len;       // Set to 4 (indicates 32-bit width mapping matches uint)
        internal ushort Index;   // Ignored on forward lookups
        internal uint Keycode;   // OUT: Kernel populates the resulting Linux Key Code ID here!
        internal uint Scancode;  // IN: The calculated target bitmask scan code payload goes here
        readonly ulong pad1;
        readonly ulong pad2;
        readonly ulong pad3;
        readonly uint pad4;
    } //InputKeymapEntry

    // --- Direct Text Constant Resolution ---
    // Kept from libevdev because it is a safe, read-only memory lookup that never crashes or needs freeing
    [DllImport(LibEvdevName, EntryPoint = "libevdev_event_code_get_name")]
    static extern IntPtr EventCodeGetNamePointer(uint type, uint code);

    const uint EV_KEY = 1;

    // end of native

    static int DiscoverKeyboardContext() {
        string[] eventDevices = Directory.GetFiles("/dev/input/", "event*");       
        foreach (string devicePath in eventDevices) {
            int fd = Open(devicePath, O_RDONLY);
            if (fd < 0) continue; 
            // Query the hardware name directly from the file descriptor via native ioctl!
            byte[] buffer = new byte[256];
            int bytesRead = IoctlName(fd, EVIOCGNAME_256, buffer);
            if (bytesRead > 0) {
                // Read the null-terminated string out of the raw byte buffer
                string name = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead).TrimEnd('\0').ToLowerInvariant();
                // Verify the canonical hardware text filters match your laptop deck perfectly
                if (name.Contains("keyboard") || name.Contains("kbd") || name.Contains("at-translated")) {
                    return fd; // Return the valid, verified file descriptor handle instantly!
                } //if
            } //if
            Close(fd); // Close the handle for all skipped/non-matching hardware paths
        } //foreach
        throw new IOException("No active, verified system keyboard devices found.");
    } //DiscoverKeyboardContext

    internal static uint ScancodeToKeyCode(int activeFd, uint scanCodeWindows) {
        InputKeymapEntry entry = new() {
            Flags = 0,
            Len = 4,
            Scancode = scanCodeWindows,
            Keycode = 0
        };
        int status = IoctlKeymap(activeFd, EVIOCGKEYCODE_V2, ref entry);
        if (status == 0)
            return entry.Keycode; // Success: Return the populated numeric Linux Key Code ID number (e.g., 58)
        else 
            return 0; 
    } //ScancodeToKeyCode

    internal static string EventCodeGetName(uint code) {
        IntPtr pointer = EventCodeGetNamePointer(EV_KEY, code);
        return pointer == IntPtr.Zero
            ? null : Marshal.PtrToStringAnsi(pointer);
    } //EventCodeGetName

    internal static void InvokeGenerator(Action<int> generator) {
        int activeFd = DiscoverKeyboardContext();
        try {
            generator(activeFd);
        } finally {
            Close(activeFd); 
        } //exception
    } //InvokeGenerator

} //class LinuxApi
