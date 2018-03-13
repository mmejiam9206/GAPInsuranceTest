USE [GAPInsurancesDB]
GO

/****** Object:  Table [dbo].[tblClientPolicies]    Script Date: 3/13/2018 1:20:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS(
	SELECT
		1
	FROM
		INFORMATION_SCHEMA.TABLES
	WHERE
		TABLE_NAME = 'tblClientPolicies'
) 
BEGIN
	CREATE TABLE [dbo].[tblClientPolicies](
		[client_policy_id] [int] IDENTITY(1,1) NOT NULL,
		[client_id] [int] NOT NULL,
		[policy_id] [int] NOT NULL,
	 CONSTRAINT [PK_tblClientPolicies] PRIMARY KEY CLUSTERED 
	(
		[client_policy_id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	GO

	ALTER TABLE [dbo].[tblClientPolicies]  WITH CHECK ADD  CONSTRAINT [FK_tblClientPolicies_tblClients] FOREIGN KEY([client_id])
	REFERENCES [dbo].[tblClients] ([client_id])
	GO

	ALTER TABLE [dbo].[tblClientPolicies] CHECK CONSTRAINT [FK_tblClientPolicies_tblClients]
	GO

	ALTER TABLE [dbo].[tblClientPolicies]  WITH CHECK ADD  CONSTRAINT [FK_tblClientPolicies_tblPolicies] FOREIGN KEY([policy_id])
	REFERENCES [dbo].[tblPolicies] ([policy_id])
	GO

	ALTER TABLE [dbo].[tblClientPolicies] CHECK CONSTRAINT [FK_tblClientPolicies_tblPolicies]
	GO
END



