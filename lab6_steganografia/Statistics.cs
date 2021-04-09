using System;
using System.Collections.Generic;
using System.Text;

namespace lab6_steganografia
{
    static class Statistics
    {

        public static double AverageAbsoluteDifference(double[,] C, double[,] S)
        {
            CheckedArrayLength(C, S);

            double result = 0;

            int X = C.GetLength(0);
            int Y = C.GetLength(1);

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    result += Math.Abs(C[i,j] - S[i,j]);
                }
            }

            result *= (double)1 / (X * Y);

            return result;
        }

        public static double MeanSquareError(double[,] C, double[,] S)
        {
            CheckedArrayLength(C, S);

            double result = 0;

            int X = C.GetLength(0);
            int Y = C.GetLength(1);

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    result += Math.Pow((C[i,j] - S[i, j]), 2);
                }
            }

            result *= (double)1 / (X * Y);

            return result;
        }
        /*
        private static double LpNorm(double[,] C, double[,] S)
        {
            double result = 0;

            CheckedArrayLength(C, S);



            return result;
        }*/

        public static double SignalToNoiseRatio(double[,] C, double[,] S)
        {
            CheckedArrayLength(C, S);

            double result = 0;

            int X = C.GetLength(0);
            int Y = C.GetLength(1);

            double M1 = 0;
            double M2 = 0;


            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    M1 += Math.Pow(C[i, j], 2);
                    M2 += Math.Pow((C[i, j]- S[i, j]), 2);
                }
            }

            result = M1 / M2;


            return result;
        }

        public static double ImageFidelity(double[,] C, double[,] S)
        {
            CheckedArrayLength(C, S);
            double result = 0;

            int X = C.GetLength(0);
            int Y = C.GetLength(1);

            double M1 = 0;
            double M2 = 0;

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    M1 += Math.Pow(C[i, j] - S[i, j], 2);
                    M2 += Math.Pow(C[i, j], 2);
                }
            }

            result = 1 - (M1 / M2);

            return result;
        }

        public static double NormalizedCrossCorrelation(double[,] C, double[,] S)
        {
            CheckedArrayLength(C, S);
            double result = 0;

            int X = C.GetLength(0);
            int Y = C.GetLength(1);

            double M1 = 0;
            double M2 = 0;

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    M1 += C[i, j] * S[i, j];
                    M2 += Math.Pow(C[i, j], 2);
                }
            }

            result = M1 / M2;

            return result;
        }

        public static double CorrelationQuality(double[,] C, double[,] S)
        {
            CheckedArrayLength(C, S);
            double result = 0;

            int X = C.GetLength(0);
            int Y = C.GetLength(1);

            double M1 = 0;
            double M2 = 0;

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    M1 += C[i, j] * S[i, j];
                    M2 += C[i, j];
                }
            }

            result = M1 / M2;

            return result;
        }

        public static double StructuralContent(double[,] C, double[,] S)
        {
            CheckedArrayLength(C, S);
            double result;

            int X = C.GetLength(0);
            int Y = C.GetLength(1);

            double M1 = 0;
            double M2 = 0;

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    M1 += Math.Pow(C[i, j], 2);
                    M2 += Math.Pow(S[i, j], 2);
                }
            }

            result = M1 / M2;

            return result;
        }

        private static void CheckedArrayLength(double[,] C, double[,] S)
        {
            if (C.Length != S.Length)
                throw new Exception("Масиви не дорівнюють один одному");
        }


    }
}
