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
    public class GeneroBLL : IEntityCRUD<Genero>
    {
        public Response Insert(Genero item)
        {
            Response response = Validate(item);
            //TODO: Implementar posteriormente regra de prevenção de gêneros repetidos no banco de dados
            //Se chegou aqui, bora pro DAL!

            //Retorna a resposta do DAL! Se tiver dúvidas do que é esta resposta,
            //analise o método do DAL!
            if (response.Erros.Count > 0)
            {
                response.Sucesso = false;
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    foreach (Genero genero in db.Generos.ToList())
                    {
                        if (genero.Nome.Equals(item.Nome))
                        {
                            response.Erros.Add("Gênero já cadastrado.");
                            response.Sucesso = false;
                            return response;
                        }
                    }
                    db.Generos.Add(item);
                    db.SaveChanges();
                }
                response.Sucesso = true;
            }
            catch(Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return response;

            //return dal.Insert(item);

        }
        public Response Update(Genero item)
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
                    Genero generoASerAtualizado = new Genero();
                    generoASerAtualizado = item;
                    db.Entry<Genero>(generoASerAtualizado).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return response;
        }
        public Response Delete(int id)
        {
            Response response = new Response();

            if (id <= 0)
            {
                response.Erros.Add("Valor do ID inválido.");
            }

            if (response.Erros.Count != 0)
            {
                response.Sucesso = false;
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Genero generoASerExcluido = new Genero();
                    generoASerExcluido.ID = id;
                    db.Entry<Genero>(generoASerExcluido).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return response;
        }

        public DataResponse<Genero> GetData()
        {
            DataResponse<Genero> response = new DataResponse<Genero>();

            try
            {
                using(LocadoraDbContext db = new LocadoraDbContext())
                {
                    response.Data = db.Generos.ToList();
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return response;
        }

        public DataResponse<Genero> GetByID(int id)
        {
            DataResponse<Genero> response = new DataResponse<Genero>();

            if (id <= 0)
            {
                response.Erros.Add("Valor do ID inválido");
                response.Sucesso = false;
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    response.Data.Add(db.Generos.Find(id));
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return response;
        }

        private Response Validate(Genero item)
        {
            Response response = new Response();
            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                response.Erros.Add("O nome do gênero deve ser informado.");
            }
            else
            {
                //Remove espaços em branco no começo e no final da string.
                item.Nome = item.Nome.Trim();
                //Remove espaços extras entre as palavras, ex: "A      B", ficaria "A B".
                item.Nome = Regex.Replace(item.Nome, @"\s+", " ");
                if (item.Nome.Length < 2 || item.Nome.Length > 50)
                {
                    response.Erros.Add("O nome do gênero deve conter entre 2 e 50 caracteres");
                }
            }

            

            return response;
        }
    }
}
