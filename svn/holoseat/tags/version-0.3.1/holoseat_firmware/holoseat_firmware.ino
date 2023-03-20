// Copyright Model B, LLC 2016.
// Author: J. Simmons 
// https://opendesignengine.net/projects/holoseat/
// 
// This file is part of the Holoseat software suite (firmware, control software, etc).
//
// The Holoseat software suite is free software: you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published by the Free Software 
// Foundation, either version 3 of the License, or (at your option) any later version.
//
// Holoseat software suite is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
// FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with Holoseat 
// software suite.  If not, see <http://www.gnu.org/licenses/>.

//
// Holoseat Firmware
// This firmware detects the cadence and direction of an exercise bike or eliptical machine 
// and when the cadence is greater than a trigger value sends the specified key to trigger 
// walking forward (w) or backward (s) as needed in video games such as FPSs, RPGs, and MMOs.
//
// The process for calculating cadence is to measure the time difference (deltaT) between
// sensor events on the primary sensor.  We then calculate the cadence from deltaT since we
// know a single revolution has occurred during the time measured as deltaT. 
//
// The process for determining the direction of the pedalling is to measure the time between 
// sensor events on sensors 1 and 2.  We record both the time from sensor 1 to 2 (SensedDirectionT2) 
// and the time from sensor 2 to 1 (SensedDirectionT1).  If SensedDirectionT1 is greater than 
// SensedDirectionT2, then the user is pedalling forward, otherwise the user is pedalling
// backward.

 //#define DEBUG

 #include "holoseat_constants.h"  // contains holoseat configuration settings
 #include "Keyboard.h"            // for sending the walk keys
 #include "math.h"                // ??
 #include <Bounce2.h>             // for debouncing the enable button
 
char FirmwareVersionString[] = "0.3.0";  // per wiki - https://opendesignengine.net/projects/holoseat/wiki/V0_3Description

// Parameter values from holoseat_constants.h
char WalkForwardCharacter = DefaultWalkForwardCharacter;
char WalkBackwardCharacter = DefaultWalkBackwardCharacter;
unsigned int TriggerCadence = DefaultTriggerCadence;
unsigned int LoggingEnabled = DefaultLoggingEnabled;
unsigned int LoggingInterval = DefaultLoggingInterval;

// when we wake up, we will go through the transition right away, so set up for that fact
unsigned int HoloseatEnabled = !DefaultHoloseatEnabled;  

// set up walking state
boolean WalkingState;
boolean LastWalkingState;
int WalkingDirection;
int LastWalkingDirection;

// calculated values
float Cadence;			// pedalling speed
float LastLocalDeltaT;	// 

// sensor data and functions
volatile unsigned long LastStepTime;				// last time the step sensor was triggered
volatile unsigned long LastDirectionTime;			// last time the direction sensor was triggered
volatile float SensedDeltaT;						// deltaT as calculated during interrupt calls
volatile float SensedDirectionT1;					// time from sensor 2 to 1 events
volatile float SensedDirectionT2;					// time from sensor 1 to 2 events
const unsigned int CadenceInterruptNumber = 0;		// interrupt pin for sensor 1 used to measure cadence
const unsigned int DirectionInterruptNumber = 1;	// interrupt pin for sensor 2 used to determine direction

// common function to attach/detach interrupts
void EnableSensors(unsigned int enable) {
  if (enable) {
    attachInterrupt(CadenceInterruptNumber, DetectCadence, FALLING);
    attachInterrupt(DirectionInterruptNumber, DetermineDirection, FALLING);
  }
  else {
    detachInterrupt(CadenceInterruptNumber);
    detachInterrupt(DirectionInterruptNumber);
  }
}

// interrupt function for sensor 1, used to measure cadence
void DetectCadence() {
  unsigned long currentTime = millis();
  SensedDeltaT = (currentTime - LastStepTime);
  SensedDirectionT1 = currentTime - LastDirectionTime;
  LastStepTime = currentTime;
}

// interrupt function for sensor 2, used to determine direction
void DetermineDirection() {
  unsigned long currentTime = millis();
  SensedDirectionT2 = currentTime - LastStepTime;
  LastDirectionTime = currentTime;
}

