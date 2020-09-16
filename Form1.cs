using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Aula_Banco_Locadora
{
    public partial class Locadora : Form
    {
        public Locadora()
        {
            InitializeComponent();
        }

        private MySqlConnectionStringBuilder conexaoBanco()//AQUI PASSA OS VALORES (LOCALHOST,BD,USUARIO E PASSWORD)
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "locadoracarro";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";

            return conexaoBD;
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();//chama conexão com o banco através dessa função
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexaoBD.Open();

                MessageBox.Show("Conexão Aberta!");
                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "INSERT INTO carro (nomeCarro, marcaCarro, anoCarro, corCarro) VALUES ('" + textBoxNome.Text + "', '" + textBoxMarca.Text + "', " + textBoxAno.Text + ", '" + textBoxCor.Text + "')";
                comandoMySql.ExecuteNonQuery();

                realizaConexaoBD.Close();
                MessageBox.Show("Inserido com sucesso!");
                atualizarGrid();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível abrir a conexão!");
                Console.WriteLine(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridViewDados.CurrentRow.Selected = true;

                textBoxID.Text = dataGridViewDados.Rows[e.RowIndex].Cells["ColumnID"].FormattedValue.ToString();
                textBoxNome.Text = dataGridViewDados.Rows[e.RowIndex].Cells["ColumnNome"].FormattedValue.ToString();
                textBoxMarca.Text = dataGridViewDados.Rows[e.RowIndex].Cells["ColumnMarca"].FormattedValue.ToString();
                textBoxAno.Text = dataGridViewDados.Rows[e.RowIndex].Cells["ColumnAno"].FormattedValue.ToString();
                textBoxCor.Text = dataGridViewDados.Rows[e.RowIndex].Cells["ColumnCor"].FormattedValue.ToString();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            atualizarGrid();
        }

        private void atualizarGrid()
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                realizaConexaoBD.Open();
                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "SELECT * FROM carro";
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dataGridViewDados.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewDados.Rows[0].Clone();
                    row.Cells[0].Value = reader.GetInt32(0);
                    row.Cells[1].Value = reader.GetString(1);
                    row.Cells[2].Value = reader.GetString(2);
                    row.Cells[3].Value = reader.GetInt32(3);
                    row.Cells[4].Value = reader.GetString(4);
                    dataGridViewDados.Rows.Add(row);
                }

                realizaConexaoBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível realizar a conexão!");
                Console.WriteLine(ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "locadoracarro";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexaoBD.Open();

                MessageBox.Show("Conexão Aberta!");
                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "UPDATE carro SET  nomeCarro = '" + textBoxNome.Text + "', marcaCarro = '" + textBoxMarca.Text + "', anoCarro = '" + textBoxAno.Text + "', corCarro = '" + textBoxCor.Text + "' WHERE idCarro = " + textBoxID.Text + "";
                comandoMySql.ExecuteNonQuery();

                realizaConexaoBD.Close();
                MessageBox.Show("Alterado com sucesso!");
                atualizarGrid();

            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "locadoracarro";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                realizaConexaoBD.Open();
                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "DELETE FROM carro WHERE idCarro = '" + textBoxID.Text + "'";
                comandoMySql.ExecuteNonQuery();

                realizaConexaoBD.Close();
                MessageBox.Show("Removido com sucesso");
                atualizarGrid();
                limparCampos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void limparCampos()
        {
            textBoxID.Clear();
            textBoxNome.Clear();
            textBoxMarca.Clear();
            textBoxAno.Clear();
            textBoxCor.Clear();
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            limparCampos();
        }
    }
}