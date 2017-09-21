using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace интелл_системы_нейросеть_попытка2 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        List<Bitmap> btmpList = new List<Bitmap>();
        List<List<double>> dataList = new List<List<double>>();
        List<List<double>> standart = new List<List<double>>();
        List<double> result;

        perceptron3 p;
        Random rnd = new Random();
        int number = 0;

        private void lbl()
        {
            label4.Text = "Подождите, идет обучение...";
        }

        private void button1_Click(object sender, EventArgs e) {

            lbl();

            p = new perceptron3(100, 70, 2);

            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.a1));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.a2));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.a3));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.a4));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.a5));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.x1));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.x2));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.x3));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.x4));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.x5));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources._21));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources._22));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources._23));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources._24));
            btmpList.Add(new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources._25));

            for (int n = 0; n < btmpList.Count; n++) {
                dataList.Add(new List<double>());
                for (int i = 0; i < btmpList[n].Height; i++) {
                    for (int j = 0; j < btmpList[n].Width; j++) {
                        if (btmpList[n].GetPixel(i, j).R > 127) {
                            dataList[n].Add(0);
                        }
                        else {
                            dataList[n].Add(1);
                        }
                    }
                }
            }

            for (int i = 0; i < btmpList.Count; i++) {
                standart.Add(new List<double>());
                if (i < 5) {
                    standart[i].Add(0);
                    standart[i].Add(1);
                }
                else if (i > 4 && i < 10) {
                    standart[i].Add(1);
                    standart[i].Add(0);
                }
                else {
                    standart[i].Add(1);
                    standart[i].Add(1);
                }
            }
            double sum = 1.0;
            System.Diagnostics.Stopwatch spw = new System.Diagnostics.Stopwatch();
            spw.Start();
            while (sum > 0.1) {
                sum = 0.0;
                for (int i = 0; i < 100; i++) {
                    number = rnd.Next(15);
                    p.inData(dataList[number]);
                    p.train(standart[number]);
                }

                for (int i = 0; i < btmpList.Count; i++) {
                    p.inData(dataList[i]);
                    result = p.work();
                    for (int j = 0; j < standart[i].Count; j++) {
                        sum += (0.5 * Math.Pow((result[j] - standart[i][j]), 2));
                    }
                }
            }
            spw.Stop();
            label4.Text = "Сеть обучилась за " + (spw.ElapsedMilliseconds / 1000).ToString() + " сек. Ошибка -   " + sum.ToString();
            button6.Enabled = true;
        }


       /* private void button2_Click(object sender, EventArgs e) {
            string str_res = "Результат - ";
            string str_stdrt = "Эталон - ";
            p.inData(dataList[Convert.ToInt32(numericUpDown1.Value - 1)]);
            result = p.work();
            for (int j = 0; j < standart[Convert.ToInt32(numericUpDown1.Value - 1)].Count; j++) {
                str_res += Convert.ToInt32(Math.Round(result[j])).ToString();
                str_stdrt += Convert.ToInt32(Math.Round(standart[Convert.ToInt32(numericUpDown1.Value - 1)][j])).ToString();
            }
            label3.Text = str_res;
            label4.Text = str_stdrt;
        }

        private void button3_Click(object sender, EventArgs e) {
            Bitmap test_bmp = new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.a_test);
            test(test_bmp);
            label6.Text = "Эталон - 0 1";
        }

        private void button4_Click(object sender, EventArgs e) {
            Bitmap test_bmp = new Bitmap(интелл_системы_нейросеть_попытка2.Properties.Resources.x_test);
            test(test_bmp);
            label6.Text = "Эталон - 1 0";
        }

        private void button5_Click(object sender, EventArgs e) {
            Bitmap test_bmp = new Bitmap(filename);
            test(test_bmp);
            //label6.Text = "Эталон - 1 1";
        }*/

        void test(Bitmap test_bmp) {
            List<double> dataList1 = new List<double>();
            for (int i = 0; i < test_bmp.Height; i++) {
                for (int j = 0; j < test_bmp.Width; j++) {
                    if (test_bmp.GetPixel(i, j).R > 127) {
                        dataList1.Add(0);
                    }
                    else {
                        dataList1.Add(1);
                    }
                }
            }
            p.inData(dataList1);
            result = p.work();
            string str_res = "Результат - ";
            for (int j = 0; j < result.Count; j++) {
                str_res += Convert.ToInt32(Math.Round(result[j])).ToString();
            }
            if (Convert.ToInt32(Math.Round(result[0])) == 0 && Convert.ToInt32(Math.Round(result[1])) == 1) {
                label3.Text = "Результат: буква - м";
            }
            if (Convert.ToInt32(Math.Round(result[0])) == 1 && Convert.ToInt32(Math.Round(result[1])) == 0) {
                label3.Text = "Результат: буква - ф";
            }
            if (Convert.ToInt32(Math.Round(result[0])) == 1 && Convert.ToInt32(Math.Round(result[1])) == 1) {
                label3.Text = "Результат: цифра - 8";
            }
            if (Convert.ToInt32(Math.Round(result[0])) == 0 && Convert.ToInt32(Math.Round(result[1])) == 0) {
                label3.Text = "Непонятно";
            }
        }
        string filename;
        private void button6_Click(object sender, EventArgs e) {
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                filename = openFileDialog1.FileName;
            }
            pictureBox1.Image = Image.FromFile(filename);
            Bitmap test_bmp = new Bitmap(filename);
            test(test_bmp);
        }

        
    }
}
