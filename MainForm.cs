using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace TicketSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private SqlDataReader reader;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadTickets()
        {
            command = new SqlCommand("SELECT * FROM Tickets", connection);
            adapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Tickets' table. You can move, or remove it, as needed.
            this.ticketsTableAdapter.Fill(this.database1DataSet.Tickets);

            connection =
                new SqlConnection(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Felix\source\repos\TicketSystem\Database1.mdf;Integrated Security=True");

            LoadTickets();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        private void CreateTicket()
        {
            //string query = "INSERT into Tickets " + () + "VALUES (@Id, @Severity, @Description)";
        }

        private void SaveInfo()
        {
            //string query = "INSERT into Customers" + 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchID_Click(object sender, EventArgs e)
        {
            var searchID = txtTicketID.Text;

            if (string.IsNullOrEmpty(searchID))
            {
                MessageBox.Show("Please enter a valid ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connection.Open();
                command.CommandText = $"SELECT * FROM Tickets WHERE ID = @ID";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID", searchID);

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    dataGridView1.DataSource = dataTable;
                }
                else
                {
                    LoadTickets();
                    MessageBox.Show("No records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.InnerException}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ResetUI()
        {
            txtTicketID.Text = "";
            txtDescription.Text = "";
            cboxSeverity.Text = "";
            dataGridView1.ClearSelection();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadTickets();
            ResetUI();
        }

        private void btnCreateTicket_Click(object sender, EventArgs e)
        {
            try
            {
                CreateTicket();
                MessageBox.Show("Ticket created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTickets();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while creating the ticket {ex.InnerException}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
