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

## TODO
* Arduino sketches vorbereiten
* Raspberry Pi IoT Hub Client
* Verarbeitung der Events auf Azure
* Unterstützung weiterer Sensoren
  * Infrarot Distanz Sensor
  * Temperatur und Luftfeuchtigkeitssensor (z.B. wenn das Paket auf Wärme anfällig ist)
