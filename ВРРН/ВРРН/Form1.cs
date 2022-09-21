using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ВРРН
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics gr = Form1.ActiveForm.CreateGraphics();
            // използва се при чертането на линийте между различните стойности
            Pen pen = new Pen(Color.Black, 3);
            string[] values = InitializeArray();
            string[] epsilons = Epsilons();
            decimal[] pepsilons = pEpsilons(epsilons);

            string[] fourthText = FourthValues(pepsilons, values);
            string[] thirdText = FindMax(fourthText);
            string[] secondValues = SecondValues(pepsilons, thirdText);
            
            Point[] fifth = FifthRow(values);
            Point[] fourth = FourthRow(fourthText);
            Point[] third = ThirdRow(thirdText);
            Point[] second = SecondRow(secondValues);

            Draw drawFinal = new Draw();
            drawFinal.Color = Color.Yellow;
            drawFinal.Textcolor = Color.Black;
            drawFinal.Text = FinalValue(secondValues);
            drawFinal.DrawBox(610, 200, 25);
            Point finalpoint = new Point(610, 220);
            for(int i=0; i<second.Length;++i)
            {
                //Чертае линия 
                gr.DrawLine(pen,second[i], finalpoint);
                second[i].Y = second[i].Y + 40;
            }

            for(int i=0; i < third.Length; i++)
            {
                if (i == 3)
                    gr.DrawLine(pen, third[i], second[i-1]);
                else
                    gr.DrawLine(pen, third[i], second[i/2]);
                // Променя координатата Y, за да може линията към следващато ниво да е в дъното на квадрата
                third[i].Y = third[i].Y + 40;
            }

            for(int i=0; i < fourth.Length; ++i)
            {
                gr.DrawLine(pen, fourth[i], third[i / 2]);
                fourth[i].Y = fourth[i].Y + 40;
            }

            for(int i=0; i < fifth.Length; i++)
            {
                gr.DrawLine(pen, fifth[i], fourth[i / 2]);
            }
        }

        // начертава втория ред от възли и връща масив с кординатите им
        private Point [] SecondRow(string[] arr)
        {
            Point[] points = new Point[3];
            for (int i = 0; i < 3; ++i)
            {
                Draw draw = new Draw();
                draw.Color = Color.Green;
                draw.Textcolor = Color.Black;
                draw.Text = arr[i];
                draw.DrawBox(250 + 360 * i, 300, 25);
                // Добавя точката на съответния квадрат, използва се при чертането на линии между отделните елементи
                points[i] = new Point(250 + 360 * i, 280);
            }

            return points;
        }

        // начертава третия ред от възли и връща масив с кординатите им
        private Point [] ThirdRow(string[] arr)
        {
            Point [] points = new Point [5];
            for (int i = 0; i < 5; ++i)
            {
                Draw draw = new Draw();
                draw.Color = Color.Red;
                draw.Textcolor = Color.Black;
                draw.Text = arr[i];
                draw.DrawBox(140 + 235 * i, 400, 25);
                points[i] = new Point(140 + 235 * i, 380);
            }
            return points;
        }

        // начертава четвъртия ред от възли и връща масив с кординатите им
        private Point [] FourthRow(string[] arr)
        {
            Point[] points = new Point[10];
            for (int i = 0; i < 10; ++i)
            {
                Draw draw = new Draw();
                draw.Color = Color.Orange;
                draw.Textcolor = Color.Black;
                draw.Text = arr[i];
                draw.DrawBox(70 + 120 * i, 500, 25);
                points[i] = new Point(70 + 120 * i, 480);
            }
            return points;
        }

        // начертава петия ред от възли и връща масив с кординатите им
        private Point[] FifthRow(string[] arr)
        {
            Point[] points = new Point[20];
            for (int i = 0; i < 20; ++i)
            {
                Draw draw = new Draw();
                draw.Color = Color.Blue;
                draw.Textcolor = Color.Yellow;
                draw.Text = arr[i];
                draw.DrawBox(35 + 60 * i, 600, 25);
                points[i] = new Point(35 + 60 * i, 580);
            }
            return points;
        }

        // Запазваме стойностите на алтернативите Vn
        private string [] InitializeArray()
        {
            string[] arr = new string[20];
            arr[0] = text1_1.Text;
            arr[1] = text2_1.Text;
            arr[2] = text1_2.Text;
            arr[3] = text2_2.Text;
            for (int i = 0; i < 4; i++)
                arr[i + 4] = arr[i];
            arr[8] = textB2_1.Text;
            arr[9] = textB3_1.Text;
            arr[10] = textB2_2.Text;
            arr[11] = textB3_2.Text;
            arr[12] = textBox1_1.Text;
            arr[13] = textBox2_1.Text;
            arr[14] = textBox1_2.Text;
            arr[15] = textBox2_2.Text;
            for (int i = 12; i < 16; ++i)
                arr[i + 4] = arr[i];

            return arr;
        }

        // Запазваме стойностите на p(Ѳ) и ε
        private string [] Epsilons()
        {
            string[] arr = new string[12];
            arr[0] = text1_3.Text;
            arr[1] = text1_4.Text;
            arr[2] = text1_5.Text;
            arr[3] = text2_3.Text;
            arr[4] = text2_4.Text;
            arr[5] = text2_5.Text;
            arr[6] = textBox1_3.Text;
            arr[7] = textBox1_4.Text;
            arr[8] = textBox1_5.Text;
            arr[9] = textBox2_3.Text;
            arr[10] = textBox2_4.Text;
            arr[11] = textBox2_5.Text;

            return arr;
        }
        // Изчислява стойностите за p(ε) и p(ε|Ѳ)
        private decimal[] pEpsilons(string[] epsilons)
        {
            decimal[] dec = new decimal[12];
            decimal[] epsy = Convert(epsilons);
            dec[0] = epsy[0] * epsy[1] + epsy[3] * epsy[4];
            dec[1] = (epsy[0] * epsy[1]) / dec[0];
            dec[2] = (epsy[3] * epsy[4]) / dec[0];
            dec[3] = epsy[0] * epsy[2] + epsy[3] * epsy[5];
            dec[4] = (epsy[0] * epsy[2]) / dec[3];
            dec[5] = (epsy[3] * epsy[5]) / dec[3];
            dec[6] = epsy[6] * epsy[7] + epsy[9] * epsy[10];
            dec[7] = (epsy[6] * epsy[7]) / dec[6];
            dec[8] = (epsy[9] * epsy[10]) / dec[6];
            dec[9] = epsy[6] * epsy[8] + epsy[9] * epsy[11];
            dec[10] = (epsy[6] * epsy[8]) / dec[9];
            dec[11] = (epsy[9] * epsy[11]) / dec[9];

            return dec;
        }

        // Трансформира текстов масив до decimal (десетични числа)
        private decimal[] Convert(string[] str)
        {
            decimal[] dec = new decimal[str.Length];
            for(int i =0; i < str.Length; ++i)
            {
                dec[i] = Decimal.Parse(str[i]);
            }

            return dec;
        }

        // Изчислява стойностите на 4то ниво
        private string [] FourthValues(decimal [] epsilons, string[] values)
        {
            string[] arr = new string[10];
            decimal[] value = Convert(values);
            arr[0] = Math.Round((value[0] * epsilons[1] + value[1] * epsilons[2])).ToString();
            arr[1] = Math.Round((value[2] * epsilons[1] + value[3] * epsilons[2])).ToString();
            arr[2] = Math.Round((value[4] * epsilons[4] + value[5] * epsilons[5])).ToString();
            arr[3] = Math.Round((value[6] * epsilons[4] + value[7] * epsilons[5])).ToString();
            arr[4] = Math.Round((value[8] * 0.6m + value[9] * 0.4m)).ToString();
            arr[5] = Math.Round((value[10] *0.6m + value[11] * 0.4m)).ToString();
            arr[6] = Math.Round((value[12] * epsilons[7] + value[13] * epsilons[8])).ToString();
            arr[7] = Math.Round((value[14] * epsilons[7] + value[15] * epsilons[8])).ToString();
            arr[8] = Math.Round((value[16] * epsilons[10] + value[17] * epsilons[11])).ToString();
            arr[9] = Math.Round((value[18] * epsilons[10] + value[19] * epsilons[11])).ToString();

            return arr;

        }
        // Намира по-големия от два текстови записа
        private string MaxValue(string v1,string v2)
        {
            if (int.Parse(v1)>int.Parse(v2))
            {
                return v1;
            }

            return v2;
        }
        //Създава нов текстов масив, като взима по големия от всеки 2 елемета (Стойностите за 3то ниво)
        private string[] FindMax(string[] aa)
        {
            string[] arr = new string[aa.Length / 2];
            for(int i = 0; i < aa.Length; i+=2)
            {
                arr[i / 2] = MaxValue(aa[i], aa[i + 1]);
            }

            return arr;
        }
        // Изчислява стойностите за 4то ниво
        private string[] SecondValues(decimal[] dec, string[] values)
        {
            string[] solution = new string[3];
            solution[1] = values[2];

            decimal[] decima = Convert(values);
            solution[0] = (Math.Round(decima[0] * dec[0] + decima[1] * dec[3]) - int.Parse(textBoxEXP1.Text)).ToString();
            solution[2] = (Math.Round(decima[3] * dec[6] + decima[4] * dec[9]) - int.Parse(textBoxEXP2.Text)).ToString();

            return solution;
        }

        // Намира най-големия елемент в масив от string-ове
        private string FinalValue(string [] aarr)
        {
            if (int.Parse(aarr[0]) > int.Parse(aarr[1]) && int.Parse(aarr[0]) > int.Parse(aarr[2]))
                return aarr[0];
            else
                if (int.Parse(aarr[1]) > int.Parse(aarr[2]))
                return aarr[1];

            return aarr[2];
        }
        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
