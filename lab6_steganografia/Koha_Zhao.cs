using System;
using System.Collections.Generic;
using System.Text;
using Accord.Math;

namespace lab6_steganografia
{
    class Koha_Zhao
    {

        /// <summary>
        /// Приховує інформацію за методом Коха-Жако.
        /// Вибирає дда коефіцієнти F1, F2.
        /// Для передачі біта 0 необхідно, щоб різниця модулів коефіцієнтів ДКП перевершує деяку позитивну величину (задається вручну) для передачі біта 1 різниця повинна бути меншою у порівнянні з деякий негативною величиною. Таким чином, при передачі 0 збільшуючи модуль першого коефіцієнта і зменшуючи модуль другого. При передачі 1 зменшуючи модуль першого коефіцієнта і збільшуючи модуль другого.
        /// </summary>
        /// <param name="matrix">Масив після ДКП</param>
        /// <param name="bit">Біт інформації який потрібно приховати</param>
        /// <param name="u1">Координата x F1</param>
        /// <param name="v1">Координата y F1</param>
        /// <param name="u2">Координата x F2</param>
        /// <param name="v2">Координата y F2</param>
        /// <param name="P">Чим більше </param>
        /// <returns>Повертає масив з прихованим </returns>
        public static double[,] dkp_bit_embed(double[,] matrix, int bit, int u1, int v1, int u2, int v2, int P)
        {
            double Abs1, Abs2;
            double z1 = 0, z2 = 0;
            Abs1 = Math.Abs(matrix[u1, v1]);
            Abs2 = Math.Abs(matrix[u2, v2]);
            if (matrix[u1, v1] >= 0) z1 = 1;
            if (matrix[u1, v1] < 0) z1 = -1;
            if (matrix[u2, v2] >= 0) z2 = 1;
            if (matrix[u2, v2] >= 0) z2 = -1;

            if (bit == 0)
            {
                if (Abs1 - Abs2 <= P)
                    Abs1 = P + Abs2 + 1;
            }
            if (bit == 1)
            {
                if (Abs1 - Abs2 >= -P)
                    Abs2 = P + Abs1 + 1;
            }
            matrix[u1, v1] = z1 * Abs1;
            matrix[u2, v2] = z2 * Abs2;
            return matrix;
        }

        /*
        private static string dkp_bit_exturn(double[,] one, int i, int u1, int v1, int u2, int v2, int P)
        {
            //int u1 = 3, v1 = 4, u2 = 4, v2 = 3; // координаты коэффициетов , которые меняем
            double Abs1, Abs2;
            Bits bits = new Bits();

            Abs1 = Math.Abs(one[u1, v1]);
            Abs2 = Math.Abs(one[u2, v2]);
            if (Abs1 > Abs2)
                bits.Add(0);
            if (Abs1 < Abs2)
                bits.Add(1);


            return text;
        }*/


    }
}
