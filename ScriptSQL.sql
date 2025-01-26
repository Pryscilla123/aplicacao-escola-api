CREATE DATABASE Escola;

CREATE TABLE alunos
(
    id int PRIMARY KEY IDENTITY,
    nome VARCHAR(80) NOT NULL,
    idade TINYINT
);

CREATE TABLE professores
(
    id int PRIMARY KEY IDENTITY,
    nome VARCHAR(80) NOT NULL,
    idade TINYINT
);

CREATE TABLE materias
(
    id int PRIMARY KEY IDENTITY,
    nome VARCHAR(80) NOT NULL,
    professor_id INT DEFAULT NULL,
    periodo INT,
    numero_vagas_total INT NOT NULL,
    numero_vagas_atual INT DEFAULT 0,
    CONSTRAINT fk_professor_id FOREIGN KEY (professor_id) REFERENCES professores(id) 
);

CREATE TABLE aluno_materia
(
    aluno_id int NOT NULL,
    materia_id int NOT NULL,
    ano int not null DEFAULT cast(YEAR(GETDATE()) as int),
    CONSTRAINT pk_alunos_materias PRIMARY KEY (aluno_id, materia_id),
    CONSTRAINT fk_aluno_id FOREIGN KEY (aluno_id) REFERENCES alunos(id),
    CONSTRAINT fk_materia_id FOREIGN KEY (materia_id) REFERENCES materias(id)
);


SELECT * FROM materias; 

SELECT * FROM professores;

UPDATE materias SET professor_id=1 WHERE id=1;
UPDATE materias SET professor_id=2 WHERE id=2;

SELECT * FROM alunos;

INSERT INTO aluno_materia (aluno_id, materia_id) VALUES (1, 1);
INSERT INTO aluno_materia (aluno_id, materia_id) VALUES (2, 1);

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

UPDATE materias SET numero_vagas_atual=23 where id = 1;
UPDATE materias SET numero_vagas_atual=25 where id = 2;