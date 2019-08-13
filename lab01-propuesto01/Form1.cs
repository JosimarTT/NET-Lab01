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

namespace lab01_propuesto01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbPropuesto01"].ConnectionString);
        public void ListaClientes(string nombre)
        {
            using (SqlDataAdapter df = new SqlDataAdapter("Usp_ListaNegocios_Neptuno", cn))
            {
                df.SelectCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@idCliente",
                    Value = inputCliente.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 100
                });
                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                using (DataSet Da = new DataSet())
                {
                    df.Fill(Da, "clientes");
                    dgNegocios.DataSource = Da.Tables["clientes"];
                    lbCantidad.Text = Da.Tables["clientes"].Rows.Count.ToString();
                }
            }
        }

        private void dgNegocios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaClientes("");
        }

        private void inputCliente_TextChanged(object sender, EventArgs e)
        {
            ListaClientes(inputCliente.Text);
        }
    }
}