// Logging data
volatile unsigned long LastLogTime;		// time since last log message was sent

// formats status string and sends to serial connection if available
// see https://opendesignengine.net/projects/holoseat/wiki/Software_Source_Code#HoloSeat-Serial-Protocol
void SendStateMessage() {
  if(!Serial) // if the serial port connection is not available, skip state message
    return;
  
  Serial.print(FirmwareVersionString);
  Serial.print(",");
  Serial.print(WalkForwardCharacter);
  Serial.print("(");
  Serial.print(DefaultWalkForwardCharacter);
  Serial.print("),");
  Serial.print(WalkBackwardCharacter);
  Serial.print("(");
  Serial.print(DefaultWalkBackwardCharacter);
  Serial.print("),");
  Serial.print(HoloseatEnabled);
  Serial.print("(");
  Serial.print(DefaultHoloseatEnabled);
  Serial.print("),");
  Serial.print(WalkingDirection * round(Cadence));
  Serial.print("/");
  Serial.print(TriggerCadence);
  Serial.print("(");
  Serial.print(DefaultTriggerCadence);
  Serial.print("),");
  Serial.print(LoggingEnabled);
  Serial.print("(");
  Serial.print(DefaultLoggingEnabled);
  Serial.print(")/");
  Serial.print(LoggingInterval);
  Serial.print("(");
  Serial.print(DefaultLoggingInterval);
  Serial.println(")");
}

// Parses commands from serial connection
// see https://opendesignengine.net/projects/holoseat/wiki/Software_Source_Code#HoloSeat-Serial-Protocol
void ProcessSerialData() {
  if (Serial && Serial.available()) {
    // FIXME - replace with C-string functions later for stability 
    String command = Serial.readStringUntil('\n'); 
    int nextStart = 0;
  
    if (command.charAt(0) == '?') {
      Serial.println("R"); // send ready signal
      SendStateMessage();
    }
    else if (command.charAt(0) == 'Q') {
      SendStateMessage();
    }
    else if (command.charAt(0) == 'S') {
      // find new walk forward character
      nextStart = 2;
      WalkForwardCharacter = command.charAt(nextStart);

      // find new walk backward character
      nextStart = command.indexOf(',', nextStart);
      WalkBackwardCharacter = command.charAt(nextStart+1);
    
      // find new enabled flag
      nextStart = command.indexOf(',', nextStart+1);
      HoloseatEnabled = command.substring(nextStart+1).toInt();
      
      // find trigger cadence
      nextStart = command.indexOf(',', nextStart+1);
      TriggerCadence = command.substring(nextStart+1).toInt();
      
      // find new enable logging
      nextStart = command.indexOf(',', nextStart+1);
      LoggingEnabled = command.substring(nextStart+1).toInt();
      
      // find the new logging interval
      nextStart = command.indexOf(',', nextStart+1);    
      LoggingInterval = command.substring(nextStart+1).toInt();
      
      Serial.println("OK");
    }
    else {
      Serial.println("ERROR");  
    }
  }
  
  if (LoggingEnabled && (millis() - LastLogTime >= (100 * LoggingInterval))) { // measured in 1/10 sec
    LastLogTime = millis();
    SendStateMessage();
  }
}

// set up enabled state variables
int enableReading = LOW;          
int enablePrevious = LOW;

// set up enable button elements
int enableLedPin = 13;
int enableButtonPin = 10;
Bounce debouncer = Bounce();

void setup() {
  // set up pins and debouncer library
  pinMode(enableLedPin, OUTPUT); //our ledPin is an output
  pinMode(enableButtonPin, INPUT_PULLUP); //we're reading from the button
  debouncer.attach(enableButtonPin);
  debouncer.interval(5); // interval in ms

  Serial.begin(SerialBaudRate);    
  //MinTriggerPeriod = floor((1/TriggerStepsPerMax) * 60 * 1000);

  // start holoseat behavior
  InitializeWalkingVariables();
  EnableSensors(true);
  LastLogTime = millis();
  Keyboard.begin();

#ifdef DEBUG  // when debugging, hold for the serial monitor and setup some test defaults
  while (!Serial)
    {
    ;
    }

  // override defaults
  WalkForwardCharacter = 'w';
  WalkBackwardCharacter = 's';
  HoloseatEnabled = !true;  // when we wake up, we will go through the transition right away, so set up for that fact
  TriggerCadence = 45;
  LoggingEnabled = 1;
  LoggingInterval = 5;

  // send ready message and state
  Serial.println("R"); // send ready signal, if there is a serial connection
  SendStateMessage();
#endif
}

