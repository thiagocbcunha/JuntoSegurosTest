USE [JuntoSegurosOnboarding]
GO

INSERT INTO [dbo].[Gender]
   ([Name] ,[Description], [CreateBy] ,[CreateDate])
VALUES
	('Feminino',NULL, 'Initial Data', GETDATE()),
	('Masculino',NULL, 'Initial Data', GETDATE()),
	('Outros',NULL, 'Initial Data', GETDATE())
GO

INSERT INTO [dbo].[PhoneType]
	([Name],[Description],[Active],[CreateBy],[CreateDate])
VALUES
	('Comercial', NULL, 1, 'Initial Data', GETDATE()),
	('Pessoal', NULL, 1, 'Initial Data', GETDATE())
GO

