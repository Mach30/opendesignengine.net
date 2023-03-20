// Copyright Model B, LLC 2016.
// Author: Bryan Christian 
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace HoloSeatConfig
{
    public partial class Configuration : Form

    {
        //Create the variable for handling the holoseat and set defaults using the constructor
        private HoloseatConfigurationFile HoloConfig = new HoloseatConfigurationFile(); 
        public Configuration()
        {
            InitializeComponent();
            WalkForwardKey.Text = HoloConfig.WalkForwardCommandChar.ToString();
            WalkBackwardKey.Text = HoloConfig.WalkBackwardCommandChar.ToString();
            TriggerSteps.Value = Convert.ToInt16(HoloConfig.TriggerStepsPerMin);
            TriggerStepsNumber.Text = TriggerSteps.Value.ToString();
            ArduinoPath.Text = HoloConfig.ArduinoPath.ToString();
            int x = 1;

            foreach (string s in SerialPort.GetPortNames())
            {
                SerialPortList.Items.Add(s);
                if (x==1)  
                    SerialPortList.SelectedItem = s;

            }
            if (HoloConfig.SerialPort != "COM0")
            {
                SerialPortList.SelectedValue = HoloConfig.SerialPort;
            }
            else
            {
                HoloConfig.SerialPort = SerialPortList.SelectedItem.ToString();
            }


        }

        private void TriggerSteps_Scroll(object sender, EventArgs e)
        {
            TriggerStepsNumber.Text = TriggerSteps.Value.ToString();
        }



        private void UpdateHoloseat_Click(object sender, EventArgs e)
        {           
            HoloConfig.TriggerStepsPerMin = TriggerSteps.Value;
            HoloConfig.WalkForwardCommandChar = Convert.ToChar(WalkForwardKey.Text);
            HoloConfig.WalkBackwardCommandChar = Convert.ToChar(WalkBackwardKey.Text);
            HoloConfig.SerialPort = SerialPortList.SelectedItem.ToString();
            HoloConfig.HardwareType = HardwareType.SelectedItem.ToString();
            SerialOutput.Text = HoloConfig.SendConfig();
        }

//Write a new confriguration file and push it to the holoseat using the Arduino software    
        private void UpdateDefaults_Click(object sender, EventArgs e)
        {
            Result.Text = "";
            UploadResult.Text = "";
            int UpdateResult;
            HoloConfig.TriggerStepsPerMin = TriggerSteps.Value;
            HoloConfig.WalkForwardCommandChar = Convert.ToChar(WalkForwardKey.Text);
            HoloConfig.WalkBackwardCommandChar = Convert.ToChar(WalkBackwardKey.Text);
            HoloConfig.SerialPort = SerialPortList.SelectedItem.ToString();
            HoloConfig.HardwareType = HardwareType.SelectedItem.ToString();
            HoloConfig.ArduinoPath = ArduinoPath.Text;
            string SaveResult;
            SaveResult = HoloConfig.WriteConfiguration();
            if (File.Exists(HoloConfig.ArduinoPath)) {
                UpdateResult = HoloConfig.UpdateHoloseat();
                switch (UpdateResult)
                {
                    case 0:
                        UploadResult.Text = "Success";
                        break;
                    default:
                        UploadResult.Text = "Failed to upload(Error code:" + UpdateResult.ToString() + ").";
                        break;
                }
                Result.Text = SaveResult;
            }
            else{
                Result.Text = "Error:  Arduino executable not found in specified path.";
            }
        }

        private void WalkKey_TextChanged(object sender, EventArgs e)
        {
            if (WalkForwardKey.TextLength>1)
                WalkForwardKey.Text=WalkForwardKey.Text.Substring(1,1);

        }

        private void Close_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void EnableHoloseat_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableHoloseat.Checked)
            {
                HoloConfig.HoloSeatOn = true;
            }
            else
            {
                HoloConfig.HoloSeatOn = false;
            }
        }

        private void Configuration_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void WalkBackwardKey_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void ArduinoPath_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
