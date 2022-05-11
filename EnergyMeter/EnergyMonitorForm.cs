using EnergyMonitorLibrary;

namespace EnergyMonitor
{
    public partial class EnergyMonitorForm : Form
    {

        private readonly Meter meter = new();
        private readonly BindingSource bsReadings = new();
        private readonly BindingSource bsPrices = new();
        private readonly BindingSource bsTaxes = new();

        public EnergyMonitorForm()
        {
            InitializeComponent();

            meter.AddReading(new Reading(new DateTime(2021, 07, 01), 10000));
            meter.AddReading(new Reading(new DateTime(2021, 10, 11), 10865));
            meter.AddReading(new Reading(new DateTime(2021, 12, 11), 11204));
            meter.AddReading(new Reading(new DateTime(2022, 01, 30), 11800));
            meter.AddReading(new Reading(new DateTime(2022, 04, 20), 12475));
            meter.AddReading(new Reading(new DateTime(2022, 05, 11), 12532));
            meter.AddPrice(new Price(new DateTime(2021, 07, 01), new DateTime(2021, 12, 31), 3));
            meter.AddPrice(new Price(new DateTime(2022, 01, 01), new DateTime(2022, 06, 30), 5.5));
            meter.AddTax(new Tax("Poplatek", new DateTime(2021, 01, 01), new DateTime(2023, 12, 31), 1000, Intervals.PerMonth));
            meter.AddTax(new Tax("Poplatek", new DateTime(2022, 03, 01), new DateTime(2022, 03, 31), 1, Intervals.PerMWh));

            textBoxMonthlyPay.Text = "2500";

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
            SaveFileDialog f = new();
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

        private void TextBoxMonthlyPay_TextChanged(object sender, EventArgs e)
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
            catch (FormatException) { };

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

        private void ButtonCalculate_Click(object sender, EventArgs e)
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
                double taxesConsumed = meter.TaxesDatabase.GetTaxesIn(meter.Readings.First().Date, meter.Readings.Last().Date, meter.ReadingsDatabase);
                textBoxTaxesConsumed.Text = taxesConsumed.ToString("N2");

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

            //drawing
            List<int> yearAvgConsumption = new();
            meter.ReadingsDatabase.GetAvgConsumptionInYear(out yearAvgConsumption);
            int canvasWidth = panelGraph.Width - 1;
            int canvasHeight = panelGraph.Height - 1;
            int shift = 10;
            double max = yearAvgConsumption.Max();
            double maximizer = (canvasHeight - (shift + 10)) / max;

            Graphics g = panelGraph.CreateGraphics();
            g.Clear(Color.LightGray);
            Pen p;
            SolidBrush brush5, brush4, brush3;
            Rectangle r;

            //draw columns
            int columnWidth = (int)Math.Round(canvasWidth / (double)yearAvgConsumption.Count);
            Color blue5 = Color.FromArgb(0, 48, 90);
            Color blue4 = Color.FromArgb(0, 75, 141);
            Color blue3 = Color.FromArgb(0, 116, 217);
            Color blue2 = Color.FromArgb(65, 146, 217);
            Color blue1 = Color.FromArgb(122, 186, 242);
            brush5 = new SolidBrush(blue5);
            brush4 = new SolidBrush(blue4);
            brush3 = new SolidBrush(blue3);

            int verticalGrids = 12;
            int verticalGridsSpacing = canvasWidth / verticalGrids;
            /*
            int horizontalGrids = 20;
            int horizontalGridsSpacing = canvasHeight / verticalGrids;
            */

            //draw backgroud grid
            p = new Pen(blue2, 3);
            for (int i = 1; i < verticalGrids; i++)
            {
                g.DrawLine(p, i * verticalGridsSpacing, canvasHeight, i * verticalGridsSpacing, 0);
            }


            //backgroud rectangles
            for (int i = 0; i < yearAvgConsumption.Count; i++)
            {
                int columnHeight = (int)(yearAvgConsumption[i] * maximizer);
                r = new Rectangle(i * columnWidth + shift, canvasHeight - columnHeight - shift, 2, columnHeight + shift);
                g.FillRectangle(brush4, r);
            }

            //foreground rectangles
            for (int i = 0; i < yearAvgConsumption.Count; i++)
            {
                int columnHeight = (int)(yearAvgConsumption[i] * maximizer);
                r = new Rectangle(i * columnWidth, canvasHeight - columnHeight, 2, columnHeight);
                g.FillRectangle(brush3, r);
            }

            /*
            //draw foreground grid
            p = new Pen(blue1, 1);
            for (int i = 1; i < verticalGrids; i++)
            {
                g.DrawLine(p, i * verticalGridsSpacing, canvasHeight, i * verticalGridsSpacing, 0);
            }
            */

            //draw border
            p = new Pen(blue5, 4);
            r = new Rectangle(2, 2, canvasWidth - 2, canvasHeight - 2);
            g.DrawRectangle(p, r);



        }
    }
}
