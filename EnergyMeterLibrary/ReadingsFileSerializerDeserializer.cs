using System.Text;

namespace EnergyMonitorLibrary
{
    public class ReadingsFileSerializerDeserializer
    {
        public readonly IReadingsSaveableLoadable readings;
        public readonly string file;

        public ReadingsFileSerializerDeserializer(IReadingsSaveableLoadable readings, string file)
        {
            this.readings = readings;
            this.file = file;
        }

        public void Save()
        {
            //Otevřít soubor file pro zápis
            using (FileStream fs = File.Create(file))
            {
                //Každý záznam (získano voláním readings.Save()) je zapsán na nový řádek do file
                Reading[] rArr = readings.Save();
                Byte[] serialized;

                foreach (Reading r in rArr)
                {
                    //Převod pomocí metody Serialize
                    serialized = new UTF8Encoding(true).GetBytes(Serialize(r));
                    fs.Write(serialized, 0, serialized.Length);
                }
                //Soubor file je uzavřen
                fs.Close();
            }
        }
        public void Load()
        {
            List<Reading?> readingsLoaded = new();
            //Otevřít soubor file pro zápis
            using (FileStream fs = File.Open(file, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs, new UTF8Encoding(false), false);

                //jsou načteny všechny řádky textu a převedeny do pole readings(s využitím metody Deserialize)
                while (!sr.EndOfStream)
                {
                    readingsLoaded.Add(Deserialize(sr.ReadLine()));
                }

                //Soubor file je uzavřen
                sr.Close();
                fs.Close();
            }
            //Záznamy jsou nahrány do původního objektu
            readings.Load(readingsLoaded.ToArray());
        }

        private static string Serialize(Reading r)
        {
            return $"R{r.StateOfGauge}<#^>{r.Date}<#^>\n";
        }
        private static Reading ? Deserialize(string? s)
        {
            StringBuilder sb = new StringBuilder();

            int wordCounter = 0;
            string delimiter = "<#^>";

            int stateDeser = -1;
            DateTime dateDeser = new DateTime(0);


            var window = new Queue<char>();
            var buffer = new List<char>();

            if (s != null && s.StartsWith("R"))
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
                                    stateDeser = int.Parse(new string(buffer.Take(nElements).ToArray()));
                                    wordCounter++;
                                    break;
                                case 1:
                                    dateDeser = DateTime.Parse(new string(buffer.Take(nElements).ToArray()));
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
            return new Reading(dateDeser, stateDeser);
            }
            return null;
        }

    }
}
