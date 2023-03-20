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

namespace HoloSeatConfig
{
    partial class Configuration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuration));
            this.WalkForwardKey = new System.Windows.Forms.TextBox();
            this.ForwardKeyLabel = new System.Windows.Forms.Label();
            this.TriggerSteps = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.TriggerStepsNumber = new System.Windows.Forms.TextBox();
            this.UpdateHoloseat = new System.Windows.Forms.Button();
            this.CloseForm = new System.Windows.Forms.Button();
            this.Result = new System.Windows.Forms.TextBox();
            this.UpdateDefaults = new System.Windows.Forms.Button();
            this.UploadResult = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SerialPortLabel = new System.Windows.Forms.Label();
            this.SerialPortList = new System.Windows.Forms.ComboBox();
            this.SerialOutput = new System.Windows.Forms.TextBox();
            this.EnableHoloseat = new System.Windows.Forms.CheckBox();
            this.HardwareType = new System.Windows.Forms.ComboBox();
            this.HardwareLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BackwardKeyLabel = new System.Windows.Forms.Label();
            this.WalkBackwardKey = new System.Windows.Forms.TextBox();
            this.ArduinoPathLabel = new System.Windows.Forms.Label();
            this.ArduinoPath = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.TriggerSteps)).BeginInit();
            this.SuspendLayout();
            // 
            // WalkForwardKey
            // 
            this.WalkForwardKey.Location = new System.Drawing.Point(85, 124);
            this.WalkForwardKey.Name = "WalkForwardKey";
            this.WalkForwardKey.Size = new System.Drawing.Size(173, 20);
            this.WalkForwardKey.TabIndex = 1;
            this.WalkForwardKey.TextChanged += new System.EventHandler(this.WalkKey_TextChanged);
            // 
            // ForwardKeyLabel
            // 
            this.ForwardKeyLabel.AutoSize = true;
            this.ForwardKeyLabel.Location = new System.Drawing.Point(9, 124);
            this.ForwardKeyLabel.Name = "ForwardKeyLabel";
            this.ForwardKeyLabel.Size = new System.Drawing.Size(66, 13);
            this.ForwardKeyLabel.TabIndex = 1;
            this.ForwardKeyLabel.Text = "Forward Key";
            this.ForwardKeyLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // TriggerSteps
            // 
            this.TriggerSteps.Location = new System.Drawing.Point(81, 168);
            this.TriggerSteps.Maximum = 120;
            this.TriggerSteps.Name = "TriggerSteps";
            this.TriggerSteps.Size = new System.Drawing.Size(173, 45);
            this.TriggerSteps.TabIndex = 2;
            this.TriggerSteps.Scroll += new System.EventHandler(this.TriggerSteps_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Trigger Rate";
            // 
            // TriggerStepsNumber
            // 
            this.TriggerStepsNumber.Location = new System.Drawing.Point(12, 193);
            this.TriggerStepsNumber.Name = "TriggerStepsNumber";
            this.TriggerStepsNumber.ReadOnly = true;
            this.TriggerStepsNumber.Size = new System.Drawing.Size(57, 20);
            this.TriggerStepsNumber.TabIndex = 4;
            this.TriggerStepsNumber.TabStop = false;
            // 
            // UpdateHoloseat
            // 
            this.UpdateHoloseat.Location = new System.Drawing.Point(12, 219);
            this.UpdateHoloseat.Name = "UpdateHoloseat";
            this.UpdateHoloseat.Size = new System.Drawing.Size(75, 43);
            this.UpdateHoloseat.TabIndex = 5;
            this.UpdateHoloseat.Text = "Update Holoseat";
            this.UpdateHoloseat.UseVisualStyleBackColor = true;
            this.UpdateHoloseat.Click += new System.EventHandler(this.UpdateHoloseat_Click);
            // 
            // CloseForm
            // 
            this.CloseForm.Location = new System.Drawing.Point(179, 219);
            this.CloseForm.Name = "CloseForm";
            this.CloseForm.Size = new System.Drawing.Size(75, 43);
            this.CloseForm.TabIndex = 6;
            this.CloseForm.Text = "Close";
            this.CloseForm.UseVisualStyleBackColor = true;
            this.CloseForm.Click += new System.EventHandler(this.Close_Click);
            // 
            // Result
            // 
            this.Result.Location = new System.Drawing.Point(301, 25);
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            this.Result.Size = new System.Drawing.Size(213, 20);
            this.Result.TabIndex = 7;
            this.Result.TabStop = false;
            // 
            // UpdateDefaults
            // 
            this.UpdateDefaults.Location = new System.Drawing.Point(93, 219);
            this.UpdateDefaults.Name = "UpdateDefaults";
            this.UpdateDefaults.Size = new System.Drawing.Size(75, 43);
            this.UpdateDefaults.TabIndex = 8;
            this.UpdateDefaults.Text = "Update Defaults";
            this.UpdateDefaults.UseVisualStyleBackColor = true;
            this.UpdateDefaults.Click += new System.EventHandler(this.UpdateDefaults_Click);
            // 
            // UploadResult
            // 
            this.UploadResult.AcceptsReturn = true;
            this.UploadResult.Location = new System.Drawing.Point(302, 77);
            this.UploadResult.Name = "UploadResult";
            this.UploadResult.ReadOnly = true;
            this.UploadResult.Size = new System.Drawing.Size(212, 20);
            this.UploadResult.TabIndex = 9;
            this.UploadResult.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Save Result";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Upload Result";
            // 
            // SerialPortLabel
            // 
            this.SerialPortLabel.AutoSize = true;
            this.SerialPortLabel.Location = new System.Drawing.Point(10, 52);
            this.SerialPortLabel.Name = "SerialPortLabel";
            this.SerialPortLabel.Size = new System.Drawing.Size(55, 13);
            this.SerialPortLabel.TabIndex = 13;
            this.SerialPortLabel.Text = "Serial Port";
            // 
            // SerialPortList
            // 
            this.SerialPortList.FormattingEnabled = true;
            this.SerialPortList.Location = new System.Drawing.Point(85, 44);
            this.SerialPortList.Name = "SerialPortList";
            this.SerialPortList.Size = new System.Drawing.Size(173, 21);
            this.SerialPortList.TabIndex = 14;
            // 
            // SerialOutput
            // 
            this.SerialOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SerialOutput.Location = new System.Drawing.Point(301, 133);
            this.SerialOutput.Multiline = true;
            this.SerialOutput.Name = "SerialOutput";
            this.SerialOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SerialOutput.Size = new System.Drawing.Size(213, 65);
            this.SerialOutput.TabIndex = 15;
            // 
            // EnableHoloseat
            // 
            this.EnableHoloseat.AutoSize = true;
            this.EnableHoloseat.Checked = true;
            this.EnableHoloseat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnableHoloseat.Location = new System.Drawing.Point(12, 101);
            this.EnableHoloseat.Name = "EnableHoloseat";
            this.EnableHoloseat.Size = new System.Drawing.Size(104, 17);
            this.EnableHoloseat.TabIndex = 17;
            this.EnableHoloseat.Text = "Enable Holoseat";
            this.EnableHoloseat.UseVisualStyleBackColor = true;
            this.EnableHoloseat.CheckedChanged += new System.EventHandler(this.EnableHoloseat_CheckedChanged);
            // 
            // HardwareType
            // 
            this.HardwareType.DisplayMember = "Leo";
            this.HardwareType.FormattingEnabled = true;
            this.HardwareType.Items.AddRange(new object[] {
            "leo",
            "feather"});
            this.HardwareType.Location = new System.Drawing.Point(85, 76);
            this.HardwareType.Name = "HardwareType";
            this.HardwareType.Size = new System.Drawing.Size(121, 21);
            this.HardwareType.TabIndex = 18;
            this.HardwareType.Text = "feather";
            // 
            // HardwareLabel
            // 
            this.HardwareLabel.AutoSize = true;
            this.HardwareLabel.Location = new System.Drawing.Point(9, 84);
            this.HardwareLabel.Name = "HardwareLabel";
            this.HardwareLabel.Size = new System.Drawing.Size(53, 13);
            this.HardwareLabel.TabIndex = 19;
            this.HardwareLabel.Text = "Hardware";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(298, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Serial Output";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // BackwardKeyLabel
            // 
            this.BackwardKeyLabel.AutoSize = true;
            this.BackwardKeyLabel.Location = new System.Drawing.Point(9, 146);
            this.BackwardKeyLabel.Name = "BackwardKeyLabel";
            this.BackwardKeyLabel.Size = new System.Drawing.Size(76, 13);
            this.BackwardKeyLabel.TabIndex = 21;
            this.BackwardKeyLabel.Text = "Backward Key";
            // 
            // WalkBackwardKey
            // 
            this.WalkBackwardKey.Location = new System.Drawing.Point(85, 146);
            this.WalkBackwardKey.Name = "WalkBackwardKey";
            this.WalkBackwardKey.Size = new System.Drawing.Size(173, 20);
            this.WalkBackwardKey.TabIndex = 22;
            this.WalkBackwardKey.TextChanged += new System.EventHandler(this.WalkBackwardKey_TextChanged);
            // 
            // ArduinoPathLabel
            // 
            this.ArduinoPathLabel.AutoSize = true;
            this.ArduinoPathLabel.Location = new System.Drawing.Point(10, 19);
            this.ArduinoPathLabel.Name = "ArduinoPathLabel";
            this.ArduinoPathLabel.Size = new System.Drawing.Size(68, 13);
            this.ArduinoPathLabel.TabIndex = 23;
            this.ArduinoPathLabel.Text = "Arduino Path";
            this.ArduinoPathLabel.Click += new System.EventHandler(this.label9_Click);
            // 
            // ArduinoPath
            // 
            this.ArduinoPath.Location = new System.Drawing.Point(85, 12);
            this.ArduinoPath.Name = "ArduinoPath";
            this.ArduinoPath.Size = new System.Drawing.Size(173, 20);
            this.ArduinoPath.TabIndex = 24;
            this.ArduinoPath.TextChanged += new System.EventHandler(this.ArduinoPath_TextChanged);
            // 
            // Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 287);
            this.Controls.Add(this.ArduinoPath);
            this.Controls.Add(this.ArduinoPathLabel);
            this.Controls.Add(this.BackwardKeyLabel);
            this.Controls.Add(this.WalkBackwardKey);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.HardwareLabel);
            this.Controls.Add(this.HardwareType);
            this.Controls.Add(this.EnableHoloseat);
            this.Controls.Add(this.SerialOutput);
            this.Controls.Add(this.SerialPortList);
            this.Controls.Add(this.SerialPortLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UploadResult);
            this.Controls.Add(this.UpdateDefaults);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.CloseForm);
            this.Controls.Add(this.UpdateHoloseat);
            this.Controls.Add(this.TriggerStepsNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TriggerSteps);
            this.Controls.Add(this.ForwardKeyLabel);
            this.Controls.Add(this.WalkForwardKey);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Configuration";
            this.Text = "Holoseat Configuration";
            this.Load += new System.EventHandler(this.Configuration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TriggerSteps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox WalkForwardKey;
        private System.Windows.Forms.Label ForwardKeyLabel;
        private System.Windows.Forms.TrackBar TriggerSteps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TriggerStepsNumber;
        private System.Windows.Forms.Button UpdateHoloseat;
        private System.Windows.Forms.Button CloseForm;
        private System.Windows.Forms.TextBox Result;
        private System.Windows.Forms.Button UpdateDefaults;
        private System.Windows.Forms.TextBox UploadResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label SerialPortLabel;
        private System.Windows.Forms.ComboBox SerialPortList;
        private System.Windows.Forms.TextBox SerialOutput;
        private System.Windows.Forms.CheckBox EnableHoloseat;
        private System.Windows.Forms.ComboBox HardwareType;
        private System.Windows.Forms.Label HardwareLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label BackwardKeyLabel;
        private System.Windows.Forms.TextBox WalkBackwardKey;
        private System.Windows.Forms.Label ArduinoPathLabel;
        private System.Windows.Forms.TextBox ArduinoPath;
    }
}