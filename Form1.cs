using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QR_Code
{
    public partial class Form1 : Form
    {
        private readonly object stream1;
        private readonly object image1;

        public Form1()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrEmpty(textBox1.Text);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Read Text For QRCODE

            string textboxtext = textBox1.Text;

            // Display QRCODE

            var request = WebRequest.Create("https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=" + textboxtext);
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                pictureBox1.Image = Bitmap.FromStream(stream);
                System.Net.ServicePointManager.Expect100Continue = false;
            }
            
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        { 
            // Save QRCODE .PNG
            if (pictureBox1.Image != null)
                    pictureBox1.Image = pictureBox1.Image;
                saveFileDialog1.Filter = "PNG|*.png";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
            {
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    switch (saveFileDialog1.FilterIndex)
                {
                        case 1:
                            this.pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                    }
                    fs.Close();
                }
        }
    }
}
