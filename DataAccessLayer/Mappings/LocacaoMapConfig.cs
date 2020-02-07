using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Mappings
{
    internal class LocacaoMapConfig : EntityTypeConfiguration<Locacao>
    {
        public LocacaoMapConfig()
        {
            this.ToTable("LOCACOES");
            this.HasRequired(l => l.Cliente).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(l => l.Funcionario).WithMany().WillCascadeOnDelete(false);
        }
    }
}
