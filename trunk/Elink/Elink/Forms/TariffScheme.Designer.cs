namespace Elink
{
    partial class TariffScheme
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbSingle = new System.Windows.Forms.CheckBox();
            this.cbDual = new System.Windows.Forms.CheckBox();
            this.tbSingle = new System.Windows.Forms.TextBox();
            this.tbPeak = new System.Windows.Forms.TextBox();
            this.lbUnit1 = new System.Windows.Forms.Label();
            this.tbOffPeak = new System.Windows.Forms.TextBox();
            this.lbType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPricePeak = new System.Windows.Forms.TextBox();
            this.tbPriceOffPeak = new System.Windows.Forms.TextBox();
            this.lbUnit2 = new System.Windows.Forms.Label();
            this.lbUnit3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbUnit4 = new System.Windows.Forms.Label();
            this.tbMonthly = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.roundFrame10 = new Elink.RoundFrame();
            this.roundFrame9 = new Elink.RoundFrame();
            this.roundFrame8 = new Elink.RoundFrame();
            this.roundFrame7 = new Elink.RoundFrame();
            this.roundFrame6 = new Elink.RoundFrame();
            this.roundFrame5 = new Elink.RoundFrame();
            this.roundFrame4 = new Elink.RoundFrame();
            this.roundFrame3 = new Elink.RoundFrame();
            this.roundFrame2 = new Elink.RoundFrame();
            this.lb1 = new Elink.RoundFrame();
            this.tbPeakFrom = new System.Windows.Forms.MaskedTextBox();
            this.tbPeakTo = new System.Windows.Forms.MaskedTextBox();
            this.tbOffFrom = new System.Windows.Forms.MaskedTextBox();
            this.tbOffTo = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // cbSingle
            // 
            this.cbSingle.AutoSize = true;
            this.cbSingle.BackColor = System.Drawing.Color.Transparent;
            this.cbSingle.ForeColor = System.Drawing.Color.White;
            this.cbSingle.Location = new System.Drawing.Point(9, 21);
            this.cbSingle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSingle.Name = "cbSingle";
            this.cbSingle.Size = new System.Drawing.Size(85, 17);
            this.cbSingle.TabIndex = 5;
            this.cbSingle.Text = "Single Tariff";
            this.cbSingle.UseVisualStyleBackColor = false;
            this.cbSingle.CheckedChanged += new System.EventHandler(this.cbSingle_CheckedChanged);
            // 
            // cbDual
            // 
            this.cbDual.AutoSize = true;
            this.cbDual.BackColor = System.Drawing.Color.Transparent;
            this.cbDual.ForeColor = System.Drawing.Color.White;
            this.cbDual.Location = new System.Drawing.Point(9, 52);
            this.cbDual.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbDual.Name = "cbDual";
            this.cbDual.Size = new System.Drawing.Size(76, 17);
            this.cbDual.TabIndex = 6;
            this.cbDual.Text = "Dual Tariff";
            this.cbDual.UseVisualStyleBackColor = false;
            this.cbDual.CheckedChanged += new System.EventHandler(this.cbDual_CheckedChanged);
            // 
            // tbSingle
            // 
            this.tbSingle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSingle.Location = new System.Drawing.Point(118, 25);
            this.tbSingle.Name = "tbSingle";
            this.tbSingle.Size = new System.Drawing.Size(99, 12);
            this.tbSingle.TabIndex = 7;
            this.tbSingle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbPeak
            // 
            this.tbPeak.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPeak.Location = new System.Drawing.Point(115, 76);
            this.tbPeak.Name = "tbPeak";
            this.tbPeak.Size = new System.Drawing.Size(102, 12);
            this.tbPeak.TabIndex = 8;
            this.tbPeak.Text = "Peak";
            // 
            // lbUnit1
            // 
            this.lbUnit1.BackColor = System.Drawing.Color.Transparent;
            this.lbUnit1.ForeColor = System.Drawing.Color.White;
            this.lbUnit1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbUnit1.Location = new System.Drawing.Point(234, 25);
            this.lbUnit1.Name = "lbUnit1";
            this.lbUnit1.Size = new System.Drawing.Size(71, 16);
            this.lbUnit1.TabIndex = 43;
            this.lbUnit1.Text = "$/kWh";
            this.lbUnit1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbOffPeak
            // 
            this.tbOffPeak.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbOffPeak.Location = new System.Drawing.Point(115, 110);
            this.tbOffPeak.Name = "tbOffPeak";
            this.tbOffPeak.Size = new System.Drawing.Size(102, 12);
            this.tbOffPeak.TabIndex = 44;
            this.tbOffPeak.Text = "Off-Peak";
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.BackColor = System.Drawing.Color.Transparent;
            this.lbType.ForeColor = System.Drawing.Color.White;
            this.lbType.Location = new System.Drawing.Point(115, 54);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(62, 13);
            this.lbType.TabIndex = 45;
            this.lbType.Text = "Type/Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(241, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Price";
            // 
            // tbPricePeak
            // 
            this.tbPricePeak.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPricePeak.Location = new System.Drawing.Point(244, 76);
            this.tbPricePeak.Name = "tbPricePeak";
            this.tbPricePeak.Size = new System.Drawing.Size(81, 12);
            this.tbPricePeak.TabIndex = 47;
            this.tbPricePeak.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbPriceOffPeak
            // 
            this.tbPriceOffPeak.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPriceOffPeak.Location = new System.Drawing.Point(244, 109);
            this.tbPriceOffPeak.Name = "tbPriceOffPeak";
            this.tbPriceOffPeak.Size = new System.Drawing.Size(81, 12);
            this.tbPriceOffPeak.TabIndex = 48;
            this.tbPriceOffPeak.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbUnit2
            // 
            this.lbUnit2.BackColor = System.Drawing.Color.Transparent;
            this.lbUnit2.ForeColor = System.Drawing.Color.White;
            this.lbUnit2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbUnit2.Location = new System.Drawing.Point(339, 74);
            this.lbUnit2.Name = "lbUnit2";
            this.lbUnit2.Size = new System.Drawing.Size(71, 16);
            this.lbUnit2.TabIndex = 49;
            this.lbUnit2.Text = "$/kWh";
            this.lbUnit2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbUnit3
            // 
            this.lbUnit3.BackColor = System.Drawing.Color.Transparent;
            this.lbUnit3.ForeColor = System.Drawing.Color.White;
            this.lbUnit3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbUnit3.Location = new System.Drawing.Point(339, 107);
            this.lbUnit3.Name = "lbUnit3";
            this.lbUnit3.Size = new System.Drawing.Size(71, 16);
            this.lbUnit3.TabIndex = 50;
            this.lbUnit3.Text = "$/kWh";
            this.lbUnit3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(407, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 51;
            this.label6.Text = "From";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(469, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 53;
            this.label7.Text = "To";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(6, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 13);
            this.label8.TabIndex = 57;
            this.label8.Text = "Monthly Standing Charge*";
            // 
            // lbUnit4
            // 
            this.lbUnit4.BackColor = System.Drawing.Color.Transparent;
            this.lbUnit4.ForeColor = System.Drawing.Color.White;
            this.lbUnit4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbUnit4.Location = new System.Drawing.Point(232, 165);
            this.lbUnit4.Name = "lbUnit4";
            this.lbUnit4.Size = new System.Drawing.Size(123, 21);
            this.lbUnit4.TabIndex = 59;
            this.lbUnit4.Text = "$/kWh/Month";
            this.lbUnit4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbMonthly
            // 
            this.tbMonthly.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMonthly.Location = new System.Drawing.Point(118, 168);
            this.tbMonthly.Name = "tbMonthly";
            this.tbMonthly.Size = new System.Drawing.Size(100, 12);
            this.tbMonthly.TabIndex = 58;
            this.tbMonthly.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(6, 204);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(296, 13);
            this.label10.TabIndex = 60;
            this.label10.Text = "*If this is given on your bill as a Quarterly figure,divide it by 4.";
            // 
            // roundFrame10
            // 
            this.roundFrame10.BackColor = System.Drawing.Color.Transparent;
            this.roundFrame10.ForeColor = System.Drawing.Color.DimGray;
            this.roundFrame10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.roundFrame10.Location = new System.Drawing.Point(108, 161);
            this.roundFrame10.Name = "roundFrame10";
            this.roundFrame10.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.roundFrame10.Size = new System.Drawing.Size(121, 26);
            this.roundFrame10.TabIndex = 70;
            // 
            // roundFrame9
            // 
            this.roundFrame9.BackColor = System.Drawing.Color.Transparent;
            this.roundFrame9.ForeColor = System.Drawing.Color.DimGray;
            this.roundFrame9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.roundFrame9.Location = new System.Drawing.Point(464, 104);
            this.roundFrame9.Name = "roundFrame9";
            this.roundFrame9.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.roundFrame9.Size = new System.Drawing.Size(61, 26);
            this.roundFrame9.TabIndex = 69;
            // 
            // roundFrame8
            // 
            this.roundFrame8.BackColor = System.Drawing.Color.Transparent;
            this.roundFrame8.ForeColor = System.Drawing.Color.DimGray;
            this.roundFrame8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.roundFrame8.Location = new System.Drawing.Point(465, 71);
            this.roundFrame8.Name = "roundFrame8";
            this.roundFrame8.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.roundFrame8.Size = new System.Drawing.Size(61, 26);
            this.roundFrame8.TabIndex = 68;
            // 
            // roundFrame7
            // 
            this.roundFrame7.BackColor = System.Drawing.Color.Transparent;
            this.roundFrame7.ForeColor = System.Drawing.Color.DimGray;
            this.roundFrame7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.roundFrame7.Location = new System.Drawing.Point(401, 104);
            this.roundFrame7.Name = "roundFrame7";
            this.roundFrame7.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.roundFrame7.Size = new System.Drawing.Size(61, 26);
            this.roundFrame7.TabIndex = 67;
            // 
            // roundFrame6
            // 
            this.roundFrame6.BackColor = System.Drawing.Color.Transparent;
            this.roundFrame6.ForeColor = System.Drawing.Color.DimGray;
            this.roundFrame6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.roundFrame6.Location = new System.Drawing.Point(402, 71);
            this.roundFrame6.Name = "roundFrame6";
            this.roundFrame6.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.roundFrame6.Size = new System.Drawing.Size(61, 26);
            this.roundFrame6.TabIndex = 66;
            // 
            // roundFrame5
            // 
            this.roundFrame5.BackColor = System.Drawing.Color.Transparent;
            this.roundFrame5.ForeColor = System.Drawing.Color.DimGray;
            this.roundFrame5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.roundFrame5.Location = new System.Drawing.Point(231, 102);
            this.roundFrame5.Name = "roundFrame5";
            this.roundFrame5.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.roundFrame5.Size = new System.Drawing.Size(105, 26);
            this.roundFrame5.TabIndex = 65;
            // 
            // roundFrame4
            // 
            this.roundFrame4.BackColor = System.Drawing.Color.Transparent;
            this.roundFrame4.ForeColor = System.Drawing.Color.DimGray;
            this.roundFrame4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.roundFrame4.Location = new System.Drawing.Point(231, 69);
            this.roundFrame4.Name = "roundFrame4";
            this.roundFrame4.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.roundFrame4.Size = new System.Drawing.Size(105, 26);
            this.roundFrame4.TabIndex = 64;
            // 
            // roundFrame3
            // 
            this.roundFrame3.BackColor = System.Drawing.Color.Transparent;
            this.roundFrame3.ForeColor = System.Drawing.Color.DimGray;
            this.roundFrame3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.roundFrame3.Location = new System.Drawing.Point(108, 103);
            this.roundFrame3.Name = "roundFrame3";
            this.roundFrame3.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.roundFrame3.Size = new System.Drawing.Size(121, 26);
            this.roundFrame3.TabIndex = 63;
            // 
            // roundFrame2
            // 
            this.roundFrame2.BackColor = System.Drawing.Color.Transparent;
            this.roundFrame2.ForeColor = System.Drawing.Color.DimGray;
            this.roundFrame2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.roundFrame2.Location = new System.Drawing.Point(108, 69);
            this.roundFrame2.Name = "roundFrame2";
            this.roundFrame2.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.roundFrame2.Size = new System.Drawing.Size(121, 26);
            this.roundFrame2.TabIndex = 62;
            // 
            // lb1
            // 
            this.lb1.BackColor = System.Drawing.Color.Transparent;
            this.lb1.ForeColor = System.Drawing.Color.DimGray;
            this.lb1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb1.Location = new System.Drawing.Point(108, 18);
            this.lb1.Name = "lb1";
            this.lb1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lb1.Size = new System.Drawing.Size(121, 26);
            this.lb1.TabIndex = 61;
            // 
            // tbPeakFrom
            // 
            this.tbPeakFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPeakFrom.Location = new System.Drawing.Point(415, 78);
            this.tbPeakFrom.Mask = "90:00";
            this.tbPeakFrom.Name = "tbPeakFrom";
            this.tbPeakFrom.Size = new System.Drawing.Size(36, 12);
            this.tbPeakFrom.TabIndex = 71;
            this.tbPeakFrom.ValidatingType = typeof(System.DateTime);
            // 
            // tbPeakTo
            // 
            this.tbPeakTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPeakTo.Location = new System.Drawing.Point(478, 78);
            this.tbPeakTo.Mask = "90:00";
            this.tbPeakTo.Name = "tbPeakTo";
            this.tbPeakTo.Size = new System.Drawing.Size(36, 12);
            this.tbPeakTo.TabIndex = 72;
            this.tbPeakTo.ValidatingType = typeof(System.DateTime);
            // 
            // tbOffFrom
            // 
            this.tbOffFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbOffFrom.Location = new System.Drawing.Point(415, 111);
            this.tbOffFrom.Mask = "90:00";
            this.tbOffFrom.Name = "tbOffFrom";
            this.tbOffFrom.Size = new System.Drawing.Size(36, 12);
            this.tbOffFrom.TabIndex = 73;
            this.tbOffFrom.ValidatingType = typeof(System.DateTime);
            // 
            // tbOffTo
            // 
            this.tbOffTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbOffTo.Location = new System.Drawing.Point(478, 111);
            this.tbOffTo.Mask = "90:00";
            this.tbOffTo.Name = "tbOffTo";
            this.tbOffTo.Size = new System.Drawing.Size(36, 12);
            this.tbOffTo.TabIndex = 74;
            this.tbOffTo.ValidatingType = typeof(System.DateTime);
            // 
            // TariffScheme
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            this.Controls.Add(this.tbOffTo);
            this.Controls.Add(this.tbOffFrom);
            this.Controls.Add(this.tbPeakTo);
            this.Controls.Add(this.tbPeakFrom);
            this.Controls.Add(this.tbMonthly);
            this.Controls.Add(this.roundFrame10);
            this.Controls.Add(this.roundFrame9);
            this.Controls.Add(this.roundFrame8);
            this.Controls.Add(this.roundFrame7);
            this.Controls.Add(this.roundFrame6);
            this.Controls.Add(this.tbPriceOffPeak);
            this.Controls.Add(this.roundFrame5);
            this.Controls.Add(this.tbPricePeak);
            this.Controls.Add(this.roundFrame4);
            this.Controls.Add(this.tbOffPeak);
            this.Controls.Add(this.roundFrame3);
            this.Controls.Add(this.tbPeak);
            this.Controls.Add(this.roundFrame2);
            this.Controls.Add(this.tbSingle);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbUnit4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbUnit3);
            this.Controls.Add(this.lbUnit2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbType);
            this.Controls.Add(this.lbUnit1);
            this.Controls.Add(this.cbDual);
            this.Controls.Add(this.cbSingle);
            this.Font = new System.Drawing.Font("Arial", 7.5F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TariffScheme";
            this.Size = new System.Drawing.Size(546, 234);
            this.Load += new System.EventHandler(this.TariffScheme_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSingle;
        private System.Windows.Forms.CheckBox cbDual;
        private System.Windows.Forms.TextBox tbSingle;
        private System.Windows.Forms.TextBox tbPeak;
        private System.Windows.Forms.Label lbUnit1;
        private System.Windows.Forms.TextBox tbOffPeak;
        private System.Windows.Forms.Label lbType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPricePeak;
        private System.Windows.Forms.TextBox tbPriceOffPeak;
        private System.Windows.Forms.Label lbUnit2;
        private System.Windows.Forms.Label lbUnit3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbUnit4;
        private System.Windows.Forms.TextBox tbMonthly;
        private System.Windows.Forms.Label label10;
        private RoundFrame lb1;
        private RoundFrame roundFrame2;
        private RoundFrame roundFrame3;
        private RoundFrame roundFrame4;
        private RoundFrame roundFrame5;
        private RoundFrame roundFrame6;
        private RoundFrame roundFrame7;
        private RoundFrame roundFrame8;
        private RoundFrame roundFrame9;
        private RoundFrame roundFrame10;
        private System.Windows.Forms.MaskedTextBox tbPeakFrom;
        private System.Windows.Forms.MaskedTextBox tbPeakTo;
        private System.Windows.Forms.MaskedTextBox tbOffFrom;
        private System.Windows.Forms.MaskedTextBox tbOffTo;
    }
}
