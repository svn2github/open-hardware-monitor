﻿/*
  
  Version: MPL 1.1/GPL 2.0/LGPL 2.1

  The contents of this file are subject to the Mozilla Public License Version
  1.1 (the "License"); you may not use this file except in compliance with
  the License. You may obtain a copy of the License at
 
  http://www.mozilla.org/MPL/

  Software distributed under the License is distributed on an "AS IS" basis,
  WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
  for the specific language governing rights and limitations under the License.

  The Original Code is the Open Hardware Monitor code.

  The Initial Developer of the Original Code is 
  Michael Möller <m.moeller@gmx.ch>.
  Portions created by the Initial Developer are Copyright (C) 2009-2010
  the Initial Developer. All Rights Reserved.

  Contributor(s):

  Alternatively, the contents of this file may be used under the terms of
  either the GNU General Public License Version 2 or later (the "GPL"), or
  the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
  in which case the provisions of the GPL or the LGPL are applicable instead
  of those above. If you wish to allow use of your version of this file only
  under the terms of either the GPL or the LGPL, and not to allow others to
  use your version of this file under the terms of the MPL, indicate your
  decision by deleting the provisions above and replace them with the notice
  and other provisions required by the GPL or the LGPL. If you do not delete
  the provisions above, a recipient may use your version of this file under
  the terms of any one of the MPL, the GPL or the LGPL.
 
*/

