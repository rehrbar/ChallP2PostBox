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

1. Stelle sicher, dass gcc 4.9 oder neuer und cmake 3.x oder neuer installiert ist. Git wird ebenfalls benötigt.
```
apt-get install gcc cmake git pkg-config
```
2. Klone Git Repository
```
cd ~
git clone --recursive https://github.com/Azure/azure-iot-sdks.git
```
3. Öffne eine Shell und navigiere ins Verzeichnis `c/build_all/linux` im lokalen Repository.
```
cd ~/azure-iot-sdks/c/build_all/linux
./setup.sh
./build.sh
```
4. Setup Python3 auf Raspberry Pi
```
sudo apt-get install python3 python3-pip python3-rpi.gpio
sudo pip3 install pyserial
```
5. Identifziere installierte Python-Version, in unserem Fall 3.4.2
```
python3 --version
```
6. Build Python Module
```
cd ~/azure-iot-sdks/python/build_all/linux
./setup.sh --python-version 3.4
./build.sh --build-python 3.4
```
7. Klone diese Git-Repo
```
cd ~
git clone https://github.com/rehrbar/ChallP2TapBox.git
```
8. Kopiere Python-Modul zum Python-Skript, welches auf dem Raspberry Pi läuft.
```
cp ~/azure-iot-sdks/python/device/samples/iothub_client.so ~/ChallP2TapBox/raspberry
```


# Troubleshooting
### Not enough virtual memory
Während dem Build des Pyhton-Moduls erhielten wir die Meldung `Virtual memory exhausted: Cannot allocate memory`.
Nach den [hier erwähnten Schritten](https://www.bitpi.co/2015/02/11/how-to-change-raspberry-pis-swapfile-size-on-rasbian/),
das Pagefile zu vergrössern, muss das Build-Skript nochmals ausgeführt werden.

# TODO
* Unterstützung weiterer Sensoren
  * Infrarot Distanz Sensor
  * Temperatur und Luftfeuchtigkeitssensor (z.B. wenn das Paket auf Wärme anfällig ist)
