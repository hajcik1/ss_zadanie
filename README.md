# Instrukcja uruchomienia


1. Pobierz projekt.
2. Na serwerze MSSQL utwórz nową bazę danych i wykonaj w niej poniższy skrypt:
```bash
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Filmy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Director] [varchar](50) NOT NULL,
	[Year] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```
3. W folderze API zmień connection string do nowo utworzonej bazy MSSQL w pliku appsettings.json.
4. Otwórz projekt MojeFilmyAPI.sln w Visual Studio i uruchom aplikację.
5. Otwórz folder WEB w Visual Studio i wykonaj polecenie:
```bash
npm install
```
a następnie uruchom aplikację poleceniem:
```bash
npm run dev
```
6. Możesz zacząc korzystać z aplikacji.
