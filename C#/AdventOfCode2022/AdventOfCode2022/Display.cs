using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Display
    {
        public int Height { get; init; }
        public int Width { get; init; }

        public char[,] Buffer { get; set; }

        public Display(int width, int height)
        {
            Height = height;
            Width = width;

            Buffer = new char[height,width];
            if (Width > Console.BufferWidth || Height > Console.BufferHeight)
            {
                Console.SetBufferSize(Width, Height);
                Console.SetWindowSize(Width, Height);
            }
            ClearBuffer();
        }

        public void Draw()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Buffer[y,x] != ' ')
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(Buffer[y, x]);
                    }
                }
            }
        }

        public void SetChar(int x, int y, char c)
        {
            Buffer[y, x] = c;
        }

        public void ClearBuffer()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Buffer[y, x] != ' ')
                    {
                        Buffer[y, x] = ' ';
                        Console.SetCursorPosition(x, y);
                        Console.Write(' ');
                    }
                    
                }
            }
        }
    }
}
