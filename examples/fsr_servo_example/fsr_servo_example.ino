/* FSR testing sketch. 
 
Connect one end of FSR to 5V, the other end to Analog 0.
Then connect one end of a 20K resistor from Analog 0 to ground
Connect Servo data pin to pin 3 on the Arduino. */


#include <Servo.h>

#define fsrAnalogPin A0 // FSR is connected to analog 0
#define servoPin 3 // Servo connected to pin 3

Servo myservo;  // create servo object to control a servo
// twelve servo objects can be created on most boards

int fsrReading;      // the analog reading from the FSR resistor divider
bool isClosed = false;
 
void setup(void) {
  Serial.begin(9600);   // We'll send debugging information via the Serial monitor
  myservo.attach(servoPin);
}
 
void loop(void) {
  fsrReading = analogRead(fsrAnalogPin);
  Serial.print("Analog reading = ");
  Serial.println(fsrReading);

  if(fsrReading > 200){
    if(!isClosed){
      delay(10000);
      myservo.write(170);
      isClosed = true;
    }
  } else {
    myservo.write(0);
    isClosed = false;
  }
 
  delay(100);
}
