using BusinessLogicalLayer.Security;
using DataAccessLayer;
using Entities;
using Entities.ResultSets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class FuncionarioBLL : IEntityCRUD<Funcionario>, IFuncionarioService
    {
        public DataResponse<Funcionario> Autenticar(string email, string senha)
        {
            //TODO: Validar email e Senha!

            senha = HashUtils.HashPassword(senha);

            //DataResponse<Funcionario> response = funcionarioDAL.Autenticar(email, senha);
            DataResponse<Funcionario> response = new DataResponse<Funcionario>();
            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    response.Data = db.Funcionarios.Where(f => f.Email == email && f.Senha == senha).ToList();
                }
                if (response.Data.Count > 0)
                {
                    response.Sucesso = true;
                }
                else
                {
                    response.Sucesso = false;
                    response.Erros.Add("Login ou senha inválidos.");
                }
            }
            catch(Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados, contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            if (response.Sucesso)
            {
                User.FuncionarioLogado = response.Data[0];
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
                    Funcionario funcionarioASerExcluido = new Funcionario();
                    funcionarioASerExcluido.ID = id;
                    db.Entry<Funcionario>(funcionarioASerExcluido).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                response.Sucesso = true;
            }
            catch(Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados,contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return response;
        }

        public DataResponse<Funcionario> GetByID(int id)
        {
            List<Funcionario> f = new List<Funcionario>();
            DataResponse<Funcionario> response = new DataResponse<Funcionario>();

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
                    f.Add(db.Funcionarios.Find(id));
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados,contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }
            response.Data = f;
            return response;
        }

        public DataResponse<Funcionario> GetData()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            DataResponse<Funcionario> response = new DataResponse<Funcionario>();

            try
            {
                using(LocadoraDbContext db = new LocadoraDbContext())
                {
                    funcionarios = db.Funcionarios.ToList();
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados,contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }
            response.Data = funcionarios;
            return response;
        }

        public DataResponse<FuncionarioResultSet> GetFuncionarios()
        {
            DataResponse<FuncionarioResultSet> response = new Entities.DataResponse<FuncionarioResultSet>();
            List<FuncionarioResultSet> funcionarios = new List<FuncionarioResultSet>();

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    funcionarios = db.Funcionarios.Select(f => new FuncionarioResultSet(){ ID = f.ID, Nome = f.Nome, CPF = f.CPF, DataNascimento = f.DataNascimento, Email = f.Email, Telefone = f.Telefone, EhAtivo = f.EhAtivo }).ToList();
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados,contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }
            response.Data = funcionarios;
            return response;

        }

        public Response Insert(Funcionario item)
        {
            Response response = Validate(item);

            if (response.HasErrors())
            {
                response.Sucesso = false;
                return response;
            }

            item.EhAtivo = true;
            item.Senha = HashUtils.HashPassword(item.Senha);

            try
            {
                using (LocadoraDbContext db = new LocadoraDbContext())
                {
                    db.Funcionarios.Add(item);
                    db.SaveChanges();
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados,contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return response;

            //return funcionarioDAL.Insert(item);
        }

        public Response Update(Funcionario item)
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
                    Funcionario funcionarioASerAtualizado = new Funcionario();
                    funcionarioASerAtualizado = item;
                    db.Entry<Funcionario>(funcionarioASerAtualizado).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                response.Sucesso = true;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Erros.Add("Erro no banco de dados,contate o administrador.");
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
            }

            return response;
        }

        private Response Validate(Funcionario item)
        {
            Response response = new Response();

            if (string.IsNullOrWhiteSpace(item.CPF))
            {
                response.Erros.Add("O cpf deve ser informado");
            }
            else
            {
                item.CPF = item.CPF.Trim();
                if (!item.CPF.IsCpf())
                {
                    response.Erros.Add("O cpf informado é inválido.");
                }
            }

            string validacaoSenha = SenhaValidator.ValidateSenha(item.Senha, item.DataNascimento);
            if (validacaoSenha != "")
            {
                response.Erros.Add(validacaoSenha);
            }

            return response;
        }
    }
}