namespace OpenHardwareMonitor.GUI {
  partial class MainForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.sensor = new Aga.Controls.Tree.TreeColumn();
      this.value = new Aga.Controls.Tree.TreeColumn();
      this.min = new Aga.Controls.Tree.TreeColumn();
      this.max = new Aga.Controls.Tree.TreeColumn();
      this.nodeImage = new Aga.Controls.Tree.NodeControls.NodeIcon();
      this.nodeCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
      this.nodeTextBoxText = new Aga.Controls.Tree.NodeControls.NodeTextBox();
      this.nodeTextBoxValue = new Aga.Controls.Tree.NodeControls.NodeTextBox();
      this.nodeTextBoxMin = new Aga.Controls.Tree.NodeControls.NodeTextBox();
      this.nodeTextBoxMax = new Aga.Controls.Tree.NodeControls.NodeTextBox();
      this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
      this.fileMenuItem = new System.Windows.Forms.MenuItem();
      this.saveReportMenuItem = new System.Windows.Forms.MenuItem();
      this.sumbitReportMenuItem = new System.Windows.Forms.MenuItem();
      this.MenuItem2 = new System.Windows.Forms.MenuItem();
      this.exitMenuItem = new System.Windows.Forms.MenuItem();
      this.viewMenuItem = new System.Windows.Forms.MenuItem();
      this.resetMinMaxMenuItem = new System.Windows.Forms.MenuItem();
      this.MenuItem3 = new System.Windows.Forms.MenuItem();
      this.hiddenMenuItem = new System.Windows.Forms.MenuItem();
      this.plotMenuItem = new System.Windows.Forms.MenuItem();
      this.gadgetMenuItem = new System.Windows.Forms.MenuItem();
      this.MenuItem1 = new System.Windows.Forms.MenuItem();
      this.columnsMenuItem = new System.Windows.Forms.MenuItem();
      this.valueMenuItem = new System.Windows.Forms.MenuItem();
      this.minMenuItem = new System.Windows.Forms.MenuItem();
      this.maxMenuItem = new System.Windows.Forms.MenuItem();
      this.optionsMenuItem = new System.Windows.Forms.MenuItem();
      this.startMinMenuItem = new System.Windows.Forms.MenuItem();
      this.minTrayMenuItem = new System.Windows.Forms.MenuItem();
      this.minCloseMenuItem = new System.Windows.Forms.MenuItem();
      this.startupMenuItem = new System.Windows.Forms.MenuItem();
      this.separatorMenuItem = new System.Windows.Forms.MenuItem();
      this.temperatureUnitsMenuItem = new System.Windows.Forms.MenuItem();
      this.celciusMenuItem = new System.Windows.Forms.MenuItem();
      this.fahrenheitMenuItem = new System.Windows.Forms.MenuItem();
      this.MenuItem4 = new System.Windows.Forms.MenuItem();
      this.hddMenuItem = new System.Windows.Forms.MenuItem();
      this.helpMenuItem = new System.Windows.Forms.MenuItem();
      this.aboutMenuItem = new System.Windows.Forms.MenuItem();
      this.sensorContextMenu = new System.Windows.Forms.ContextMenu();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.timer = new System.Windows.Forms.Timer(this.components);
      this.splitContainer = new OpenHardwareMonitor.GUI.SplitContainerAdv();
      this.treeView = new Aga.Controls.Tree.TreeViewAdv();
      this.plotPanel = new OpenHardwareMonitor.GUI.PlotPanel();
      this.resetMenuItem = new System.Windows.Forms.MenuItem();
      this.menuItem6 = new System.Windows.Forms.MenuItem();
      this.splitContainer.Panel1.SuspendLayout();
      this.splitContainer.Panel2.SuspendLayout();
      this.splitContainer.SuspendLayout();
      this.SuspendLayout();
      // 
      // sensor
      // 
      this.sensor.Header = "Sensor";
      this.sensor.SortOrder = System.Windows.Forms.SortOrder.None;
      this.sensor.TooltipText = null;
      this.sensor.Width = 250;
      // 
      // value
      // 
      this.value.Header = "Value";
      this.value.SortOrder = System.Windows.Forms.SortOrder.None;
      this.value.TooltipText = null;
      this.value.Width = 100;
      // 
      // min
      // 
      this.min.Header = "Min";
      this.min.SortOrder = System.Windows.Forms.SortOrder.None;
      this.min.TooltipText = null;
      this.min.Width = 100;
      // 
      // max
      // 
      this.max.Header = "Max";
      this.max.SortOrder = System.Windows.Forms.SortOrder.None;
      this.max.TooltipText = null;
      this.max.Width = 100;
      // 
      // nodeImage
      // 
      this.nodeImage.DataPropertyName = "Image";
      this.nodeImage.LeftMargin = 1;
      this.nodeImage.ParentColumn = this.sensor;
      this.nodeImage.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Fit;
      // 
      // nodeCheckBox
      // 
      this.nodeCheckBox.DataPropertyName = "Plot";
      this.nodeCheckBox.EditEnabled = true;
      this.nodeCheckBox.LeftMargin = 3;
      this.nodeCheckBox.ParentColumn = this.sensor;
      // 
      // nodeTextBoxText
      // 
      this.nodeTextBoxText.DataPropertyName = "Text";
      this.nodeTextBoxText.EditEnabled = true;
      this.nodeTextBoxText.IncrementalSearchEnabled = true;
      this.nodeTextBoxText.LeftMargin = 3;
      this.nodeTextBoxText.ParentColumn = this.sensor;
      this.nodeTextBoxText.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
      // 
      // nodeTextBoxValue
      // 
      this.nodeTextBoxValue.DataPropertyName = "Value";
      this.nodeTextBoxValue.IncrementalSearchEnabled = true;
      this.nodeTextBoxValue.LeftMargin = 3;
      this.nodeTextBoxValue.ParentColumn = this.value;
      this.nodeTextBoxValue.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
      // 
      // nodeTextBoxMin
      // 
      this.nodeTextBoxMin.DataPropertyName = "Min";
      this.nodeTextBoxMin.IncrementalSearchEnabled = true;
      this.nodeTextBoxMin.LeftMargin = 3;
      this.nodeTextBoxMin.ParentColumn = this.min;
      this.nodeTextBoxMin.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
      // 
      // nodeTextBoxMax
      // 
      this.nodeTextBoxMax.DataPropertyName = "Max";
      this.nodeTextBoxMax.IncrementalSearchEnabled = true;
      this.nodeTextBoxMax.LeftMargin = 3;
      this.nodeTextBoxMax.ParentColumn = this.max;
      this.nodeTextBoxMax.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
      // 
      // mainMenu
      // 
      this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.viewMenuItem,
            this.optionsMenuItem,
            this.helpMenuItem});
      // 
      // fileMenuItem
      // 
      this.fileMenuItem.Index = 0;
      this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.saveReportMenuItem,
            this.sumbitReportMenuItem,
            this.MenuItem2,
            this.resetMenuItem,
            this.menuItem6,
            this.exitMenuItem});
      this.fileMenuItem.Text = "File";
      // 
      // saveReportMenuItem
      // 
      this.saveReportMenuItem.Index = 0;
      this.saveReportMenuItem.Text = "Save Report...";
      this.saveReportMenuItem.Click += new System.EventHandler(this.saveReportMenuItem_Click);
      // 
      // sumbitReportMenuItem
      // 
      this.sumbitReportMenuItem.Index = 1;
      this.sumbitReportMenuItem.Text = "Submit Report...";
      this.sumbitReportMenuItem.Click += new System.EventHandler(this.sumbitReportMenuItem_Click);
      // 
      // MenuItem2
      // 
      this.MenuItem2.Index = 2;
      this.MenuItem2.Text = "-";
      // 
      // exitMenuItem
      // 
      this.exitMenuItem.Index = 5;
      this.exitMenuItem.Text = "Exit";
      this.exitMenuItem.Click += new System.EventHandler(this.exitClick);
      // 
      // viewMenuItem
      // 
      this.viewMenuItem.Index = 1;
      this.viewMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.resetMinMaxMenuItem,
            this.MenuItem3,
            this.hiddenMenuItem,
            this.plotMenuItem,
            this.gadgetMenuItem,
            this.MenuItem1,
            this.columnsMenuItem});
      this.viewMenuItem.Text = "View";
      // 
      // resetMinMaxMenuItem
      // 
      this.resetMinMaxMenuItem.Index = 0;
      this.resetMinMaxMenuItem.Text = "Reset Min/Max";
      this.resetMinMaxMenuItem.Click += new System.EventHandler(this.resetMinMaxMenuItem_Click);
      // 
      // MenuItem3
      // 
      this.MenuItem3.Index = 1;
      this.MenuItem3.Text = "-";
      // 
      // hiddenMenuItem
      // 
      this.hiddenMenuItem.Index = 2;
      this.hiddenMenuItem.Text = "Show Hidden Sensors";
      // 
      // plotMenuItem
      // 
      this.plotMenuItem.Index = 3;
      this.plotMenuItem.Text = "Show Plot";
      // 
      // gadgetMenuItem
      // 
      this.gadgetMenuItem.Index = 4;
      this.gadgetMenuItem.Text = "Show Gadget";
      // 
      // MenuItem1
      // 
      this.MenuItem1.Index = 5;
      this.MenuItem1.Text = "-";
      // 
      // columnsMenuItem
      // 
      this.columnsMenuItem.Index = 6;
      this.columnsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.valueMenuItem,
            this.minMenuItem,
            this.maxMenuItem});
      this.columnsMenuItem.Text = "Columns";
      // 
      // valueMenuItem
      // 
      this.valueMenuItem.Index = 0;
      this.valueMenuItem.Text = "Value";
      // 
      // minMenuItem
      // 
      this.minMenuItem.Index = 1;
      this.minMenuItem.Text = "Min";
      // 
      // maxMenuItem
      // 
      this.maxMenuItem.Index = 2;
      this.maxMenuItem.Text = "Max";
      // 
      // optionsMenuItem
      // 
      this.optionsMenuItem.Index = 2;
      this.optionsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.startMinMenuItem,
            this.minTrayMenuItem,
            this.minCloseMenuItem,
            this.startupMenuItem,
            this.separatorMenuItem,
            this.temperatureUnitsMenuItem,
            this.MenuItem4,
            this.hddMenuItem});
      this.optionsMenuItem.Text = "Options";
      // 
      // startMinMenuItem
      // 
      this.startMinMenuItem.Index = 0;
      this.startMinMenuItem.Text = "Start Minimized";
      // 
      // minTrayMenuItem
      // 
      this.minTrayMenuItem.Index = 1;
      this.minTrayMenuItem.Text = "Minimize To Tray";
      // 
      // minCloseMenuItem
      // 
      this.minCloseMenuItem.Index = 2;
      this.minCloseMenuItem.Text = "Minimize On Close";
      // 
      // startupMenuItem
      // 
      this.startupMenuItem.Index = 3;
      this.startupMenuItem.Text = "Run On Windows Startup";
      // 
      // separatorMenuItem
      // 
      this.separatorMenuItem.Index = 4;
      this.separatorMenuItem.Text = "-";
      // 
      // temperatureUnitsMenuItem
      // 
      this.temperatureUnitsMenuItem.Index = 5;
      this.temperatureUnitsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.celciusMenuItem,
            this.fahrenheitMenuItem});
      this.temperatureUnitsMenuItem.Text = "Temperature Unit";
      // 
      // celciusMenuItem
      // 
      this.celciusMenuItem.Index = 0;
      this.celciusMenuItem.Text = "Celcius";
      this.celciusMenuItem.Click += new System.EventHandler(this.celciusMenuItem_Click);
      // 
      // fahrenheitMenuItem
      // 
      this.fahrenheitMenuItem.Index = 1;
      this.fahrenheitMenuItem.Text = "Fahrenheit";
      this.fahrenheitMenuItem.Click += new System.EventHandler(this.fahrenheitMenuItem_Click);
      // 
      // MenuItem4
      // 
      this.MenuItem4.Index = 6;
      this.MenuItem4.Text = "-";
      // 
      // hddMenuItem
      // 
      this.hddMenuItem.Index = 7;
      this.hddMenuItem.Text = "Read HDD sensors";
      // 
      // helpMenuItem
      // 
      this.helpMenuItem.Index = 3;
      this.helpMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.aboutMenuItem});
      this.helpMenuItem.Text = "Help";
      // 
      // aboutMenuItem
      // 
      this.aboutMenuItem.Index = 0;
      this.aboutMenuItem.Text = "About";
      this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
      // 
      // saveFileDialog
      // 
      this.saveFileDialog.DefaultExt = "txt";
      this.saveFileDialog.FileName = "OpenHardwareMonitor.Report.txt";
      this.saveFileDialog.Filter = "Text Documents|*.txt|All Files|*.*";
      this.saveFileDialog.RestoreDirectory = true;
      this.saveFileDialog.Title = "Save Report As";
      // 
      // timer
      // 
      this.timer.Interval = 1000;
      this.timer.Tick += new System.EventHandler(this.timer_Tick);
      // 
      // splitContainer
      // 
      this.splitContainer.Border3DStyle = System.Windows.Forms.Border3DStyle.Raised;
      this.splitContainer.Color = System.Drawing.SystemColors.Control;
      this.splitContainer.Cursor = System.Windows.Forms.Cursors.Default;
      this.splitContainer.Location = new System.Drawing.Point(12, 12);
      this.splitContainer.Name = "splitContainer";
      this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer.Panel1
      // 
      this.splitContainer.Panel1.Controls.Add(this.treeView);
      // 
      // splitContainer.Panel2
      // 
      this.splitContainer.Panel2.Controls.Add(this.plotPanel);
      this.splitContainer.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
      this.splitContainer.Size = new System.Drawing.Size(386, 483);
      this.splitContainer.SplitterDistance = 354;
      this.splitContainer.SplitterWidth = 5;
      this.splitContainer.TabIndex = 3;
      // 
      // treeView
      // 
      this.treeView.BackColor = System.Drawing.SystemColors.Window;
      this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.treeView.Columns.Add(this.sensor);
      this.treeView.Columns.Add(this.value);
      this.treeView.Columns.Add(this.min);
      this.treeView.Columns.Add(this.max);
      this.treeView.DefaultToolTipProvider = null;
      this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView.DragDropMarkColor = System.Drawing.Color.Black;
      this.treeView.FullRowSelect = true;
      this.treeView.GridLineStyle = Aga.Controls.Tree.GridLineStyle.Horizontal;
      this.treeView.LineColor = System.Drawing.SystemColors.ControlDark;
      this.treeView.Location = new System.Drawing.Point(0, 0);
      this.treeView.Model = null;
      this.treeView.Name = "treeView";
      this.treeView.NodeControls.Add(this.nodeImage);
      this.treeView.NodeControls.Add(this.nodeCheckBox);
      this.treeView.NodeControls.Add(this.nodeTextBoxText);
      this.treeView.NodeControls.Add(this.nodeTextBoxValue);
      this.treeView.NodeControls.Add(this.nodeTextBoxMin);
      this.treeView.NodeControls.Add(this.nodeTextBoxMax);
      this.treeView.SelectedNode = null;
      this.treeView.Size = new System.Drawing.Size(386, 354);
      this.treeView.TabIndex = 0;
      this.treeView.Text = "treeView";
      this.treeView.UseColumns = true;
      this.treeView.NodeMouseDoubleClick += new System.EventHandler<Aga.Controls.Tree.TreeNodeAdvMouseEventArgs>(this.treeView_NodeMouseDoubleClick);
      this.treeView.Click += new System.EventHandler(this.treeView_Click);
      // 
      // plotPanel
      // 
      this.plotPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.plotPanel.Location = new System.Drawing.Point(0, 0);
      this.plotPanel.Name = "plotPanel";
      this.plotPanel.Size = new System.Drawing.Size(386, 124);
      this.plotPanel.TabIndex = 0;
      // 
      // resetMenuItem
      // 
      this.resetMenuItem.Index = 3;
      this.resetMenuItem.Text = "Reset";
      this.resetMenuItem.Click += new System.EventHandler(this.resetClick);
      // 
      // menuItem6
      // 
      this.menuItem6.Index = 4;
      this.menuItem6.Text = "-";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(418, 554);
      this.Controls.Add(this.splitContainer);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Menu = this.mainMenu;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Open Hardware Monitor";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.ResizeEnd += new System.EventHandler(this.MainForm_MoveOrResize);
      this.Move += new System.EventHandler(this.MainForm_MoveOrResize);
      this.splitContainer.Panel1.ResumeLayout(false);
      this.splitContainer.Panel2.ResumeLayout(false);
      this.splitContainer.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Aga.Controls.Tree.TreeViewAdv treeView;
    private System.Windows.Forms.MainMenu mainMenu;
    private System.Windows.Forms.MenuItem fileMenuItem;
    private System.Windows.Forms.MenuItem exitMenuItem;
    private Aga.Controls.Tree.TreeColumn sensor;
    private Aga.Controls.Tree.TreeColumn value;
    private Aga.Controls.Tree.TreeColumn min;
    private Aga.Controls.Tree.TreeColumn max;
    private Aga.Controls.Tree.NodeControls.NodeIcon nodeImage;
    private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxText;
    private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxValue;
    private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxMin;
    private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxMax;
    private SplitContainerAdv splitContainer;
    private PlotPanel plotPanel;
    private System.Windows.Forms.MenuItem viewMenuItem;
    private System.Windows.Forms.MenuItem plotMenuItem;
    private Aga.Controls.Tree.NodeControls.NodeCheckBox nodeCheckBox;
    private System.Windows.Forms.MenuItem helpMenuItem;
    private System.Windows.Forms.MenuItem aboutMenuItem;
    private System.Windows.Forms.MenuItem saveReportMenuItem;
    private System.Windows.Forms.MenuItem optionsMenuItem;
    private System.Windows.Forms.MenuItem hddMenuItem;
    private System.Windows.Forms.MenuItem minTrayMenuItem;
    private System.Windows.Forms.MenuItem separatorMenuItem;
    private System.Windows.Forms.ContextMenu sensorContextMenu;
    private System.Windows.Forms.MenuItem startMinMenuItem;
    private System.Windows.Forms.MenuItem startupMenuItem;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
    private System.Windows.Forms.Timer timer;
    private System.Windows.Forms.MenuItem hiddenMenuItem;
    private System.Windows.Forms.MenuItem MenuItem1;
    private System.Windows.Forms.MenuItem columnsMenuItem;
    private System.Windows.Forms.MenuItem valueMenuItem;
    private System.Windows.Forms.MenuItem minMenuItem;
    private System.Windows.Forms.MenuItem maxMenuItem;
    private System.Windows.Forms.MenuItem temperatureUnitsMenuItem;
    private System.Windows.Forms.MenuItem MenuItem4;
    private System.Windows.Forms.MenuItem celciusMenuItem;
    private System.Windows.Forms.MenuItem fahrenheitMenuItem;
    private System.Windows.Forms.MenuItem sumbitReportMenuItem;
    private System.Windows.Forms.MenuItem MenuItem2;
    private System.Windows.Forms.MenuItem resetMinMaxMenuItem;
    private System.Windows.Forms.MenuItem MenuItem3;
    private System.Windows.Forms.MenuItem gadgetMenuItem;
    private System.Windows.Forms.MenuItem minCloseMenuItem;
    private System.Windows.Forms.MenuItem resetMenuItem;
    private System.Windows.Forms.MenuItem menuItem6;
  }
}

