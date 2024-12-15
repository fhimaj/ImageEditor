using System.Drawing;
using System.Windows.Forms;

namespace ImageEditorWF
{
    partial class MainForm
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

        private System.Windows.Forms.PictureBox pictureBox_image;
        private System.Windows.Forms.CheckBox checkBox_drawing;
        private System.Windows.Forms.TrackBar trackBar_thickness;
        private System.Windows.Forms.Label label_thikness;
        private System.Windows.Forms.Button button_setColor;
        private System.Windows.Forms.Label label_color;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PictureBox pictureDetails;
        private System.Windows.Forms.Label details;
        private System.Windows.Forms.Label widthDisplay;
        private Rectangle cropArea;
        private bool isSelecting = false;
        private Point croppingStartPoint;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            this.pictureBox_image = new System.Windows.Forms.PictureBox();
            this.label_color = new System.Windows.Forms.Label();
            this.button_setColor = new System.Windows.Forms.Button();
            this.label_thikness = new System.Windows.Forms.Label();
            this.trackBar_thickness = new System.Windows.Forms.TrackBar();
            this.checkBox_drawing = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pictureDetails = new System.Windows.Forms.PictureBox();
            this.details = new System.Windows.Forms.Label();
            this.widthDisplay = new System.Windows.Forms.Label();
            this.heightDisplay = new System.Windows.Forms.Label();
            this.pixelsDisplay = new System.Windows.Forms.Label();
            this.tabPage_settings = new System.Windows.Forms.TabPage();
            this.label_contrast = new System.Windows.Forms.Label();
            this.button_rotate = new System.Windows.Forms.Button();
            this.label_brightness = new System.Windows.Forms.Label();
            this.button_reset = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.button_loadImage = new System.Windows.Forms.Button();
            this.resizeBtn = new System.Windows.Forms.Button();
            this.resizeWidth = new System.Windows.Forms.TextBox();
            this.resizeHeight = new System.Windows.Forms.TextBox();
            this.increaseBrightness = new System.Windows.Forms.Button();
            this.decreaseBrightness = new System.Windows.Forms.Button();
            this.increaseContrast = new System.Windows.Forms.Button();
            this.decreaseContrast = new System.Windows.Forms.Button();
            this.generateHistogramBtn = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_thickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDetails)).BeginInit();
            this.tabPage_settings.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_image
            // 
            this.pictureBox_image.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_image.Location = new System.Drawing.Point(543, 34);
            this.pictureBox_image.Margin = new System.Windows.Forms.Padding(400, 3, 3, 3);
            this.pictureBox_image.MaximumSize = new System.Drawing.Size(2000, 2000);
            this.pictureBox_image.MinimumSize = new System.Drawing.Size(150, 150);
            this.pictureBox_image.Name = "pictureBox_image";
            this.pictureBox_image.Size = new System.Drawing.Size(600, 600);
            this.pictureBox_image.TabIndex = 1;
            this.pictureBox_image.TabStop = false;
            this.pictureBox_image.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_image_MouseDown);
            this.pictureBox_image.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_image_MouseMove);
            this.pictureBox_image.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_image_MouseUp);
            // 
            // label_color
            // 
            this.label_color.AutoSize = true;
            this.label_color.Location = new System.Drawing.Point(13, 293);
            this.label_color.Name = "label_color";
            this.label_color.Size = new System.Drawing.Size(37, 13);
            this.label_color.TabIndex = 4;
            this.label_color.Text = "Color: ";
            // 
            // button_setColor
            // 
            this.button_setColor.Location = new System.Drawing.Point(61, 288);
            this.button_setColor.Name = "button_setColor";
            this.button_setColor.Size = new System.Drawing.Size(75, 23);
            this.button_setColor.TabIndex = 3;
            this.button_setColor.Text = "Set color";
            this.button_setColor.UseVisualStyleBackColor = true;
            this.button_setColor.Click += new System.EventHandler(this.button_setColor_Click);
            // 
            // label_thikness
            // 
            this.label_thikness.AutoSize = true;
            this.label_thikness.Location = new System.Drawing.Point(6, 335);
            this.label_thikness.Name = "label_thikness";
            this.label_thikness.Size = new System.Drawing.Size(56, 13);
            this.label_thikness.TabIndex = 2;
            this.label_thikness.Text = "Thickness";
            // 
            // trackBar_thickness
            // 
            this.trackBar_thickness.Location = new System.Drawing.Point(6, 351);
            this.trackBar_thickness.Name = "trackBar_thickness";
            this.trackBar_thickness.Size = new System.Drawing.Size(200, 45);
            this.trackBar_thickness.TabIndex = 1;
            this.trackBar_thickness.Value = 5;
            this.trackBar_thickness.Scroll += new System.EventHandler(this.trackBar_thickness_Scroll);
            // 
            // checkBox_drawing
            // 
            this.checkBox_drawing.AutoSize = true;
            this.checkBox_drawing.Location = new System.Drawing.Point(13, 265);
            this.checkBox_drawing.Name = "checkBox_drawing";
            this.checkBox_drawing.Size = new System.Drawing.Size(94, 17);
            this.checkBox_drawing.TabIndex = 0;
            this.checkBox_drawing.Text = "Drawing mode";
            this.checkBox_drawing.UseVisualStyleBackColor = true;
            this.checkBox_drawing.CheckedChanged += new System.EventHandler(this.checkBox_drawing_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            // 
            // pictureDetails
            // 
            this.pictureDetails.AccessibleDescription = "Details";
            this.pictureDetails.AccessibleName = "Details";
            this.pictureDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureDetails.BackColor = System.Drawing.Color.Transparent;
            this.pictureDetails.Location = new System.Drawing.Point(12, 557);
            this.pictureDetails.Name = "pictureDetails";
            this.pictureDetails.Size = new System.Drawing.Size(256, 164);
            this.pictureDetails.TabIndex = 8;
            this.pictureDetails.TabStop = false;
            // 
            // details
            // 
            this.details.AutoSize = true;
            this.details.Location = new System.Drawing.Point(101, 566);
            this.details.Name = "details";
            this.details.Size = new System.Drawing.Size(39, 13);
            this.details.TabIndex = 6;
            this.details.Text = "Details";
            // 
            // widthDisplay
            // 
            this.widthDisplay.AutoSize = true;
            this.widthDisplay.Location = new System.Drawing.Point(26, 589);
            this.widthDisplay.Name = "widthDisplay";
            this.widthDisplay.Size = new System.Drawing.Size(38, 13);
            this.widthDisplay.TabIndex = 9;
            this.widthDisplay.Text = "Width:";
            // 
            // heightDisplay
            // 
            this.heightDisplay.AutoSize = true;
            this.heightDisplay.Location = new System.Drawing.Point(25, 622);
            this.heightDisplay.Name = "heightDisplay";
            this.heightDisplay.Size = new System.Drawing.Size(41, 13);
            this.heightDisplay.TabIndex = 10;
            this.heightDisplay.Text = "Height:";
            // 
            // pixelsDisplay
            // 
            this.pixelsDisplay.AutoSize = true;
            this.pixelsDisplay.Location = new System.Drawing.Point(27, 651);
            this.pixelsDisplay.Name = "pixelsDisplay";
            this.pixelsDisplay.Size = new System.Drawing.Size(37, 13);
            this.pixelsDisplay.TabIndex = 11;
            this.pixelsDisplay.Text = "Pixels:";
            // 
            // tabPage_settings
            // 
            this.tabPage_settings.BackColor = System.Drawing.Color.Snow;
            this.tabPage_settings.Controls.Add(this.label_thikness);
            this.tabPage_settings.Controls.Add(this.checkBox_drawing);
            this.tabPage_settings.Controls.Add(this.trackBar_thickness);
            this.tabPage_settings.Controls.Add(this.generateHistogramBtn);
            this.tabPage_settings.Controls.Add(this.decreaseContrast);
            this.tabPage_settings.Controls.Add(this.button_setColor);
            this.tabPage_settings.Controls.Add(this.increaseContrast);
            this.tabPage_settings.Controls.Add(this.decreaseBrightness);
            this.tabPage_settings.Controls.Add(this.label_color);
            this.tabPage_settings.Controls.Add(this.increaseBrightness);
            this.tabPage_settings.Controls.Add(this.resizeHeight);
            this.tabPage_settings.Controls.Add(this.resizeWidth);
            this.tabPage_settings.Controls.Add(this.resizeBtn);
            this.tabPage_settings.Controls.Add(this.button_loadImage);
            this.tabPage_settings.Controls.Add(this.button_save);
            this.tabPage_settings.Controls.Add(this.button_reset);
            this.tabPage_settings.Controls.Add(this.label_brightness);
            this.tabPage_settings.Controls.Add(this.button_rotate);
            this.tabPage_settings.Controls.Add(this.label_contrast);
            this.tabPage_settings.Location = new System.Drawing.Point(4, 22);
            this.tabPage_settings.Name = "tabPage_settings";
            this.tabPage_settings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_settings.Size = new System.Drawing.Size(256, 460);
            this.tabPage_settings.TabIndex = 0;
            this.tabPage_settings.Text = "Tools";
            // 
            // label_contrast
            // 
            this.label_contrast.AutoSize = true;
            this.label_contrast.Location = new System.Drawing.Point(6, 102);
            this.label_contrast.Name = "label_contrast";
            this.label_contrast.Size = new System.Drawing.Size(101, 13);
            this.label_contrast.TabIndex = 3;
            this.label_contrast.Text = "Contrast Adjustment";
            // 
            // button_rotate
            // 
            this.button_rotate.Location = new System.Drawing.Point(9, 205);
            this.button_rotate.Name = "button_rotate";
            this.button_rotate.Size = new System.Drawing.Size(94, 23);
            this.button_rotate.TabIndex = 4;
            this.button_rotate.Text = "Rotate 90";
            this.button_rotate.UseVisualStyleBackColor = true;
            this.button_rotate.Click += new System.EventHandler(this.button_rotate_Click);
            // 
            // label_brightness
            // 
            this.label_brightness.AutoSize = true;
            this.label_brightness.Location = new System.Drawing.Point(6, 44);
            this.label_brightness.Name = "label_brightness";
            this.label_brightness.Size = new System.Drawing.Size(111, 13);
            this.label_brightness.TabIndex = 1;
            this.label_brightness.Text = "Brightness Adjustment";
            // 
            // button_reset
            // 
            this.button_reset.BackColor = System.Drawing.Color.PaleGreen;
            this.button_reset.Location = new System.Drawing.Point(17, 431);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(100, 23);
            this.button_reset.TabIndex = 5;
            this.button_reset.Text = "Reset";
            this.button_reset.UseVisualStyleBackColor = false;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // button_save
            // 
            this.button_save.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button_save.Location = new System.Drawing.Point(120, 431);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(130, 23);
            this.button_save.TabIndex = 3;
            this.button_save.Text = "Save result";
            this.button_save.UseVisualStyleBackColor = false;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_loadImage
            // 
            this.button_loadImage.BackColor = System.Drawing.Color.Orange;
            this.button_loadImage.Location = new System.Drawing.Point(6, 6);
            this.button_loadImage.Name = "button_loadImage";
            this.button_loadImage.Size = new System.Drawing.Size(130, 23);
            this.button_loadImage.TabIndex = 2;
            this.button_loadImage.Text = "Open image";
            this.button_loadImage.UseVisualStyleBackColor = false;
            this.button_loadImage.Click += new System.EventHandler(this.button_loadImage_Click);
            // 
            // resizeBtn
            // 
            this.resizeBtn.Location = new System.Drawing.Point(177, 162);
            this.resizeBtn.Name = "resizeBtn";
            this.resizeBtn.Size = new System.Drawing.Size(73, 27);
            this.resizeBtn.TabIndex = 6;
            this.resizeBtn.Text = "Resize";
            this.resizeBtn.UseVisualStyleBackColor = true;
            this.resizeBtn.Click += new System.EventHandler(this.resizeBtn_Click);
            // 
            // resizeWidth
            // 
            this.resizeWidth.Location = new System.Drawing.Point(95, 166);
            this.resizeWidth.Name = "resizeWidth";
            this.resizeWidth.Size = new System.Drawing.Size(76, 20);
            this.resizeWidth.TabIndex = 7;
            this.resizeWidth.Text = "200";
            this.resizeWidth.TextChanged += new System.EventHandler(this.resizeWidth_TextChanged);
            // 
            // resizeHeight
            // 
            this.resizeHeight.Location = new System.Drawing.Point(13, 166);
            this.resizeHeight.Name = "resizeHeight";
            this.resizeHeight.Size = new System.Drawing.Size(75, 20);
            this.resizeHeight.TabIndex = 8;
            this.resizeHeight.Text = "200";
            this.resizeHeight.TextChanged += new System.EventHandler(this.resizeHeight_TextChanged);
            // 
            // increaseBrightness
            // 
            this.increaseBrightness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.increaseBrightness.Location = new System.Drawing.Point(106, 60);
            this.increaseBrightness.Name = "increaseBrightness";
            this.increaseBrightness.Size = new System.Drawing.Size(94, 23);
            this.increaseBrightness.TabIndex = 9;
            this.increaseBrightness.Text = "+";
            this.increaseBrightness.UseVisualStyleBackColor = true;
            this.increaseBrightness.Click += new System.EventHandler(this.increaseBrightness_Click);
            // 
            // decreaseBrightness
            // 
            this.decreaseBrightness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decreaseBrightness.Location = new System.Drawing.Point(6, 60);
            this.decreaseBrightness.Name = "decreaseBrightness";
            this.decreaseBrightness.Size = new System.Drawing.Size(94, 23);
            this.decreaseBrightness.TabIndex = 10;
            this.decreaseBrightness.Text = "-";
            this.decreaseBrightness.UseVisualStyleBackColor = true;
            this.decreaseBrightness.Click += new System.EventHandler(this.decreaseBrightness_Click);
            // 
            // increaseContrast
            // 
            this.increaseContrast.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.increaseContrast.Location = new System.Drawing.Point(106, 118);
            this.increaseContrast.Name = "increaseContrast";
            this.increaseContrast.Size = new System.Drawing.Size(94, 23);
            this.increaseContrast.TabIndex = 11;
            this.increaseContrast.Text = "+";
            this.increaseContrast.UseVisualStyleBackColor = true;
            this.increaseContrast.Click += new System.EventHandler(this.increaseContrast_Click);
            // 
            // decreaseContrast
            // 
            this.decreaseContrast.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decreaseContrast.Location = new System.Drawing.Point(6, 118);
            this.decreaseContrast.Name = "decreaseContrast";
            this.decreaseContrast.Size = new System.Drawing.Size(94, 23);
            this.decreaseContrast.TabIndex = 12;
            this.decreaseContrast.Text = "-";
            this.decreaseContrast.UseVisualStyleBackColor = true;
            this.decreaseContrast.Click += new System.EventHandler(this.decreaseContrast_Click);
            // 
            // generateHistogramBtn
            // 
            this.generateHistogramBtn.Location = new System.Drawing.Point(124, 205);
            this.generateHistogramBtn.Name = "generateHistogramBtn";
            this.generateHistogramBtn.Size = new System.Drawing.Size(128, 23);
            this.generateHistogramBtn.TabIndex = 13;
            this.generateHistogramBtn.Text = "Generate Histogram";
            this.generateHistogramBtn.UseVisualStyleBackColor = true;
            this.generateHistogramBtn.Click += new System.EventHandler(this.generateHistogramBtn_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_settings);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(264, 486);
            this.tabControl.TabIndex = 7;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1396, 726);
            this.Controls.Add(this.pictureBox_image);
            this.Controls.Add(this.pixelsDisplay);
            this.Controls.Add(this.heightDisplay);
            this.Controls.Add(this.widthDisplay);
            this.Controls.Add(this.details);
            this.Controls.Add(this.pictureDetails);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Image Editor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_thickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDetails)).EndInit();
            this.tabPage_settings.ResumeLayout(false);
            this.tabPage_settings.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label heightDisplay;
        private System.Windows.Forms.Label pixelsDisplay;
        private TabPage tabPage_settings;
        private Button generateHistogramBtn;
        private Button decreaseContrast;
        private Button increaseContrast;
        private Button decreaseBrightness;
        private Button increaseBrightness;
        private TextBox resizeHeight;
        private TextBox resizeWidth;
        private Button resizeBtn;
        private Button button_loadImage;
        private Button button_save;
        private Button button_reset;
        private Label label_brightness;
        private Button button_rotate;
        private Label label_contrast;
        private TabControl tabControl;
    }
}
