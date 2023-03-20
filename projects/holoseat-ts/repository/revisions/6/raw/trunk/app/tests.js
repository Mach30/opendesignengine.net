var assert = require('assert');
var SerialPort = require('serialport');
const Readline = require('parser-readline');
var config = require('./config')

var CW = 1;
var CCW = -1;
var Controllers = {};
var Parsers = {};

// controller API calls
var sensorDeleteStats = '{"messageId":"1000","uri":"/stats","verb":"DELETE"}';
var sensorGetStats = '{"messageId":"1000","uri":"/stats","verb":"GET"}';
function toneringRun(revolutions, cadence, direction) {
	var apiCall = JSON.parse('{"messageId":"1000","uri":"/motor","verb":"PUT", "args":{}}');
	apiCall.args.revolutions = revolutions;
	apiCall.args.cadence = cadence;
	apiCall.args.direction = direction;

	return JSON.stringify(apiCall);
}

describe('Holoseat Hardware', function() {
	this.timeout(20000);

	before(function(done) {
		SerialPort.list(function (err, ports) {
			ports.forEach(function (port) {
				//console.log(port.comName + ':' + port.serialNumber);
				if (port.serialNumber === config.toneringSerialNumber) {
					Controllers.tonering = new SerialPort(port.comName, {
						baudRate: 57600
					  });
					Parsers.tonering = Controllers.tonering.pipe(new Readline({ delimiter: '\r\n' }));
				}
				else if (port.serialNumber === config.sensorSerialNumber) {
					Controllers.sensor = new SerialPort(port.comName, {
						baudRate: 57600
					  });
					Parsers.sensor = Controllers.sensor.pipe(new Readline({ delimiter: '\r\n' }));
				}
				else if (port.vendorId === '239A' && port.productId === '800C') {
					Controllers.holoseat = new SerialPort(port.comName);
					Parsers.holoseat = Controllers.holoseat.pipe(new Readline({ delimiter: '\r\n' }));
				}
			});

			done();
		});
	});
	
	describe('sensor', function() {
		beforeEach(function(done) {
			Controllers.sensor.write(sensorDeleteStats);
			Controllers.sensor.drain(function(err) {
				Parsers.sensor.once('data', function(data) {
					//console.log(data);
					done();
				});
			});
		});

		describe('direction', function() {
			it('should report cw when wheel turns 20 rpm cw', function(done) {
				Controllers.tonering.write(toneringRun(1,20,1));
				Controllers.tonering.drain(function(err) {
					Parsers.tonering.once('data', function(data) {
						Controllers.sensor.write(sensorGetStats);
						Controllers.sensor.drain(function(err) {
							Parsers.sensor.once('data', function(data){
								//console.log(data);
								var results = JSON.parse(data);
								assert.equal(results.direction, 1);
								done();
							});
						});
					});
				});
			});

			it('should report ccw when wheel turns 20 rpm ccw', function(done) {
				Controllers.tonering.write(toneringRun(1,20,-1));
				Controllers.tonering.drain(function(err) {
					Parsers.tonering.once('data', function(data) {
						Controllers.sensor.write(sensorGetStats);
						Controllers.sensor.drain(function(err) {
							Parsers.sensor.once('data', function(data){
								//console.log(data);
								var results = JSON.parse(data);
								assert.equal(results.direction, -1);
								done();
							});
						});
					});
				});
			});
		});

		describe('cadence', function() {
			it('should report 30 +/- 2 when wheel turns cw at 30 rpm', function(done) {
				Controllers.tonering.write(toneringRun(1,30,1));
				Controllers.tonering.drain(function(err) {
					Parsers.tonering.once('data', function(data) {
						Controllers.sensor.write(sensorGetStats);
						Controllers.sensor.drain(function(err) {
							Parsers.sensor.once('data', function(data){
								//console.log(data);
								var cadence = JSON.parse(data).averageCadence;
								assert(cadence >= 28 && cadence <= 32);
								done();
							});
						});
					});
				});
			});

			it('should report 50 +/- 2 when wheel turns cw at 50 rpm', function(done) {
				Controllers.tonering.write(toneringRun(1,50,1));
				Controllers.tonering.drain(function(err) {
					Parsers.tonering.once('data', function(data) {
						Controllers.sensor.write(sensorGetStats);
						Controllers.sensor.drain(function(err) {
							Parsers.sensor.once('data', function(data){
								//console.log(data);
								var cadence = JSON.parse(data).averageCadence;
								assert(cadence >= 48 && cadence <= 52);
								done();
							});
						});
					});
				});
			});

			it('should report 45 +/- 2 when wheel turns cw at 45 rpm', function(done) {
				Controllers.tonering.write(toneringRun(1,45,1));
				Controllers.tonering.drain(function(err) {
					Parsers.tonering.once('data', function(data) {
						Controllers.sensor.write(sensorGetStats);
						Controllers.sensor.drain(function(err) {
							Parsers.sensor.once('data', function(data){
								//console.log(data);
								var cadence = JSON.parse(data).averageCadence;
								assert(cadence >= 43 && cadence <= 47);
								done();
							});
						});
					});
				});
			});

			it('should report 25 +/- 2 when wheel turns cw at 25 rpm', function(done) {
				Controllers.tonering.write(toneringRun(1,25,1));
				Controllers.tonering.drain(function(err) {
					Parsers.tonering.once('data', function(data) {
						Controllers.sensor.write(sensorGetStats);
						Controllers.sensor.drain(function(err) {
							Parsers.sensor.once('data', function(data){
								//console.log(data);
								var cadence = JSON.parse(data).averageCadence;
								assert(cadence >= 23 && cadence <= 27);
								done();
							});
						});
					});
				});
			});
		});
	});
	
	describe('tonering', function() {
		beforeEach(function(done) {
			Controllers.sensor.write(sensorDeleteStats);
			Controllers.sensor.drain(function(err) {
				Parsers.sensor.once('data', function(data) {
					//console.log(data);
					done();
				});
			});
		});

		it('should have 18 poles', function(done) {
			// taking average from 5 revolutions to wash out noise in motor
			// looking for a conistently wrong number of poles
			Controllers.tonering.write(toneringRun(5,20,1));
			Controllers.tonering.drain(function(err) {
				Parsers.tonering.once('data', function(data) {
					Controllers.sensor.write(sensorGetStats);
					Controllers.sensor.drain(function(err) {
						Parsers.sensor.once('data', function(data){
							//console.log(data);
							var results = JSON.parse(data);
							assert.equal(Math.round(results.poles/5), 18);
							done();
						});
					});
				});
			});
		});
	});
	
	describe('controller', function() {
		describe('api/version', function() {
			it('should report the HW ver = 1', function(done) {
				Controllers.holoseat.write('.');
				Controllers.holoseat.drain(function(err) {
					Parsers.holoseat.once('data', function(data) {
						console.log(data);
						done();
					});
				});
			});
		});
	});
});