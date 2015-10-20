using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Utilities
{
    public class ImageUtility
    {
        public static void CombinImage(string sourceImg1, string sourceImg2,string destImg)
        {
            Image img1 = System.Drawing.Image.FromFile(sourceImg1);     //相框图片  
            Image img2 = System.Drawing.Image.FromFile(sourceImg2);     //照片图片

            Bitmap destBmp = new Bitmap(img1.Width,img1.Height+img2.Height);
            Graphics g = Graphics.FromImage(destBmp);
            g.DrawImage(img1, 0, 0);
            g.DrawImage(img2, 0, img1.Height);
            destBmp.Save(destImg);
            destBmp.Dispose();
            img1.Dispose();
            img2.Dispose();
        }
    }
}
