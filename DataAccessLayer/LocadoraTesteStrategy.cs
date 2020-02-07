using Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    class LocadoraTesteStrategy : DropCreateDatabaseAlways<LocadoraDbContext>
    {
        protected override void Seed(LocadoraDbContext context)
        {
            using (context)
            {
                Funcionario c = new Funcionario()
                {
                    Nome = "Natanael",
                    CPF = "108.758.449-36",
                    Email = "natanael@gmail.com",
                    Senha = "123@Natanael",
                    Telefone = "33350158",
                    DataNascimento = DateTime.Now.AddYears(-17),
                    EhAtivo = true
                };

                context.Funcionarios.Add(c);
                context.SaveChanges();
            }
            base.Seed(context);
        }
    }
}
