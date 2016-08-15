using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;


namespace SimpleCABApp
{
    [SmartPart]
    public partial class ContactView : UserControl
    {
        private ContactController controller;

        public ContactView()
        {
            InitializeComponent();
        }

        [CreateNew]
        public ContactController Controller
        {
            set { controller = value; }
        }

        public string ContactName
        {
            get { return Parent.Text = "Contact" ; }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Double x = System.Convert.ToDouble(XTextBox.Text);
            Double y = System.Convert.ToDouble(YTextBox.Text);
            try
            {
                // use service
                MyService.ContractsClient client = new MyService.ContractsClient();
                ZTextBox.Text = client.Add(x, y).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("service is not apply");
            }
          
        }

        private void SubtractButton_Click(object sender, EventArgs e)
        {
            Double x = System.Convert.ToDouble(XTextBox.Text);
            Double y = System.Convert.ToDouble(YTextBox.Text);
            try
            {
                // use service
                MyService.ContractsClient client = new MyService.ContractsClient();
                ZTextBox.Text = client.Subtract(x, y).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("service is not apply");
            }
        }

        private void MultiplyButton_Click(object sender, EventArgs e)
        {
            Double x = System.Convert.ToDouble(XTextBox.Text);
            Double y = System.Convert.ToDouble(YTextBox.Text);
            try
            {
                // use service
                MyService.ContractsClient client = new MyService.ContractsClient();
                ZTextBox.Text = client.Multiply(x, y).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("service is not apply");
            }
        }

        private void DivideButton_Click(object sender, EventArgs e)
        {
            Double x = System.Convert.ToDouble(XTextBox.Text);
            Double y = System.Convert.ToDouble(YTextBox.Text);
            try
            {
                // use service
                MyService.ContractsClient client = new MyService.ContractsClient();
                ZTextBox.Text = client.Divide(x, y).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("service is not apply");
            }
        }
    }
}
