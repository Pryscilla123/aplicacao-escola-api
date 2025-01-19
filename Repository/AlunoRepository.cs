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
            using(var conn = _db.Connection)
            {
                string query = "SELECT * FROM Alunos";
                List<Aluno> alunos = (await conn.QueryAsync<Aluno>(sql: query)).ToList();
                return alunos;
            }
        }

        public async Task<Aluno> GetAlunoByIdAsync(int id)
        {
            using(var conn = _db.Connection)
            {
                string query = "SELECT * FROM Alunos WHERE id = @id";
                var parametros = new DynamicParameters();
                parametros.Add("@id", id, DbType.Int32);

                Aluno aluno = await conn.QueryFirstOrDefaultAsync<Aluno>(query, parametros);

                return aluno;
            }
        }

        public async Task<int> SaveAlunoAsync(Aluno aluno)
        {
            using(var conn = _db.Connection)
            {
                string query = "INSERT INTO Alunos(nome, idade) VALUES (@nome, @idade)";
                var parametros = new DynamicParameters();
                parametros.Add("@nome", aluno.Nome, DbType.AnsiString);
                parametros.Add("@idade", aluno.Idade, DbType.Int16);

                var result = await conn.ExecuteAsync(query, parametros);
                return result;
            }
        }

        public async Task<int> UpdateAlunoAsync(int id, Aluno aluno)
        {
            using(var conn = _db.Connection)
            {
                string query = @"
                    UPDATE alunos SET nome = @nome, idade = @idade
                    WHERE id = @id
                    ";

                var parametros = new DynamicParameters();
                parametros.Add("@id", id);
                parametros.Add("@nome", aluno.Nome, DbType.AnsiString);
                parametros.Add("@idade", aluno.Idade, DbType.Int16);

                var result = await conn.ExecuteAsync(query, parametros); 
                return result;
            }
        }

        public async Task<int> DeleteAlunoAsync(int id)
        {
            using(var conn = _db.Connection)
            {
                string query = @"DELETE FROM Alunos WHERE id = @id";
                var parametros = new DynamicParameters();
                parametros.Add("@id", id, DbType.Int32);

                var result = await conn.ExecuteAsync(query, parametros);
                return result;
            }
        }
    }
}
