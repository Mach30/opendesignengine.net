#include <ArduinoJson.h>         // for using JSON as input and output

// constants
const char Error_Illegal_Verb[] = "Illegal VERB";
const unsigned int SerialBaudRate = 57600;
const int CadencePin = 3;                         // pin used to measure cadence
const int DirectionPin = 2;                       // pin to read direction
const int NumPoles = 18;    

// values for cadence calculations
float Cadence;                    // pedalling speed
volatile float SensedDeltaT;      // deltaT as calculated during interrupt calls
volatile boolean WalkingForward;  // walking direction
volatile boolean StepTriggered;   // have we had an unhandled step event?
volatile unsigned long LastStepTime;              // last time the step sensor was triggered

// testing values
volatile unsigned long PoleCount;
int MaxCadence;
int Direction;

// common function to attach/detach interrupts
void EnableSensors(unsigned int enable) {
  if (enable) {
    attachInterrupt(digitalPinToInterrupt(CadencePin), DetectCadence, FALLING);
  }
  else {
    detachInterrupt(digitalPinToInterrupt(CadencePin));
  }
}

// interrupt function for sensor 1, used to measure cadence
void DetectCadence() {
  PoleCount++;
  unsigned long currentTime = millis();
  SensedDeltaT = (currentTime - LastStepTime);
  LastStepTime = currentTime;
  WalkingForward = digitalRead(DirectionPin);  // CCW is forward
  StepTriggered = true;
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
    if (strcmp(uri, "/stats") == 0) {
      if (strcmp(verb, "GET") == 0) {
        StatsGet(messageId);
      }
      else if (strcmp(verb, "DELETE") == 0) {
        StatsDelete(messageId);
      }
      else {
        SerialReturnError(messageId, Error_Illegal_Verb);
      }
    }
  }
}

void StatsGet(char* messageId) {
  const size_t bufferSize = JSON_OBJECT_SIZE(4);
  DynamicJsonBuffer jsonBuffer(bufferSize);
  JsonObject& root = jsonBuffer.createObject();

  root["messageId"] = messageId;  
  root["poles"] = PoleCount;
  root["maxCadence"] = MaxCadence;
  root["direction"] = Direction;
  root.printTo(Serial);
  Serial.println();
}

void StatsDelete(char* messageId) {
  Cadence = 0.0;
  StepTriggered = false;
  WalkingForward = true;
  SensedDeltaT = 5000;            // initialize sensed deltaT (and the value used to compute it, LastStepTime) to 5 seconds in the past
  LastStepTime = millis() - 5000; // as above
  
  PoleCount = 0;
  MaxCadence = 0;
  Direction = 0;

  StatsGet(messageId);
}

void setup() {
  // put your setup code here, to run once:
  // configure DirectionPin for reading
  pinMode(DirectionPin, INPUT);

  Serial.begin(SerialBaudRate);   
  StatsDelete("Setup");
  EnableSensors(true);
}

void loop() {
  // put your main code here, to run repeatedly:
  // read from serial port
  ProcessSerialData();

  if (StepTriggered) {
    // disable interrupts while we do some math
    EnableSensors(false);
  
    // calculate the Cadence
    unsigned long currentTime = millis();
    float localDeltaT = (currentTime - LastStepTime);  
    float deltaT = max(SensedDeltaT, localDeltaT)/1000; // in seconds
    Cadence = round(60.0/deltaT/NumPoles);  // in RPM
  
    // check for new max cadence
    if (Cadence > MaxCadence)
      MaxCadence = Cadence;
  
    Direction = WalkingForward?1:-1;
  
    // re-enbale the interrupts now that we are done
    EnableSensors(true);
  }
}
