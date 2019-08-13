using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace lab01_propuesto02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbPropuesto02"].ConnectionString);
        public void ListaClientes(string nombre)
        {
            using (SqlDataAdapter df = new SqlDataAdapter("Usp_Listar_Pedidos", cn))
            {
                df.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@idCliente",
                    Value = cbCliente.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 100
                });
                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                using (DataSet Da = new DataSet())
                {
                    df.Fill(Da, "Pedidos");
                    dgCliente.DataSource = Da.Tables["pedidos"];
                    lblPedidos.Text = Da.Tables["Pedidos"].Rows.Count.ToString();
                }
            }
        }

        public void Listacombo()
        {
            using (SqlDataAdapter df = new SqlDataAdapter("Usp_Listar_Clientes_All", cn))
            {
                using (DataSet Da = new DataSet())
                {
                    df.Fill(Da, "Clientes");
                    var clientes = Da.Tables["Clientes"];
                    cbCliente.DataSource = Da.Tables["Clientes"];
                    cbCliente.DisplayMember = "NombreContacto";
                    lblPedidos.Text = Da.Tables["Clientes"].Rows.Count.ToString();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ListaClientes("");
        }

        private void cbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            Listacombo();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Listacombo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaClientes("");
        }
    }
}
