--CREACION DE LA BASE DE DATOS
CREATE DATABASE SoftOne

USE DATABASE SoftOne

CREATE TABLE USUARIOS(
UsuaioID int identity(1,1)primary key,
LoginName nvarchar(100)unique not null,
password nvarchar(100)not null,
FirstName nvarchar(100)not null,
LastName nvarchar(100)not null,
position nvarchar(100)not null,
Email nvarchar(150)not null
)

insert into USUARIOS(LoginName,password,FirstName,LastName,position,Email) values('Admin','Admin','Rivera','Neri','Administrador','adolforivera@gmail.com');
insert into USUARIOS(LoginName,password,FirstName,LastName,position,Email) values('Root','Root','Hernandez','Garcia','Administrador','HernadezGarcia@gmail.com');

select * from USUARIOS

--PROCEDIMIETO ALAMACENADO INGRESAR USUARIO
CREATE PROCEDURE Ins_User(
@LoginName nvarchar(100),
@password nvarchar(100),
@FirstName nvarchar(100),
@LastName nvarchar(100),
@position nvarchar(100),
@Email nvarchar(150)
)
AS
BEGIN
insert into USUARIOS(LoginName,password,FirstName,LastName,position,Email)values
(@LoginName,@password,@FirstName,@LastName,@position,@Email);
END
GO

execute Ins_User 'Adolfo','12345678','Rivera','Neri','Administrador','Adolfo001@gmail.com';

DELETE FROM USUARIOS WHERE LoginName='';







