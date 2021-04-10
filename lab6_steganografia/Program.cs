using System;
using System.IO;
using System.Text;
using Accord.Math;

namespace lab6_steganografia
{
    class Program
    {
        static void Main(string[] args)
        {
            const int P = 25;

            Console.WriteLine($"P - {P}");

            int[,] F1Array = {
                {3, 0 },
                {3, 1 },
                {3, 2 },
                {4, 0 },
                {4, 1 },
                {4, 2 },
                {5, 0 },
                {5, 1 }
            };//координати коефіцієнтів F1
            int[,] F2Array = {
                {0, 3 },
                {1, 3 },
                {2, 3 },
                {0, 4 },
                {1, 4 },
                {2, 4 },
                {0, 5 },
                {1, 5 }
            };//координати коефіцієнтів F2

            int[] CharByte = {0,1,0,0,0,0,0,1 };//буква в бітах

            double[,] matrix =
            {
                {71, 61, 59, 59, 68, 70, 93, 73},
                {68, 67, 71, 69, 76, 76, 91, 80},
                {80, 77, 81, 83, 88, 84, 84, 90},
                {93, 83, 84, 91, 101, 91, 97, 97},
                {85, 80, 80, 87, 94, 91, 97, 101},
                {77, 76, 78, 78, 76, 76, 93, 99},
                {70, 73, 79, 76, 67, 65, 85, 95},
                {74, 80, 91, 83, 65, 56, 79, 102},
            };

            double[,] original = new double[matrix.GetLength(0), matrix.GetLength(1)];
            double[,] embedBit = new double[matrix.GetLength(0), matrix.GetLength(1)];
            double[,] DCT = new double[matrix.GetLength(0), matrix.GetLength(1)];

            Array.Copy(matrix, original, matrix.Length);

            

            if (F1Array.GetLength(0) != F2Array.GetLength(0) || F1Array.GetLength(0) != CharByte.Length)
            {
                Console.WriteLine("F1Array F2Array та CharByte мають бути однакової довжини!!!");
                Console.ReadLine();
                return;
            }

            

           

            Console.WriteLine("\n                  massage");
            WriteArrayToConsole(CharByte);


            Console.WriteLine("\n                  original");
            WriteArrayToConsole(matrix);

            CosineTransform.DCT(matrix);
            Console.WriteLine("\n                  DCT");
            WriteArrayToConsole(matrix);

            Array.Copy(matrix, DCT, DCT.Length);

            /*
            for (int i = 0; i < CharByte.Length; i++)
            {
                matrix = Koha_Zhao.dkp_bit_embed(matrix, CharByte[i], F1Array[i,0], F1Array[i, 1], F2Array[i, 0], F2Array[i, 1], P);
            }*/

            matrix = Koha_Zhao.dkp_bit_embed(matrix, CharByte[0], 5, 4, 4, 5, P);

            Console.WriteLine("\n                  embed bit");
            WriteArrayToConsole(matrix);

            Array.Copy(matrix, embedBit, embedBit.Length);

            CosineTransform.IDCT(matrix);
            Console.WriteLine("\n                  IDCT");
            WriteArrayToConsole(matrix);

            Console.WriteLine("\n                  AverageAbsoluteDifference");
            Console.WriteLine(Statistics.AverageAbsoluteDifference(original, matrix));
            Console.WriteLine("\n                  MeanSquareError");
            Console.WriteLine(Statistics.MeanSquareError(original, matrix));
            Console.WriteLine("\n                  SignalToNoiseRatio");
            Console.WriteLine(Statistics.SignalToNoiseRatio(original, matrix));
            Console.WriteLine("\n                  ImageFidelity");
            Console.WriteLine(Statistics.ImageFidelity(original, matrix));
            Console.WriteLine("\n                  NormalizedCrossCorrelation");
            Console.WriteLine(Statistics.NormalizedCrossCorrelation(original, matrix));
            Console.WriteLine("\n                  CorrelationQuality");
            Console.WriteLine(Statistics.CorrelationQuality(original, matrix));
            Console.WriteLine("\n                  StructuralContent");
            Console.WriteLine(Statistics.StructuralContent(original, matrix));

            string data = FillInTheVariable(CharByte, original, DCT, embedBit, matrix, Statistics.AverageAbsoluteDifference(original, matrix), Statistics.MeanSquareError(original, matrix), Statistics.SignalToNoiseRatio(original, matrix), Statistics.ImageFidelity(original, matrix), Statistics.NormalizedCrossCorrelation(original, matrix), Statistics.CorrelationQuality(original, matrix), Statistics.StructuralContent(original, matrix), P);

            // создаем каталог для файла
            string path = @"C:\DCT";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            SaveFile(@"C:\DCT\lab6.txt", data);

            Console.Read();

        }

