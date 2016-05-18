-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE P_K2_GetProcessOriginalXMLValue
	-- Add the parameters for the stored procedure here
	@SrcProcInstID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    declare @xmlProcessApprovalChain xml='';
	SELECT @xmlProcessApprovalChain=[Value]
			FROM [K2ServerLog].[dbo].[_ProcInstXml] where procinstid=@SrcProcInstID and name='ProcessApprovalChain'

	select @xmlProcessApprovalChain ProcessApprovalChain
END
GO
