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
            meter.AddPrice(new DateTime(2022, 01, 01), new DateTime(2023, 12, 31), 2);
            meter.AddPrice(new DateTime(2024, 01, 01), new DateTime(2026, 12, 31), 10);
            meter.AddReading(new Reading(new DateTime(2022, 04, 10), 11000));
            meter.AddTax("Poplatek", new DateTime(2022, 01, 01), new DateTime(2023, 12, 31), 1000, Intervals.PerMonth);
            
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
                    if (meter is not null)
                    {
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
        }

        private void ButtonRemoveReading_Click(object sender, EventArgs e)
        {
            if (meter is not null)
            {
                meter.RemoveReading(listBoxReadings.SelectedIndex);

                //obnov DataSource v ListBoxu
                bsReadings.DataSource = null;
                bsReadings.DataSource = meter.Readings;
            }
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
                bsReadings.DataSource = null;
                bsReadings.DataSource = meter.Readings;
            }
        }

        private void ButtonEditPrice_Click(object sender, EventArgs e)
        {

        }

        private void ButtonRemovePrice_Click(object sender, EventArgs e)
        {

        }


        private void ButtonSave_Click(object sender, EventArgs e)
        {

        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {

        }
    }
}
