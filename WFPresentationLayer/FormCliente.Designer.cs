namespace WFPresentationLayer
{
    partial class FormCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCadastrar = new System.Windows.Forms.Button();
            this.txtClienteNome = new System.Windows.Forms.TextBox();
            this.lblClienteNome = new System.Windows.Forms.Label();
            this.lblClienteEmail = new System.Windows.Forms.Label();
            this.txtClienteEmail = new System.Windows.Forms.TextBox();
            this.lblClienteCPF = new System.Windows.Forms.Label();
            this.txtClienteCPF = new System.Windows.Forms.TextBox();
            this.dtpClienteDataNascimento = new System.Windows.Forms.DateTimePicker();
            this.lblClienteDataNascimento = new System.Windows.Forms.Label();
            this.chbClienteEhAtivo = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.Location = new System.Drawing.Point(123, 441);
            this.btnCadastrar.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(131, 42);
            this.btnCadastrar.TabIndex = 0;
            this.btnCadastrar.Text = "Cadastrar";
            this.btnCadastrar.UseVisualStyleBackColor = true;
            this.btnCadastrar.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtClienteNome
            // 
            this.txtClienteNome.Location = new System.Drawing.Point(14, 54);
            this.txtClienteNome.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtClienteNome.Name = "txtClienteNome";
            this.txtClienteNome.Size = new System.Drawing.Size(385, 34);
            this.txtClienteNome.TabIndex = 1;
            // 
            // lblClienteNome
            // 
            this.lblClienteNome.AutoSize = true;
            this.lblClienteNome.Location = new System.Drawing.Point(9, 18);
            this.lblClienteNome.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblClienteNome.Name = "lblClienteNome";
            this.lblClienteNome.Size = new System.Drawing.Size(85, 29);
            this.lblClienteNome.TabIndex = 2;
            this.lblClienteNome.Text = "Nome:";
            // 
            // lblClienteEmail
            // 
            this.lblClienteEmail.AutoSize = true;
            this.lblClienteEmail.Location = new System.Drawing.Point(9, 189);
            this.lblClienteEmail.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblClienteEmail.Name = "lblClienteEmail";
            this.lblClienteEmail.Size = new System.Drawing.Size(80, 29);
            this.lblClienteEmail.TabIndex = 4;
            this.lblClienteEmail.Text = "Email:";
            // 
            // txtClienteEmail
            // 
            this.txtClienteEmail.Location = new System.Drawing.Point(14, 225);
            this.txtClienteEmail.Margin = new System.Windows.Forms.Padding(5);
            this.txtClienteEmail.Name = "txtClienteEmail";
            this.txtClienteEmail.Size = new System.Drawing.Size(385, 34);
            this.txtClienteEmail.TabIndex = 3;
            // 
            // lblClienteCPF
            // 
            this.lblClienteCPF.AutoSize = true;
            this.lblClienteCPF.Location = new System.Drawing.Point(9, 101);
            this.lblClienteCPF.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblClienteCPF.Name = "lblClienteCPF";
            this.lblClienteCPF.Size = new System.Drawing.Size(67, 29);
            this.lblClienteCPF.TabIndex = 6;
            this.lblClienteCPF.Text = "CPF:";
            // 
            // txtClienteCPF
            // 
            this.txtClienteCPF.Location = new System.Drawing.Point(14, 137);
            this.txtClienteCPF.Margin = new System.Windows.Forms.Padding(5);
            this.txtClienteCPF.Name = "txtClienteCPF";
            this.txtClienteCPF.Size = new System.Drawing.Size(385, 34);
            this.txtClienteCPF.TabIndex = 5;
            // 
            // dtpClienteDataNascimento
            // 
            this.dtpClienteDataNascimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpClienteDataNascimento.Location = new System.Drawing.Point(14, 312);
            this.dtpClienteDataNascimento.Name = "dtpClienteDataNascimento";
            this.dtpClienteDataNascimento.Size = new System.Drawing.Size(385, 34);
            this.dtpClienteDataNascimento.TabIndex = 7;
            // 
            // lblClienteDataNascimento
            // 
            this.lblClienteDataNascimento.AutoSize = true;
            this.lblClienteDataNascimento.Location = new System.Drawing.Point(9, 280);
            this.lblClienteDataNascimento.Name = "lblClienteDataNascimento";
            this.lblClienteDataNascimento.Size = new System.Drawing.Size(236, 29);
            this.lblClienteDataNascimento.TabIndex = 8;
            this.lblClienteDataNascimento.Text = "Data de Nascimento:";
            // 
            // chbClienteEhAtivo
            // 
            this.chbClienteEhAtivo.AutoSize = true;
            this.chbClienteEhAtivo.Location = new System.Drawing.Point(14, 381);
            this.chbClienteEhAtivo.Name = "chbClienteEhAtivo";
            this.chbClienteEhAtivo.Size = new System.Drawing.Size(87, 33);
            this.chbClienteEhAtivo.TabIndex = 9;
            this.chbClienteEhAtivo.Text = "Ativo";
            this.chbClienteEhAtivo.UseVisualStyleBackColor = true;
            // 
            // FormCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 518);
            this.Controls.Add(this.chbClienteEhAtivo);
            this.Controls.Add(this.lblClienteDataNascimento);
            this.Controls.Add(this.dtpClienteDataNascimento);
            this.Controls.Add(this.lblClienteCPF);
            this.Controls.Add(this.txtClienteCPF);
            this.Controls.Add(this.lblClienteEmail);
            this.Controls.Add(this.txtClienteEmail);
            this.Controls.Add(this.lblClienteNome);
            this.Controls.Add(this.txtClienteNome);
            this.Controls.Add(this.btnCadastrar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FormCliente";
            this.Text = "FormCliente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCadastrar;
        private System.Windows.Forms.TextBox txtClienteNome;
        private System.Windows.Forms.Label lblClienteNome;
        private System.Windows.Forms.Label lblClienteEmail;
        private System.Windows.Forms.TextBox txtClienteEmail;
        private System.Windows.Forms.Label lblClienteCPF;
        private System.Windows.Forms.TextBox txtClienteCPF;
        private System.Windows.Forms.DateTimePicker dtpClienteDataNascimento;
        private System.Windows.Forms.Label lblClienteDataNascimento;
        private System.Windows.Forms.CheckBox chbClienteEhAtivo;
    }
}