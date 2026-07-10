namespace SA.Model;
using IntPtr = nint;

enum Flags : ushort {
    RI_KEY_MAKE = 0, // 	The key is down.
    RI_KEY_BREAK = 1, // 	The key is up.
    RI_KEY_E0 = 2, // 	The scan code has the E0 prefix.
    RI_KEY_E1 = 4, //   The scan code has the E1 prefix.
} // Flags

enum MapVirtualKeyType : uint {
    MAPVK_VK_TO_VSC = 0,
    MAPVK_VSC_TO_VK = 1,
    MAPVK_VK_TO_CHAR = 2,
    MAPVK_VSC_TO_VK_EX = 3,
    MAPVK_VK_TO_VSC_EX = 4,
} //MapVirtualKeyType

struct RAWKEYBOARD {
    public RAWKEYBOARD() { }
    internal ushort MakeCode = 0;
    internal Flags Flags = 0;
    internal ushort Reserved = 0;
    internal ushort VKey = 0;
    internal uint Message = 0;
    internal IntPtr ExtraInformation = 0;
    internal static ushort Size { get; } = 4 * 2 + 4 + 8;
} //struct RAWKEYBOARD

struct RAWINPUTHEADER {
    public RAWINPUTHEADER() { }
    internal uint type = 0;
    internal uint size = 0;
    internal IntPtr device = 0;
    internal IntPtr wParam = 0;
    internal static ushort Size { get; } = 2 * (4 + 8);
} //struct RAWINPUTHEADER

struct RAWINPUT {
    public RAWINPUT() { }
    internal RAWINPUTHEADER header = new();
    internal RAWKEYBOARD keyboard = new();
} //struct RAWINPUT

internal enum UsagePage : ushort {
    HID_USAGE_PAGE_GENERIC = 1,
    HID_USAGE_PAGE_GAME = 5,
    HID_USAGE_PAGE_LED = 8,
    HID_USAGE_PAGE_BUTTON = 9,
} //UsagePage   

internal enum UsageId : ushort {
    HID_USAGE_GENERIC_POINTER = 1,
    HID_USAGE_GENERIC_MOUSE = 2,
    HID_USAGE_GENERIC_JOYSTICK = 4,
    HID_USAGE_GENERIC_GAMEPAD = 5,
    HID_USAGE_GENERIC_KEYBOARD = 6,
    HID_USAGE_GENERIC_KEYPAD = 7,
    HID_USAGE_GENERIC_MULTI_AXIS_CONTROLLER = 8,
} //UsageId

internal enum UsageFlags : uint {
    RIDEV_REMOVE = 0x00000001,
    RIDEV_EXCLUDE = 0x00000010,
    RIDEV_PAGEONLY = 0x00000020,
    RIDEV_NOLEGACY = 0x00000030,
    RIDEV_INPUTSINK = 0x00000100,
    RIDEV_CAPTUREMOUSE = 0x00000200,
    RIDEV_NOHOTKEYS = RIDEV_CAPTUREMOUSE,
    RIDEV_APPKEYS = 0x00000400,
    RIDEV_EXINPUTSINK = 0x00001000,
    RIDEV_DEVNOTIFY = 0x00002000,
} //UsageFlags

struct RAWINPUTDEVICE {
    public RAWINPUTDEVICE() { }
    internal UsagePage usUsagePage = UsagePage.HID_USAGE_PAGE_GENERIC;
    internal UsageId usUsage = UsageId.HID_USAGE_GENERIC_KEYBOARD;
    internal UsageFlags dwFlags = UsageFlags.RIDEV_INPUTSINK;
    internal nint hwndTarget = 0;
} //struct RAWINPUTDEVICE

internal enum WindowsMessages : int {
    WM_INPUT = 0x00FF,
    //...
} //WindowsMessages

internal enum RawinputCommand : int {
    RID_HEADER = 0x10000005,
    RID_INPUT = 0x10000003,
} //RawinputCommand

static class WindowsAPI {

    const string dllName = "user32.dll";
    [System.Runtime.InteropServices.DllImport(dllName)]
    internal static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

    [System.Runtime.InteropServices.DllImport(dllName)]
    internal static extern int GetRawInputData(IntPtr hRawInput, RawinputCommand uiCommand, out RAWINPUT pData, ref uint pcbSize, uint cbSizeHeader);

    [System.Runtime.InteropServices.DllImport(dllName)]
    internal static extern uint MapVirtualKeyA(uint code, MapVirtualKeyType mapType);

} //class WindowsAPI
