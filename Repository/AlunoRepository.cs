using AplicacaoEscola.Data;
using AplicacaoEscola.Models;
using Dapper;

namespace AplicacaoEscola.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private DbSession _db;

        public AlunoRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<List<Aluno>> GetAlunosAsync()
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
                parametros.Add("@id", id);

                Aluno aluno = await conn.QueryFirstOrDefaultAsync<Aluno>(query, parametros);

                return aluno;
            }
        }

        public async Task<int> SaveAsync(Aluno aluno)
        {
            using(var conn = _db.Connection)
            {
                string query = "INSERT INTO Alunos(nome, idade) VALUES (@nome, @idade)";
                var parametros = new DynamicParameters();
                parametros.Add("@nome", aluno.Nome);
                parametros.Add("@idade", aluno.Idade);

                var result = await conn.ExecuteAsync(query, parametros);
                return result;
            }
        }
    }
}
