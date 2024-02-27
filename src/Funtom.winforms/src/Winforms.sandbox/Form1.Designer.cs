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
    flowLayoutPanel1 = new FlowLayoutPanel();
    button1 = new Button();
    button2 = new Button();
    groupBox1 = new GroupBox();
    radioButton1 = new RadioButton();
    flowLayoutPanel1.SuspendLayout();
    groupBox1.SuspendLayout();
    SuspendLayout();
    // 
    // flowLayoutPanel1
    // 
    flowLayoutPanel1.Controls.Add(button1);
    flowLayoutPanel1.Controls.Add(button2);
    flowLayoutPanel1.Location = new Point(12, 12);
    flowLayoutPanel1.Name = "flowLayoutPanel1";
    flowLayoutPanel1.Size = new Size(390, 46);
    flowLayoutPanel1.TabIndex = 0;
    // 
    // button1
    // 
    button1.Anchor = AnchorStyles.None;
    button1.Location = new Point(3, 3);
    button1.Name = "button1";
    button1.Size = new Size(88, 38);
    button1.TabIndex = 0;
    button1.Text = "button1";
    button1.UseVisualStyleBackColor = true;
    // 
    // button2
    // 
    button2.Anchor = AnchorStyles.None;
    button2.Location = new Point(97, 10);
    button2.Name = "button2";
    button2.Size = new Size(75, 23);
    button2.TabIndex = 1;
    button2.Text = "button2";
    button2.UseVisualStyleBackColor = true;
    // 
    // groupBox1
    // 
    groupBox1.Controls.Add(radioButton1);
    groupBox1.Location = new Point(15, 64);
    groupBox1.Name = "groupBox1";
    groupBox1.Size = new Size(277, 131);
    groupBox1.TabIndex = 1;
    groupBox1.TabStop = false;
    groupBox1.Text = "groupBox1";
    // 
    // radioButton1
    // 
    radioButton1.AutoSize = true;
    radioButton1.Location = new Point(6, 33);
    radioButton1.Name = "radioButton1";
    radioButton1.Size = new Size(94, 19);
    radioButton1.TabIndex = 0;
    radioButton1.TabStop = true;
    radioButton1.Text = "radioButton1";
    radioButton1.UseVisualStyleBackColor = true;
    // 
    // Form1
    // 
    AutoScaleDimensions = new SizeF(7F, 15F);
    AutoScaleMode = AutoScaleMode.Font;
    ClientSize = new Size(636, 339);
    Controls.Add(groupBox1);
    Controls.Add(flowLayoutPanel1);
    Name = "Form1";
    Text = "Form1";
    flowLayoutPanel1.ResumeLayout(false);
    groupBox1.ResumeLayout(false);
    groupBox1.PerformLayout();
    ResumeLayout(false);
  }

  #endregion

  private FlowLayoutPanel flowLayoutPanel1;
  private Button button1;
  private Button button2;
  private GroupBox groupBox1;
  private RadioButton radioButton1;
}
