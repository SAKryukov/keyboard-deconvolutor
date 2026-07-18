Keyboard Deconvolutor{title}

@toc

# Why This Name?

The software product is called Keyboard Deconvolutor because the keyboard input ecosystem is highly *convoluted*. This state of affairs originated from various historical reasons.

Keyboard Deconvolutor provides a flat set of all keys on all keyboards Windows can recognize, and this key set is the widest. The key data is not hard-coded. Instead, the application [automatically discovers](#heading-advanced-topic3a-automatic-key-discovery) all the keys, not only those available on the user’s keyboard. Moreover, not all keys are handled by the OS using the same method. The OS API does not present сome codes to the software developer during keyboard handling. Nevertheless, they are also discovered.

Ultimately, all the discovered keys can be used for remapping.

# What Does It Do?

The key remapping goal is to force physical keys to behave as if they were some other ones. This feature is very important as it can fix some existing defects of physical keyboards and customize the user experience.

Keyboard Deconvolutor can be used to create scan code mappings and modify existing remapping data. It works with the unified platform-agnostic data model and .scan-code-mapping files. It does not modify the system configuration directly. Instead, the user can *export* the remapping data as system files. The export functionality is based on the plugin system. Presently, two formats are supported: Windows Registry .reg files and Linux .hwdb files.

The application Admin.To-Windows-Registry writes scan code remapping directly to the system Registry.

Windows uses information found in the Registry to remap the keyboard. This is the location of this information:

***Key***: HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Keyboard Layout
<br/>***Value***: Scancode Map

Modification of the Registry can be dangerous if the feature is used without caution. Let’s consider the safety precautions.

# Safety Precautions

A corrupted payload or bad mapping can render your user login password permanently non-functional at the next Windows logon.

- Note that the scan code mappings will become effective after the next user logon, after a system reboot, or after the user logs off. Be careful not to log out accidentally or turn off the computer power when the Registry data is not validated.
* One advanced safety technique is to disable user authentication after a system reboot or the return from sleep. Please refer to the Settings documentation for your particular Windows version and configuration.
- Before any changes to the Registry, preserve the current Registry state. To do so, run Regedit and export the Registry data found at the key `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Keyboard Layout`. In the exported file, all data can be wiped except the value `Scancode Map`. Later, click on this file to restore the Registry.
- * Avoid remapping any keys used to enter text data. The usual reason for remapping is changing the functionality of keys used to perform functions: toggle buttons, prefix buttons, buttons handling the keyboard, audio system, media players, and the like.

## Emergency Recovery: If You Are Locked Out of Windows

If you accidentally remap or disable a key required to type your Windows password or PIN, do not panic. You can easily sign in to your computer and fix your configuration without using your physical keyboard at all. Method 1: Use the On-Screen Visual Keyboard. Windows includes a fully interactive virtual keyboard on the login screen that can be controlled entirely with your mouse or a trackpad.
    
1. On the Windows sign-in screen, look at the bottom-right corner.
1. Click the Accessibility icon (this icon changes by Windows version but typically looks like a human figure, a clock-like icon, or a broken circle).
1. Select On-Screen Keyboard from the menu that appears.
1. A visual keyboard will pop up on your display. Use your mouse to click the letters and numbers to enter your password.
1. Click the arrow button or press Enter on the visual screen to log in.
1. Once you are back on your desktop, open Keyboard Deconvolutor and adjust your mappings.

# Operation

## Key Selection

There are three sources of key data used to create a scan code remapping:

1. The on-screen keyboard presented by the Keyboard Deconvolutor application
1. The physical keyboard attached to the computer running the application.
1. The complete list of keys found in each combo box found at the bottom of the application window.

The first two methods do not necessarily reveal all the keys required to remap. The on-screen keyboard presents only the standard basic keyboard. Likewise, the physical keyboard can be different from the keyboards tagged by the mapping. For example, few keyboards have the keys F13 to F24.

In contrast to the first two sources, the list of keys presented in the combo boxes is really comprehensive. The key can be manually selected. Note that the search feature helps to locate the key in the list by entering the character on the physical keyboard.

Each scan code mapping element requires two keys: Original and Replacement. These names mean that the OS will replace each Original key with a Replacement key when the user hits the key.

The arrows between the on-screen keyboard and the combo boxes show the flow of data. They are labeled as *Original Scan Code* and *Replacement Scan Code*. An active arrow is shown in green. If an active arrow points to a combo box, this combo box changes its selection when the user presses a key on the physical keyboard or clicks a key on the on-screen keyboard. The arrows behave like radio buttons: there is exactly one active arrow at a time.

The physical keyboard key detection works only when a corresponding check box is checked.

When a mapping element is defined by the selection, you can add it to the remapping table on the right.

Note that the key pair is not added if the Original and Replacement keys are the same, so, in this case, the <u>A</u>dd button is disabled.

Also, duplicate Original keys make no sense. (Of course, a Replacement key can replace more than one Original key, so it can appear more than once.) Nevertheless, the mapping element with a redundant Original key can be added. In this case, the warning is shown in the status bar. It is implemented this way to give the user the possibility to decide which mapping element to remove. If the duplicates are not removed, they will be removed when the mapping is saved to a file. This way, only the first occurrence of the Original key will be left.

## Remapping Table

The remapping table is shown on the right under the label "Scan Code Map". It can be saved or loaded using the context menu. This menu is also activated by the <b>&#x22EE;</b> "kebab" button found right of the label.

The human-readable representation of the currently selected mapping element in the table is shown in the status bar.

## Export

The "Export" menu item is enabled only if there is at least one export plugin and the current remapping table is non-empty. The export dialog provides a choice of output file types. The filename filters for each file type are supplied by the plugin implementations.

With the current software version, the user can choose one of the two export file formats: Windows Registry .reg files or Linux .hwdb files.

Please see the [Linux remapping instructions](https://sakryukov.github.io/keyboard-deconvolutor/docs/Linux-usage.html).

## Advanced Topic: Automatic Key Discovery

It is unlikely that the user has a keyboard with all keys Windows can process. The on-screen keyboard also presents only the most used keys. There are many missing keys there. At the same time, the list of keys shown in the combo boxes Original Scan Code and Replacement Scan Code is comprehensive.

Windows handles each scan code generated by any compatible keyboard and exposes it to the developer via Raw Input. However, there are a number of exclusions. First, it shows one scan code for all the “newer” audio and media player control keys. Of course, in reality, all these keys have different scan codes, but Windows handles them internally and shows them as the same. Also, the set of virtual keys includes not only the elements corresponding to real physical keys, but also generic keys not found on the physical keyboards. In particular, the keys Shift, Alt, and Win (Super, Meta, Windows Logo) do not exist because those are just generic names for corresponding left and right keys. Besides, the Pause key scan code is presented to the developer as 0xE11D, while in reality the Pause sends the sequence 0xE1, 0x1D, 0x45, 0xE1, 0x9D, and 0xC5

Keyboard Deconvolutor discovers all the keys by probing all possible scan code values in Windows format and passing them through the Windows API. In several cases, different scan code values are mapped to the same virtual key. In this case, the correct scan code can be found by the reverse transform.

Keyboard Deconvolutor resolves all convolution: the historical redundancy and ambiguity of the keyboard data and, on every application start, flattens it automatically into a set of scan codes suitable for remapping during the system runtime.