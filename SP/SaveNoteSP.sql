USE [School]
GO

/****** Object:  StoredProcedure [dbo].[SaveNote]    Script Date: 29/10/2019 22:19:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveNote]
	@type numeric ,
	@createdBy nvarchar(50),
	@teacherID numeric,
	@classID numeric,
    @studentID numeric ,
	@description nvarchar(50),
	@noteDate datetime
AS
insert into Note  (
      [Type]
      ,[Created by]
      ,[Created Date]
      ,[TeacherID]
      ,[ClassID]
      ,[StudentID]
      ,[Description]
      ,[Note_Date]) values (@type,@createdBy,GETDATE(),@teacherID,@classID,@studentID,@description,@noteDate)

GO


