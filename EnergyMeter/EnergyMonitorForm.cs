using EnergyMonitorLibrary;

namespace EnergyMonitor
{
    public partial class EnergyMonitorForm : Form
    {

        private Meter meter = new();
        private BindingSource bsReadings = new();
        private BindingSource bsPrices = new();
        private BindingSource bsTaxes = new();

        public EnergyMonitorForm()
        {
            InitializeComponent();

            meter.AddReading(new Reading(new DateTime(2022, 04, 01), 10000));
            meter.AddPrice(new Price(new DateTime(2022, 01, 01), new DateTime(2023, 12, 31), 2));
            meter.AddPrice(new Price(new DateTime(2024, 01, 01), new DateTime(2026, 12, 31), 10));
            meter.AddReading(new Reading(new DateTime(2022, 04, 10), 11000));
            meter.AddTax(new Tax("Poplatek", new DateTime(2022, 01, 01), new DateTime(2023, 12, 31), 1000, Intervals.PerMonth));

            bsReadings.DataSource = meter.Readings;
            listBoxReadings.DataSource = bsReadings;

            bsPrices.DataSource = meter.Prices;
            listBoxPrices.DataSource = bsPrices;

            bsTaxes.DataSource = meter.Taxes;
            listBoxTaxes.DataSource = bsTaxes;
        }

        private void ButtonAddReading_Click(object sender, EventArgs e)
        {
            using AddEditReadingForm f = new();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Reading newReading = new(f.Date, f.StateOfGauge);

                try
                {
                    meter.AddReading(newReading);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //obnov DataSource v ListBoxu
                bsReadings.DataSource = null;
                bsReadings.DataSource = meter.Readings;
            }
        }

        private void ButtonEditReading_Click(object sender, EventArgs e)
        {
            if (listBoxReadings.SelectedItem is Reading actualReading)
            {
                using AddEditReadingForm f = new(actualReading.Date, actualReading.StateOfGauge);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Reading editedReading = new(f.Date, f.StateOfGauge);
                    try
                    {
                        meter.EditReading(listBoxReadings.SelectedIndex, editedReading);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //obnov DataSource v ListBoxu
                    bsReadings.DataSource = null;
                    bsReadings.DataSource = meter.Readings;
                }
            }
        }

        private void ButtonRemoveReading_Click(object sender, EventArgs e)
        {
            meter.RemoveReading(listBoxReadings.SelectedIndex);

            //obnov DataSource v ListBoxu
            bsReadings.DataSource = null;
            bsReadings.DataSource = meter.Readings;
        }


        private void ButtonNewPrice_Click(object sender, EventArgs e)
        {
            using AddEditPriceForm f = new();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Price newPrice = new(f.StartDate, f.EndDate, f.Cost);

                try
                {
                    meter.AddPrice(newPrice);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //obnov DataSource v ListBoxu
                bsPrices.DataSource = null;
                bsPrices.DataSource = meter.Prices;
            }
        }

        private void ButtonEditPrice_Click(object sender, EventArgs e)
        {
            if (listBoxPrices.SelectedItem is Price actualPrice)
            {
                using AddEditPriceForm f = new(actualPrice.StartDate, actualPrice.EndDate, actualPrice.Cost);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Price editedPrice = new(f.StartDate, f.EndDate, f.Cost);
                    try
                    {
                        meter.EditPrice(listBoxPrices.SelectedIndex, editedPrice);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //obnov DataSource v ListBoxu
                    bsReadings.DataSource = null;
                    bsReadings.DataSource = meter.Readings;
                }
            }
        }

        private void ButtonRemovePrice_Click(object sender, EventArgs e)
        {
            meter.RemovePrice(listBoxPrices.SelectedIndex);

            //obnov DataSource v ListBoxu
            bsPrices.DataSource = null;
            bsPrices.DataSource = meter.Prices;
        }


        private void ButtonNewTax_Click(object sender, EventArgs e)
        {
            using AddEditTaxForm f = new();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Tax newTax = new(f.Description, f.StartDate, f.EndDate, f.Cost, f.Interval);

                try
                {
                    meter.AddTax(newTax);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //obnov DataSource v ListBoxu
                bsTaxes.DataSource = null;
                bsTaxes.DataSource = meter.Taxes;
            }
        }

