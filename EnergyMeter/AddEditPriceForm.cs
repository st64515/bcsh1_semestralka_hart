namespace EnergyMonitor
{
    public partial class AddEditPriceForm : Form
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Cost { get; set; }

        public AddEditPriceForm()
        {
            InitializeComponent();
        }

        public AddEditPriceForm(DateTime startDate, DateTime endDate, double cost)
        {
            InitializeComponent();

            StartDate = startDate;
            EndDate = endDate;
            Cost = cost;

            dateTimePickerStartDate.Value = startDate;
            dateTimePickerEndDate.Value = endDate;
            textBoxCost.Text = cost.ToString();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxCost.Text, out double costParsed))
            {
                Cost = costParsed;
                StartDate = dateTimePickerStartDate.Value.Date;
                EndDate = dateTimePickerEndDate.Value.Date;
            }
            else
            {
                MessageBox.Show("Neplatná cena. Změny nebyly provedeny.");
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
