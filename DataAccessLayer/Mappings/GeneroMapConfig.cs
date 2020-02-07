using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Mappings
{
    internal class GeneroMapConfig : EntityTypeConfiguration<Genero>
    {
        public GeneroMapConfig()
        {
            this.ToTable("GENEROS");
            this.Property(c => c.Nome).HasMaxLength(30);

        }
    }
}
