namespace EnergyMonitorLibrary
{
    public class EMValidator
    {
        public static string Message { get; set; } = string.Empty;

        /// <summary>
        /// Metoda zjistí, zda v dané databázi cen existuje cena pro dny zadané v intervalu.
        /// Při zjištění chyby uloží do vlastnosti Message první nalezené datum bez ceny.
        /// </summary>
        /// <param name="prices">Databáze cen</param>
        /// <param name="intervalBegin">Počátek intervalu</param>
        /// <param name="intervalEnd">Konec intervalu</param>
        /// <returns>True, pokud pro všechny dny v intervalu existuje cena. Jinak false.</returns>
        public static bool ExistsPriceBetween(PricesDatabase prices, DateTime intervalBegin, DateTime intervalEnd)
        {
            Message = string.Empty;
            DateTime iteration = intervalBegin;
            bool found;
            while (iteration < intervalEnd)
            {
                found = false;
                foreach (Price priceTag in prices)
                {
                    if ((iteration >= priceTag.StartDate.AddDays(-1)) && (iteration < priceTag.EndDate))
                    {
                        iteration = priceTag.EndDate;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Message = iteration.AddDays(1).ToString();
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// Metoda zjistí, zda v listu cen, není již uložená nějaká cena,
        /// která by se překrývala s tou, jež chce uživatel přidat.
        /// </summary>
        /// <param name="tmpPricesList">List cen, musí být seřazený</param>
        /// <returns>True, pokud se některé ceny překrývají. Jinak false.</returns>
        public static bool ContainsOverlapingPrices(List<Price> tmpPricesList)
        {
            for (int i = 0; i < (tmpPricesList.Count - 1); i++)
            {
                if (tmpPricesList[i].EndDate >= tmpPricesList[i + 1].StartDate)
                {
                    return true;
                }
            }

            return false;
        }
    }
}