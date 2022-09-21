using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ВРРН
{
    class Draw
    {
        public int x, y;
        public String Text;
        public int Size { get; set; }
        public Color Color { get; set; }
        public Color Textcolor { get; set; }
        // Чертае квадрат със стойност от Property-то на квадрата
        public void DrawBox(int x, int y, int size)
        {

            Graphics g = Form1.ActiveForm.CreateGraphics();
            //Цвета на квадрата
            Brush b = new SolidBrush(Color);
            //Задава шрифт и размер на текста
            Font format = new Font("Arial", 10);
            // Цвета на текста
            SolidBrush drawBrush = new SolidBrush(Textcolor);

            //Чертае квадрата
            g.FillRectangle(b, x - size, y - size, 2 * size, 2 * size);
            //нанася текста
            g.DrawString(Text, format, drawBrush, x - size, y - size / 2);

            this.x = x;
            this.y = y;
            this.Size = size;
        }
    }
}
