using EnergyMonitorLibrary;
namespace EnergyMonitor
{
    public partial class AddEditTaxForm : Form
    {
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Cost { get; set; }
        public Intervals Interval { get; set; }

        private BindingSource bsIntervals = new();

        public AddEditTaxForm()
        {
            InitializeComponent();

            comboBoxIntervals.DataSource = Enum.GetValues(typeof(Intervals)).Cast<Intervals>();
        }

        public AddEditTaxForm(string description, DateTime startDate, DateTime endDate, int cost, Intervals interval)
        {
            InitializeComponent();

            comboBoxIntervals.DataSource = Enum.GetValues(typeof(Intervals)).Cast<Intervals>();

            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Cost = cost;
            Interval = interval;

            textBoxDescription.Text = description;
            dateTimePickerStartDate.Value = startDate;
            dateTimePickerEndDate.Value = endDate;
            textBoxCost.Text = cost.ToString();
            comboBoxIntervals.SelectedItem = interval;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxCost.Text, out int costParsed))
            {
                Description = textBoxDescription.Text;
                StartDate = dateTimePickerStartDate.Value.Date;
                EndDate = dateTimePickerEndDate.Value.Date;
                Cost = costParsed;
                Interval = (Intervals)comboBoxIntervals.SelectedItem;

            }
            else
            {
                MessageBox.Show("Neplatná cena. Změny nebyly provedeny.");
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
