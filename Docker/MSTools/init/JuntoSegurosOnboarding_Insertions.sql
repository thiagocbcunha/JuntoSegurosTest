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

INSERT INTO[dbo].[Person] ([Id], [Name], [BirthDate]) OUTPUT INSERTED.Id VALUES ('BDC873D7-DC10-4CCD-937D-68BBF9BF4861', 'INITIAL TESTE', '1988-09-11')
INSERT INTO [dbo].[PersonEvent] ([PersonId] ,[VersionNum] ,[GenderId], [CreateBy], [CreateDate]) VALUES ('BDC873D7-DC10-4CCD-937D-68BBF9BF4861', 1, 1, 'INITIAL TESTE', getdate())
