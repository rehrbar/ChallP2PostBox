
#include <Servo.h>


#define fsrAnalogPin A0 // FSR is connected to analog 0
#define servoPin 3 // Pin for servo
#define loraSSPin 10 // Pin for Slave Select Lora Shield
#define rfidSSPin 9 // Pin for Slave Select RFID Shield

#define STATE_EMPTY 0
#define STATE_FULL 1
#define STATE_LOCKED 2
int state = 0;

Servo myservo;  // create servo object to control a servo
String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete

void setup() {
  myservo.attach(servoPin);  // attaches the servo on pin 9 to the servo object
  pinMode(rfidSSPin, OUTPUT);
  digitalWrite(rfidSSPin, HIGH);
  // Disable Lora shield since we use usb communication for the moment.
  pinMode(loraSSPin, OUTPUT);
  digitalWrite(loraSSPin, HIGH);

  Serial.begin(9600);
  while (!Serial) ; // Wait for serial port to be available
  // reserve 200 bytes for the inputString:
  inputString.reserve(200);
}

void loop() {
  switch(state){
    case(STATE_EMPTY):
      StateEmpty();
      break;
    case(STATE_FULL):
      StateFull();
      break;
    case(STATE_LOCKED):
      StateLocked();
      break;
  }

}

void StateEmpty(){
  if(GetFsrValue() > 0){
    state = STATE_FULL;
  } else {
    delay(1000);
  }
}

void StateLocked(){
  if(stringComplete){
    Serial.print("Received message: ");
    Serial.print(inputString);
    Serial.println(";");

    if(inputString == "unlock"){
      Open();
      state = STATE_FULL;
    }
    // clear the string:
    inputString = "";
    stringComplete = false;
  }
  // TODO Read RFID
}

// Close after a delay, so you have time to close the lid.
void StateFull(){
  delay(5000);
  int fsrValue = GetFsrValue();
  if(fsrValue > 0){
    Close();
    printFsrValue(fsrValue);
    state = STATE_LOCKED;
  } else {
    printFsrValue(fsrValue);
    state = STATE_EMPTY;
  }
}

int GetFsrValue(){
  return analogRead(fsrAnalogPin);
}

void Close(){
  myservo.write(170);
}

void Open(){
    myservo.write(0);
}

void printFsrValue(int value){
    Serial.print("fsrValue:");
    Serial.print(value);
    Serial.println();
}


/*
  SerialEvent occurs whenever a new data comes in the
 hardware serial RX.  This routine is run between each
 time loop() runs, so using delay inside loop can delay
 response.  Multiple bytes of data may be available.
 */
void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read();
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '\n') {
      stringComplete = true;
    } else {
      // add it to the inputString:
      inputString += inChar;
    }
  }
}