void loop() {
  // read from enable button
  debouncer.update();
  enableReading = debouncer.read();

  // read from serial port
  ProcessSerialData();
  
  // handle enable switching
  if (enableReading == HIGH && enablePrevious == LOW)
    HoloseatEnabled = !HoloseatEnabled;

  // handle "walking" (or not)
  if (HoloseatEnabled) {
    digitalWrite(enableLedPin, HIGH);
    HandleWalking();
  }
  else {
    digitalWrite(enableLedPin, LOW);
    InitializeWalkingVariables();
  }
    
  // store previous enable reading for use in next iteration
  enablePrevious = enableReading;
}

// handle sensor data to determine walk state and act accordingly
void HandleWalking() {
  // disable interrupts while we do some math and work the keyboard
  EnableSensors(false);

  // calculate the Cadence
  unsigned long currentTime = millis();
  float localDeltaT = (currentTime - LastStepTime);  
  float deltaT = max(SensedDeltaT, localDeltaT)/1000; // in seconds
  Cadence = 60.0/deltaT;  // in RPM

  // deal with walking
  WalkingState = Cadence > TriggerCadence;
  float directionDeltaT = SensedDirectionT1 - SensedDirectionT2;
  WalkingDirection = (directionDeltaT >= 0)?1:-1;
 
/*
  Serial.print(SensedDirectionT1);
  Serial.print(" - ");
  Serial.print(SensedDirectionT2);
  Serial.print(" = ");
  Serial.println(directionDeltaT);
*/
  // determine if we just started walking and if so, send single character in the correct direction
  if ((!WalkingState) && (LastLocalDeltaT > localDeltaT)) {  
    if (WalkingDirection > 0)
      WalkNSteps(20, WalkForwardCharacter);
    else
      WalkNSteps(20, WalkBackwardCharacter);
  }  

  // determine if we just had a sudden reversal in direction, if so stop
  if (WalkingState && (WalkingDirection * LastWalkingDirection < 0))  // we reversed direction, stop!
    InitializeWalkingVariables();

  // handle change in walking state
  if ((WalkingState != LastWalkingState)) {
    LastWalkingState = WalkingState;
    if (WalkingState) {								// started walking
      if (WalkingDirection > 0) {
        Keyboard.release(WalkBackwardCharacter);
        Keyboard.press(WalkForwardCharacter);
      }
      else {
        Keyboard.release(WalkForwardCharacter);
        Keyboard.press(WalkBackwardCharacter);
      }
    }
    else {										// stopped walking
      Keyboard.releaseAll();
      WalkingState = false;
    }
  }

  // store state for next iteration
  LastWalkingDirection = WalkingDirection;
  LastLocalDeltaT = localDeltaT;
  
  // re-enbale the interrupts now that we are done
  EnableSensors(true);
}

// presses specified character for 10*n millis (used for single key press)
void WalkNSteps(int n, char c) {
  Keyboard.press(c);
  delay(10*n);
  Keyboard.releaseAll(); 
}

// resets walking state variables, used when holoseat is disabled
void InitializeWalkingVariables() {
  Keyboard.releaseAll();
  Cadence = 0.0;
  SensedDeltaT = 5000;            // initialize sensed deltaT (and the value used to compute it, LastStepTime) to 5 seconds in the past
  LastStepTime = millis() - 5000; // as above
  SensedDirectionT1 = 1000;         
  SensedDirectionT2 = 100;
  LastLocalDeltaT = 0;
  WalkingState = false;
  LastWalkingState = false;
  WalkingDirection = 1;  
  LastWalkingDirection = 1;
}
