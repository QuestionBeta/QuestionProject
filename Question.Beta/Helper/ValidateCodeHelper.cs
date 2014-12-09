using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace Shop.Web.Helper
{
    /// <summary>
    /// 验证码生成辅助类
    /// </summary>
    public class ValidateCodeHelper
    {
        //// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="length">指定验证码的长度</param>
        /// <returns></returns>
        public string CreateValidateCode(int length)
        {
            return Str(length);
        }

        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="containsPage">要输出到的page对象</param>
        /// <param name="validateNum">验证码</param>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            int iwidth = (int)(validateCode.Length * 11);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 19);
            //Image newImage = new Bitmap(800, 600);
            Graphics g = Graphics.FromImage(image);
            //g.DrawImage(image, 0, 0);
            //image.Dispose();
            try
            {                
                g.Clear(Color.White);
                //定义颜色
                Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Chocolate, Color.Brown, Color.DarkCyan, Color.Purple };
                Random rand = new Random();
            
                //输出不同字体和颜色的验证码字符
                for (int i = 0; i < validateCode.Length; i++)
                {
                    int cindex = rand.Next(5);
                    Font font = new Font("Arial", 11, (FontStyle.Italic));
                    Brush b = new System.Drawing.SolidBrush(c[cindex]);
                    g.DrawString(validateCode, font, b, 1, 0, StringFormat.GenericDefault);
                }
                //画一个边框
                //g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);
                int x = 0;
                int y = 0;

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    x = rand.Next(image.Width);
                    y = rand.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(rand.Next()));
                }
                //输出到浏览器
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //Response.ClearContent();
                g.Dispose();
                image.Dispose();
                return ms.ToArray();               
            }
            finally
            {
                //g.Dispose();
                //image.Dispose();
            }
        }

        #region 生成随机字母与数字
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="IntStr">生成长度</param>
        public static string Str(int Length)
        {
            return Str(Length, false);
        }

        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string Str(int Length, bool Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        #endregion
    }
}