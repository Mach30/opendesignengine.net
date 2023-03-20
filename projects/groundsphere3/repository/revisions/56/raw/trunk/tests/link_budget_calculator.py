import pint
import math

class LinkBudgetCalculator():
	"""Calculator for link budgets	"""

	def __init__(self, ureg):
		"""Constructor
		arg1: ureg -- pint Unit Registry
		"""
		# set the unit registry to given pint registry
		self._ureg = ureg

		# inputs
		self._altitude_ground_station = 0 * ureg.meter        # m
		self._altitude_satellite =      0 * ureg.meter        # m
		self._orbit_elevation_angle =   0 * ureg.degree       # deg
		self._downlink_frequency =      0 * ureg.hertz        # Hz
		self._target_energy_noise_ratio = 0.0                 # dB
		self._implementation_loss =       0.0                 # dB
		self._transmit_power =          0 * ureg.watt         # Watt
		self._transmit_losses =         0.0                   # dB 
		self._transmit_antenna_gain =   0.0                   # dB
		self._transmit_pointing_loss =  0.0                   # dB
		self._polarization_losses =     0.0                   # dB
		self._atmospheric_loss =        0.0                   # dB
		self._receive_antenna_gain =   0.0                   # dB
		self._receiver_gain =           0.0                   # dB
		self._receiving_pointing_loss = 0.0                   # dB 
		self._system_noise_figure =     0.0                   # dB
		self._noise_bandwidth =         0 * ureg.hertz        # Hz
		# intermediates
		self._downlink_wavelength =     0 * ureg.meter        # m
		self._link_distance =           0 * ureg.meter        # m
		self._required_ebno =           0.0                   # dB
		self._transmit_power_dBm =      0.0                   # dBm
		self._transmit_eirp =           0.0                   # dBm
		self._downlink_path_loss =      0                     # dB
		# outputs
		self._received_power =            0.0                 # dBm
		self._minimum_detectable_signal = 0.0                 # dBm
		self._energy_noise_ratio =        0.0                 # dB
		self._link_margin =               0.0                 # dB
		# constants
		c = 2.9979*pow(10,8) * ureg.meter / ureg.second	# Speed of Light
		Re = 6371 * ureg.kilometers	# Average Earth Radius
		pi = math.pi # pi
	
	@property
	def altitude_ground_station(self):
		"""Get the altitude of the ground station
		ret: altitude of ground station relative to sea level
		"""
		return self._altitude_ground_station
	
	@altitude_ground_station.setter
	def altitude_ground_station(self, value):
		"""Change the altitude of the ground station
		arg1: value -- altitude of ground station relative to sea level
		"""
		self._ureg.check('[length]')(value)
		self._altitude_ground_station = value
		
	# TODO -- add all methods for each variable
	
	def run(self):
		"""Calculate Link Budget
	`	"""
		# Downlink Wavelength m
		_downlink_wavelength = (c/_downlink_frequency).to('m')
		
		# Link Distance m 
		_orbit_elevation_angle_rad = math.radians(_orbit_elevation_angle)
		beta = _orbit_elevation_angle_rad+(pi/2)
		alpha = math.asin((_altitude_satellite/(_altitude_satellite+Re))*math.sin(beta))
		theta = (pi/2)-alpha-beta
		_link_distance = math.sin(theta)*(_altitude_satellite+Re)/math.sin(beta)
		
		# Transmit Power dBm
		_transmit_power_dBm = power_to_dBm(ureg, _transmit_power)
		
		# Transmit EIRP dBm
		_transmit_eirp = add_dBm_power(ureg, _transmit_power_dBm, _transmit_losses + _transmit_antenna_gain + _transmit_pointing_loss)
	
		# Downlink Path Loss dB
		_downlink_path_loss = -20*math.log10(4*pi*_link_distance/_downlink_wavelength)
		
		# Required En/N0 dB)
		_required_ebno = _target_energy_noise_ratio + _implementation_loss
		
		# Recieved Power dBm
		_received_power = add_dBm_power(ureg, _transmit_eirp, _transmit_antenna_gain + _downlink_path_loss + _polarization_losses + _atmospheric_loss +_receive_antenna_gain + _receiving_pointing_loss)
		
		# MDS dBm
		_minimum_detectable_signal = dBm_to_string(-174+10*math.log10(_noise_bandwidth)+_system_noise_figure)
		
		# Eb/N0 Receieved dB
		_energy_noise_ratio = add_dBm_power(ureg, _received_power, -_minimum_detectable_signal)
	
		# Link Margin dB
		_link_margin = add_dBm_power(ureg, dBm_to_string(_required_ebno), - _energy_noise_ratio)
	
	
		
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	




