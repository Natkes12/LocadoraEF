using BusinessLogicalLayer;
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
    public partial class FormPesquisaFIlme : Form
    {
        public FormPesquisaFIlme()
        {
            InitializeComponent();
            this.Load += FormPesquisaFIlme_Load;
            this.dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
        }

        public Filme FilmeSelecionado { get; private set; }
        public FilmeResultSet FilmeResultSetSelecionado { get; private set; }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.FilmeResultSetSelecionado = (FilmeResultSet)this.dataGridView1.SelectedRows[0].DataBoundItem;
            this.dataGridView1.DataSource = new FilmeBLL().GetData().Data;
            this.FilmeSelecionado = (Filme)this.dataGridView1.SelectedRows[0].DataBoundItem;
            this.Close();
        }

        private void FormPesquisaFIlme_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = new FilmeBLL().GetFilmes().Data;
        }
    }
}
