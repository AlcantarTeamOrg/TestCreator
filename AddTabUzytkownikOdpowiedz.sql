USE [TestCreator]
GO

/****** Object:  Table [dbo].[UzytkownikOdpowiedz]    Script Date: 2018-06-17 15:41:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UzytkownikOdpowiedz](
	[id] [int] NOT NULL,
	[id_uzytkownik] [int] NOT NULL,
	[odpowiedz] [nvarchar](max) NOT NULL,
	[id_testy] [int] NOT NULL,
 CONSTRAINT [PK_UzytkownikOdpowiedz] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


