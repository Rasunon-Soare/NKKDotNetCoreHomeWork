Tbl_Book
```sql
CREATE TABLE [dbo].[Tbl_Book](
	[Book_Id] [int] IDENTITY(1,1) NOT NULL,
	[Book_Title] [nvarchar](50) NULL,
	[Book_Author] [nvarchar](50) NULL,
	[Book_Category] [nvarchar](30) NULL,
	[Book_Content] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Book_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```