        private static string FillInTheVariable(int[] massage, double[,] original, double[,] DCT, double[,] embedBit, double[,] matrix, double v1, double v2, double v3, double v4, double v5, double v6, double v7, int P)
        {
            string data = String.Empty;

            data += $"\r\nP - {P}\r\n";

            data += "\r\n                  massage\r\n";
            SetArrayForData(massage, ref data);

            data += "\r\n                  original\r\n";
            Set2DArrayForData(original, ref data);

            data += "\r\n                  DCT\r\n";
            Set2DArrayForData(DCT, ref data);

            data += "\r\n                  embed bit\r\n";
            Set2DArrayForData(embedBit, ref data);

            data += "\r\n                  IDCT\r\n";
            Set2DArrayForData(matrix, ref data);

            data += "\r\n                  AverageAbsoluteDifference\r\n";
            data += v1;

            data += "\r\n                  MeanSquareError\r\n";
            data += v2;

            data += "\r\n                  SignalToNoiseRatio\r\n";
            data += v3;

            data += "\r\n                  ImageFidelity\r\n";
            data += v4;

            data += "\r\n                  NormalizedCrossCorrelation\r\n";
            data += v5;

            data += "\r\n                  CorrelationQuality\r\n";
            data += v6;

            data += "\r\n                  StructuralContent\r\n";
            data += v7;


            return data;
        }

        static void WriteArrayToConsole(double[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(Math.Round(array[i, j]) + "\t");
                }
                Console.WriteLine();
            }
        }

        static void WriteArrayToConsole(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static void WriteArrayToConsole(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + "\t");
            }
            Console.WriteLine();
        }

        static void WriteArrayToConsole(byte[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static void Set2DArrayForData<T>(T[,] array, ref string data)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    data += Convert.ToString(Convert.ToInt32(array[i, j]));
                    data += "\t";
                }
                data += "\r\n";
            }
        }

        static void SetArrayForData<T>(T[] array, ref string data)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                data += Convert.ToString(Convert.ToInt32(array[i]));
                data += "\t";            
            }
            data += "\r\n";
        }

        static void SaveFile(string path, string data)
        {
            // сохраняем текст в файл
            using (FileStream fstream = new FileStream($@"{path}", FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = Encoding.Default.GetBytes(data);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
                Console.WriteLine($"Файл з iнформацiєю було створено за наступним шляхом - {path}");
            }
        }
    }
}


/*
             double[,] matrix =
            {


            {255, 255, 255, 255, 255, 255, 255, 255},
                {255, 255, 255, 255, 255, 255, 255, 255},
                {255, 255, 255, 255, 255, 255, 255, 255},
                {255, 255, 255, 255, 255, 255, 255, 255},
                {255, 255, 255, 255, 255, 255, 255, 255},
                {255, 255, 255, 255, 255, 255, 255, 255},
                {255, 255, 255, 255, 255, 255, 255, 255},
                {255, 255, 255, 255, 255, 255, 255, 255}
            };*/
