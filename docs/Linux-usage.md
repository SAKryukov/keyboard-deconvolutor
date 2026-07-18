# 1. Copy your generated file cleanly into the system overrides directory
~~~bash
sudo cp ./your-generated-file.hwdb /etc/udev/hwdb.d/99-keyboard-deconvolutor.hwdb
~~~

# 2. Compile the text overrides into the kernel's binary system database
~~~bash
sudo systemd-hwdb update
~~~

# 3. Force the active udev daemon to reload its configuration state maps
~~~bash
sudo udevadm control --reload
~~~

# 4. Trigger the active hardware driver matrix to activate your layout live!
~~~bash
sudo udevadm trigger --sysname-match="event*"
~~~

