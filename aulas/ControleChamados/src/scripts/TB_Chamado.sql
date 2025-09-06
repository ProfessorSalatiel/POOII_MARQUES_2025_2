USE [SuporteUnip]
GO

/****** Object:  Table [dbo].[TB_Chamado]    Script Date: 06/09/2025 10:07:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_Chamado](
	[id_chamado] [bigint] IDENTITY(1,1) NOT NULL,
	[desc_chamado] [varchar](50) NULL,
	[status_chamado] [bit] NULL,
	[dt_criacao_chamado] [datetime] NULL,
 CONSTRAINT [PK_TB_Chamado] PRIMARY KEY CLUSTERED 
(
	[id_chamado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


