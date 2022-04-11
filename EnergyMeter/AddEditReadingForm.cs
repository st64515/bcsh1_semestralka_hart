namespace EnergyMonitor
{
    public partial class AddEditReadingForm : Form
    {
        public DateTime Date { get; set; }
        public int StateOfGauge { get; set; }

        public AddEditReadingForm()
        {
            InitializeComponent();
        }

        public AddEditReadingForm(DateTime date, int stateOfGauge)
        {
            InitializeComponent();
            
            Date = date;
            StateOfGauge = stateOfGauge;

            textBoxGaugeState.Text = stateOfGauge.ToString();
            dateTimePickerReadingDate.Value = date;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxGaugeState.Text, out int gaugeStateParsed))
            {
                StateOfGauge = gaugeStateParsed;
                Date = dateTimePickerReadingDate.Value.Date;
            }
            else
            {
                MessageBox.Show("Neplatný stav měřiče. Změny nebyly provedeny.");
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
