# Azure für Tapbox vorbereiten

## Voraussetzungen

* Account von Microsoft Azure
* Aktive Subscription mit genügend Guthaben (MSDN oder Azure Pass sind auch möglich)
* Aktueller Webbrowser
* [Visual Studio 2015](https://www.visualstudio.com/)
* [Azure Portal](https://portal.azure.com)

## Azure IoT Hub

TODO

## Azure Storage Account

1. Im Hub-Menu, wähle "Browse" > gib "Storage" ins Suchfeld ein > "Storage accounts".

   Pro tip: Klicke auf den Stern, um den Punkt zu dienen Favoriten hinzuzufügen. Favoriten können direkt über das Hub-Menu zugegriffen werden.
 
2. Klicke auf "add" um einen neuen Storage Account zu erstellen.
3. Gib einen aussagekräftigen Namen für den Storage Account an, zum Beispiel "challpstorage".
4. Wähle deine Subscription aus, die verwendet werden soll.
5. Wähle die zuvor erstellte Ressourcengruppe aus, zum Beispiel "ChallengeProj".
6. Der Rest kann beim Standard belassen werden. Klicke nun auf "Create".


## Azure Cloud Service

1. Im Hub-Menu, klicke auf "Cloud service (classic)"
2. Gib einen gültigen DNS-Namen ein, zum Beispiel "tapbox"
3. Wähle deine Subscription aus, die verwendet werden soll.
4. Wähle die zuvor erstellte Ressourcengruppe aus, zum Beispiel "ChallengeProj".
5. Wähle den Standort in deiner Nähe aus.
6. Klicke auf "Create".

![Creation of Azure Cloud Service](/doc/img/AzureCloudService.png)