using System.Text;

namespace EnergyMonitorLibrary
{
    public class PricesFileSerializerDeserializer
    {
        public readonly IPricesSaveableLoadable prices;
        public readonly string file;

        public PricesFileSerializerDeserializer(IPricesSaveableLoadable prices, string file)
        {
            this.prices = prices;
            this.file = file;
        }

        public void Save()
        {
            //Otevřít soubor file pro zápis
            using (FileStream fs = File.Open(file, FileMode.Append))
            {
                //Každý záznam (získano voláním prices.Save()) je zapsán na nový řádek do file
                Price[] pArr = prices.Save();
                Byte[] serialized;

                foreach (Price p in pArr)
                {
                    //Převod pomocí metody Serialize
                    serialized = new UTF8Encoding(true).GetBytes(Serialize(p));
                    fs.Write(serialized, 0, serialized.Length);
                }
                //Soubor file je uzavřen
                fs.Close();
            }
        }
        public void Load()
        {
            List<Price?> pricesLoad = new();
            //Otevřít soubor file pro zápis
            using (FileStream fs = File.Open(file, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs, new UTF8Encoding(false), false);

                //jsou načteny všechny řádky textu a převedeny do pole prices(s využitím metody Deserialize)
                while (!sr.EndOfStream)
                {
                    pricesLoad.Add(Deserialize(sr.ReadLine()));
                }

                //Soubor file je uzavřen
                sr.Close();
                fs.Close();
            }
            //Záznamy jsou nahrány do původního objektu
            prices.Load(pricesLoad.ToArray());
        }

        private static string Serialize(Price p)
        {
            return $"P{p.StartDate}<#^>{p.EndDate}<#^>{p.Cost}<#^>\n";
        }
        private static Price? Deserialize(string? s)
        {
            StringBuilder sb = new StringBuilder();

            int wordCounter = 0;
            string delimiter = "<#^>";

            DateTime startDateDeser = new DateTime(0);
            DateTime endDateDeser = new DateTime(0);
            double cost = -1;


            var window = new Queue<char>();
            var buffer = new List<char>();

            if (s != null && s.StartsWith("P"))
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
                                    startDateDeser = DateTime.Parse(new string(buffer.Take(nElements).ToArray()));
                                    wordCounter++;
                                    break;
                                case 1:
                                    endDateDeser = DateTime.Parse(new string(buffer.Take(nElements).ToArray()));
                                    wordCounter++;
                                    break;
                                case 2:
                                    cost = double.Parse(new string(buffer.Take(nElements).ToArray()));
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
                return new Price(startDateDeser, endDateDeser, cost);
            }
            return null;
        }

    }
}
