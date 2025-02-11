using AplicacaoEscola.Data;
using AplicacaoEscola.Models;
using Dapper;
using System.Data;

namespace AplicacaoEscola.Repository
{
    public class AlunoMateriaRepository : IAlunoMateriaRepository
    {
        private DbSession _db;

        public AlunoMateriaRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> DeleteAlunoMateriaAsync(int alunoId, int materiaId)
        {
            try
            {
                string removeAlunoMateria = @"
                    DELETE FROM aluno_materia WHERE aluno_id=@aluno_id AND materia_id=@materia_id
                ";

                var parametros = new DynamicParameters();
                parametros.Add("@aluno_id", alunoId, DbType.Int16);
                parametros.Add("@materia_id", materiaId, DbType.Int16);

                var result = await _db.Connection.ExecuteAsync(removeAlunoMateria, parametros);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<AlunoMateria>> GetAlunoMaterias(int id)
        {
            try
            {
                string query = @"
                    SELECT 
                    a.nome as NomeAluno,
                    m.nome as NomeMateria,
                    p.nome as NomeProfessor,
                    m.periodo as Periodo,
                    am.ano as Ano
                    FROM aluno_materia as am
                    INNER JOIN alunos as a on a.id=am.aluno_id
                    INNER JOIN materias as m on m.id=am.materia_id
                    INNER JOIN professores as p on p.id=m.professor_id
                    WHERE a.id=@id
                ";

                var parametros = new DynamicParameters();
                parametros.Add("@id", id, DbType.Int16);

                IEnumerable<AlunoMateria> alunoMaterias = (await _db.Connection.QueryAsync<AlunoMateria>(query, parametros)).ToList();

                return alunoMaterias;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<AlunoMateria>> GetAlunosMaterias()
        {
            try
            {
                string query = @"
                    SELECT 
                    a.nome as NomeAluno,
                    m.nome as NomeMateria,
                    p.nome as NomeProfessor,
                    m.periodo as Periodo,
                    am.ano as Ano
                    FROM aluno_materia as am
                    INNER JOIN alunos as a on a.id=am.aluno_id
                    INNER JOIN materias as m on m.id=am.materia_id
                    LEFT JOIN professores as p on p.id=m.professor_id
                    ";

                IEnumerable<AlunoMateria> alunosMaterias = (await _db.Connection.QueryAsync<AlunoMateria>(query)).ToList();

                return alunosMaterias;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<int> GetCountAlunosByMateria(int materiaId)
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) FROM aluno_materia 
                    WHERE materia_id = @materia_id
                    ";

                var parametros = new DynamicParameters();
                parametros.Add("@materia_id", materiaId, DbType.Int16);

                int result = await _db.Connection.QueryFirstOrDefaultAsync<int>(query, parametros);

                return 0;
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<int> SaveAlunoMateriaAsync(int alunoId, int materiaId)
        {
            try
            {
                string addAlunoMateria = @"
                    INSERT INTO aluno_materia (aluno_id, materia_id) VALUES (@aluno_id, @materia_id)
                ";

                var parametros = new DynamicParameters();
                parametros.Add("@aluno_id", alunoId, DbType.Int16);
                parametros.Add("@materia_id", materiaId, DbType.Int16);

                var result = await _db.Connection.ExecuteAsync(addAlunoMateria, parametros);

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
