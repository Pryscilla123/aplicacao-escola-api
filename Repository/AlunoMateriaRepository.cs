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
            using (var conn = _db.Connection)
            {
                string removeAlunoMateria = @"
                    DELETE FROM aluno_materia WHERE aluno_id=@aluno_id AND materia_id=@materia_id
                ";

                var parametros = new DynamicParameters();
                parametros.Add("@aluno_id", alunoId, DbType.Int16);
                parametros.Add("@materia_id", materiaId, DbType.Int16);

                var result = await conn.ExecuteAsync(removeAlunoMateria, parametros);

                if (result == 0) return result;

                string diminuiNumeroVagasMateriaQuery = @"UPDATE materias SET numero_vagas_atual=numero_vagas_atual+1 WHERE id=@materia_id";

                result = await conn.ExecuteAsync(diminuiNumeroVagasMateriaQuery, parametros);

                return result;
            }
        }

        public async Task<IEnumerable<AlunoMateria>> GetAlunoMaterias(int id)
        {
            using (var conn = _db.Connection)
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

                IEnumerable<AlunoMateria> alunoMaterias = (await conn.QueryAsync<AlunoMateria>(query, parametros)).ToList();

                return alunoMaterias;
            }
        }

        public async Task<IEnumerable<AlunoMateria>> GetAlunosMaterias()
        {
            using (var conn = _db.Connection)
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
                    ";

                IEnumerable<AlunoMateria> alunosMaterias = (await conn.QueryAsync<AlunoMateria>(query)).ToList();

                return alunosMaterias;
            }
        }

        public async Task<int> SaveAlunoMateriaAsync(int alunoId, int materiaId)
        {
            using(var conn = _db.Connection)
            {
                string addAlunoMateria = @"
                    INSERT INTO aluno_materia (aluno_id, materia_id) VALUES (@aluno_id, @materia_id)
                ";

                var parametros = new DynamicParameters();
                parametros.Add("@aluno_id", alunoId, DbType.Int16);
                parametros.Add("@materia_id", materiaId, DbType.Int16);

                var result = await conn.ExecuteAsync(addAlunoMateria, parametros);

                if (result == 0) return result;

                string diminuiNumeroVagasMateriaQuery = @"UPDATE materias SET numero_vagas_atual=numero_vagas_atual-1 WHERE id=@materia_id";
                
                result = await conn.ExecuteAsync(diminuiNumeroVagasMateriaQuery, parametros);

                return result;
            }
        }
    }
}
