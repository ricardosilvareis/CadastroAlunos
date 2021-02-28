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

namespace CadAlunos
{
    public partial class Form1 : Form
    {
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;

        string strSQL;
        public Form1()
        {
            InitializeComponent();


            // Conteúdo dos combobox "Espaço Café e Sala de aula "
            cbxEspaco.Items.Add("Espaço 1");
            cbxEspaco.Items.Add("Espaço 2");

            cbxSala.Items.Add("Sala Java");
            cbxSala.Items.Add("Sala C#");
            cbxSala.Items.Add("Sala Pyton");
            cbxSala.Items.Add("Sala Delphi");

        }

        #region "Método para Gravar os dados no Banco"
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Conexão com o banco de gravação dos dos dados ao clicar no botão "NOVO"
                conexao = new MySqlConnection("Server=localhost;Database=cad_aluno;Uid=root;Pwd=root;");
                strSQL = "INSERT INTO ALUNOS (nome, sobrenome,espaco_cafe, sala) VALUES (@nome, @sobrenome, @espaco_cafe, @sala)";
                comando = new MySqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@nome", txtNome.Text);
                comando.Parameters.AddWithValue("@sobrenome", txtSobrenome.Text);
                comando.Parameters.AddWithValue("@espaco_cafe", cbxEspaco.Text);
                comando.Parameters.AddWithValue("@sala", cbxSala.Text);

                conexao.Open();
                comando.ExecuteNonQuery();

                txtNome.Text = "";
                txtSobrenome.Text = "";
                cbxEspaco.Text = "";
                cbxSala.Text = "";

                AtualizaLista();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }


        }
        #endregion

        #region "Método que Edita os usuários no Banco"
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {

                // Edita o usuario no banco de dados.
                conexao = new MySqlConnection("Server=localhost;Database=cad_aluno;Uid=root;Pwd=root;");
                strSQL = "UPDATE ALUNOS SET NOME = @NOME, SOBRENOME = @SOBRENOME, ESPACO_CAFE = @ESPACO_CAFE, SALA = @SALA WHERE ID = @ID";
                comando = new MySqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@id", txtId.Text);

                comando.Parameters.AddWithValue("@nome", txtNome.Text);
                comando.Parameters.AddWithValue("@sobrenome", txtSobrenome.Text);
                comando.Parameters.AddWithValue("@espaco_cafe", cbxEspaco.Text);
                comando.Parameters.AddWithValue("@sala", cbxSala.Text);

                conexao.Open();
                comando.ExecuteNonQuery();

                da = new MySqlDataAdapter(strSQL, conexao);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dtgGrid.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }
        #endregion

        #region "Método que excluir os usuários do Banco"
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection("Server=localhost;Database=cad_aluno;Uid=root;Pwd=root;");
                strSQL = "DELETE FROM ALUNOS WHERE ID = @ID";
                comando = new MySqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@id", txtId.Text);
                conexao.Open();
                comando.ExecuteNonQuery();

                txtId.Text = "";

                AtualizaLista();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }
        #endregion

        #region "Método que consulta o Usuario no banco"
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection("Server=localhost;Database=cad_aluno;Uid=root;Pwd=root;");
                strSQL = "SELECT * FROM alunos WHERE ID = @ID";
                comando = new MySqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@id", txtId.Text);
                conexao.Open();

                dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    txtNome.Text = Convert.ToString(dr["nome"]);
                    txtSobrenome.Text = Convert.ToString(dr["sobrenome"]);
                }

                AtualizaLista();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }
        #endregion

        #region "Método que exibi todos os usuários no Banco" 
        private void btnExibir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection("Server=localhost;Database=cad_aluno;Uid=root;Pwd=root;");
                strSQL = "SELECT * FROM ALUNOS";
                da = new MySqlDataAdapter(strSQL, conexao);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dtgGrid.DataSource = dt;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }
        #endregion


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'cad_alunoDataSet.alunos'. Você pode movê-la ou removê-la conforme necessário.
            this.alunosTableAdapter.Fill(this.cad_alunoDataSet.alunos);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        // Método criado para fazer atualização da lista de alunos ao ser criada e mostrando em tela. 
        public void AtualizaLista()
        {
            conexao = new MySqlConnection("Server=localhost;Database=cad_aluno;Uid=root;Pwd=root;");
            strSQL = "SELECT * FROM ALUNOS";
            da = new MySqlDataAdapter(strSQL, conexao);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dtgGrid.DataSource = dt;
        }
    }
}
