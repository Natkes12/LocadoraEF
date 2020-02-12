using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccessLayer;
using Entities.ResultSets;
using Entities.Enums;
using System.IO;

namespace BusinessLogicalLayer
{
    public class FilmeBLL : IEntityCRUD<Filme>, IFilmeService
    {
        public Response Delete(int id)
        {
            Response response = new Response();
            if (id<= 0)
            {
                response.Erros.Add("ID do filme não foi informado.");
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
                    Filme filmeASerExcluido = new Filme();
                    filmeASerExcluido.ID = id;
                    db.Entry<Filme>(filmeASerExcluido).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return response;

            //return filmeDAL.Delete(id);
        }

        public DataResponse<Filme> GetByID(int id)
        {
            List<Filme> f = new List<Filme>();
            DataResponse<Filme> response = new DataResponse<Filme>();

            if (id <= 0)
            {
                response.Erros.Add("Valor do ID inválido!");
                response.Sucesso = false;
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    f.Add(db.Filmes.Find(id));
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }
            response.Data = f;
            return response;
        }

        public DataResponse<Filme> GetData()
        {
            DataResponse<Filme> f = new DataResponse<Filme>();

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    f.Data = db.Filmes.ToList();
                }
            }
            catch (Exception ex)
            {
                f.Erros.Add("Erro no banco de dados, contate o administrador.");
                f.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return f;
        }

        public DataResponse<FilmeResultSet> GetFilmes()
        {
            List<FilmeResultSet> filmes = new List<FilmeResultSet>();
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    var data = db.Filmes.Join(db.Generos, f => f.Genero.ID, g => g.ID, (f, g) => new FilmeResultSet { ID = f.ID, Nome = f.Nome, Genero = g.Nome, Classificacao = f.Classificacao }).ToList();
                    
                    foreach (var item in data)
                    {
                        filmes.Add(item);
                    }
                }
                response.Sucesso = true;
            }
            catch(Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }
            response.Data = filmes;
            return response;

        }

        public DataResponse<FilmeResultSet> GetFilmesByClassificacao(Classificacao classificacao)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
            List<FilmeResultSet> filmes = new List<FilmeResultSet>();

            try
            {
                using(LocadoraDbContext db = new LocadoraDbContext())
                {
                    var data = db.Filmes.Where(f => f.Classificacao == classificacao).Join(db.Generos, f => f.Genero.ID, g => g.ID, (f, g) => new FilmeResultSet { ID = f.ID, Nome = f.Nome, Genero = g.Nome, Classificacao = f.Classificacao }).ToList();

                    foreach (var item in data)
                    {
                        filmes.Add(item);
                    }
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }
            response.Data = filmes;
            return response;

        }

        public DataResponse<FilmeResultSet> GetFilmesByGenero(int genero)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
            List<FilmeResultSet> filmes = new List<FilmeResultSet>();

            if (genero <= 0)
            {
                response.Sucesso = false;
                response.Erros.Add("Gênero deve ser informado.");
                return response;
            }

            try
            {
                using(LocadoraDbContext db = new LocadoraDbContext())
                {
                    var data = db.Filmes.Where(f => f.Genero.ID == genero).Join(db.Generos, f => f.Genero.ID, g => g.ID, (f, g) => new FilmeResultSet { ID = f.ID, Nome = f.Nome, Genero = g.Nome, Classificacao = f.Classificacao }).ToList();

                    foreach (var item in data)
                    {
                        filmes.Add(item);
                    }
                }
                response.Sucesso = true;
            }
            catch(Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }
            response.Data = filmes;
            return response;
        }

        public DataResponse<FilmeResultSet> GetFilmesByName(string nome)
        {
            DataResponse<FilmeResultSet> response = new DataResponse<FilmeResultSet>();
            List<FilmeResultSet> filmes = new List<FilmeResultSet>();

            if (string.IsNullOrWhiteSpace(nome))
            {
                response.Sucesso = false;
                response.Erros.Add("Nome deve ser informado.");
                return response;
            }
            nome = nome.Trim();

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    var data = db.Filmes.Where(f => f.Nome == nome).Join(db.Generos, f => f.Genero.ID, g => g.ID, (f, g) => new FilmeResultSet { ID = f.ID, Nome = f.Nome, Genero = g.Nome, Classificacao = f.Classificacao }).ToList();

                    foreach (var item in data)
                    {
                        filmes.Add(item);
                    }
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }
            response.Data = filmes;
            return response;

        }

        public Response Insert(Filme item)
        {
            Response response = Validate(item);
            //TODO: Verificar a existência desse gênero na base de dados
            //generoBLL.LerID(item.GeneroID);

            //Verifica se tem erros!
            if (response.Erros.Count != 0)
            {
                response.Sucesso = false;
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    db.Filmes.Add(item);
                    db.SaveChanges();
                }
                response.Sucesso = true;
            }
            catch(Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return response;

            //return filmeDAL.Insert(item);
        }
        public Response Update(Filme item)
        {
           Response response = Validate(item);
            //TODO: Verificar a existência desse gênero na base de dados
            //generoBLL.LerID(item.GeneroID);
            //Verifica se tem erros!
            if (response.Erros.Count != 0)
            {
                response.Sucesso = false;
                return response;
            }

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    Filme filmeASerAtualizado = new Filme();
                    filmeASerAtualizado = item;
                    db.Entry<Filme>(filmeASerAtualizado).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                response.Sucesso = false;
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return response;

            //return filmeDAL.Update(item);
        }

        private Response Validate(Filme item)
        {
            Response response = new Response();

            if (item.Duracao <= 10)
            {
                response.Erros.Add("Duração não pode ser menor que 10 minutos.");
            }

            if (item.DataLancamento == DateTime.MinValue
                                    ||
                item.DataLancamento > DateTime.Now)
            {
                response.Erros.Add("Data inválida.");
            }

            return response;
        }
    }
}
