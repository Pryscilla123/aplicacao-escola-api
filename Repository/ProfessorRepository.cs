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
            try
            {
                string query = @"SELECT * FROM Professores";

                IEnumerable<Professor> professores = (await _db.Connection.QueryAsync<Professor>(sql: query)).ToList();

                return professores;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Professor> GetProfessorAsync(int id)
        {
            try
            {
                string query = @"SELECT * FROM Professores WHERE id = @id";

                var parametros = new DynamicParameters();
                parametros.Add("@id", id);

                Professor professor = await _db.Connection.QueryFirstOrDefaultAsync<Professor>(query, parametros);

                return professor;
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<int> SaveProfessorAsync(Professor professor)
        {
            try
            {
                string query = @"INSERT INTO Professores(nome, idade) VALUES (@nome, @idade)";

                var parametros = new DynamicParameters();
                parametros.Add("@nome", professor.Nome);
                parametros.Add("@idade", professor.Idade);

                var result = await _db.Connection.ExecuteAsync(query, parametros);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                throw;
            }
        }

        public async Task<int> UpdateProfessorAsync(int id, Professor professor)
        {
            try
            {
                string query = @"
                UPDATE Professores SET nome = @nome, idade = @idade
                WHERE id = @id
                ";

                var parametros = new DynamicParameters();
                parametros.Add("@nome", professor.Nome);
                parametros.Add("@idade", professor.Idade);
                parametros.Add("@id", id);

                var result = await _db.Connection.ExecuteAsync(query, parametros);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<int> DeleteProfessorAsync(int id)
        {
            try
            {
                string query = @"DELETE FROM Professores WHERE @id = id";

                var parametros = new DynamicParameters();
                parametros.Add("@id", id);

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
