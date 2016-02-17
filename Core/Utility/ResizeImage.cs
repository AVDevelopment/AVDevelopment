using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.Development.Core.Utility
{
    public class ResizeImage
    {
        public static void SaveAs(string destfileName, string srcfileName, int width, int height, string websiteName)
        {
            //SaveAs(destfileName, New Bitmap(srcfileName), width, height, websiteName)
            WaterMarkImage(srcfileName, destfileName, width, height, websiteName);
        }

        public static void SaveAs(string destfileName, Bitmap src, int width, int height, string websiteName)
        {
            Bitmap result = null;
            double aspectRatio = 0.0;
            int newHeight = 0;

            aspectRatio = width / src.Width;
            newHeight = Convert.ToInt32(Math.Floor(src.Height * aspectRatio));

            if (newHeight > height & height != 0)
            {
                newHeight = height;
            }

            Graphics g = System.Drawing.Graphics.FromImage(src);
            result = new Bitmap(src, width, newHeight);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.DrawImage(result, 0, 0, width, newHeight);
            g.Flush();

            if (!string.IsNullOrEmpty(websiteName.Trim()))
            {
                //WaterMarkImage(destfileName, destfileName, websiteName)
                //g.DrawString(websiteName, New Font(FontFamily.GenericSerif, 10, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Black, rect)
            }

            result.Save(destfileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            src.Dispose();
            result.Dispose();
        }

        public static void ResizeAs(string destfileName, string srcfileName, int width, int height)
        {
            Image img = Image.FromFile(srcfileName);
            Bitmap src = new Bitmap(img);
            Bitmap result = null;
            double aspectRatio = 0.0;
            int newHeight = 0;

            if (width > 0 & height > 0)
            {
                aspectRatio = Convert.ToDouble(width) / Convert.ToDouble(src.Width);
                newHeight = Convert.ToInt32(Math.Floor(src.Height * aspectRatio));

                if (newHeight > height & height != 0)
                {
                    newHeight = height;
                }
            }

            if (width > 0 & height == 0)
            {
                aspectRatio = width / src.Width;
                newHeight = Convert.ToInt32(Math.Floor(src.Height * aspectRatio));
            }

            if (width == 0 & height > 0)
            {
                aspectRatio = height / src.Height;
                newHeight = height;
                width = Convert.ToInt32(Math.Floor(src.Width * aspectRatio));
            }

            Graphics g = System.Drawing.Graphics.FromImage(src);
            result = new Bitmap(src, width, newHeight);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.DrawImage(result, 0, 0, width, newHeight);
            g.Flush();

            result.Save(destfileName);
            src.Dispose();
            img.Dispose();
            result.Dispose();
        }


        /// <summary>
        /// Creates a resized bitmap from an existing image on disk. Resizes the image by 
        /// creating an aspect ratio safe image. Image is sized to the larger size of width
        /// height and then smaller size is adjusted by aspect ratio.
        /// 
        /// Image is returned as Bitmap - call Dispose() on the returned Bitmap object
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>Bitmap or null</returns>
        public static bool ForcefullyResizeImage(string filename, string outputFilename,
                                       int height)
        {
            Bitmap bmpOut = null;

            try
            {
                Bitmap bmp = new Bitmap(filename);
                System.Drawing.Imaging.ImageFormat format = bmp.RawFormat;

                decimal ratio;
                int newWidth = 0;
                int newHeight = 0;

                //*** If the image is smaller than a thumbnail just return it
                if (bmp.Height < height)
                {
                    if (outputFilename != filename)
                        bmp.Save(outputFilename);
                    bmp.Dispose();
                    return true;
                }

                ratio = (decimal)height / bmp.Height;
                newHeight = height;
                newWidth = Convert.ToInt32(bmp.Width * ratio);


                bmpOut = new Bitmap(newWidth, newHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, newWidth, newHeight);
                g.DrawImage(bmp, 0, 0, newWidth, newHeight);

                bmp.Dispose();

                bmpOut.Save(outputFilename, format);
                bmpOut.Dispose();
            }
            catch (Exception ex)
            {
                var msg = ex.GetBaseException();
                return false;
            }

            return true;
        }



        public static void WaterMarkImage(string fromImage, string toImage, int width, int height, string copyright)
        {
            //create a image object containing the photograph to watermark 
            Image imgPhoto = Image.FromFile(fromImage);
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            double aspectRatio = 0.0;
            int newHeight = 0;

            if (width > imgPhoto.Width)
            {
                width = imgPhoto.Width;
                aspectRatio = 1;
            }
            else
            {
                aspectRatio = width / imgPhoto.Width;
            }

            newHeight = Convert.ToInt32(Math.Floor(imgPhoto.Height * aspectRatio));

            if (newHeight > height & height != 0)
            {
                newHeight = height;
            }

            //create a Bitmap the Size of the original photograph 
            Bitmap bmPhoto = new Bitmap(width, newHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //phWidth = width
            //phHeight = newHeight

            //load the Bitmap into a Graphics object 
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //'create a image object containing the watermark 
            //Dim imgWatermark As Image = New Bitmap(fromImage)
            //Dim wmWidth As Integer = imgWatermark.Width
            //Dim wmHeight As Integer = imgWatermark.Height

            //------------------------------------------------------------ 
            //Step #1 - Insert Copyright message 
            //------------------------------------------------------------ 

            //Set the rendering quality for this Graphics object 
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //Draws the photo Image object at original size to the graphics object. 
            // Photo Image object 
            // Rectangle structure 
            // x-coordinate of the portion of the source image to draw. 
            // y-coordinate of the portion of the source image to draw. 
            // Width of the portion of the source image to draw. 
            // Height of the portion of the source image to draw. 
            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, width, newHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);

            phWidth = width;
            phHeight = newHeight;


            // Units of measure 
            //------------------------------------------------------- 
            //to maximize the size of the Copyright message we will 
            //test multiple Font sizes to determine the largest posible 
            //font we can use for the width of the Photograph 
            //define an array of point sizes you would like to consider as possiblities 
            //------------------------------------------------------- 
            int[] sizes = new int[] {
			16,
			14,
			12,
			10,
			8,
			6,
			4
		};

            Font crFont = null;
            SizeF crSize = new SizeF();

            //Loop through the defined sizes checking the length of the Copyright string 
            //If its length in pixles is less then the image width choose this Font size. 
            for (int i = 0; i <= 6; i++)
            {
                //set a Font object to Arial (i)pt, Bold 
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                //Measure the Copyright string in this Font 
                crSize = grPhoto.MeasureString(copyright, crFont);

                if (Convert.ToUInt16(crSize.Width) < Convert.ToUInt16(phWidth))
                {
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            //Since all photographs will have varying heights, determine a 
            //position 5% from the bottom of the image 
            int yPixlesFromBottom = Convert.ToInt32((phHeight * 0.05));

            //Now that we have a point size use the Copyrights string height 
            //to determine a y-coordinate to draw the string of the photograph 
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));

            //Determine its x-coordinate by calculating the center of the width of the image 
            float xCenterOfImg = (phWidth / 2);

            //Define the text layout by setting the text alignment to centered 
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //define a Brush which is semi trasparent black (Alpha set to 153) 
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            //Draw the Copyright string 
            //string of text 
            //font 
            //Brush 
            //Position 
            if (copyright.Length > 0)
            {
                grPhoto.DrawString(copyright, crFont, semiTransBrush2, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), StrFormat);
            }

            //define a Brush which is semi trasparent white (Alpha set to 153) 
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            //Draw the Copyright string a second time to create a shadow effect 
            //Make sure to move this text 1 pixel to the right and down 1 pixel 
            //string of text 
            //font 
            //Brush 
            //Position 
            if (copyright.Length > 0)
            {
                grPhoto.DrawString(copyright, crFont, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom), StrFormat);
            }
            //Text alignment 


            //'------------------------------------------------------------ 
            //'Step #2 - Insert Watermark image 
            //'------------------------------------------------------------ 

            //'Create a Bitmap based on the previously modified photograph Bitmap 
            //Dim bmWatermark As New Bitmap(bmPhoto)
            //bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)
            //'Load this Bitmap into a new Graphic Object 
            //Dim grWatermark As Graphics = Graphics.FromImage(bmWatermark)

            //'To achieve a transulcent watermark we will apply (2) color 
            //'manipulations by defineing a ImageAttributes object and 
            //'seting (2) of its properties. 
            //Dim imageAttributes As New ImageAttributes()

            //'The first step in manipulating the watermark image is to replace 
            //'the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0) 
            //'to do this we will use a Colormap and use this to define a RemapTable 
            //Dim colorMap As New ColorMap()

            //'My watermark was defined with a background of 100% Green this will 
            //'be the color we search for and replace with transparency 
            //colorMap.OldColor = Color.FromArgb(255, 0, 255, 0)
            //colorMap.NewColor = Color.FromArgb(0, 0, 0, 0)

            //Dim remapTable As ColorMap() = {colorMap}

            //imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap)

            //'The second color manipulation is used to change the opacity of the 
            //'watermark. This is done by applying a 5x5 matrix that contains the 
            //'coordinates for the RGBA space. By setting the 3rd row and 3rd column 
            //'to 0.3f we achive a level of opacity 
            //Dim colorMatrixElements As Single()() = {New Single() {1.0F, 0.0F, 0.0F, 0.0F, 0.0F}, New Single() {0.0F, 1.0F, 0.0F, 0.0F, 0.0F}, New Single() {0.0F, 0.0F, 1.0F, 0.0F, 0.0F}, New Single() {0.0F, 0.0F, 0.0F, 0.3F, 0.0F}, New Single() {0.0F, 0.0F, 0.0F, 0.0F, 1.0F}}
            //Dim wmColorMatrix As New ColorMatrix(colorMatrixElements)

            //imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)

            //'For this example we will place the watermark in the upper right 
            //'hand corner of the photograph. offset down 10 pixels and to the 
            //'left 10 pixles 

            //Dim xPosOfWm As Integer = ((phWidth - wmWidth) - 10)
            //Dim yPosOfWm As Integer = 10

            //'Set the detination Position 
            //' x-coordinate of the portion of the source image to draw. 
            //' y-coordinate of the portion of the source image to draw. 
            //' Watermark Width 
            //' Watermark Height 
            //' Unit of measurment 
            //grWatermark.DrawImage(imgWatermark, New Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), 0, 0, wmWidth, wmHeight, _
            //GraphicsUnit.Pixel, imageAttributes)
            //'ImageAttributes Object 
            //'Replace the original photgraphs bitmap with the new Bitmap 
            //'imgPhoto = bmWatermark
            imgPhoto = bmPhoto;
            grPhoto.Dispose();
            //grWatermark.Dispose()

            //save new image to file system. 
            imgPhoto.Save(toImage, ImageFormat.Jpeg);
            imgPhoto.Dispose();
            bmPhoto.Dispose();
            //imgWatermark.Dispose()

        }


        public static void WaterMarkImage(string fromImage, string toImage, string copyright)
        {
            //create a image object containing the photograph to watermark 
            Image imgPhoto = Image.FromFile(fromImage);
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //create a Bitmap the Size of the original photograph 
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //'create a image object containing the watermark 
            //Dim imgWatermark As Image = New Bitmap(fromImage)
            //Dim wmWidth As Integer = imgWatermark.Width
            //Dim wmHeight As Integer = imgWatermark.Height

            //------------------------------------------------------------ 
            //Step #1 - Insert Copyright message 
            //------------------------------------------------------------ 

            //Set the rendering quality for this Graphics object 
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //Draws the photo Image object at original size to the graphics object. 
            // Photo Image object 
            // Rectangle structure 
            // x-coordinate of the portion of the source image to draw. 
            // y-coordinate of the portion of the source image to draw. 
            // Width of the portion of the source image to draw. 
            // Height of the portion of the source image to draw. 
            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);
            // Units of measure 
            //------------------------------------------------------- 
            //to maximize the size of the Copyright message we will 
            //test multiple Font sizes to determine the largest posible 
            //font we can use for the width of the Photograph 
            //define an array of point sizes you would like to consider as possiblities 
            //------------------------------------------------------- 
            int[] sizes = new int[] {
			16,
			14,
			12,
			10,
			8,
			6,
			4
		};

            Font crFont = null;
            SizeF crSize = new SizeF();

            //Loop through the defined sizes checking the length of the Copyright string 
            //If its length in pixles is less then the image width choose this Font size. 
            for (int i = 0; i <= 6; i++)
            {
                //set a Font object to Arial (i)pt, Bold 
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                //Measure the Copyright string in this Font 
                crSize = grPhoto.MeasureString(copyright, crFont);

                if (Convert.ToUInt16(crSize.Width) < Convert.ToUInt16(phWidth))
                {
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            //Since all photographs will have varying heights, determine a 
            //position 5% from the bottom of the image 
            int yPixlesFromBottom = Convert.ToInt32((phHeight * 0.05));

            //Now that we have a point size use the Copyrights string height 
            //to determine a y-coordinate to draw the string of the photograph 
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));

            //Determine its x-coordinate by calculating the center of the width of the image 
            float xCenterOfImg = (phWidth / 2);

            //Define the text layout by setting the text alignment to centered 
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //define a Brush which is semi trasparent black (Alpha set to 153) 
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            //Draw the Copyright string 
            //string of text 
            //font 
            //Brush 
            //Position 
            grPhoto.DrawString(copyright, crFont, semiTransBrush2, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), StrFormat);

            //define a Brush which is semi trasparent white (Alpha set to 153) 
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            //Draw the Copyright string a second time to create a shadow effect 
            //Make sure to move this text 1 pixel to the right and down 1 pixel 
            //string of text 
            //font 
            //Brush 
            //Position 
            grPhoto.DrawString(copyright, crFont, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom), StrFormat);
            //Text alignment 


            //'------------------------------------------------------------ 
            //'Step #2 - Insert Watermark image 
            //'------------------------------------------------------------ 

            //'Create a Bitmap based on the previously modified photograph Bitmap 
            //Dim bmWatermark As New Bitmap(bmPhoto)
            //bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)
            //'Load this Bitmap into a new Graphic Object 
            //Dim grWatermark As Graphics = Graphics.FromImage(bmWatermark)

            //'To achieve a transulcent watermark we will apply (2) color 
            //'manipulations by defineing a ImageAttributes object and 
            //'seting (2) of its properties. 
            //Dim imageAttributes As New ImageAttributes()

            //'The first step in manipulating the watermark image is to replace 
            //'the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0) 
            //'to do this we will use a Colormap and use this to define a RemapTable 
            //Dim colorMap As New ColorMap()

            //'My watermark was defined with a background of 100% Green this will 
            //'be the color we search for and replace with transparency 
            //colorMap.OldColor = Color.FromArgb(255, 0, 255, 0)
            //colorMap.NewColor = Color.FromArgb(0, 0, 0, 0)

            //Dim remapTable As ColorMap() = {colorMap}

            //imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap)

            //'The second color manipulation is used to change the opacity of the 
            //'watermark. This is done by applying a 5x5 matrix that contains the 
            //'coordinates for the RGBA space. By setting the 3rd row and 3rd column 
            //'to 0.3f we achive a level of opacity 
            //Dim colorMatrixElements As Single()() = {New Single() {1.0F, 0.0F, 0.0F, 0.0F, 0.0F}, New Single() {0.0F, 1.0F, 0.0F, 0.0F, 0.0F}, New Single() {0.0F, 0.0F, 1.0F, 0.0F, 0.0F}, New Single() {0.0F, 0.0F, 0.0F, 0.3F, 0.0F}, New Single() {0.0F, 0.0F, 0.0F, 0.0F, 1.0F}}
            //Dim wmColorMatrix As New ColorMatrix(colorMatrixElements)

            //imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)

            //'For this example we will place the watermark in the upper right 
            //'hand corner of the photograph. offset down 10 pixels and to the 
            //'left 10 pixles 

            //Dim xPosOfWm As Integer = ((phWidth - wmWidth) - 10)
            //Dim yPosOfWm As Integer = 10

            //'Set the detination Position 
            //' x-coordinate of the portion of the source image to draw. 
            //' y-coordinate of the portion of the source image to draw. 
            //' Watermark Width 
            //' Watermark Height 
            //' Unit of measurment 
            //grWatermark.DrawImage(imgWatermark, New Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), 0, 0, wmWidth, wmHeight, _
            //GraphicsUnit.Pixel, imageAttributes)
            //'ImageAttributes Object 
            //'Replace the original photgraphs bitmap with the new Bitmap 
            //'imgPhoto = bmWatermark
            imgPhoto = bmPhoto;
            grPhoto.Dispose();
            //grWatermark.Dispose()

            //save new image to file system. 
            imgPhoto.Save(toImage, ImageFormat.Jpeg);
            imgPhoto.Dispose();
            //imgWatermark.Dispose()

        }

    }
}
