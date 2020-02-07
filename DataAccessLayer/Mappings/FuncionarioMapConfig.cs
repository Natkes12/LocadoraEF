using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Mappings
{
    internal class FuncionarioMapConfig : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioMapConfig()
        {
            this.ToTable("FUNCIONARIOS");
            this.Property(f => f.Nome).HasMaxLength(100);
            this.Property(f => f.CPF).IsFixedLength().HasMaxLength(14);
            this.Property(f => f.Email).HasMaxLength(40);
            this.Property(f => f.Senha).HasMaxLength(30);
            this.Property(f => f.Telefone).HasMaxLength(18);
            //this.HasIndex(f => f.Email).IsUnique();
            //this.HasIndex(f => f.CPF).IsUnique();
            //this.HasIndex(f => f.Telefone).IsUnique();
        }
    }
}
