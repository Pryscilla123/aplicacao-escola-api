using AplicacaoEscola.Data;
using AplicacaoEscola.Models;
using Dapper;
using System.Data;

namespace AplicacaoEscola.Repository
{
    public class MateriaRepository : IMateriaRepository
    {
        private DbSession _db; 

        public MateriaRepository(DbSession dbSession) 
        {
            _db = dbSession;
        }

        public async Task<Materia> GetMateriaByIdAsync(int id)
        {
            using(var conn = _db.Connection)
            {
                string query = @"
                    SELECT 
                    id as Id,
                    nome as Nome,
                    professor_id as ProfessorId,
                    periodo as Periodo,
                    numero_vagas_total as NumeroVagasTotal,
                    numero_vagas_atual as NumeroVagasAtual
                    FROM materias WHERE id = @id
                    ";

                var parametros = new DynamicParameters();
                parametros.Add("@id", id, DbType.Int16);

                Materia materia = await conn.QueryFirstOrDefaultAsync<Materia>(query);

                return materia;
            }
        }

        public async Task<IEnumerable<Materia>> GetMeteriasAsync()
        {
            using (var conn = _db.Connection)
            {
                string query = @"
                    SELECT 
                    id as Id,
                    nome as Nome,
                    professor_id as ProfessorId,
                    periodo as Periodo,
                    numero_vagas_total as NumeroVagasTotal,
                    numero_vagas_atual as NumeroVagasAtual
                    FROM materias
                    ";

                IEnumerable<Materia> materias = (await conn.QueryAsync<Materia>(sql:query)).ToList();

                return materias;
            }
        }

        public async Task<int> SaveMateriaAsync(Materia materia)
        {
            using (var conn = _db.Connection)
            {
                string query = @"
                    INSERT INTO materias (nome, periodo, numero_vagas_total, numero_vagas_atual) VALUES (@nome, @periodo, @numero_vagas_total, @numero_vagas_atual);
                ";

                var parametros = new DynamicParameters();
                parametros.Add("@nome", materia.Nome, DbType.AnsiString, size: 80);
                parametros.Add("@periodo", materia.Periodo, DbType.Int16);
                parametros.Add("@numero_vagas_total", materia.NumeroVagasTotal, DbType.Int16);
                parametros.Add("@numero_vagas_atual", materia.NumeroVagasTotal, DbType.Int16);

                int result = await conn.ExecuteAsync(query, parametros);

                return result;
            }
        }

        public async Task<int> UpdateMateriaAsync(int id, Materia materia)
        {
            using (var conn = _db.Connection)
            {
                string query = @"
                    UPDATE materias SET nome=@nome, periodo=@periodo, numero_vagas_total=@numero_vagas_total
                    WHERE id=@id;
                ";

                var parametros = new DynamicParameters();
                parametros.Add("@nome", materia.Nome, DbType.AnsiString, size: 80);
                parametros.Add("@periodo", materia.Periodo, DbType.Int16);
                parametros.Add("@numero_vagas_total", materia.NumeroVagasTotal, DbType.Int16);
                parametros.Add("@id", id, DbType.Int16);

                int result = await conn.ExecuteAsync(query, parametros);

                return result;
            }
        }

        public async Task<int> UpdateMateriaProfessorAsync(int materiaId, int professorId)
        {
            using(var conn = _db.Connection)
            {
                string query = @"
                UPDATE materias SET professor_id=@professor_id
                WHERE id=@id
                ";

                var parametros = new DynamicParameters();
                parametros.Add("@id", materiaId, DbType.Int16);
                parametros.Add("@professor_id", professorId, DbType.Int16);

                int result = await conn.ExecuteAsync(query,parametros);

                return result;
            }
        }
        public async Task<int> DeleteMateriaAsync(int id)
        {
            using(var conn = _db.Connection)
            {
                string query = "DELETE materias WHERE id=@id";

                var parametros = new DynamicParameters();
                parametros.Add("@id", id, DbType.Int16);

                int result = await conn.ExecuteAsync(query,parametros);

                return result;
            }
        }
    }
}
