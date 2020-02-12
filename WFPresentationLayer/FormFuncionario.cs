using BusinessLogicalLayer;
using BusinessLogicalLayer.Security;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFPresentationLayer
{
    public partial class FormFuncionario : Form
    {
        public FormFuncionario()
        {
            InitializeComponent();
            this.dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
        }

        private void FormFuncionario_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = funcionarioBLL.GetFuncionarios().Data;
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Funcionario result = (Funcionario)dataGridView1.SelectedRows[0].DataBoundItem;
            DataResponse<Funcionario> response = funcionarioBLL.GetByID(result.ID);
            if (response.Sucesso)
            {
                Funcionario funcionario = response.Data[0];
                funcionarioASerAtualizadoExcluido = funcionario.ID;
                txtNome.Text = funcionario.Nome;
                txtCpf.Text = funcionario.CPF;
                txtEmail.Text = funcionario.Email;
                txtTelefone.Text = funcionario.Telefone;
                dtpDataNascimento.Value = funcionario.DataNascimento;
            }
        }

        private FuncionarioBLL funcionarioBLL = new FuncionarioBLL();
        private int funcionarioASerAtualizadoExcluido = 0;


        private void button1_Click(object sender, EventArgs e)
        {
            if (txtConfirmarSenha.Text != txtSenha.Text)
            {
                MessageBox.Show("Senha diferentes!");
                return;
            }

            Funcionario funcionario = new Funcionario();
            funcionario.CPF = txtCpf.Text;
            funcionario.DataNascimento = dtpDataNascimento.Value;
            funcionario.Email = txtEmail.Text;
            funcionario.Nome = txtNome.Text;
            funcionario.Senha = txtSenha.Text;
            funcionario.Telefone = txtTelefone.Text;

            Response response = funcionarioBLL.Insert(funcionario);
            if (response.Sucesso)
            {
                MessageBox.Show("Cadastrado com sucesso!");
                dataGridView1.DataSource = funcionarioBLL.GetFuncionarios().Data;
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = !txtSenha.UseSystemPasswordChar;
        }

        private void btnMostrarSenha2_Click(object sender, EventArgs e)
        {
            txtConfirmarSenha.UseSystemPasswordChar = !txtConfirmarSenha.UseSystemPasswordChar;
        }

        

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            DataResponse<Funcionario> func = funcionarioBLL.GetByID(funcionarioASerAtualizadoExcluido);
            List<Funcionario> funcionarios = func.Data;

            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                MessageBox.Show("Confirmação da senha incorreta.");
                return;
            }

            string senha = SenhaValidator.TransferToHashPassword(txtSenha.Text);

            if (senha != funcionarios[0].Senha)
            {
                MessageBox.Show("Senha incorreta.");
                return;
            }

            Funcionario funcionario = new Funcionario();
            funcionario.ID = funcionarioASerAtualizadoExcluido;
            funcionario.Nome = txtNome.Text;
            funcionario.Email = txtEmail.Text;
            funcionario.CPF = txtCpf.Text;
            funcionario.Telefone = txtTelefone.Text;
            funcionario.Senha = senha;
            funcionario.DataNascimento = dtpDataNascimento.Value;
            funcionario.EhAtivo = true;

            Response response = funcionarioBLL.Update(funcionario);
            if (response.Sucesso)
            {
                MessageBox.Show("Funcionário atualizado com sucesso!");
                dataGridView1.DataSource = funcionarioBLL.GetFuncionarios().Data;
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            Response response = funcionarioBLL.Delete(funcionarioASerAtualizadoExcluido);
            if (response.Sucesso)
            {
                MessageBox.Show("Funcionário excluído com sucesso!");
                dataGridView1.DataSource = funcionarioBLL.GetFuncionarios().Data;
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }
        }
    }
}
