var assert = require('assert');
var SerialPort = require('serialport');

var CW = 1;
var CCW = -1;

describe('Holoseat Hardware', function() {
	before(function(done) {
		SerialPort.list(function (err, ports) {
			console.log('ports', ports);
			done();
		});
	});
	
	describe('sensor', function() {
		describe('direction', function() {
			it('should report cw when wheel turns 20 rpm cw', function() {
			});
			it('should report ccw when wheel turns 20 rpm ccw', function() {
			});
		});
		describe('cadence', function() {
			it('should report 30 +/- 10% when wheel turns cw at 30 rpm', function() {
			});
			it('should report 40 +/- 15% when wheel turns cw at 45 rpm', function() {
			});
		});
	});
	
	describe('tonering', function() {
		it('should have 18 poles', function() {
		});
	});
	
	describe('controller', function() {
		describe('api/version', function() {
			it('should report the HW ver = 1', function() {
			});
		});
	});
});