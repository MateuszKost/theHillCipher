using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace JAproject
{
    public partial class Form1 : Form
    {      
        private String fileContent; //String with text to code
        private String filePath;
        private String fileName;
        private Functions Fun = new Functions();
        List<string> wynik = new List<string>();

        DateTime pocz, pocz2;
        DateTime stop, stop2;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int logicalProc = Environment.ProcessorCount;

            label5.Text = logicalProc.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*" })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    pocz = DateTime.Now;

                    label1.Text = ofd.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = ofd.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }

                    int length = fileContent.Length;

                    fileContent = fileContent.ToUpper();

                    List<string> elements = new List<string>(); // list which includes amount of words
                    Boolean letter = false;

                    int start = 0, dl = 0;

                    for (int i = 0; i < length; i++)
                    {
                        if ((fileContent[i] == 32) || (fileContent[i] == 10) || (i == length - 1))
                        {                            
                            if (letter == true)
                            {
                                if (i == length - 1)
                                {
                                    i += 1;
                                }
                                dl = i - start;
                                string dodawany = fileContent.Substring(start, dl);
                                if ((i != length) && fileContent[i] == 10)
                                {
                                    dodawany = dodawany.Remove(dodawany.Length - 1, 1);
                                }
                                elements.Add(dodawany);
                            }
                            if (i < length && fileContent[i] == 10)
                            {
                                string dodawany2 = fileContent[i].ToString();
                                elements.Add(dodawany2);
                            }
                            letter = false;
                            start = 0;
                            dl = 0;
                        }
                        else if ((fileContent[i] != 32) && (letter == false))
                        {
                            string x = fileContent[i].ToString();
                            int z = x[0];
                            start = i;
                            letter = true;
                        }
                    }

                    int amountOfElements = elements.Count;

                    wynik = Fun.rozdziel(elements, amountOfElements);
                    //wynik.RemoveRange(3200, 3200);
                    int test = wynik.Count;

                    for (int i = 1; i <= (wynik.Count/2); i++)
                    {
                        if (wynik.Count % i == 0 && i!= 1)
                            comboBox3.Items.Add(i);
                    }

                    stop = DateTime.Now;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    label2.Text = ofd.FileName;

                    filePath = ofd.FileName;

                    fileName = Path.GetFileName(filePath);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pocz2 = DateTime.Now;

            var watch = Stopwatch.StartNew();

            int choice = comboBox2.SelectedIndex;

            Object XD = comboBox3.SelectedItem;

            int iloscWatkow = Convert.ToInt32(XD);

            int iloscDoZakodowania = wynik.Count;

            List<string> zakodowane;

            Task[] threads = new Task[iloscWatkow];           
            
            if (fileContent != null)
            {                

                if (comboBox1.SelectedIndex == 0)
                {
                    if (iloscWatkow != 1)
                    {
                        int dlaKazdegoWatku = iloscDoZakodowania / iloscWatkow;

                        List<List<string>> lists = new List<List<string>>();
                        List<List<string>> lists2 = new List<List<string>>();

                        int startowy = 0;

                        for (int i = 0; i < iloscWatkow; i++)
                        {
                            string[] tmp = new string[dlaKazdegoWatku];
                            wynik.CopyTo(startowy, tmp, 0, dlaKazdegoWatku);
                            lists.Add(tmp.ToList());
                            //lists[i] = new List<string>();
                            //lists[i] = tmp.ToList();
                            startowy += dlaKazdegoWatku;
                        }

                        for (int t = 0; t < iloscWatkow; t++)
                        {
                            threads[t] = new Task(delegate { lists2.Add(Fun.zakoduj(lists[t], lists[t].Count(), choice)); });
                            threads[t].Start();
                            Thread.Sleep(1000);
                        }
                        //List<string> zakodowane = Fun.zakoduj(wynik, wynik.Count(), choice);

                        zakodowane = new List<string>();

                        for (int u = 0; u < iloscWatkow; u++)
                        {
                            do { } while (!threads[u].IsCompleted);
                            //zakodowane.AddRange(lists2[u]);
                        }

                        for (int u = 0; u < iloscWatkow; u++)
                        {
                            //threads[u].Wait();
                            zakodowane.AddRange(lists2[u]);
                        }
                    }
                    else 
                    {
                        zakodowane = Fun.zakoduj(wynik, wynik.Count(), choice);
                    }

                    zakodowane = Fun.zlacz(zakodowane, zakodowane.Count());

                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        for (int i = 0; i < zakodowane.Count(); i++)
                        {
                            writer.Write(zakodowane[i]);
                            string x = zakodowane[i];
                            if (x.Length != 0 && i != zakodowane.Count() - 1 && x[0] != 10)
                                writer.Write(" ");
                            // zapisywanie do pliku wynikowego
                        }
                    }
                    //MessageBox.Show("Zaszyfrowany tekst znajduje sie w: " + fileName);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    //zakodowane = hillCs.rozdziel(zakodowane, zakodowane.Count());
                    List<string> odkodowane;
                    if (iloscWatkow != 1)
                    {
                        int dlaKazdegoWatku = iloscDoZakodowania / iloscWatkow;

                        List<List<string>> lists = new List<List<string>>();
                        List<List<string>> lists2 = new List<List<string>>();

                        int startowy = 0;

                        for (int i = 0; i < iloscWatkow; i++)
                        {
                            string[] tmp = new string[dlaKazdegoWatku];
                            wynik.CopyTo(startowy, tmp, 0, dlaKazdegoWatku);
                            lists.Add(tmp.ToList());
                            //lists[i] = new List<string>();
                            //lists[i] = tmp.ToList();
                            startowy += dlaKazdegoWatku;
                        }

                        for (int t = 0; t < iloscWatkow; t++)
                        {
                            threads[t] = new Task(delegate { lists2.Add(Fun.zakoduj(lists[t], lists[t].Count(), choice)); });
                            threads[t].Start();
                            Thread.Sleep(1000);
                        }
                        //List<string> zakodowane = Fun.zakoduj(wynik, wynik.Count(), choice);

                        odkodowane = new List<string>();

                        for (int u = 0; u < iloscWatkow; u++)
                        {
                            do { } while (!threads[u].IsCompleted);
                            //zakodowane.AddRange(lists2[u]);
                        }

                        for (int u = 0; u < iloscWatkow; u++)
                        {
                            //threads[u].Wait();
                            odkodowane.AddRange(lists2[u]);
                        }
                    }
                    else
                    {
                        odkodowane = Fun.zakoduj(wynik, wynik.Count(), choice);
                    }
                    odkodowane = Fun.odkoduj(wynik, wynik.Count(), choice);

                    odkodowane = Fun.zlacz(odkodowane, odkodowane.Count());

                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        for (int i = 0; i < odkodowane.Count(); i++)
                        {
                            writer.Write(odkodowane[i]);
                            string x = odkodowane[i];
                            if (x.Length != 0 && i != odkodowane.Count() - 1 && x[0] != 10)
                                writer.Write(" ");

                            // zapisywanie do pliku wynikowego
                        }
                    }
                    //MessageBox.Show("Odszyfrowany tekst znajduje sie w: " + fileName);
                }

                //DateTime stop = DateTime.Now;                

                watch.Stop();
                stop2 = DateTime.Now;

                TimeSpan diff = (stop - pocz) + (stop2 - pocz2);

                TimeSpan diff2 = (stop2 - pocz2);

                var elapsedMs = watch.ElapsedMilliseconds;

                MessageBox.Show("Czas: " + diff.ToString("fff") + "ms");
                //MessageBox.Show("Czas: " + diff2.ToString("fff") + "s");

                //MessageBox.Show("Czas: " + elapsedMs + "ms");
            }
            else
            {
                MessageBox.Show("Nie podano pliku wejściowego");
            }
        }
    }
}
