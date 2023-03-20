INSERT License info
INSERT intro

Setup Notes:

This app requires node serialport, which has special setup instructions in Electron which differ by OS.  Use the following procedure(s) before running npm install in the app directory.

* Windows - see https://github.com/nodejs/node-gyp#on-windows (use option 1)
* MacOS - tbd
* Linux - tbd

Running Tests from the Command Line

Use the following command line call to run the tests on the command line.  <suite name> can be 'sensor', 'tonering', or 'controller'.

node .\node_modules\electron-mocha\bin\electron-mocha .\tests.js -g <suite name>

Example (sensor suite):  node .\node_modules\electron-mocha\bin\electron-mocha .\tests.js -g sensor
