﻿using BusinessLogicalLayer;
using BusinessLogicalLayer.Security;
using Entities;
using Entities.ResultSets;
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
    public partial class FormLocacao : Form
    {
        private Cliente cliente;

        public FormLocacao()
        {
            InitializeComponent();
        }

        private void btnPesquisaCLiente_Click(object sender, EventArgs e)
        {
            FormPesquisaCliente frm = new FormPesquisaCliente();
            frm.ShowDialog();
            //Esta linha só será executada quando o formpesquisacliente for fechado! ^^
            if (frm.ClienteSelecionado != null)
            {
                this.cliente = frm.ClienteSelecionado;
                this.txtClienteID.Text = cliente.ID.ToString();
                this.txtClienteNome.Text = cliente.Nome;
                this.txtClienteCPF.Text = cliente.CPF;
            }
           
        }

        private BindingList<FilmeResultSet> listFilmesResultSetSelecionados = new BindingList<FilmeResultSet>();
        private BindingList<Filme> listFilmesSelecionados = new BindingList<Filme>();

        private void btnPesquisaFilme_Click(object sender, EventArgs e)
        {
            FormPesquisaFIlme frm = new FormPesquisaFIlme();
            this.Hide();
            frm.ShowDialog();
            this.Show();

            if (frm.FilmeResultSetSelecionado != null)
            {
                foreach (FilmeResultSet filme in listFilmesResultSetSelecionados)
                {
                    if (filme.ID == frm.FilmeResultSetSelecionado.ID)
                    {
                        MessageBox.Show("Este filme já foi selecionado.");
                        return;
                    }
                }
                listFilmesResultSetSelecionados.Add(frm.FilmeResultSetSelecionado);
                listFilmesSelecionados.Add(frm.FilmeSelecionado);
            }

            this.dataGridView1.DataSource = listFilmesResultSetSelecionados;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Locacao locacao = new Locacao();
            locacao.Cliente = this.cliente;
            locacao.Filmes = listFilmesSelecionados.ToList();
            locacao.FoiPago = chkFoiPago.Checked;
            locacao.Funcionario = User.FuncionarioLogado;

            LocacaoBLL bll = new LocacaoBLL();
            Response response = bll.EfetuarLocacao(locacao);
            if (response.Sucesso)
            {
                MessageBox.Show("Locação efetuada com sucesso!");
                this.dataGridView1.DataSource = null;
                chkFoiPago.Checked = false;
                txtClienteID.Clear();
                txtClienteNome.Clear();
                txtClienteCPF.Clear();
            }
            else
            {
                MessageBox.Show(response.GetErrorMessage());
            }
        }
    }
}
