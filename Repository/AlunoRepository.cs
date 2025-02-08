using AplicacaoEscola.Data;
using AplicacaoEscola.Models;
using Dapper;
using System.Data;

namespace AplicacaoEscola.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private DbSession _db;

        public AlunoRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<IEnumerable<Aluno>> GetAlunosAsync()
        {
            try
            {
                string query = "SELECT * FROM Alunos";
                List<Aluno> alunos = (await _db.Connection.QueryAsync<Aluno>(sql: query)).ToList();

                return alunos;
            } 
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
               throw;
            }
        }

        public async Task<Aluno> GetAlunoByIdAsync(int id)
        {
            try
            {
                string query = "SELECT * FROM Alunos WHERE id = @id";
                var parametros = new DynamicParameters();
                parametros.Add("@id", id, DbType.Int32);

                Aluno aluno = await _db.Connection.QueryFirstOrDefaultAsync<Aluno>(query, parametros);

                return aluno;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<int> SaveAlunoAsync(Aluno aluno)
        {
            try
            {
                string query = "INSERT INTO Alunos(nome, idade, periodo) VALUES (@nome, @idade, @periodo)";
                var parametros = new DynamicParameters();
                parametros.Add("@nome", aluno.Nome, DbType.AnsiString);
                parametros.Add("@idade", aluno.Idade, DbType.Int16);
                parametros.Add("@periodo", aluno.Periodo, DbType.Int16);

                var result = await _db.Connection.ExecuteAsync(query, parametros);
                return result;
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<int> UpdateAlunoAsync(int id, Aluno aluno)
        {
            using(var conn = _db.Connection)
            {
                string query = @"
                    UPDATE alunos SET nome = @nome, idade = @idade, periodo = @periodo
                    WHERE id = @id
                    ";

                var parametros = new DynamicParameters();
                parametros.Add("@id", id);
                parametros.Add("@nome", aluno.Nome, DbType.AnsiString);
                parametros.Add("@idade", aluno.Idade, DbType.Int16);
                parametros.Add("@periodo", aluno.Periodo, DbType.Int16);

                var result = await conn.ExecuteAsync(query, parametros); 
                return result;
            }
        }

        public async Task<int> DeleteAlunoAsync(int id)
        {
            try
            {
                string query = @"DELETE FROM Alunos WHERE id = @id";
                var parametros = new DynamicParameters();
                parametros.Add("@id", id, DbType.Int32);

                var result = await _db.Connection.ExecuteAsync(query, parametros);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
