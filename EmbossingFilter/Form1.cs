using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmbossingFilter
{
    public partial class Form1 : Form
    {
        [DllImport(@"D:\VS projects\EmbossingFilter\x64\Debug\FilterAsm.dll")]
        //[DllImport(@"D:\VS projects\EmbossingFilter\x64\Release\FilterAsm.dll")]
        static extern void RunAsm(byte[] outputArray, byte[] maskArray, int startingPoint, int finishPoint, int width, int height);

        [DllImport(@"D:\VS projects\EmbossingFilter\x64\Debug\FilterCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        //[DllImport(@"D:\VS projects\EmbossingFilter\x64\Release\FilterCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RunCpp(byte[] outputArray, byte[] maskArray, int startingPoint, int finishPoint, int width, int height);

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "bmp files (*.bmp)|*.bmp";
            if (dlg.ShowDialog() == DialogResult.OK) {
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = Image.FromFile(dlg.FileName);
            }
            dlg.Dispose();

            filterButton.Visible = true;
            threadsCounter.Visible = true;
            threadsLabel.Visible = true;
            asmCheckbox.Visible = true;
            cppCheckbox.Visible = true;

            originalPathLabel.Text = dlg.FileName;
            originalPathLabel.ForeColor = Color.Green;
        }


        private void button2_Click(object sender, EventArgs e) {
            string path = originalPathLabel.Text;
            Bitmap bmp = new Bitmap(path);

            byte[] byteArray = getColorData(bmp, false); // Array of bytes with GBR values inside // !!! WORKING !!! WORKING !!!
            makeGrayscale(byteArray);

            Bitmap maskBmp = new Bitmap(bmp);

            byte[] outputArray = new byte[byteArray.Length];

            //////////////////////////////////// THREADS ////////////////////////////////////

            int numberOfThreads = this.threadsCounter.Value;


            int finishPoint = (byteArray.Length / numberOfThreads);
            int rgbAlignment = 0;

            if(finishPoint % 3 == 1) {
                finishPoint -= 1;
                rgbAlignment = 1 * numberOfThreads;
            } else if(finishPoint % 3 == 2) {
                finishPoint -= 2;
                rgbAlignment = 2 * numberOfThreads;
            }

            int exactFinishPoint = finishPoint;
            int remainder = (byteArray.Length % numberOfThreads);


            Thread[] threadsArray = new Thread[numberOfThreads];

            int width = bmp.Width;
            int height = bmp.Height;

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            if (cppCheckbox.Checked == true) {
                for (int i = 0, startingPoint = 0; i < numberOfThreads; i++) {
                    if (i == 0) {
                        startingPoint = 0;
                    }
                    else {
                        int temp = i;

                        exactFinishPoint = finishPoint * (temp + 1);
                        startingPoint = (finishPoint * (temp + 1)) - finishPoint;
                    }

                    if (i == numberOfThreads - 1) {
                        int temp = startingPoint;
                        int temp2 = exactFinishPoint + remainder + rgbAlignment;
                        int temp3 = i;
                        threadsArray[temp3] = new Thread(() => RunCpp(outputArray, byteArray, temp, temp2, width, height));
                    }
                    else {
                        int temp = startingPoint;
                        int temp2 = exactFinishPoint;
                        int temp3 = i;
                        threadsArray[temp3] = new Thread(() => RunCpp(outputArray, byteArray, temp, temp2, width, height));
                    }
                }
                for (int i = 0; i < numberOfThreads; i++) {
                    threadsArray[i].Start();
                }
                for (int i = 0; i < numberOfThreads; i++) {
                    threadsArray[i].Join();
                }
                watch.Stop();
                cppTime.Text = "C++ time: " + watch.ElapsedMilliseconds + " ms";
            } else if (asmCheckbox.Checked == true) {
                for (int i = 0, startingPoint = 0; i < numberOfThreads; i++) {
                    if (i == 0) {
                        startingPoint = 0;
                    }
                    else {
                        int temp = i;
                        exactFinishPoint = finishPoint * (temp + 1);
                        startingPoint = (finishPoint * (temp + 1)) - finishPoint;
                    }

                    if (i == numberOfThreads - 1) {
                        int temp = startingPoint;
                        int temp2 = exactFinishPoint + remainder + rgbAlignment;
                        int temp3 = i;
                        threadsArray[temp3] = new Thread(() => RunAsm(outputArray, byteArray, temp, temp2, width, height));
                    }
                    else {
                        int temp = startingPoint;
                        int temp2 = exactFinishPoint;
                        int temp3 = i;
                        threadsArray[temp3] = new Thread(() => RunAsm(outputArray, byteArray, temp, temp2, width, height));
                    }
                }
                for (int i = 0; i < numberOfThreads; i++) {
                    threadsArray[i].Start();
                }
                for (int i = 0; i < numberOfThreads; i++) {
                    threadsArray[i].Join();
                }
                watch.Stop();
                asmTime.Text = "Asm time: " + watch.ElapsedMilliseconds + " ms";
            }
            //watch.Stop();
            //Console.WriteLine($"time = {watch.ElapsedMilliseconds.ToString()} ms");

            //////////////////////////////////// THREADS ////////////////////////////////////


            int bitsPerPixel = ((int)PixelFormat.Format24bppRgb & 0xff00) >> 8;
            int bytesPerPixel = (bitsPerPixel + 7) / 8;
            int stride = 4 * ((width * bytesPerPixel + 3) / 4);
            //int stride = 4 * ((bmp.Width * 24 + 31) / 32); //bmp.Width*3
            Bitmap newBmp = new Bitmap(BuildImage(outputArray, bmp.Width, bmp.Height, stride, PixelFormat.Format24bppRgb)); // !!! WORKING !!! WORKING !!!
            pictureBox2.Image = newBmp;
            newBmp.Save("output.bmp", ImageFormat.Bmp);
        }


        private void threadsCounter_Scroll(object sender, EventArgs e) {
            threadsLabel.Text = "Number of threads: " + threadsCounter.Value;
        }

        private static void makeGrayscale(byte[] byteArray) {
            int i;
            double avg;

            for (i = 0; i < byteArray.Length; i += 3) {
                //swap R and B; raw_image[i + 1] is G, so it stays where it is.
                //avg = 0;
                avg = 0.114 * byteArray[i];
                avg += 0.587 * byteArray[i + 1];
                avg += 0.299 * byteArray[i + 2];
                //avg /= 3;
                byteArray[i] = (byte)avg;
                byteArray[i + 1] = (byte)avg;
                byteArray[i + 2] = (byte)avg;
            }
        }

        private static byte[] getColorData(Bitmap bmp, bool reverseRGB)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpdata = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            IntPtr ptr = bmpdata.Scan0;
            int bytes = bmpdata.Stride * bmp.Height;
            byte[] results = new byte[bytes];
            Marshal.Copy(ptr, results, 0, bytes);

            if (reverseRGB)
            {
                byte tmp;
                int pos = 0;
                while (pos + 2 < bytes)
                {
                    tmp = results[pos];
                    results[pos] = results[pos + 2];
                    results[pos + 2] = tmp;
                    pos += 3;
                }
            }

            bmp.UnlockBits(bmpdata);
            return results;
        }


        private byte[] rgbToBgr(byte[] array) {
            int i;
            byte temp;
            for (i = 0; i < array.Length; i += 3) {
                //swap R and B; raw_image[i + 1] is G, so it stays where it is.
                temp = array[i + 0];
                array[i + 0] = array[i + 2];
                array[i + 2] = temp;
            }
            return array;
        }

        
        /// <summary>
        /// Creates a bitmap based on data, width, height, stride and pixel format.
        /// </summary>
        /// <param name="sourceData">Byte array of raw source data</param>
        /// <param name="width">Width of the image</param>
        /// <param name="height">Height of the image</param>
        /// <param name="stride">Scanline length inside the data</param>
        /// <param name="pixelFormat">Pixel format</param>
        /// <param name="palette">Color palette</param>
        /// <param name="defaultColor">Default color to fill in on the palette if the given colors don't fully fill it.</param>
        /// <returns>The new image</returns>
        public static Bitmap BuildImage(byte[] sourceData, Int32 width, Int32 height, Int32 stride, PixelFormat pixelFormat)//, Color[] palette, Color? defaultColor)
        {
            Bitmap newImage = new Bitmap(width, height, pixelFormat);
            BitmapData targetData = newImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, newImage.PixelFormat);
            Int32 newDataWidth = ((Image.GetPixelFormatSize(pixelFormat) * width) + 7) / 8;
            // Compensate for possible negative stride on BMP format.
            Boolean isFlipped = stride < 0;
            stride = Math.Abs(stride);
            // Cache these to avoid unnecessary getter calls.
            Int32 targetStride = targetData.Stride;
            Int64 scan0 = targetData.Scan0.ToInt64();
            for (Int32 y = 0; y < height; y++)
                Marshal.Copy(sourceData, y * stride, new IntPtr(scan0 + y * targetStride), newDataWidth);
            newImage.UnlockBits(targetData);
            // Fix negative stride on BMP format.
            if (isFlipped)
                newImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            //// For indexed images, set the palette.
            //if ((pixelFormat & PixelFormat.Indexed) != 0 && palette != null)
            //{
            //    ColorPalette pal = newImage.Palette;
            //    for (Int32 i = 0; i < pal.Entries.Length; i++)
            //    {
            //        if (i < palette.Length)
            //            pal.Entries[i] = palette[i];
            //        else if (defaultColor.HasValue)
            //            pal.Entries[i] = defaultColor.Value;
            //        else
            //            break;
            //    }
            //    newImage.Palette = pal;
            //}
            return newImage;
        }


        private void asmCheckbox_OnClick(object sender, EventArgs e) {
            if (cppCheckbox.Checked == false && asmCheckbox.Checked == true) {
                cppCheckbox.Checked = true;
                asmCheckbox.Checked = false;
            } else if (cppCheckbox.Checked == true && asmCheckbox.Checked == false) {
                cppCheckbox.Checked = false;
                asmCheckbox.Checked = true;
            }
        }

        private void cppCheckbox_OnClick(object sender, EventArgs e) {
            if (cppCheckbox.Checked == true && asmCheckbox.Checked == false) {
                cppCheckbox.Checked = false;
                asmCheckbox.Checked = true;
            } else if (cppCheckbox.Checked == false && asmCheckbox.Checked == true) {
                cppCheckbox.Checked = true;
                asmCheckbox.Checked = false;
            }
        }
    }
}
