# ChallP2TapBox

## Hardware

* Raspberry Pi 2 mit Micro SD card
* Arduino/Genuino Uno
* 2x Dragino LoRa Shield v1.1 (oder neuer)
* RFID-RC522 Board mit einigen RFID-Tags
* Force Sensing Resistor 0.5 Zoll durchmesser
* ein haufen Kabel, Netzteile und weiterer Krimskrams

## Funktionen

1. Wird ein Brief oder Paket in den Kasten gelegt, so erkennt dies der FSR und es wird eine Benachrichtigung ausgelöst.
2. Der Zustand des Briefkastens kann aus der Ferne überwacht werden:
  * Befindet sich was im Kasten?
  * Ist er geschlossen?
3. Öffnen des Briefkastens mittels RFID-Tag.
4. Erlauben eines weiteren RFID-Tags via Web-interface.

## Setup Raspberry Pi
Folgende Schritte entstammen der Installationsanleitung für [Azure IoTHub Client](https://github.com/Azure/azure-iot-sdks/blob/master/doc/get_started/python-devbox-setup.md).

1. Make sure gcc 4.9 or higher and cmake 3.x or higher is installed. Also git is required.
```
apt-get install gcc cmake git pkg-config
```
2. Clone Git Repository
```
git clone --recursive https://github.com/Azure/azure-iot-sdks.git
```
3. Open a shell and navigate to the folder c/build_all/linux in your local copy of the repository. (setup.sh nicht benötigt auf Arch, wohl auch nicht auf Debian)
```
cd c/build_all/linux
./setup.sh
./build.sh
```
4. Setup Python3 on raspberry pi
```
sudo apt-get install python3 python3-pip python3-rpi.gpio
sudo pip3 install pyserial
```
5. Identify Python version, in unserem Fall 3.5.1
```
python3 --version
```
6. Build Python Module
```
cd python/build_all/linux
./setup.sh  --python-version 3.5
./build.sh --build-python 3.5
```


## TODO
* Unterstützung weiterer Sensoren
  * Infrarot Distanz Sensor
  * Temperatur und Luftfeuchtigkeitssensor (z.B. wenn das Paket auf Wärme anfällig ist)
