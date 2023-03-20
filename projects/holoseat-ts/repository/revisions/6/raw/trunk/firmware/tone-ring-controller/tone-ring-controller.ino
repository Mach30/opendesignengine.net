#include <ArduinoJson.h>         // for using JSON as input and output
#include <Wire.h>                // the next 3 are for the Adafruit motor shield
#include <Adafruit_MotorShield.h>
#include "utility/Adafruit_MS_PWMServoDriver.h"

// constants
const unsigned int SerialBaudRate = 57600;
const int NumStepsPerRevolution = 202;
const char Error_Illegal_Verb[] = "Illegal VERB";

// Create the motor shield object with the default I2C address
Adafruit_MotorShield AFMS = Adafruit_MotorShield(); 

// Connect a stepper motor with 200 steps per revolution (1.8 degree)
// to motor port #2 (M3 and M4)
Adafruit_StepperMotor *toneRing = AFMS.getStepper(NumStepsPerRevolution, 2);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(SerialBaudRate);   // init serial line
  AFMS.begin();  // create with the default frequency 1.6KHz
}

void SerialReturnError(char* messageId, char* errorMessage) {
  const size_t bufferSize = JSON_OBJECT_SIZE(2);
  DynamicJsonBuffer jsonBuffer(bufferSize);
  JsonObject& root = jsonBuffer.createObject();

  root["messageId"] = messageId;  
  root["Error"] = errorMessage;
  root.printTo(Serial);
  Serial.println();
}

void ProcessSerialData() {
  if (Serial && Serial.available()) {
    DynamicJsonBuffer jsonBuffer(1024);
    JsonObject& root = jsonBuffer.parse(Serial);

     if (!root.success()) {
      Serial.println("{\"Error\":\"JSON Parsing Failed\"}");
      return;
    }

    // get the messageId, uri, and verb
    const char* messageId = root["messageId"];
    const char* uri = root["uri"];
    const char* verb = root["verb"];

    // find the method for uri/verb pair
    if (strcmp(uri, "/motor") == 0) {
      if (strcmp(verb, "PUT") == 0) {
        MotorPut(messageId, root["args"]["revolutions"], root["args"]["cadence"], root["args"]["direction"]);
      }
      else {
        SerialReturnError(messageId, Error_Illegal_Verb);
      }
    }
  }
}

// based on testing and a curve fit
int adjustCadence(int x) {
  int newCadence = round(41.7 - 2.47*x + 0.0925*x*x);
  return newCadence;
}

void MotorPut(char* messageId, int revolutions, int cadence, int dir) {
  // run motor
  toneRing->setSpeed(adjustCadence(cadence));
  int numSteps = NumStepsPerRevolution * revolutions;
  unsigned long startTime = millis();
  
  if (dir > 0)
    toneRing->step(numSteps, FORWARD, DOUBLE);
  else if (dir < 0)
    toneRing->step(numSteps, BACKWARD, DOUBLE);
    
  unsigned long endTime = millis();
  toneRing->release();

  double duration = (endTime - startTime) / 1000.0;
  int calculatedCadence = round(revolutions/duration * 60.0);
  
  // report results
  const size_t bufferSize = JSON_OBJECT_SIZE(4);
  DynamicJsonBuffer jsonBuffer(bufferSize);
  JsonObject& root = jsonBuffer.createObject();

  root["messageId"] = messageId;  
  root["revolutions"] = revolutions;
  root["cadence"] = calculatedCadence;
  root["direction"] = dir;
  root.printTo(Serial);
  Serial.println();
}

void loop() {
  // put your main code here, to run repeatedly:
  ProcessSerialData();
}





