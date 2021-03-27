using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace JACsharpLIB
{
    public class HillCs
    {
		//static int macierz2[2][2] = { {3, 3}, {2, 5} };
		//static int macierz2odwrotna[2][2] = { {15, 17}, {20, 9} };
		//static int macierz3[3][3] = { {6, 24, 1}, {13, 16, 10}, {20, 17, 15} };
		//static int macierz3odwrotna[3][3] = { {8, 5, 10}, {21, 8, 21}, {21, 12, 8} };
		//static int stala = 65;
		
		public int[] kodujdla2(int[] dane, int[,]macierz)
		{
			int []tmp = new int[2];

			tmp[0] = (dane[0] * macierz[0,0] + dane[1] * macierz[0,1]) % 26;
			tmp[1] = (dane[0] * macierz[1,0] + dane[1] * macierz[1,1]) % 26;
			dane[0] = tmp[0];
			dane[1] = tmp[1];

			return dane;
		}

		public int[] kodujdla3(int[] dane, int[,] macierz)
		{
			int []tmp = new int[3];

			tmp[0] = (dane[0] * macierz[0,0] + dane[1] * macierz[0,1] + dane[2] * macierz[0,2]) % 26;
			tmp[1] = (dane[0] * macierz[1,0] + dane[1] * macierz[1,1] + dane[2] * macierz[1,2]) % 26;
			tmp[2] = (dane[0] * macierz[2,0] + dane[1] * macierz[2,1] + dane[2] * macierz[2,2]) % 26;
			dane[0] = tmp[0];
			dane[1] = tmp[1];
			dane[2] = tmp[2];

			return dane;
		}

		public int[] odkodujdla2(int[] dane, int[,] macierz)
		{
			int[] tmp = new int[2];

			tmp[0] = (dane[0] * macierz[0,0] + dane[1] * macierz[0,1]) % 26;
			tmp[1] = (dane[0] * macierz[1,0] + dane[1] * macierz[1,1]) % 26;
			dane[0] = tmp[0];
			dane[1] = tmp[1];

			return dane;
		}

		public int[] odkodujdla3(int[] dane, int[,] macierz)
		{
			int[] tmp = new int[3];

			tmp[0] = (dane[0] * macierz[0,0] + dane[1] * macierz[0,1] + dane[2] * macierz[0,2]) % 26;
			tmp[1] = (dane[0] * macierz[1,0] + dane[1] * macierz[1,1] + dane[2] * macierz[1,2]) % 26;
			tmp[2] = (dane[0] * macierz[2,0] + dane[1] * macierz[2,1] + dane[2] * macierz[2,2]) % 26;
			dane[0] = tmp[0];
			dane[1] = tmp[1];
			dane[2] = tmp[2];

			return dane;
		}		
	}
}
