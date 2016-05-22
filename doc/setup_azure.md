# Azure für Tapbox vorbereiten

## Voraussetzungen

* Account von Microsoft Azure
* Aktive Subscription mit genügend Guthaben (MSDN oder Azure Pass sind auch möglich)
* Aktueller Webbrowser
* [Visual Studio 2015](https://www.visualstudio.com/)
* [Azure Portal](https://portal.azure.com)

## Azure IoT Hub


1. Im Hub-Menu, wähle "Browse" > gib "IoT" ins Suchfeld ein > "IoT Hub".

   Pro tip: Klicke auf den Stern, um den Punkt zu dienen Favoriten hinzuzufügen. Favoriten können direkt über das Hub-Menu zugegriffen werden.
2. Klicke auf "add" um einen neuen IoT Hub zu erstellen.
3. Gib einen aussagekräftigen Namen für den IoT Hub an, zum Beispiel "ChallengeProjTapbox".
4. Wähle als Pricing Tier das für dich passende Tier, zum Beispiel S1 (oder F1 für tests).
3. Wähle deine Subscription aus, die verwendet werden soll.
3. Erzeuge eine neue Ressourcengruppe mit einem aussagekräftigen Namen, zum Beispiel "ChallengeProj".
6. Der Rest kann beim Standard belassen werden. Klicke nun auf "Create".

## Azure Storage Account

1. Im Hub-Menu, wähle "Browse" > gib "Storage" ins Suchfeld ein > "Storage accounts".

   Pro tip: Klicke auf den Stern, um den Punkt zu dienen Favoriten hinzuzufügen. Favoriten können direkt über das Hub-Menu zugegriffen werden.
2. Klicke auf "add" um einen neuen Storage Account zu erstellen.
3. Gib einen aussagekräftigen Namen für den Storage Account an, zum Beispiel "challpstorage".
4. Wähle deine Subscription aus, die verwendet werden soll.
5. Wähle die zuvor erstellte Ressourcengruppe aus, zum Beispiel "ChallengeProj".
5. Wähle den Standort in deiner Nähe aus.
6. Der Rest kann beim Standard belassen werden. Klicke nun auf "Create".


## Azure App Service

1. Im Hub-Menu, klicke auf "App Services"
2. Gib einen gültigen App-namen ein, zum Beispiel "tapbox"
3. Wähle deine Subscription aus, die verwendet werden soll.
4. Wähle die zuvor erstellte Ressourcengruppe aus, zum Beispiel "ChallengeProj".
5. Wähle den Service Plan aus, welcher auch den Standort bestimmt. Erzeuge falls nötig einen neuen Service plan (z.B. mit Standort "West Europe" und Pricing Tier "B1").
6. Klicke auf "Create".