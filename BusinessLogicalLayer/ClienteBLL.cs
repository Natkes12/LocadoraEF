using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    /// <summary>
    /// Classe responsável pelas regras de negócio 
    /// da entidade Gênero.
    /// </summary>
    public class ClienteBLL : IEntityCRUD<Cliente>
    {
        public Response Insert(Cliente item)
        {
            Response response = new Response();
            response = Validate(item);

            if (response.Erros.Count > 0)
            {
                response.Sucesso = false;
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    db.Clientes.Add(item);
                    db.SaveChanges();
                }
                response.Sucesso = true;
                return response;
            }
            catch(Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                return response;
            }

            //return dal.Insert(item);

        }
        public Response Delete(int id)
        {
            Response response = new Response();

            if (response.Erros.Count != 0)
            {
                response.Sucesso = false;
                return response;
            }

            if (id <= 0)
            {
                response.Sucesso = false;
                response.Erros.Add("Valor do ID inválido.");
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Cliente clienteASerExcluido = new Cliente();
                    clienteASerExcluido.ID = id;
                    db.Entry<Cliente>(clienteASerExcluido).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                response.Sucesso = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                return response;
            }

        }

        public Response Update(Cliente item)
        {
            Response response = Validate(item);

            if (response.Erros.Count != 0)
            {
                response.Sucesso = false;
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Cliente clienteASerAtualizado = new Cliente();
                    clienteASerAtualizado = item;
                    db.Entry<Cliente>(clienteASerAtualizado).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                response.Sucesso = true;
                return response;
            }
            catch(Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                return response;
            }
        }

        public DataResponse<Cliente> GetData()
        {
            DataResponse<Cliente> cli = new DataResponse<Cliente>();

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    cli.Data = db.Clientes.ToList();
                }
                cli.Sucesso = true;
                return cli;
            }
            catch(Exception ex)
            {
                cli.Erros.Add("Erro no banco de dados, contate o administrador.");
                cli.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                return cli;
            }
        }

        public DataResponse<Cliente> GetByID(int id)
        {
            DataResponse<Cliente> response = new DataResponse<Cliente>();

            if (id <= 0)
            {
                response.Sucesso = false;
                response.Erros.Add("Valor do ID inválido.");
                return response;
            }

            try
            {
                using(LocadoraDbContext db = new LocadoraDbContext())
                {
                    response.Data.Add(db.Clientes.Find(id));
                }
                response.Sucesso = true;
                return response;
            }
            catch(Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                return response;
            }
        }

        private Response Validate(Cliente item)
        {
            Response response = new Response();
            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                response.Erros.Add("O nome do cliente deve ser informado.");
            }
            else
            {
                //Remove espaços em branco no começo e no final da string.
                item.Nome = item.Nome.Trim();
                //Remove espaços extras entre as palavras, ex: "A      B", ficaria "A B".
                item.Nome = Regex.Replace(item.Nome, @"\s+", " ");
                if (item.Nome.Length < 2 || item.Nome.Length > 100)
                {
                    response.Erros.Add("O nome do cliente deve conter entre 2 e 100 caracteres");
                }
            }
            if (string.IsNullOrWhiteSpace(item.Email))
            {
                response.Erros.Add("O email do cliente deve ser informado.");
            }
            else
            {
                //Remove espaços em branco no começo e no final da string.
                item.Email = item.Email.Trim();
                //Remove espaços extras entre as palavras, ex: "A      B", ficaria "A B".
                item.Email = Regex.Replace(item.Email, @"\s+", " ");
                if (item.Email.Length < 5 || item.Email.Length > 50)
                {
                    response.Erros.Add("O email do cliente deve conter entre 5 e 40 caracteres");
                }
                //TODO: Validar email 
            }
            return response;
        }
    }
}
