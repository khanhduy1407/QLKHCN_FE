USE [QLKHCN_Data_Api_9]
GO
/****** Object:  Table [dbo].[DanhMucXetDuyet]    Script Date: 09/11/2024 9:56:59 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhMucXetDuyetSinhVien](
	[IDDanhMuc] [int] IDENTITY(1,1) NOT NULL,
	[userId] [nvarchar](1000) NULL,
	[tenBaiBao] [nvarchar](1000) NULL,
	[moTaBaiBao] [nvarchar](1000) NULL,
	[thoiGianThucHien] [int] NULL,
	[groupUser] [nvarchar](4000) NULL,
	[quantity] [nvarchar](1000) NULL,
	[category] [nvarchar](1000) NULL,
	[kinhPhi] [bigint] NULL,
	[ghiChu] [nvarchar](2000) NULL,
	[jci] [nvarchar](2000) NULL,
	[dateSubmit] [datetime2](7) NULL,
	[dateSubmit_now] [datetime2](7) NULL,
	[fileDangKy] [nvarchar](max) NULL,
	[fileThuyetMinh] [nvarchar](max) NULL,
	[fileDinhKemLyLich] [nvarchar](max) NULL,
	[status] [nvarchar](1000) NULL,
	[fileStatus] [varchar](3) NULL DEFAULT '000', -- 3 số 0 tương ứng với trạng thái kiểm duyệt của 3 file
	[review1] [nvarchar](1000) NULL,
	[review2] [nvarchar](1000) NULL,
	[rejectBy] [nvarchar](1000) NULL,
	[completedBy] [nvarchar](1000) NULL,
 CONSTRAINT [PK_DanhMucXetDuyetSinhVien] PRIMARY KEY CLUSTERED 
(
	[IDDanhMuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
