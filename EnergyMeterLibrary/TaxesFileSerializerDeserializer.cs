using System.Text;

namespace EnergyMonitorLibrary
{
    public class TaxesFileSerializerDeserializer
    {
        public readonly ITaxesSaveableLoadable taxes;
        public readonly string file;

        public TaxesFileSerializerDeserializer(ITaxesSaveableLoadable taxes, string file)
        {
            this.taxes = taxes;
            this.file = file;
        }

        public void Save()
        {
            //Otevřít soubor file pro zápis
            using (FileStream fs = File.Open(file, FileMode.Append))
            {
                //Každý záznam (získano voláním taxes.Save()) je zapsán na nový řádek do file
                Tax[] tArr = taxes.Save();
                Byte[] serialized;

                foreach (Tax t in tArr)
                {
                    //Převod pomocí metody Serialize
                    serialized = new UTF8Encoding(true).GetBytes(Serialize(t));
                    fs.Write(serialized, 0, serialized.Length);
                }
                //Soubor file je uzavřen
                fs.Close();
            }
        }
        public void Load()
        {
            List<Tax?> taxesLoad = new();
            //Otevřít soubor file pro zápis
            using (FileStream fs = File.Open(file, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs, new UTF8Encoding(false), false);

                //jsou načteny všechny řádky textu a převedeny do pole taxes(s využitím metody Deserialize)
                while (!sr.EndOfStream)
                {
                    taxesLoad.Add(Deserialize(sr.ReadLine()));
                }

                //Soubor file je uzavřen
                sr.Close();
                fs.Close();
            }
            //Záznamy jsou nahrány do původního objektu
            taxes.Load(taxesLoad.ToArray());
        }

        private static string Serialize(Tax t)
        {
            return $"T{t.Description}<#^>{t.StartDate}<#^>{t.EndDate}<#^>{t.Cost}<#^>{(int)t.Interval}<#^>\n";
        }
        private static Tax? Deserialize(string? s)
        {
            StringBuilder sb = new StringBuilder();

            int wordCounter = 0;
            string delimiter = "<#^>";

            string descriptionDeser = string.Empty;
            DateTime startDateDeser = new DateTime(0);
            DateTime endDateDeser = new DateTime(0);
            double costDeser = -1;
            int intervalNumber = -1;


            var window = new Queue<char>();
            var buffer = new List<char>();

            if (s != null && s.StartsWith("T"))
            {
                s = s.Substring(1);
                foreach (char element in s)
                {
                    buffer.Add(element);
                    window.Enqueue(element);
                    if (window.Count > delimiter.Length)
                    {
                        window.Dequeue();
                    }

                    if (window.SequenceEqual(delimiter))
                    {
                        // nElements - počet prvků v bufferu co nejsou delimitrem
                        int nElements = buffer.Count - delimiter.Length;
                        if (nElements > 0)
                        {
                            switch (wordCounter)
                            {
                                case 0:
                                    descriptionDeser = new string(buffer.Take(nElements).ToArray());
                                    wordCounter++;
                                    break;
                                case 1:
                                    startDateDeser = DateTime.Parse(new string(buffer.Take(nElements).ToArray()));
                                    wordCounter++;
                                    break;
                                case 2:
                                    endDateDeser = DateTime.Parse(new string(buffer.Take(nElements).ToArray()));
                                    wordCounter++;
                                    break;
                                case 3:
                                    costDeser = double.Parse(new string(buffer.Take(nElements).ToArray()));
                                    wordCounter++;
                                    break;
                                case 4:
                                    intervalNumber = int.Parse(new string(buffer.Take(nElements).ToArray()));
                                    wordCounter++;
                                    break;
                                default:
                                    break;
                            }
                        }

                        window.Clear();
                        buffer.Clear();
                    }
                }
                return new Tax(descriptionDeser, startDateDeser, endDateDeser, costDeser, (Intervals)intervalNumber);
            }
            return null;
        }

    }
}
