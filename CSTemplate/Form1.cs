using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CSTemplate
{
    public partial class Form1 : Form
    {
        //G:\VS测试项目\WebApplication1\CSTemplate\UIFunction\CategoryFun.cs
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        void LoadFile()
        {
            string path = @"G:\VS测试项目\WebApplication1\CSTemplate\templete.txt";
            string path1 = @"G:\VS测试项目\WebApplication1\CSTemplate\templeteFun.txt";
            string[] classnames = null;
            string[] tablenames = null;
            if (!textBox1.Text.Contains(','))
            {
                textBox1.Text += ",";
            }
            if (!textBox2.Text.Contains(','))
            {
                textBox2.Text += ",";
            }
            classnames = textBox1.Text.Split(',');
            tablenames = textBox2.Text.Split(',');

            if (classnames != null && tablenames != null && classnames.Length == tablenames.Length)
            {
                for (int i = 0; i < classnames.Length; i++)
                {
                    Write(path, 0, classnames[i], tablenames[i]);
                    Write(path1, 1, classnames[i], tablenames[i]);
                }
            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path"></param>
        protected void Write(string path,int flag,string classname,string tablename)
        {
            File.Copy(path, @"G:\VS测试项目\WebApplication1\CSTemplate\templete.cs", true);
            StreamReader sr = new StreamReader(@"G:\VS测试项目\WebApplication1\CSTemplate\templete.cs", Encoding.UTF8);
            string line = string.Empty;
            string fileName = string.Empty;
            if (flag == 1)
            {
                fileName = @"G:\VS测试项目\WebApplication1\CSTemplate\UIFunction\" + classname.Trim() + "Fun.cs";
            }
            else
            {
                fileName = @"G:\VS测试项目\WebApplication1\CSTemplate\DataBase\" + classname.Trim() + "DB.cs";
            }
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            string tmp = string.Empty;
            int total = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (flag == 1)
                {
                    tmp = "Fun";
                    line = line.Replace("$refclassname$", classname.Trim() + "DB");
                }
                else
                {
                    tmp = "DB";
                }

                line = line.Replace("$classname$", classname.Trim() + tmp);
                line = line.Replace("$tablemodel$", tablename.Trim());
                sw.WriteLine(line);
                total++;
                listBox1.Items.Add(line);
            }

            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            sr.Dispose();
            sr.Close();
            fs.Close();
            File.Delete(@"G:\VS测试项目\WebApplication1\CSTemplate\templete.cs");
        }
    }
}
