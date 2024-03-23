namespace Winforms.sandbox;

partial class Form1 {
  /// <summary>
  ///  Required designer variable.
  /// </summary>
  private System.ComponentModel.IContainer components = null;

  /// <summary>
  ///  Clean up any resources being used.
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
  ///  Required method for Designer support - do not modify
  ///  the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent() {
    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
    flowLayoutPanel1 = new FlowLayoutPanel();
    button1 = new Button();
    button2 = new Button();
    webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
    groupBox1 = new GroupBox();
    comboBox1 = new ComboBox();
    checkBox1 = new CheckBox();
    radioButton1 = new RadioButton();
    panel1 = new Panel();
    menuStrip1 = new MenuStrip();
    testToolStripMenuItem = new ToolStripMenuItem();
    aaaToolStripMenuItem = new ToolStripMenuItem();
    bbbbToolStripMenuItem = new ToolStripMenuItem();
    folderBrowserDialog1 = new FolderBrowserDialog();
    flowLayoutPanel1.SuspendLayout();
    ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
    groupBox1.SuspendLayout();
    panel1.SuspendLayout();
    menuStrip1.SuspendLayout();
    SuspendLayout();
    // 
    // flowLayoutPanel1
    // 
    flowLayoutPanel1.Controls.Add(button1);
    flowLayoutPanel1.Controls.Add(button2);
    flowLayoutPanel1.Controls.Add(webView21);
    flowLayoutPanel1.Dock = DockStyle.Top;
    flowLayoutPanel1.Location = new Point(0, 0);
    flowLayoutPanel1.Margin = new Padding(6);
    flowLayoutPanel1.Name = "flowLayoutPanel1";
    flowLayoutPanel1.Size = new Size(1181, 98);
    flowLayoutPanel1.TabIndex = 0;
    // 
    // button1
    // 
    button1.Anchor = AnchorStyles.None;
    button1.Image = Properties.Resources.phantom_16x16;
    button1.ImageAlign = ContentAlignment.MiddleLeft;
    button1.Location = new Point(6, 11);
    button1.Margin = new Padding(6);
    button1.Name = "button1";
    button1.Size = new Size(163, 81);
    button1.TabIndex = 0;
    button1.Text = "button1";
    button1.UseVisualStyleBackColor = true;
    button1.Click += button1_Click;
    // 
    // button2
    // 
    button2.Anchor = AnchorStyles.None;
    button2.Location = new Point(181, 27);
    button2.Margin = new Padding(6);
    button2.Name = "button2";
    button2.Size = new Size(139, 49);
    button2.TabIndex = 1;
    button2.Text = "button2";
    button2.UseVisualStyleBackColor = true;
    // 
    // webView21
    // 
    webView21.AllowExternalDrop = true;
    webView21.CreationProperties = null;
    webView21.DefaultBackgroundColor = Color.White;
    webView21.Location = new Point(332, 6);
    webView21.Margin = new Padding(6);
    webView21.Name = "webView21";
    webView21.Size = new Size(232, 92);
    webView21.Source = new Uri("https://fsdoc.jp", UriKind.Absolute);
    webView21.TabIndex = 2;
    webView21.ZoomFactor = 1D;
    // 
    // groupBox1
    // 
    groupBox1.Controls.Add(comboBox1);
    groupBox1.Controls.Add(checkBox1);
    groupBox1.Controls.Add(radioButton1);
    groupBox1.Dock = DockStyle.Bottom;
    groupBox1.Location = new Point(0, 400);
    groupBox1.Margin = new Padding(6);
    groupBox1.Name = "groupBox1";
    groupBox1.Padding = new Padding(6);
    groupBox1.Size = new Size(1181, 279);
    groupBox1.TabIndex = 1;
    groupBox1.TabStop = false;
    groupBox1.Text = "groupBox1";
    // 
    // comboBox1
    // 
    comboBox1.FormattingEnabled = true;
    comboBox1.Location = new Point(429, 73);
    comboBox1.Margin = new Padding(4, 2, 4, 2);
    comboBox1.Name = "comboBox1";
    comboBox1.Size = new Size(247, 40);
    comboBox1.TabIndex = 2;
    // 
    // checkBox1
    // 
    checkBox1.AutoSize = true;
    checkBox1.Location = new Point(240, 70);
    checkBox1.Margin = new Padding(4, 2, 4, 2);
    checkBox1.Name = "checkBox1";
    checkBox1.Size = new Size(159, 36);
    checkBox1.TabIndex = 1;
    checkBox1.Text = "checkBox1";
    checkBox1.UseVisualStyleBackColor = true;
    // 
    // radioButton1
    // 
    radioButton1.AutoSize = true;
    radioButton1.Location = new Point(11, 70);
    radioButton1.Margin = new Padding(6);
    radioButton1.Name = "radioButton1";
    radioButton1.Size = new Size(184, 36);
    radioButton1.TabIndex = 0;
    radioButton1.TabStop = true;
    radioButton1.Text = "radioButton1";
    radioButton1.UseVisualStyleBackColor = true;
    // 
    // panel1
    // 
    panel1.Controls.Add(groupBox1);
    panel1.Controls.Add(flowLayoutPanel1);
    panel1.Dock = DockStyle.Fill;
    panel1.Location = new Point(0, 44);
    panel1.Margin = new Padding(6);
    panel1.Name = "panel1";
    panel1.Size = new Size(1181, 679);
    panel1.TabIndex = 2;
    // 
    // menuStrip1
    // 
    menuStrip1.ImageScalingSize = new Size(32, 32);
    menuStrip1.Items.AddRange(new ToolStripItem[] { testToolStripMenuItem, bbbbToolStripMenuItem });
    menuStrip1.Location = new Point(0, 0);
    menuStrip1.Name = "menuStrip1";
    menuStrip1.Padding = new Padding(11, 4, 0, 4);
    menuStrip1.Size = new Size(1181, 44);
    menuStrip1.TabIndex = 3;
    menuStrip1.Text = "menuStrip1";
    // 
    // testToolStripMenuItem
    // 
    testToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aaaToolStripMenuItem });
    testToolStripMenuItem.Image = Properties.Resources.phantom_16x16;
    testToolStripMenuItem.Name = "testToolStripMenuItem";
    testToolStripMenuItem.Size = new Size(105, 36);
    testToolStripMenuItem.Text = "test";
    // 
    // aaaToolStripMenuItem
    // 
    aaaToolStripMenuItem.Name = "aaaToolStripMenuItem";
    aaaToolStripMenuItem.Size = new Size(359, 44);
    aaaToolStripMenuItem.Text = "aaa";
    // 
    // bbbbToolStripMenuItem
    // 
    bbbbToolStripMenuItem.Name = "bbbbToolStripMenuItem";
    bbbbToolStripMenuItem.Size = new Size(90, 36);
    bbbbToolStripMenuItem.Text = "bbbb";
    // 
    // Form1
    // 
    AutoScaleDimensions = new SizeF(13F, 32F);
    AutoScaleMode = AutoScaleMode.Font;
    ClientSize = new Size(1181, 723);
    Controls.Add(panel1);
    Controls.Add(menuStrip1);
    Icon = (Icon)resources.GetObject("$this.Icon");
    MainMenuStrip = menuStrip1;
    Margin = new Padding(6);
    Name = "Form1";
    Text = "Form1";
    flowLayoutPanel1.ResumeLayout(false);
    ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
    groupBox1.ResumeLayout(false);
    groupBox1.PerformLayout();
    panel1.ResumeLayout(false);
    menuStrip1.ResumeLayout(false);
    menuStrip1.PerformLayout();
    ResumeLayout(false);
    PerformLayout();
  }

  #endregion

  private FlowLayoutPanel flowLayoutPanel1;
  private Button button1;
  private Button button2;
  private GroupBox groupBox1;
  private RadioButton radioButton1;
  private Panel panel1;
  private MenuStrip menuStrip1;
  private ToolStripMenuItem testToolStripMenuItem;
  private ToolStripMenuItem aaaToolStripMenuItem;
  private ToolStripMenuItem bbbbToolStripMenuItem;
  private CheckBox checkBox1;
  private ComboBox comboBox1;
  private FolderBrowserDialog folderBrowserDialog1;
  private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
}
