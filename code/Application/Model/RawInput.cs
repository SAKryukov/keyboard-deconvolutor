namespace SA.Model;
using Window = System.Windows.Window;
using IntPtr = nint;
using Marshal = System.Runtime.InteropServices.Marshal;
using WindowInteropHelper = System.Windows.Interop.WindowInteropHelper;
using HwndSource = System.Windows.Interop.HwndSource;
using KeyboardClickEventArgs = Controls.Keyboard.KeyboardClickEventArgs;
using Debug = System.Diagnostics.Debug;
using SA.Semantic;

class RawInput {

    internal RawInput(Window window) {
        IntPtr windowHandle = new WindowInteropHelper(window).Handle;
        HwndSource source = HwndSource.FromHwnd(windowHandle);
        RAWINPUTDEVICE[] rid = new RAWINPUTDEVICE[1];
        rid[0].usUsagePage = UsagePage.HID_USAGE_PAGE_GENERIC; // mouse, power, keyboard... https://learn.microsoft.com/en-us/windows-hardware/drivers/hid/hid-architecture#hid-clients-supported-in-windows
        rid[0].usUsage = UsageId.HID_USAGE_GENERIC_KEYBOARD;
        rid[0].dwFlags = UsageFlags.RIDEV_INPUTSINK;
        rid[0].hwndTarget = windowHandle;
        WindowsAPI.RegisterRawInputDevices(rid, 1, (uint)Marshal.SizeOf(rid[0]));
        source?.AddHook(HwndMessageHook);
    } //RawInput

    internal event System.EventHandler<KeyboardClickEventArgs> KeyClick;

    void HandleRawInput(IntPtr lParam) {
        uint cbSizeHeader = RAWINPUTHEADER.Size;
        uint cbSizeKeyboard = RAWKEYBOARD.Size;
        uint pcbSize = cbSizeHeader + cbSizeKeyboard;
        int result = WindowsAPI.GetRawInputData(lParam, RawinputCommand.RID_INPUT, out RAWINPUT rawInput, ref pcbSize, cbSizeHeader);
        if (result < 1) return;
        bool isUp = (rawInput.keyboard.Flags & Flags.RI_KEY_BREAK) > 0;
        if (isUp) return;
        bool prefixE0 = (rawInput.keyboard.Flags & Flags.RI_KEY_E0) > 0;
        bool prefixE1 = (rawInput.keyboard.Flags & Flags.RI_KEY_E1) > 0;
        ushort rawScanCode = rawInput.keyboard.MakeCode;
        Debug.WriteLine($"Scan code: {rawScanCode:X4}");
        ushort packedScanCode = rawScanCode;
        if (prefixE0) packedScanCode |= 0xE000;
        if (prefixE1) packedScanCode |= 0xE100;
        Debug.WriteLine($"Extended scan code: {packedScanCode:X4}");
        nuint mappedVirtualKey = WindowsAPI.MapVirtualKeyA(packedScanCode, MapVirtualKeyType.MAPVK_VSC_TO_VK_EX);
        string keyName = ((System.Windows.Forms.Keys)mappedVirtualKey).ToString();
        ushort genericVirtualKey = rawInput.keyboard.VKey;
        string genericKeyName = ((System.Windows.Forms.Keys)genericVirtualKey).ToString();
        ScanCode scanCode = new(packedScanCode);
        bool arrowKeyPrefix = packedScanCode == 0xE02A;
        keyName = mappedVirtualKey == 0 ? genericKeyName : keyName;
        if (arrowKeyPrefix) keyName = null;
        bool isValidKey = !(rawScanCode == 0 || arrowKeyPrefix);
        if (!isValidKey && !arrowKeyPrefix) {
            int effectiveVirtualKey = mappedVirtualKey == 0 ? (int)genericVirtualKey : (int)mappedVirtualKey;           
            ScanCode retrievedScanCode = ViewModel.KeySetView.ScanCodeFromVirtualKey((System.Windows.Forms.Keys)effectiveVirtualKey);
            if (retrievedScanCode != null) {
               isValidKey = true;
               scanCode = retrievedScanCode;
            } //if
        } //if
        KeyClick?.Invoke(null, new KeyboardClickEventArgs(scanCode, true, isValidKey, keyName));
    } //HandleRawInput

    IntPtr HwndMessageHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
        if (msg == (int)WindowsMessages.WM_INPUT)
            HandleRawInput(lParam);
        return IntPtr.Zero;
    } //HwndMessageHook

} //class RawInput
