using EditorTools;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImageEditorWF
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        #region Generated events
        private void button_loadImage_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = ImageEditor.IMAGE_EXTENSIONS_PATTERN;
                openFileDialog1.ShowDialog();
                ImageEditor.loadedImage = Image.FromFile(openFileDialog1.FileName);
                ImageEditor.initialImage = Image.FromFile(openFileDialog1.FileName);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("File you tried you it is corrupted or it is not a valid image file!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (System.IO.FileNotFoundException)
            {
                return;
            }

            pictureBox_image.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox_image.Image = ImageEditor.initialImage;
            pictureBox_image.SizeMode = PictureBoxSizeMode.StretchImage;
            ImageEditor.widthMultiplier = pictureBox_image.Width / (float)pictureBox_image.Image.Width;
            ImageEditor.heightMultiplier = pictureBox_image.Height / (float)pictureBox_image.Image.Height;
            tabControl.Enabled = true;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = ImageEditor.IMAGE_EXTENSIONS_PATTERN;
                saveFileDialog1.ShowDialog();
                pictureBox_image.Image.Save(saveFileDialog1.FileName);
            }
            catch (ArgumentException)
            {
                return;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Image is not loaded yet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }           

        private void MainForm_Load(object sender, EventArgs e)
        {
            tabControl.Enabled = false;
        }

        private void button_rotate_Click(object sender, EventArgs e)
        {
            if (!IsImageExists())
                return;

            ImageEditor.initialImage = ImageEditor.Rotate90(ImageEditor.initialImage);
            pictureBox_image.Image = ImageEditor.initialImage;
            pictureBox_image.SizeMode = PictureBoxSizeMode.StretchImage;
            PerformChanges(ImageEditor.initialImage);
        }

        private void checkBox_drawing_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_drawing.Checked == true)
            {
                pictureBox_image.Cursor = Cursors.Cross;
                ImageEditor.isDrawing = true;
            }
            else
            {
                pictureBox_image.Cursor = Cursors.Default;
                ImageEditor.isDrawing = false;
            }
        }

        private void pictureBox_image_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (ImageEditor.isMouseDown && ImageEditor.isDrawing)
                {
                    using (Graphics g = Graphics.FromImage(pictureBox_image.Image))
                    {
                        PointF currLocation = e.Location;
                        currLocation.X /= ImageEditor.widthMultiplier;
                        currLocation.Y /= ImageEditor.heightMultiplier;
                        g.DrawLine(new Pen(ImageEditor.brushColor, ImageEditor.brushThickness), ImageEditor.lastPoint, currLocation);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                    }

                    pictureBox_image.Invalidate();
                    ImageEditor.lastPoint = new PointF(e.Location.X / ImageEditor.widthMultiplier, e.Location.Y / ImageEditor.heightMultiplier);
                    ImageEditor.initialImage = pictureBox_image.Image;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Could not write! Please try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trackBar_thickness_Scroll(object sender, EventArgs e)
            => ImageEditor.brushThickness = trackBar_thickness.Value;

        private void button_setColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            ImageEditor.brushColor = colorDialog1.Color;
            label_color.BackColor = ImageEditor.brushColor;
        }

        private void pictureBox_image_MouseUp(object sender, MouseEventArgs e)
        {
            ImageEditor.isMouseDown = false;
            ImageEditor.lastPoint.X = 0; ImageEditor.lastPoint.Y = 0;
        }

        private void pictureBox_image_MouseDown(object sender, MouseEventArgs e)
        {
            ImageEditor.lastPoint = e.Location;
            ImageEditor.lastPoint.X /= ImageEditor.widthMultiplier;
            ImageEditor.lastPoint.Y /= ImageEditor.heightMultiplier;
            ImageEditor.isMouseDown = true;
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            pictureBox_image.Image = ImageEditor.loadedImage;
            ImageEditor.initialImage = ImageEditor.loadedImage;        
        }
        #endregion

        #region Custom private fields

        private void PerformChanges(Image currentImage)
        {
            if (!IsImageExists())
                return;
            pictureBox_image.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox_image.Image = currentImage;
            ImageEditor.initialImage = currentImage;

            pictureBox_image.Parent.Invalidate();
            pictureBox_image.Parent.Refresh();

            pictureBox_image.Invalidate();
            pictureBox_image.Refresh();

            UpdateImageDetails(currentImage);
        }     

        private bool IsImageExists()
        {
            if (pictureBox_image.Image == null)
            {
                MessageBox.Show("Image is not loaded yet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        #endregion

        private void resizeWidth_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(resizeWidth.Text, out int val))
                ImageEditor.ResizeWidth = val;
            else
                MessageBox.Show("Only integer values are allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void resizeHeight_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(resizeHeight.Text, out int val))
                ImageEditor.ResizeHeight = val;
            else
                MessageBox.Show("Only integer values are allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void resizeBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox_image.Image == null)
            {
                MessageBox.Show("Please load an image first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ImageEditor.ResizeHeight == 0 || ImageEditor.ResizeWidth == 0)
            {
                MessageBox.Show("Please give valid width and height values!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var imgBmp = ImageEditor.ResizeImage(ImageEditor.initialImage, ImageEditor.ResizeWidth, ImageEditor.ResizeHeight);

            PerformChanges(imgBmp);
        }

        private void increaseBrightness_Click(object sender, EventArgs e)
        {
            var img = ImageEditor.AdjustBrightness(new Bitmap(ImageEditor.initialImage),
               ImageEditor.BRIGHTNESS_INCREASE_RATE);

            PerformChanges(img);
        }

        private void decreaseBrightness_Click(object sender, EventArgs e)
        {
            var img = ImageEditor.AdjustBrightness(new Bitmap(ImageEditor.initialImage),
               ImageEditor.BRIGHTNESS_DECREASE_RATE);

            PerformChanges(img);
        }

        private void increaseContrast_Click(object sender, EventArgs e)
        {
            var img = ImageEditor.AdjustContrast(new Bitmap(ImageEditor.initialImage),
               ImageEditor.BRIGHTNESS_INCREASE_RATE);

            PerformChanges(img);
        }

        private void decreaseContrast_Click(object sender, EventArgs e)
        {
            var img = ImageEditor.AdjustContrast(new Bitmap(ImageEditor.initialImage),
              ImageEditor.BRIGHTNESS_DECREASE_RATE);

            PerformChanges(img);
        }       

        private void UpdateImageDetails(Image image)
        {
            widthDisplay.Text = "Width: " + image.Width;
            heightDisplay.Text = "Height: " + image.Height;
            pixelsDisplay.Text = "Pixels: " + (image.Width * image.Height);
        }

        private void generateHistogramBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox_image != null)
            {
                Bitmap histogramBitmap = ImageEditor.GenerateHistogram(ImageEditor.initialImage);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|BMP Image|*.bmp";
                saveFileDialog.DefaultExt = "png";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    if (filePath.EndsWith(".png"))
                    {
                        histogramBitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    else if (filePath.EndsWith(".jpg"))
                    {
                        histogramBitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else if (filePath.EndsWith(".bmp"))
                    {
                        histogramBitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);
                    }

                    MessageBox.Show("Histogram saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
