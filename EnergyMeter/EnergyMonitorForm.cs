using EnergyMonitorLibrary;

namespace EnergyMonitor
{
    public partial class EnergyMonitorForm : Form
    {

        private Meter? meter;
        private BindingSource bs = new();

        public EnergyMonitorForm()
        {
            InitializeComponent();

            meter = new(new Reading(new DateTime(2022, 04, 01), 10000));
            meter.AddElectricityPrice(new DateTime(2022, 01, 01), new DateTime(2023, 12, 31), 2);
            meter.AddElectricityPrice(new DateTime(2024, 01, 01), new DateTime(2026, 12, 31), 10);
            meter.AddReading(new Reading(new DateTime(2022, 04, 10), 11000));

            bs.DataSource = meter.Readings;
            listBoxReadings.DataSource = bs;
        }

        private void ButtonAddReading_Click(object sender, EventArgs e)
        {
            using AddEditReadingForm f = new();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Reading newReading = new(f.Date, f.StateOfGauge);
                if (meter == null)
                {
                    meter = new(newReading);
                }
                else
                {
                    try
                    {
                        meter.AddReading(newReading);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                //obnov DataSource v ListBoxu
                bs.DataSource = null;
                bs.DataSource = meter.Readings;
            }
        }

        private void buttonEditReading_Click(object sender, EventArgs e)
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
                        bs.DataSource = null;
                        bs.DataSource = meter.Readings;
                    }
                }
            }
        }

        private void buttonRemoveReading_Click(object sender, EventArgs e)
        {
            if (meter is not null)
            {
                meter.RemoveReading(listBoxReadings.SelectedIndex);

                //obnov DataSource v ListBoxu
                bs.DataSource = null;
                bs.DataSource = meter.Readings;
            }
        }
    }
}