        private void ButtonEditTax_Click(object sender, EventArgs e)
        {
            if (listBoxTaxes.SelectedItem is Tax actualTax)
            {
                using AddEditTaxForm f = new(actualTax.Description, actualTax.StartDate, actualTax.EndDate, actualTax.Cost, actualTax.Interval);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Tax editedTax = new(f.Description, f.StartDate, f.EndDate, f.Cost, f.Interval);
                    try
                    {
                        meter.EditTax(listBoxTaxes.SelectedIndex, editedTax);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //obnov DataSource v ListBoxu
                    bsTaxes.DataSource = null;
                    bsTaxes.DataSource = meter.Taxes;
                }
            }
        }

        private void ButtonRemoveTax_Click(object sender, EventArgs e)
        {
            meter.RemoveTax(listBoxTaxes.SelectedIndex);

            //obnov DataSource v ListBoxu
            bsTaxes.DataSource = null;
            bsTaxes.DataSource = meter.Taxes;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Energy Monitor Saves (*.EMSave)|*.EMSave";
            DialogResult res = f.ShowDialog();
            if (res == DialogResult.OK)
            {
                try
                {
                    meter.Save(f.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nastal problém s ukládáním dat.\n{ex.Message}");
                }
            }
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new();
            f.Filter = "Energy Monitor Saves (*.EMSave)|*.EMSave";
            DialogResult res = f.ShowDialog();
            if (res == DialogResult.OK)
            {
                try
                {
                    meter.Load(f.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nastal problém s ukládáním dat.\n{ex.Message}");
                }

                //refresh ListBox's DataSource
                bsReadings.DataSource = null;
                bsReadings.DataSource = meter.Readings;
                bsPrices.DataSource = null;
                bsPrices.DataSource = meter.Prices;
                bsTaxes.DataSource = null;
                bsTaxes.DataSource = meter.Taxes;
            }

        }

        private void textBoxMonthlyPay_TextChanged(object sender, EventArgs e)
        {
            if (textBoxMonthlyPay.Text == String.Empty)
            {
                return;
            }
            double monthly = -1;
            try
            {
                monthly = Double.Parse(textBoxMonthlyPay.Text);
            }
            catch (FormatException ex) { };

            if (monthly <= 0)
            {
                MessageBox.Show("Neplatná hodnota Měsíční záloha!\n");
                textBoxYearPay.Text = "???";
            }
            else
            {
                textBoxYearPay.Text = (monthly * 12).ToString("N2");
            }
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                //denní limit
                double dayLimit = meter.PricesDatabase.GetVeightedAverageYearConsumption();
                textBoxDayLimit.Text = dayLimit.ToString("N2");

                //spotřebováno kWh
                int consumedKwh = meter.Readings.Last().StateOfGauge - meter.Readings.First().StateOfGauge;
                textBoxConsumedKwh.Text = consumedKwh.ToString();

                //spotřebováno peněz
                double consumedMoney = 0;
                for (int i = 0; i < meter.Readings.Count - 1; i++)
                {
                    int consumedKwhInThisInterval = meter.Readings[i + 1].StateOfGauge - meter.Readings[i].StateOfGauge;
                    double avgPriceInThisInterval = meter.PricesDatabase.GetAveragePriceIn(meter.Readings[i].Date, meter.Readings[i + 1].Date);
                    consumedMoney += (avgPriceInThisInterval * consumedKwhInThisInterval);
                }
                if (consumedMoney == 0)
                {
                    textBoxConsumedMoney.Text = "???";
                }
                else
                {
                    textBoxConsumedMoney.Text = consumedMoney.ToString("N2");
                }

                //spotřebováno na poplatcích
                    
                double taxesConsumed = meter.TaxesDatabase.GetTaxesIn(meter.Readings.First().Date, meter.Readings.Last().Date, consumedKwh);

                //výpis
                textBoxRemainigMoney.Text = (Double.Parse(textBoxYearPay.Text) - consumedMoney - taxesConsumed).ToString("N2");
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException || ex is FormatException)
                {
                    MessageBox.Show($"Některá data pro výpočet chybí nebo nejsou zadána správně.\n\n{ex.Message}");
                    return;
                }

                throw;
            }
        }
    }
}
