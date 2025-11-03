USE persona_db;
GO

/* =========================================================
   1) Inserción de datos en la tabla persona
   ========================================================= */
IF NOT EXISTS (SELECT * FROM arq_per_db.persona WHERE cc = 11111111)
INSERT INTO arq_per_db.persona (cc, nombre, apellido, genero, edad)
VALUES (11111111, 'Sofia', 'Ramirez', 'F', 26);

IF NOT EXISTS (SELECT * FROM arq_per_db.persona WHERE cc = 22222222)
INSERT INTO arq_per_db.persona (cc, nombre, apellido, genero, edad)
VALUES (22222222, 'Mateo', 'Cardenas', 'M', 31);

IF NOT EXISTS (SELECT * FROM arq_per_db.persona WHERE cc = 33333333)
INSERT INTO arq_per_db.persona (cc, nombre, apellido, genero, edad)
VALUES (33333333, 'Valentina', 'Gomez', 'F', 29);

IF NOT EXISTS (SELECT * FROM arq_per_db.persona WHERE cc = 44444444)
INSERT INTO arq_per_db.persona (cc, nombre, apellido, genero, edad)
VALUES (44444444, 'Andres', 'Morales', 'M', 35);
GO


/* =========================================================
   2) Inserción de datos en la tabla profesión
   ========================================================= */
IF NOT EXISTS (SELECT * FROM arq_per_db.profesion WHERE id = 1)
INSERT INTO arq_per_db.profesion (id, nom, des)
VALUES (1, 'Arquitecto', 'Diseño y planificacion de espacios habitables.');

IF NOT EXISTS (SELECT * FROM arq_per_db.profesion WHERE id = 2)
INSERT INTO arq_per_db.profesion (id, nom, des)
VALUES (2, 'Enfermero', 'Profesional del area de salud encargado del cuidado de pacientes.');

IF NOT EXISTS (SELECT * FROM arq_per_db.profesion WHERE id = 3)
INSERT INTO arq_per_db.profesion (id, nom, des)
VALUES (3, 'Economista', 'Especialista en analisis financiero y gestión económica.');

IF NOT EXISTS (SELECT * FROM arq_per_db.profesion WHERE id = 4)
INSERT INTO arq_per_db.profesion (id, nom, des)
VALUES (4, 'Docente', 'Encargado de la formacian acadamica en instituciones educativas.');
GO


/* =========================================================
   3) Inserción de datos en la tabla estudios
   ========================================================= */
IF NOT EXISTS (SELECT * FROM arq_per_db.estudios WHERE id_prof = 1 AND cc_per = 11111111)
INSERT INTO arq_per_db.estudios (id_prof, cc_per, fecha, univer)
VALUES (1, 11111111, '2016-08-15', 'Universidad de los Andes');

IF NOT EXISTS (SELECT * FROM arq_per_db.estudios WHERE id_prof = 2 AND cc_per = 22222222)
INSERT INTO arq_per_db.estudios (id_prof, cc_per, fecha, univer)
VALUES (2, 22222222, '2014-11-20', 'Universidad del Rosario');

IF NOT EXISTS (SELECT * FROM arq_per_db.estudios WHERE id_prof = 3 AND cc_per = 33333333)
INSERT INTO arq_per_db.estudios (id_prof, cc_per, fecha, univer)
VALUES (3, 33333333, '2012-05-10', 'Pontificia Universidad Javeriana');

IF NOT EXISTS (SELECT * FROM arq_per_db.estudios WHERE id_prof = 4 AND cc_per = 44444444)
INSERT INTO arq_per_db.estudios (id_prof, cc_per, fecha, univer)
VALUES (4, 44444444, '2018-03-25', 'Universidad Nacional de Colombia');
GO


/* =========================================================
   4) Inserción de datos en la tabla teléfono
   ========================================================= */
IF NOT EXISTS (SELECT * FROM arq_per_db.telefono WHERE num = '3101112233')
INSERT INTO arq_per_db.telefono (num, oper, duenio)
VALUES ('3101112233', 'Claro', 11111111);

IF NOT EXISTS (SELECT * FROM arq_per_db.telefono WHERE num = '3152223344')
INSERT INTO arq_per_db.telefono (num, oper, duenio)
VALUES ('3152223344', 'Tigo', 22222222);

IF NOT EXISTS (SELECT * FROM arq_per_db.telefono WHERE num = '3203334455')
INSERT INTO arq_per_db.telefono (num, oper, duenio)
VALUES ('3203334455', 'Movistar', 33333333);

IF NOT EXISTS (SELECT * FROM arq_per_db.telefono WHERE num = '3214445566')
INSERT INTO arq_per_db.telefono (num, oper, duenio)
VALUES ('3214445566', 'WOM', 44444444);
GO
