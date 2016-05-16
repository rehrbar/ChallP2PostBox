# Azure f�r Tapbox vorbereiten

## Voraussetzungen

* Account von Microsoft Azure
* Aktive Subscription mit gen�gend Guthaben (MSDN oder Azure Pass sind auch m�glich)
* Aktueller Webbrowser
* [Visual Studio 2015](https://www.visualstudio.com/)
* [Azure Portal](https://portal.azure.com)

## Azure IoT Hub

TODO

## Azure Storage Account

1. Im Hub-Menu, w�hle "Browse" > gib "Storage" ins Suchfeld ein > "Storage accounts".

   Pro tip: Klicke auf den Stern, um den Punkt zu dienen Favoriten hinzuzuf�gen. Favoriten k�nnen direkt �ber das Hub-Menu zugegriffen werden.
 
2. Klicke auf "add" um einen neuen Storage Account zu erstellen.
3. Gib einen aussagekr�ftigen Namen f�r den Storage Account an, zum Beispiel "challpstorage".
4. W�hle deine Subscription aus, die verwendet werden soll.
5. W�hle die zuvor erstellte Ressourcengruppe aus, zum Beispiel "ChallengeProj".
6. Der Rest kann beim Standard belassen werden. Klicke nun auf "Create".


## Azure Cloud Service

1. Im Hub-Menu, klicke auf "Cloud service (classic)"
2. Gib einen g�ltigen DNS-Namen ein, zum Beispiel "tapbox"
3. W�hle deine Subscription aus, die verwendet werden soll.
4. W�hle die zuvor erstellte Ressourcengruppe aus, zum Beispiel "ChallengeProj".
5. W�hle den Standort in deiner N�he aus.
6. Klicke auf "Create".

![Creation of Azure Cloud Service](/doc/img/AzureCloudService.png)