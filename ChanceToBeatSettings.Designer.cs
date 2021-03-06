﻿namespace LiveSplit.ChanceToBeat
{
    partial class ChanceToBeatSettings
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
            if (disposing)
            {
                if (CurrentState != null)
                {
                    CurrentState.OnReset -= UpdatePb;
                }

                components?.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnColor2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkTwoRows = new System.Windows.Forms.CheckBox();
            this.btnColor1 = new System.Windows.Forms.Button();
            this.cmbGradientType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chkOverrideTextColor = new System.Windows.Forms.CheckBox();
            this.lblTextColor = new System.Windows.Forms.Label();
            this.btnTextColor = new System.Windows.Forms.Button();
            this.dataGridSplits = new System.Windows.Forms.DataGridView();
            this.SplitNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResetChances = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBarWeight = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.chkUsePb = new System.Windows.Forms.CheckBox();
            this.lblCustomCutoff = new System.Windows.Forms.Label();
            this.txtBoxTimeCutoff = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxText = new System.Windows.Forms.TextBox();
            this.toolTipWeight = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSplits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWeight)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 212F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnColor2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkTwoRows, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnColor1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbGradientType, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dataGridSplits, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.trackBarWeight, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtBoxText, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(617, 632);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnColor2
            // 
            this.btnColor2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColor2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnColor2.Location = new System.Drawing.Point(254, 3);
            this.btnColor2.Name = "btnColor2";
            this.btnColor2.Size = new System.Drawing.Size(33, 30);
            this.btnColor2.TabIndex = 3;
            this.btnColor2.UseVisualStyleBackColor = true;
            this.btnColor2.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Background Color:";
            // 
            // chkTwoRows
            // 
            this.chkTwoRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTwoRows.AutoSize = true;
            this.chkTwoRows.Location = new System.Drawing.Point(9, 43);
            this.chkTwoRows.Margin = new System.Windows.Forms.Padding(9, 4, 4, 4);
            this.chkTwoRows.Name = "chkTwoRows";
            this.chkTwoRows.Size = new System.Drawing.Size(199, 21);
            this.chkTwoRows.TabIndex = 1;
            this.chkTwoRows.Text = "Display 2 Rows";
            this.chkTwoRows.UseVisualStyleBackColor = true;
            // 
            // btnColor1
            // 
            this.btnColor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColor1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnColor1.Location = new System.Drawing.Point(215, 3);
            this.btnColor1.Name = "btnColor1";
            this.btnColor1.Size = new System.Drawing.Size(33, 30);
            this.btnColor1.TabIndex = 2;
            this.btnColor1.UseVisualStyleBackColor = true;
            this.btnColor1.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // cmbGradientType
            // 
            this.cmbGradientType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGradientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGradientType.FormattingEnabled = true;
            this.cmbGradientType.Items.AddRange(new object[] {
            "Plain",
            "Vertical",
            "Horizontal"});
            this.cmbGradientType.Location = new System.Drawing.Point(293, 5);
            this.cmbGradientType.Name = "cmbGradientType";
            this.cmbGradientType.Size = new System.Drawing.Size(321, 24);
            this.cmbGradientType.TabIndex = 4;
            this.cmbGradientType.SelectedIndexChanged += new System.EventHandler(this.cmbGradientType_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 4);
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(4, 112);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(609, 94);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Text Color";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 204F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.chkOverrideTextColor, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTextColor, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnTextColor, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 19);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(601, 71);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // chkOverrideTextColor
            // 
            this.chkOverrideTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOverrideTextColor.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.chkOverrideTextColor, 3);
            this.chkOverrideTextColor.Location = new System.Drawing.Point(9, 7);
            this.chkOverrideTextColor.Margin = new System.Windows.Forms.Padding(9, 4, 4, 4);
            this.chkOverrideTextColor.Name = "chkOverrideTextColor";
            this.chkOverrideTextColor.Size = new System.Drawing.Size(588, 21);
            this.chkOverrideTextColor.TabIndex = 0;
            this.chkOverrideTextColor.Text = "Override Layout Settings";
            this.chkOverrideTextColor.UseVisualStyleBackColor = true;
            this.chkOverrideTextColor.CheckedChanged += new System.EventHandler(this.chkOverrideTextColor_CheckedChanged);
            // 
            // lblTextColor
            // 
            this.lblTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTextColor.AutoSize = true;
            this.lblTextColor.Location = new System.Drawing.Point(4, 45);
            this.lblTextColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTextColor.Name = "lblTextColor";
            this.lblTextColor.Size = new System.Drawing.Size(196, 17);
            this.lblTextColor.TabIndex = 1;
            this.lblTextColor.Text = "Color:";
            // 
            // btnTextColor
            // 
            this.btnTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTextColor.Location = new System.Drawing.Point(208, 40);
            this.btnTextColor.Margin = new System.Windows.Forms.Padding(4);
            this.btnTextColor.Name = "btnTextColor";
            this.btnTextColor.Size = new System.Drawing.Size(31, 28);
            this.btnTextColor.TabIndex = 2;
            this.btnTextColor.UseVisualStyleBackColor = false;
            this.btnTextColor.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // dataGridSplits
            // 
            this.dataGridSplits.AllowUserToAddRows = false;
            this.dataGridSplits.AllowUserToDeleteRows = false;
            this.dataGridSplits.AllowUserToResizeColumns = false;
            this.dataGridSplits.AllowUserToResizeRows = false;
            this.dataGridSplits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSplits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SplitNames,
            this.ResetChances});
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridSplits, 4);
            this.dataGridSplits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridSplits.Location = new System.Drawing.Point(3, 351);
            this.dataGridSplits.Name = "dataGridSplits";
            this.dataGridSplits.RowHeadersVisible = false;
            this.dataGridSplits.RowTemplate.Height = 24;
            this.dataGridSplits.Size = new System.Drawing.Size(611, 278);
            this.dataGridSplits.TabIndex = 10;
            this.dataGridSplits.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridSplits_CellValidating);
            // 
            // SplitNames
            // 
            this.SplitNames.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SplitNames.HeaderText = "Segment Name";
            this.SplitNames.Name = "SplitNames";
            this.SplitNames.ReadOnly = true;
            // 
            // ResetChances
            // 
            this.ResetChances.HeaderText = "Reset Chance";
            this.ResetChances.Name = "ResetChances";
            this.ResetChances.Width = 150;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 321);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Weight to previous attempts:";
            // 
            // trackBarWeight
            // 
            this.trackBarWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.trackBarWeight, 3);
            this.trackBarWeight.LargeChange = 10;
            this.trackBarWeight.Location = new System.Drawing.Point(215, 315);
            this.trackBarWeight.Maximum = 1000;
            this.trackBarWeight.Name = "trackBarWeight";
            this.trackBarWeight.Size = new System.Drawing.Size(399, 30);
            this.trackBarWeight.TabIndex = 12;
            this.trackBarWeight.TickFrequency = 0;
            this.trackBarWeight.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarWeight.Scroll += new System.EventHandler(this.trackBarWeight_Scroll);
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 4);
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(4, 214);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(609, 94);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Time Cutoff";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 204F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.chkUsePb, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblCustomCutoff, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtBoxTimeCutoff, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 19);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(601, 71);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // chkUsePb
            // 
            this.chkUsePb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUsePb.AutoSize = true;
            this.chkUsePb.Checked = true;
            this.chkUsePb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel3.SetColumnSpan(this.chkUsePb, 2);
            this.chkUsePb.Location = new System.Drawing.Point(9, 7);
            this.chkUsePb.Margin = new System.Windows.Forms.Padding(9, 4, 4, 4);
            this.chkUsePb.Name = "chkUsePb";
            this.chkUsePb.Size = new System.Drawing.Size(588, 21);
            this.chkUsePb.TabIndex = 0;
            this.chkUsePb.Text = "Use Personal Best";
            this.chkUsePb.UseVisualStyleBackColor = true;
            this.chkUsePb.CheckedChanged += new System.EventHandler(this.chkUsePb_CheckedChanged);
            // 
            // lblCustomCutoff
            // 
            this.lblCustomCutoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustomCutoff.AutoSize = true;
            this.lblCustomCutoff.Enabled = false;
            this.lblCustomCutoff.Location = new System.Drawing.Point(4, 45);
            this.lblCustomCutoff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCustomCutoff.Name = "lblCustomCutoff";
            this.lblCustomCutoff.Size = new System.Drawing.Size(196, 17);
            this.lblCustomCutoff.TabIndex = 1;
            this.lblCustomCutoff.Text = "Custom Cutoff:";
            // 
            // txtBoxTimeCutoff
            // 
            this.txtBoxTimeCutoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxTimeCutoff.Enabled = false;
            this.txtBoxTimeCutoff.Location = new System.Drawing.Point(207, 43);
            this.txtBoxTimeCutoff.Name = "txtBoxTimeCutoff";
            this.txtBoxTimeCutoff.Size = new System.Drawing.Size(391, 22);
            this.txtBoxTimeCutoff.TabIndex = 2;
            this.txtBoxTimeCutoff.Validating += new System.ComponentModel.CancelEventHandler(this.txtBoxTimeCutoff_Validating);
            this.txtBoxTimeCutoff.Validated += new System.EventHandler(this.txtBoxTimeCutoff_Validated);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Text:";
            // 
            // txtBoxText
            // 
            this.txtBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtBoxText, 3);
            this.txtBoxText.Location = new System.Drawing.Point(215, 79);
            this.txtBoxText.Name = "txtBoxText";
            this.txtBoxText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtBoxText.Size = new System.Drawing.Size(399, 22);
            this.txtBoxText.TabIndex = 8;
            // 
            // ChanceToBeatSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChanceToBeatSettings";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.Size = new System.Drawing.Size(635, 650);
            this.Load += new System.EventHandler(this.TimeProbabilitySettings_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSplits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWeight)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkTwoRows;
        private System.Windows.Forms.Button btnColor2;
        private System.Windows.Forms.Button btnColor1;
        private System.Windows.Forms.ComboBox cmbGradientType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox chkOverrideTextColor;
        private System.Windows.Forms.Label lblTextColor;
        private System.Windows.Forms.Button btnTextColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxText;
        private System.Windows.Forms.DataGridView dataGridSplits;
        private System.Windows.Forms.DataGridViewTextBoxColumn SplitNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResetChances;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBarWeight;
        private System.Windows.Forms.ToolTip toolTipWeight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox chkUsePb;
        private System.Windows.Forms.Label lblCustomCutoff;
        private System.Windows.Forms.TextBox txtBoxTimeCutoff;
    }
}
