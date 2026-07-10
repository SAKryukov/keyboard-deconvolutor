namespace SA.Controls;
using LabelDictionary = System.Collections.Generic.Dictionary<uint, string[]>;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

static class DefinitionSet {

    internal static readonly Brush ColorKey = Brushes.White;
    internal static readonly Brush ColorHighlightedKey = Brushes.SteelBlue;
    internal static readonly Brush ColorHoverKey = Brushes.LightSteelBlue;

    internal static Semantic.ScanCode ParseTag(string tag) =>
        new(System.Convert.ToUInt16(tag.ToString(), 16));

    internal static string ArrangeLabel(string[] label) => string.Join("\n", label);

    internal static readonly LabelDictionary KeyboardLabels = new() {
        [1] = ["Esc"],
        //
        [0x3B] = ["F1"],
        [0x3C] = ["F2"],
        [0x3D] = ["F3"],
        [0x3E] = ["F4"],
        [0x3F] = ["F5"],
        [0x40] = ["F6"],
        [0x41] = ["F7"],
        [0x42] = ["F8"],
        [0x43] = ["F9"],
        [0x44] = ["F10"],
        [0x57] = ["F11"],
        [0x58] = ["F12"],
        //
        [0x29] = ["~", "`"],
        //
        [0x02] = ["!", "1"],
        [0x03] = ["@", "2"],
        [0x04] = ["#", "3"],
        [0x05] = ["$", "4"],
        [0x06] = ["%", "5"],
        [0x07] = ["^", "6"],
        [0x08] = ["&", "7"],
        [0x09] = ["*", "8"],
        [0x0A] = ["(", "9"],
        [0x0B] = [")", "0"],
        //
        [0x0C] = ["_", "-"],
        [0x0D] = ["+", "="],
        [0x0E] = ["Backspace"],
        [0x10] = TwoCaseArray("q"),
        [0x11] = TwoCaseArray("w"),
        [0x12] = TwoCaseArray("e"),
        [0x13] = TwoCaseArray("r"),
        [0x14] = TwoCaseArray("t"),
        [0x15] = TwoCaseArray("y"),
        [0x16] = TwoCaseArray("u"),
        [0x17] = TwoCaseArray("i"),
        [0x18] = TwoCaseArray("o"),
        [0x19] = TwoCaseArray("p"),
        //
        [0x1A] = ["{", "["],
        [0x1B] = ["}", "]"],
        [0x2B] = ["|", "\\"],
        [0x1E] = TwoCaseArray("a"),
        [0x1F] = TwoCaseArray("s"),        
        [0x20] = TwoCaseArray("d"),
        [0x21] = TwoCaseArray("f"),
        [0x22] = TwoCaseArray("g"),      
        [0x23] = TwoCaseArray("h"),
        [0x24] = TwoCaseArray("j"),
        [0x25] = TwoCaseArray("k"),
        [0x26] = TwoCaseArray("l"),
        [0x27] = [":", ";"],
        [0x28] = ["\"", "'"],
        [0x1C] = ["Enter"],
        //
        [0x2C] = TwoCaseArray("z"),
        [0x2D] = TwoCaseArray("x"),
        [0x2E] = TwoCaseArray("c"),
        [0x2F] = TwoCaseArray("v"),
        [0x30] = TwoCaseArray("b"),
        [0x31] = TwoCaseArray("n"),
        [0x32] = TwoCaseArray("m"),
        [0x33] = ["<", ","],
        [0x34] = [">", "."],
        [0x35] = ["?", "/"],
        [0x36] = ["Shift"],
        [0x1D] = ["Ctrl"],
        //
        [0xE05B] = [Ideograph.superLegend, "Meta"],
        [0x38] = ["Alt"],
        [0x39] = [" "],
        [0xE038] = ["Alt"],
        [0xE05C] = [Ideograph.superLegend, "Meta"],
        [0xE05D] = [Ideograph.menuLegend, "Menu"],
        [0xE01D] = ["Ctrl"],
        //
        [0x54] = ["Print", "Scr."],
        [0x46] = ["Scroll", "Lock"],
        [0xE11D] = ["Pause"],
        //
        [0xE052] = ["Insert"],
        [0xE047] = ["Home"],
        [0xE049] = ["PgUp"],
        [0xE053] = ["Del."],
        [0xE04F] = ["End"],
        [0xE051] = ["PgDn"],
        [0xE048] = [Ideograph.up],
        [0xE04B] = [Ideograph.left],
        [0xE050] = [Ideograph.down],
        [0xE04D] = [Ideograph.right],
        [0x45] = ["Num", "Lock"],
        [0xE035] = ["/"],
        [0x37] = ["*"],
        [0x4A] = [Ideograph.minus],
        [0x47] = ["7", "Home"],
        [0x48] = ["8", Ideograph.up],
        [0x49] = ["9", "PgUp"],
        [0x4B] = ["4", Ideograph.left],
        [0x4C] = ["5", "Clear"],
        [0x4D] = ["6", Ideograph.right],
        [0x4F] = ["1", "End"],
        [0x50] = ["2", Ideograph.down],
        [0x51] = ["3", "PgDn"],
        [0x52] = ["0", "Insert"],
        [0x53] = [".", "Del."],
        [0x4E] = ["+"],
        [0xE01C] = ["Enter"],
        //
        [0x0F] = ["Tab"],
        [0x3A] = ["Caps Lock"],
        [0x2A] = ["Shift"],
    }; //KeyboardLabels

    static string[] TwoCaseArray(string letter) => [letter.ToUpper(), letter.ToLower()];

    static class Ideograph {
        internal const string up = "\u2191";
        internal const string down = "\u2193";
        internal const string left = "\u2190";
        internal const string right = "\u2192";
        internal const string minus = "\u2212";
        internal const string superLegend = "\u229E";
        internal const string menuLegend = "\U0001f5b9";
    } //Ideograph

} //class DefinitionSet

