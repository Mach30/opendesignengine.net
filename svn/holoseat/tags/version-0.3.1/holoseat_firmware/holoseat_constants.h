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

#ifndef holoseat_constants_h
#define holoseat_constants_h

// default parameter values for Holoseat
// What key is sent to move the character forward in the game
const char DefaultWalkForwardCharacter = 'w';
// What key is sent to move the character backward in the game
const char DefaultWalkBackwardCharacter = 's';
// Is the Holoseat enabled by default?
const unsigned int DefaultHoloseatEnabled = 1; 
// How fast does the user need to pedal (in RPM) to trigger walking?
const unsigned int DefaultTriggerCadence = 75; 
// Is serial logging enabled by default?
const unsigned int DefaultLoggingEnabled = 0; 
// How long between messages in serial logging in deci-seconds (0.1 of a second)
const unsigned int DefaultLoggingInterval = 10; 

//serial port = 6;
//hardware type = leo;
// other boot parameters
const unsigned int SerialBaudRate = 115200;

#endif
