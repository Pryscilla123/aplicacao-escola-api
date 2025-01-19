using AplicacaoEscola.Data;
using AplicacaoEscola.Models;
using Dapper;

namespace AplicacaoEscola.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {
        private DbSession _db;

        public ProfessorRepository(DbSession dbSession) 
        {
            _db = dbSession;
        }

        public async Task<IEnumerable<Professor>> GetProfessoresAsync()
        {
            using(var conn = _db.Connection)
            {
                string query = @"SELECT * FROM Professores";

                IEnumerable<Professor> professores = (await conn.QueryAsync<Professor>(sql: query)).ToList();

                return professores;
            }
        }

        public async Task<Professor> GetProfessorAsync(int id)
        {
            using(var conn = _db.Connection)
            {
                string query = @"SELECT * FROM Professores WHERE id = @id";

                var parametros = new DynamicParameters();
                parametros.Add("@id", id);

                Professor professor = await conn.QueryFirstOrDefaultAsync<Professor>(query, parametros);

                return professor;
            }
        }

        public async Task<int> SaveProfessorAsync(Professor professor)
        {
            using(var conn = _db.Connection)
            {
                string query = @"INSERT INTO Professores(nome, idade) VALUES (@nome, @idade)";

                var parametros = new DynamicParameters();
                parametros.Add("@nome", professor.Nome);
                parametros.Add("@idade", professor.Idade);

                var result = await conn.ExecuteAsync(query, parametros);

                return result;
            }
        }

        public async Task<int> UpdateProfessorAsync(int id, Professor professor)
        {
            using(var conn = _db.Connection)
            {
                string query = @"
                UPDATE Professores SET nome = @nome, idade = @idade
                WHERE id = @id
                ";

                var parametros = new DynamicParameters();
                parametros.Add("@nome", professor.Nome);
                parametros.Add("@idade", professor.Idade);
                parametros.Add("@id", id);

                var result = await conn.ExecuteAsync(query, parametros);

                return result;
            }

        }

        public async Task<int> DeleteProfessorAsync(int id)
        {
            using (var conn = _db.Connection)
            {
                string query = @"DELETE FROM Professores WHERE @id = id";

                var parametros = new DynamicParameters();
                parametros.Add("@id", id);

                var result = await conn.ExecuteAsync(query, parametros);

                return result;
            }

        }
    }
}
