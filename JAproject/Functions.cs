using JACsharpLIB;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace JAproject
{
    class Functions
    {
		int[,] macierz2 = new int[2, 2] { { 3, 3 }, { 2, 5 } };
		int[] kol21 = new int[2] { 3, 3 };
		int[] kol22 = new int[2] { 2, 5 };

		int[,] macierz2odwrotna = new int[2, 2] { { 15, 17 }, { 20, 9 } };
		int[] kol21odwrotna = new int[2] { 15, 17 };
		int[] kol22odwrotna = new int[2] { 20, 9 };

		int[,] macierz3 = new int[3, 3] { { 6, 24, 1 }, { 13, 16, 10 }, { 20, 17, 15 } };
		int[] kol31 = new int[3] { 6, 24, 1 };
		int[] kol32 = new int[3] { 13, 16, 10 };
		int[] kol33 = new int[3] { 20, 17, 15 };

		int[,] macierz3odwrotna = new int[3, 3] { { 8, 5, 10 }, { 21, 8, 21 }, { 21, 12, 8 } };
		int[] kol31odwrotna = new int[3] { 8, 5, 10 };
		int[] kol32odwrotna = new int[3] { 21, 8, 21 };
		int[] kol33odwrotna = new int[3] { 21, 12, 8 };


		[DllImport("JAAsmLIB.dll")]
		static extern int Multi2(int[] x, int[] y);
		[DllImport("JAAsmLIB.dll")]
		static extern int Multi3(int[] x, int[] y);

		int stala = 65;

		HillCs hillCs = new HillCs();
		//funckja rozdzielajaca slowa na 2 i 3 literowe czesci
		public List<string> rozdziel(List<string> elementy, int ilosc_elementow)
		{
			int dlugosc_el;
			List<string> temp = new List<string>();
			int modulo3;
			int tmp = 0;
			int dla2 = 0;
			string przerwa = " ";
			string dodawany;
			for (int i = 0; i < ilosc_elementow; i++)
			{
				dlugosc_el = elementy[i].Length;
				if (dlugosc_el == 1)
				{
					string usuwany = temp[temp.Count - 1];
					string dousu = elementy[i - 1];
					dodawany = elementy[i];
					if(dodawany[0] == 10)
						temp.RemoveAt(temp.Count-1);

					temp.Add(dodawany);
					
					if(dodawany[0] != 10)
						temp.Add(przerwa);
				}
				else
				{
					modulo3 = dlugosc_el % 3;
					if (modulo3 == 0)
					{
						tmp = dlugosc_el / 3;
						for (int j = 0; j < tmp; j++)
						{
							int tmp2 = j * 3;
							dodawany = elementy[i].Substring(tmp2, 3);
							temp.Add(dodawany);
						}
						temp.Add(przerwa);
					}
					else if (modulo3 == 1)
					{
						dla2 = 0;
						int tmp2 = 0;
						tmp = dlugosc_el / 3;
						tmp = tmp - 1;
						for (int j = 0; j < tmp; j++)
						{
							tmp2 = j * 3;
							dodawany = elementy[i].Substring(tmp2, 3);
							temp.Add(dodawany);
							dla2 = tmp2 + 3;
						}
						for (int x = 0; x < 2; x++)
						{
							dodawany = elementy[i].Substring(dla2, 2);
							temp.Add(dodawany);
							dla2 += 2;
						}
						temp.Add(przerwa);
					}
					else if (modulo3 == 2)
					{
						dla2 = 0;
						tmp = dlugosc_el / 3;
						for (int j = 0; j < tmp; j++)
						{
							int tmp2 = j * 3;
							dodawany = elementy[i].Substring(tmp2, 3);
							temp.Add(dodawany);
							dla2 = tmp2 + 3;
						}
						dodawany = elementy[i].Substring(dla2, 2);
						temp.Add(dodawany);
						temp.Add(przerwa);
					}
				}
			}
			return temp;
		}

		//funckcja zlaczajaca slowa z powrotem
		public List<string> zlacz(List<string> dane, int ilosc)
		{
			List<string> temp = new List<string>();
			string tmp = null;
			for (int i = 0; i < ilosc; i++)
			{
				string x = dane[i];
				if (dane[i].Length != 1)
				{
					tmp += dane[i];
				}
				else
				{	
					//string d = dane[i];
					if (x[0] > 32)
					{
						// dodawanie pojedynczej litery
						string z = x;
						temp.Add(dane[i]);
						tmp = "";
					}
					else if(x[0] == 10)
                    {
						temp.Add(tmp);
						tmp = " ";
						temp.Add(tmp);
						temp.Add(dane[i]);
						tmp = "";
					}
                    else
                    {
						temp.Add(tmp);
						tmp = "";
					}
				}
			}
			return temp;
		}

		public List<string> zakoduj(List<string> dane, int ilosc_danych, int choice)
		{
			string tmp;
			int[] tab2 = new int[2];
			int[] tab3 = new int[3];

			for (int i = 0; i < ilosc_danych; i++)
			{
				tmp = dane[i];
				StringBuilder sb = new StringBuilder(tmp);				
				if (tmp.Length == 2)
				{
					tab2[0] = tmp[0] - stala;
					tab2[1] = tmp[1] - stala;

					if (choice == 0)
					{
						tab2 = hillCs.kodujdla2(tab2, macierz2);
					}
					else if (choice == 1)
					{
						int d = Multi2(tab2, kol21);
						int e = Multi2(tab2, kol22);

						tab2[0] = d % 26;
						tab2[1] = e % 26;
					}

					sb[0] = (char)(tab2[0] + stala);
					sb[1] = (char)(tab2[1] + stala);

					tmp = sb.ToString();

					dane[i] = tmp;
				}
				else if (tmp.Length == 3)
				{
					tab3[0] = tmp[0] - stala;
					tab3[1] = tmp[1] - stala;
					tab3[2] = tmp[2] - stala;

					if (choice == 0)
					{
						tab3 = hillCs.kodujdla3(tab3, macierz3);
					}
					else if (choice == 1)
					{
						int d = Multi3(tab3, kol31);
						int e = Multi3(tab3, kol32);
						int f = Multi3(tab3, kol33);

						tab3[0] = d % 26;
						tab3[1] = e % 26;
						tab3[2] = f % 26;
					}

					sb[0] = (char)(tab3[0] + stala);
					sb[1] = (char)(tab3[1] + stala);
					sb[2] = (char)(tab3[2] + stala);

					tmp = sb.ToString();

					dane[i] = tmp;
				}
				else if(tmp.Length  == 1 )
                {
					string x = tmp;
					int y = i;
				}
			}
			return dane;
		}

		public List<string> odkoduj(List<string> dane, int ilosc_danych, int choice)
		{
			string tmp;
			//List<int> tab2 = new List<int>(2);
			//List<int> tab3 = new List<int>(3);
			int[] tab2 = new int[2];
			int[] tab3 = new int[3];

			for (int i = 0; i < ilosc_danych; i++)
			{
				tmp = dane[i];
				StringBuilder sb = new StringBuilder(tmp);
				if (tmp.Length == 2)
				{
					tab2[0] = tmp[0] - stala;
					tab2[1] = tmp[1] - stala;

                    if (choice == 0)
                    {
                        tab2 = hillCs.odkodujdla2(tab2, macierz2odwrotna);
                    }
					else if(choice == 1)
                    {
						int d = Multi2(tab2, kol21odwrotna);
						int e = Multi2(tab2, kol22odwrotna);
						tab2[0] = d % 26;
						tab2[1] = e % 26;
						//wywoływanie funkcji kodujacej dla asm
					}

					//StringBuilder sb = new StringBuilder(tmp);
					sb[0] = (char)(tab2[0] + stala);
					sb[1] = (char)(tab2[1] + stala);

					//tmp[0] = tab2[0] + stala;
					//tmp[1] = tab2[1] + stala;

					tmp = sb.ToString();

					dane[i] = tmp;
				}
				else if (tmp.Length == 3)
				{
					tab3[0] = tmp[0] - stala;
					tab3[1] = tmp[1] - stala;
					tab3[2] = tmp[2] - stala;

					if (choice == 0)
					{
						tab3 = hillCs.odkodujdla3(tab3, macierz3odwrotna);
					}
					else if (choice == 1)
					{
						int d = Multi3(tab3, kol31odwrotna);
						int e = Multi3(tab3, kol32odwrotna);
						int f = Multi3(tab3, kol33odwrotna);
						tab3[0] = d % 26;
						tab3[1] = e % 26;
						tab3[2] = f % 26;
						//wywoływanie funkcji kodujacej dla asm
					}

					sb[0] = (char)(tab3[0] + stala);
					sb[1] = (char)(tab3[1] + stala);
					sb[2] = (char)(tab3[2] + stala);

					//tmp[0] = tab3[0] + stala;
					//tmp[1] = tab3[1] + stala;
					//tmp[2] = tab3[2] + stala;

					tmp = sb.ToString();

					dane[i] = tmp;
				}
				else if (tmp.Length == 1)
				{
					dane[i] = tmp;
				}
			}
			return dane;
		}

	}
}
