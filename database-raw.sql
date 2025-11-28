
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_MoTaCSDL] AS
SELECT 
    c.TABLE_NAME,
    c.COLUMN_NAME,
    c.DATA_TYPE,
    c.CHARACTER_MAXIMUM_LENGTH,
    c.IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS c;
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 11/15/2025 12:02:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[MaCT] [int] IDENTITY(1,1) NOT NULL,
	[MaHD] [int] NOT NULL,
	[DanhMuc] [nvarchar](max) NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [decimal](18, 0) NOT NULL,
	[ThanhTien]  AS ([DonGia]*[SoLuong]),
 CONSTRAINT [PK_ChiTietHoaDon_1] PRIMARY KEY CLUSTERED 
(
	[MaCT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DatPhong]    Script Date: 11/15/2025 12:02:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatPhong](
	[MaDat] [int] IDENTITY(1,1) NOT NULL,
	[MaKH] [int] NOT NULL,
	[MaPhong] [int] NOT NULL,
	[NgayNhan] [datetime2](0) NOT NULL,
	[NgayTraDuKien] [datetime2](0) NOT NULL,
	[NgayTraThucTe] [datetime2](0) NULL,
	[TienCoc] [decimal](18, 0) NOT NULL,
	[TienThue] [decimal](18, 0) NOT NULL,
	[TrangThai] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DatPhong] PRIMARY KEY CLUSTERED 
(
	[MaDat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 11/15/2025 12:02:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHD] [int] IDENTITY(1,1) NOT NULL,
	[MaDat] [int] NOT NULL,
	[MaNV] [int] NULL,
	[NgayLap] [datetime2](0) NOT NULL,
	[LoaiHoaDon] [nvarchar](40) NOT NULL,
	[TongThanhToan] [decimal](18, 0) NULL,
	[GhiChu] [nvarchar](max) NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 11/15/2025 12:02:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](100) NOT NULL,
	[CCCD] [varchar](15) NOT NULL,
	[SDT] [varchar](15) NULL,
	[NgayThamGia] [datetime] NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichSuHoaDon]    Script Date: 11/15/2025 12:02:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichSuHoaDon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaHD] [int] NULL,
	[MaDat] [int] NULL,
	[ThoiGianIn] [datetime2](0) NOT NULL,
	[MaNV] [int] NULL,
	[SoPhong] [nvarchar](max) NULL,
 CONSTRAINT [PK_LichSuHoaDon] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiPhong]    Script Date: 11/15/2025 12:02:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiPhong](
	[MaLoaiPhong] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiPhong] [nvarchar](100) NOT NULL,
	[GiaPhong] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_LoaiPhong] PRIMARY KEY CLUSTERED 
(
	[MaLoaiPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 11/15/2025 12:02:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [int] IDENTITY(1,1) NOT NULL,
	[TenNV] [nvarchar](100) NOT NULL,
	[CCCD] [varchar](15) NULL,
	[SDT] [varchar](15) NULL,
	[ChucVu] [nvarchar](50) NULL,
	[NgayThamGia] [datetime] NULL,
	[TrangThaiHoatDong] [nvarchar](100) NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhatKyHeThong]    Script Date: 11/15/2025 12:02:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhatKyHeThong](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ThoiGian] [datetime2](0) NOT NULL,
	[MaNV] [int] NULL,
	[TenDangNhap] [nvarchar](100) NULL,
	[HanhDong] [nvarchar](50) NOT NULL,
	[DoiTuong] [nvarchar](50) NULL,
	[KhoaChinh] [nvarchar](50) NULL,
	[MoTa] [nvarchar](500) NULL,
	[DuLieuCu] [nvarchar](max) NULL,
	[DuLieuMoi] [nvarchar](max) NULL,
	[KetQua] [bit] NOT NULL,
	[Loi] [nvarchar](1000) NULL,
	[DiaChiIP] [nvarchar](45) NULL,
	[TenMay] [nvarchar](100) NULL,
 CONSTRAINT [PK__NhatKyHe__3214EC07C080B631] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phong]    Script Date: 11/15/2025 12:02:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phong](
	[MaPhong] [int] IDENTITY(1,1) NOT NULL,
	[SoPhong] [int] NOT NULL,
	[LoaiPhong] [nvarchar](100) NULL,
	[Gia] [decimal](18, 0) NULL,
	[TrangThai] [nvarchar](40) NOT NULL,
	[MaLoaiPhong] [int] NULL,
 CONSTRAINT [PK_Phong] PRIMARY KEY CLUSTERED 
(
	[MaPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 11/15/2025 12:02:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[MaTK] [int] IDENTITY(1,1) NOT NULL,
	[TenDangNhap] [char](50) NOT NULL,
	[MatKhau] [char](50) NOT NULL,
	[Quyen] [int] NOT NULL,
	[MaNV] [int] NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[MaTK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ChiTietHoaDon] ON 

INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (7, 7, N'Phòng 7 (5 đêm x 500,000) + cọc', 1, CAST(2700000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (8, 7, N'Phòng 8 (5 đêm x 300,000) + cọc', 1, CAST(1700000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (9, 9, N'Tiền phòng - Phòng 1 (3 đêm x 500,000)', 3, CAST(500000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (10, 9, N'Tiền cọc - Phòng 1', 1, CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (14, 13, N'Phòng 9 (3 đêm x 500,000) + cọc', 1, CAST(1700000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (15, 13, N'Phòng 10 (3 đêm x 200,000) + cọc', 1, CAST(800000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (16, 15, N'Phòng 9 (10 đêm x 200,000) + cọc', 1, CAST(2200000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (17, 15, N'Phòng 10 (10 đêm x 500,000) + cọc', 1, CAST(5200000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (18, 17, N'Phòng 9 (2 đêm x 200,000) + cọc', 1, CAST(600000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (19, 17, N'Phòng 10 (2 đêm x 300,000) + cọc', 1, CAST(800000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (20, 20, N'Phòng 8 (1 đêm x 300,000) + cọc', 1, CAST(500000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (21, 20, N'Phòng 9 (1 đêm x 200,000) + cọc', 1, CAST(400000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (22, 20, N'Phòng 10 (1 đêm x 300,000) + cọc', 1, CAST(500000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (23, 24, N'Tiền phòng - Phòng 10 (11 đêm x 200,000)', 11, CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (24, 24, N'Tiền cọc - Phòng 10', 1, CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (25, 26, N'Phòng 9 (1 đêm x 200,000) + cọc', 1, CAST(400000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaCT], [MaHD], [DanhMuc], [SoLuong], [DonGia]) VALUES (26, 26, N'Phòng 10 (1 đêm x 200,000) + cọc', 1, CAST(400000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[ChiTietHoaDon] OFF
GO
SET IDENTITY_INSERT [dbo].[DatPhong] ON 

INSERT [dbo].[DatPhong] ([MaDat], [MaKH], [MaPhong], [NgayNhan], [NgayTraDuKien], [NgayTraThucTe], [TienCoc], [TienThue], [TrangThai]) VALUES (5, 4, 7, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-20T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-15T10:57:17.0000000' AS DateTime2), CAST(200000 AS Decimal(18, 0)), CAST(2500000 AS Decimal(18, 0)), N'Hoàn thành')
INSERT [dbo].[DatPhong] ([MaDat], [MaKH], [MaPhong], [NgayNhan], [NgayTraDuKien], [NgayTraThucTe], [TienCoc], [TienThue], [TrangThai]) VALUES (7, 5, 15, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-18T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(200000 AS Decimal(18, 0)), CAST(1500000 AS Decimal(18, 0)), N'Hoàn thành')
INSERT [dbo].[DatPhong] ([MaDat], [MaKH], [MaPhong], [NgayNhan], [NgayTraDuKien], [NgayTraThucTe], [TienCoc], [TienThue], [TrangThai]) VALUES (10, 6, 15, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-16T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-15T10:58:51.0000000' AS DateTime2), CAST(200000 AS Decimal(18, 0)), CAST(500000 AS Decimal(18, 0)), N'Hoàn thành')
INSERT [dbo].[DatPhong] ([MaDat], [MaKH], [MaPhong], [NgayNhan], [NgayTraDuKien], [NgayTraThucTe], [TienCoc], [TienThue], [TrangThai]) VALUES (11, 7, 19, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-18T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-15T10:59:53.0000000' AS DateTime2), CAST(200000 AS Decimal(18, 0)), CAST(1500000 AS Decimal(18, 0)), N'Hoàn thành')
INSERT [dbo].[DatPhong] ([MaDat], [MaKH], [MaPhong], [NgayNhan], [NgayTraDuKien], [NgayTraThucTe], [TienCoc], [TienThue], [TrangThai]) VALUES (13, 8, 19, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-25T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-15T11:01:06.0000000' AS DateTime2), CAST(200000 AS Decimal(18, 0)), CAST(2000000 AS Decimal(18, 0)), N'Hoàn thành')
INSERT [dbo].[DatPhong] ([MaDat], [MaKH], [MaPhong], [NgayNhan], [NgayTraDuKien], [NgayTraThucTe], [TienCoc], [TienThue], [TrangThai]) VALUES (15, 9, 19, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-17T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-15T11:20:34.0000000' AS DateTime2), CAST(200000 AS Decimal(18, 0)), CAST(400000 AS Decimal(18, 0)), N'Hoàn thành')
INSERT [dbo].[DatPhong] ([MaDat], [MaKH], [MaPhong], [NgayNhan], [NgayTraDuKien], [NgayTraThucTe], [TienCoc], [TienThue], [TrangThai]) VALUES (17, 10, 18, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-16T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-15T11:29:00.0000000' AS DateTime2), CAST(200000 AS Decimal(18, 0)), CAST(300000 AS Decimal(18, 0)), N'Hoàn thành')
INSERT [dbo].[DatPhong] ([MaDat], [MaKH], [MaPhong], [NgayNhan], [NgayTraDuKien], [NgayTraThucTe], [TienCoc], [TienThue], [TrangThai]) VALUES (18, 10, 19, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-16T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-15T11:29:00.0000000' AS DateTime2), CAST(200000 AS Decimal(18, 0)), CAST(200000 AS Decimal(18, 0)), N'Hoàn thành')
INSERT [dbo].[DatPhong] ([MaDat], [MaKH], [MaPhong], [NgayNhan], [NgayTraDuKien], [NgayTraThucTe], [TienCoc], [TienThue], [TrangThai]) VALUES (20, 11, 22, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-26T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(200000 AS Decimal(18, 0)), CAST(2200000 AS Decimal(18, 0)), N'Hoàn thành')
INSERT [dbo].[DatPhong] ([MaDat], [MaKH], [MaPhong], [NgayNhan], [NgayTraDuKien], [NgayTraThucTe], [TienCoc], [TienThue], [TrangThai]) VALUES (21, 12, 19, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-16T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-15T11:48:10.0000000' AS DateTime2), CAST(200000 AS Decimal(18, 0)), CAST(200000 AS Decimal(18, 0)), N'Hoàn thành')
INSERT [dbo].[DatPhong] ([MaDat], [MaKH], [MaPhong], [NgayNhan], [NgayTraDuKien], [NgayTraThucTe], [TienCoc], [TienThue], [TrangThai]) VALUES (22, 12, 22, CAST(N'2025-11-15T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-16T00:00:00.0000000' AS DateTime2), CAST(N'2025-11-15T11:48:10.0000000' AS DateTime2), CAST(200000 AS Decimal(18, 0)), CAST(200000 AS Decimal(18, 0)), N'Hoàn thành')
SET IDENTITY_INSERT [dbo].[DatPhong] OFF
GO
SET IDENTITY_INSERT [dbo].[HoaDon] ON 

INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (7, 5, NULL, CAST(N'2025-11-15T10:57:08.0000000' AS DateTime2), N'Lần 1', CAST(4400000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (8, 5, NULL, CAST(N'2025-11-15T10:57:15.0000000' AS DateTime2), N'Lần 2', CAST(-800000 AS Decimal(18, 0)), N'')
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (9, 7, NULL, CAST(N'2025-11-15T10:57:46.0000000' AS DateTime2), N'Lần 1', CAST(1700000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (10, 7, NULL, CAST(N'2025-11-15T10:57:50.0000000' AS DateTime2), N'Lần 2', CAST(0 AS Decimal(18, 0)), N'')
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (13, 11, NULL, CAST(N'2025-11-15T10:59:47.0000000' AS DateTime2), N'Lần 1', CAST(2500000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (14, 11, NULL, CAST(N'2025-11-15T10:59:50.0000000' AS DateTime2), N'Lần 2', CAST(-800000 AS Decimal(18, 0)), N'')
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (15, 13, NULL, CAST(N'2025-11-15T11:00:58.0000000' AS DateTime2), N'Lần 1', CAST(7400000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (16, 13, NULL, CAST(N'2025-11-15T11:01:04.0000000' AS DateTime2), N'Lần 2', CAST(-800000 AS Decimal(18, 0)), N'')
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (17, 15, NULL, CAST(N'2025-11-15T11:20:21.0000000' AS DateTime2), N'Lần 1', CAST(1400000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (19, 15, NULL, CAST(N'2025-11-15T11:20:30.0000000' AS DateTime2), N'Lần 2', CAST(-800000 AS Decimal(18, 0)), N'')
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (20, 17, NULL, CAST(N'2025-11-15T11:25:48.0000000' AS DateTime2), N'Lần 1', CAST(1400000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (21, 18, NULL, CAST(N'2025-11-15T11:25:48.0000000' AS DateTime2), N'Lần 1', CAST(0 AS Decimal(18, 0)), N'Thuộc HĐ gộp #20')
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (23, 17, NULL, CAST(N'2025-11-15T11:28:41.0000000' AS DateTime2), N'Lần 2', CAST(-1170000 AS Decimal(18, 0)), N'mì gói: 20.000đ
nước: 10.000đ')
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (24, 20, NULL, CAST(N'2025-11-15T11:29:30.0000000' AS DateTime2), N'Lần 1', CAST(2400000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (25, 20, NULL, CAST(N'2025-11-15T11:29:34.0000000' AS DateTime2), N'Lần 2', CAST(0 AS Decimal(18, 0)), N'')
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (26, 21, NULL, CAST(N'2025-11-15T11:48:02.0000000' AS DateTime2), N'Lần 1', CAST(800000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (27, 22, NULL, CAST(N'2025-11-15T11:48:02.0000000' AS DateTime2), N'Lần 1', CAST(0 AS Decimal(18, 0)), N'Thuộc HĐ gộp #26')
INSERT [dbo].[HoaDon] ([MaHD], [MaDat], [MaNV], [NgayLap], [LoaiHoaDon], [TongThanhToan], [GhiChu]) VALUES (28, 21, NULL, CAST(N'2025-11-15T11:48:08.0000000' AS DateTime2), N'Lần 2', CAST(-800000 AS Decimal(18, 0)), N'')
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (1, N'Nguyễn Van B', N'435345345345', N'345342324234', CAST(N'2025-11-15T10:49:49.827' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (2, N'Nguyễn Thị Uyên', N'0993983838', N'2345345345', CAST(N'2025-11-15T10:55:56.947' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (3, N'Nguyễn Tuấn Phát', N'3924893284', N'4534534543', CAST(N'2025-11-15T10:56:41.177' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (4, N'Huỳnh Tấn Phát', N'3432534534', N'345345345345', CAST(N'2025-11-15T10:57:08.417' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (5, N'Huỳnh Văn Khang', N'4353453453', N'34534534534', CAST(N'2025-11-15T10:57:47.947' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (6, N'Hoài Bo', N'00983993893', N'0345345323', CAST(N'2025-11-15T10:58:39.560' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (7, N'Hưng Thịnh', N'2343242345', N'43534534534', CAST(N'2025-11-15T10:59:47.430' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (8, N'Nguyễn Hoài An', N'34534523423', N'234234532432', CAST(N'2025-11-15T11:00:58.250' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (9, N'Nguyễn Thị My', N'4309430953', N'3453453423', CAST(N'2025-11-15T11:20:20.760' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (10, N'Vũ Thế Huỳnh', N'099338383', N'345345345', CAST(N'2025-11-15T11:25:48.030' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (11, N'Huỳnh Tấn Khang', N'234543234', N'3245346563', CAST(N'2025-11-15T11:29:31.853' AS DateTime))
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [CCCD], [SDT], [NgayThamGia]) VALUES (12, N'Nguyễn Vũ', N'009398832', N'3443543543', CAST(N'2025-11-15T11:48:02.127' AS DateTime))
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
GO
SET IDENTITY_INSERT [dbo].[LichSuHoaDon] ON 

INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (7, 7, 5, CAST(N'2025-11-15T10:57:08.0000000' AS DateTime2), 4, N'7 - 8')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (8, 8, 5, CAST(N'2025-11-15T10:57:16.0000000' AS DateTime2), 4, N'7 - 8')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (9, 9, 7, CAST(N'2025-11-15T10:57:46.0000000' AS DateTime2), 5, N'1')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (10, 10, 7, CAST(N'2025-11-15T10:57:51.0000000' AS DateTime2), 5, N'1')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (13, 13, 11, CAST(N'2025-11-15T10:59:47.0000000' AS DateTime2), 7, N'9 - 10')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (14, 14, 11, CAST(N'2025-11-15T10:59:52.0000000' AS DateTime2), 7, N'9 - 10')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (15, 15, 13, CAST(N'2025-11-15T11:00:58.0000000' AS DateTime2), 6, N'9 - 10')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (16, 16, 13, CAST(N'2025-11-15T11:01:04.0000000' AS DateTime2), 6, N'9 - 10')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (17, 17, 15, CAST(N'2025-11-15T11:20:21.0000000' AS DateTime2), 3, N'9 - 10')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (18, 19, 15, CAST(N'2025-11-15T11:20:32.0000000' AS DateTime2), 3, N'9 - 10')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (19, 20, 17, CAST(N'2025-11-15T11:25:48.0000000' AS DateTime2), 3, N'8 - 9 - 10')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (20, 23, 17, CAST(N'2025-11-15T11:28:59.0000000' AS DateTime2), 3, N'8 - 9 - 10')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (21, 24, 20, CAST(N'2025-11-15T11:29:30.0000000' AS DateTime2), 3, N'10')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (22, 25, 20, CAST(N'2025-11-15T11:29:35.0000000' AS DateTime2), 3, N'10')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (23, 26, 21, CAST(N'2025-11-15T11:48:02.0000000' AS DateTime2), 3, N'9 - 10')
INSERT [dbo].[LichSuHoaDon] ([Id], [MaHD], [MaDat], [ThoiGianIn], [MaNV], [SoPhong]) VALUES (24, 28, 21, CAST(N'2025-11-15T11:48:09.0000000' AS DateTime2), 3, N'9 - 10')
SET IDENTITY_INSERT [dbo].[LichSuHoaDon] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiPhong] ON 

INSERT [dbo].[LoaiPhong] ([MaLoaiPhong], [TenLoaiPhong], [GiaPhong]) VALUES (1, N'Phòng đơn', CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[LoaiPhong] ([MaLoaiPhong], [TenLoaiPhong], [GiaPhong]) VALUES (2, N'Phòng đôi', CAST(300000 AS Decimal(18, 0)))
INSERT [dbo].[LoaiPhong] ([MaLoaiPhong], [TenLoaiPhong], [GiaPhong]) VALUES (3, N'Phòng VIP', CAST(500000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[LoaiPhong] OFF
GO
SET IDENTITY_INSERT [dbo].[NhanVien] ON 

INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [CCCD], [SDT], [ChucVu], [NgayThamGia], [TrangThaiHoatDong]) VALUES (1, N'Nguyễn Ngọc Trường Dân', N'075205007509', N'0369255321', N'Admin', CAST(N'2025-09-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [CCCD], [SDT], [ChucVu], [NgayThamGia], [TrangThaiHoatDong]) VALUES (2, N'Nguyễn Ngọc Ánh', N'075204894029', N'0369229292', N'Admin', CAST(N'2025-06-17T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [CCCD], [SDT], [ChucVu], [NgayThamGia], [TrangThaiHoatDong]) VALUES (3, N'Phan Khánh Vương', N'073837263844', N'0988847363', N'Nhân viên', CAST(N'2025-09-14T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [CCCD], [SDT], [ChucVu], [NgayThamGia], [TrangThaiHoatDong]) VALUES (4, N'Nguyễn Thịnh', N'099372837333', N'099283746', N'Nhân viên', CAST(N'2025-09-12T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [CCCD], [SDT], [ChucVu], [NgayThamGia], [TrangThaiHoatDong]) VALUES (5, N'Nguyễn Huỳnh', N'075848392745', N'0992839343', N'Nhân viên', CAST(N'2025-09-12T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [CCCD], [SDT], [ChucVu], [NgayThamGia], [TrangThaiHoatDong]) VALUES (6, N'Tạ Hàm Mặc', N'098527384734', N'098782736', N'Nhân viên', CAST(N'2025-09-12T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [CCCD], [SDT], [ChucVu], [NgayThamGia], [TrangThaiHoatDong]) VALUES (7, N'Đào Chú', N'093748576345', N'009882734', N'Nhân viên', CAST(N'2025-09-12T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[NhanVien] OFF
GO
SET IDENTITY_INSERT [dbo].[NhatKyHeThong] ON 

INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (1, CAST(N'2025-11-15T10:48:50.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'1', N'Thêm phòng số 1, loại Phòng đôi', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (2, CAST(N'2025-11-15T10:48:53.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'2', N'Thêm phòng số 2, loại Phòng đơn', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (3, CAST(N'2025-11-15T10:48:55.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'3', N'Thêm phòng số 3, loại Phòng VIP', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (4, CAST(N'2025-11-15T10:48:57.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'4', N'Thêm phòng số 4, loại Phòng đơn', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (5, CAST(N'2025-11-15T10:49:00.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'5', N'Thêm phòng số 5, loại Phòng đơn', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (6, CAST(N'2025-11-15T10:49:02.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'6', N'Thêm phòng số 6, loại Phòng đôi', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (7, CAST(N'2025-11-15T10:49:05.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'7', N'Thêm phòng số 7, loại Phòng VIP', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (8, CAST(N'2025-11-15T10:49:06.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'8', N'Thêm phòng số 8, loại Phòng đôi', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (9, CAST(N'2025-11-15T10:49:10.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'9', N'Thêm phòng số 9, loại Phòng đơn', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (10, CAST(N'2025-11-15T10:49:18.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'10', N'Thêm phòng số 10, loại Phòng VIP', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (11, CAST(N'2025-11-15T10:49:50.0000000' AS DateTime2), 3, N'vuong                                             ', N'Upsert', N'KhachHang', N'1', N'KH: Nguyễn Van B - 345342324234', NULL, N'Ten=Nguyễn Van B; CCCD=435345345345; SDT=345342324234', 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (12, CAST(N'2025-11-15T10:49:50.0000000' AS DateTime2), 3, N'vuong                                             ', N'Sửa', N'DatPhong', N'1', N'Cập nhật đặt phòng | Phòng=10 | 15/11→20/11 | Cọc=200,000 | Thuế=2,500,000 | Trạng thái=Đang sử dụng', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (13, CAST(N'2025-11-15T10:49:50.0000000' AS DateTime2), 3, N'vuong                                             ', N'Sửa', N'Phong', N'10', N'Cập nhật trạng thái phòng 10 = Đang sử dụng', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (14, CAST(N'2025-11-15T10:49:58.0000000' AS DateTime2), 3, N'vuong                                             ', N'Xoá', N'Phong', N'9,10', N'Xóa phòng số 9, 10', NULL, NULL, 0, N'The DELETE statement conflicted with the REFERENCE constraint "FK_LichSuHoaDon_HoaDon". The conflict occurred in database "QuanLyPhongKhachSan", table "dbo.LichSuHoaDon", column ''MaHD''.
The statement has been terminated.', N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (15, CAST(N'2025-11-15T10:50:11.0000000' AS DateTime2), 3, N'vuong                                             ', N'Xoá', N'Phong', N'10', N'Xóa phòng số 10', NULL, NULL, 0, N'The DELETE statement conflicted with the REFERENCE constraint "FK_LichSuHoaDon_HoaDon". The conflict occurred in database "QuanLyPhongKhachSan", table "dbo.LichSuHoaDon", column ''MaHD''.
The statement has been terminated.', N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (16, CAST(N'2025-11-15T10:53:12.0000000' AS DateTime2), 3, N'vuong                                             ', N'Xoá', N'Phong', N'8', N'Xóa phòng số 8', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (17, CAST(N'2025-11-15T10:53:16.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'11', N'Thêm phòng số 8, loại Phòng đôi', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (18, CAST(N'2025-11-15T10:53:32.0000000' AS DateTime2), 4, N'thinh                                             ', N'Thêm', N'Phong', N'12', N'Thêm phòng số 9, loại Phòng VIP', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (19, CAST(N'2025-11-15T10:53:35.0000000' AS DateTime2), 4, N'thinh                                             ', N'Xoá', N'Phong', N'12', N'Xóa phòng số 9', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (20, CAST(N'2025-11-15T10:53:39.0000000' AS DateTime2), 4, N'thinh                                             ', N'Thêm', N'Phong', N'13', N'Thêm phòng số 9, loại Phòng đơn', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (21, CAST(N'2025-11-15T10:55:18.0000000' AS DateTime2), 3, N'vuong                                             ', N'Xoá', N'Phong', N'10', N'Xóa phòng số 10', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (22, CAST(N'2025-11-15T10:55:24.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'14', N'Thêm phòng số 10, loại Phòng VIP', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (23, CAST(N'2025-11-15T10:56:41.0000000' AS DateTime2), 4, N'thinh                                             ', N'Upsert', N'KhachHang', N'3', N'KH: Nguyễn Tuấn Phát - 4534534543', NULL, N'Ten=Nguyễn Tuấn Phát; CCCD=3924893284; SDT=4534534543', 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (24, CAST(N'2025-11-15T10:56:41.0000000' AS DateTime2), 4, N'thinh                                             ', N'Sửa', N'DatPhong', N'4', N'Cập nhật đặt phòng | Phòng=10 | 15/11→19/11 | Cọc=200,000 | Thuế=2,000,000 | Trạng thái=Đang sử dụng', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (25, CAST(N'2025-11-15T10:56:41.0000000' AS DateTime2), 4, N'thinh                                             ', N'Sửa', N'Phong', N'14', N'Cập nhật trạng thái phòng 10 = Đang sử dụng', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (26, CAST(N'2025-11-15T10:57:30.0000000' AS DateTime2), 5, N'huynh                                             ', N'Xoá', N'Phong', N'1', N'Xóa phòng số 1', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (27, CAST(N'2025-11-15T10:57:33.0000000' AS DateTime2), 5, N'huynh                                             ', N'Thêm', N'Phong', N'15', N'Thêm phòng số 1, loại Phòng VIP', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (28, CAST(N'2025-11-15T10:57:48.0000000' AS DateTime2), 5, N'huynh                                             ', N'Upsert', N'KhachHang', N'5', N'KH: Huỳnh Văn Khang - 34534534534', NULL, N'Ten=Huỳnh Văn Khang; CCCD=4353453453; SDT=34534534534', 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (29, CAST(N'2025-11-15T10:57:48.0000000' AS DateTime2), 5, N'huynh                                             ', N'Sửa', N'DatPhong', N'7', N'Cập nhật đặt phòng | Phòng=1 | 15/11→18/11 | Cọc=200,000 | Thuế=1,500,000 | Trạng thái=Đang sử dụng', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (30, CAST(N'2025-11-15T10:57:48.0000000' AS DateTime2), 5, N'huynh                                             ', N'Sửa', N'Phong', N'15', N'Cập nhật trạng thái phòng 1 = Đang sử dụng', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (31, CAST(N'2025-11-15T10:58:56.0000000' AS DateTime2), 5, N'huynh                                             ', N'Xoá', N'Phong', N'3,2', N'Xóa phòng số 2, 3', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (32, CAST(N'2025-11-15T10:58:58.0000000' AS DateTime2), 5, N'huynh                                             ', N'Thêm', N'Phong', N'16', N'Thêm phòng số 2, loại Phòng VIP', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (33, CAST(N'2025-11-15T10:59:00.0000000' AS DateTime2), 5, N'huynh                                             ', N'Thêm', N'Phong', N'17', N'Thêm phòng số 3, loại Phòng đôi', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (34, CAST(N'2025-11-15T10:59:14.0000000' AS DateTime2), 7, N'peak                                              ', N'Xoá', N'Phong', N'14', N'Xóa phòng số 10', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (35, CAST(N'2025-11-15T10:59:17.0000000' AS DateTime2), 7, N'peak                                              ', N'Xoá', N'Phong', N'13,11', N'Xóa phòng số 8, 9', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (36, CAST(N'2025-11-15T10:59:22.0000000' AS DateTime2), 7, N'peak                                              ', N'Thêm', N'Phong', N'18', N'Thêm phòng số 8, loại Phòng đôi', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (37, CAST(N'2025-11-15T10:59:25.0000000' AS DateTime2), 7, N'peak                                              ', N'Thêm', N'Phong', N'19', N'Thêm phòng số 9, loại Phòng VIP', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (38, CAST(N'2025-11-15T10:59:28.0000000' AS DateTime2), 7, N'peak                                              ', N'Thêm', N'Phong', N'20', N'Thêm phòng số 10, loại Phòng đơn', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (39, CAST(N'2025-11-15T11:00:01.0000000' AS DateTime2), 7, N'peak                                              ', N'Sửa', N'Phong', N'20', N'Đổi loại phòng 10: Phòng đơn -> Phòng VIP', N'Phòng đơn', N'Phòng VIP', 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (40, CAST(N'2025-11-15T11:00:08.0000000' AS DateTime2), 7, N'peak                                              ', N'Sửa', N'Phong', N'19', N'Đổi loại phòng 9: Phòng VIP -> Phòng đơn', N'Phòng VIP', N'Phòng đơn', 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (41, CAST(N'2025-11-15T11:01:09.0000000' AS DateTime2), 6, N'thm                                               ', N'Xoá', N'Phong', N'20', N'Xóa phòng số 10', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (42, CAST(N'2025-11-15T11:01:11.0000000' AS DateTime2), 6, N'thm                                               ', N'Thêm', N'Phong', N'21', N'Thêm phòng số 10, loại Phòng đôi', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (43, CAST(N'2025-11-15T11:01:16.0000000' AS DateTime2), 6, N'thm                                               ', N'Sửa', N'Phong', N'5', N'Đổi loại phòng 5: Phòng đơn -> Phòng VIP', N'Phòng đơn', N'Phòng VIP', 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (44, CAST(N'2025-11-15T11:01:19.0000000' AS DateTime2), 6, N'thm                                               ', N'Sửa', N'Phong', N'7', N'Đổi loại phòng 7: Phòng VIP -> Phòng đơn', N'Phòng VIP', N'Phòng đơn', 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (45, CAST(N'2025-11-15T11:29:04.0000000' AS DateTime2), 3, N'vuong                                             ', N'Xoá', N'Phong', N'21', N'Xóa phòng số 10', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (46, CAST(N'2025-11-15T11:29:08.0000000' AS DateTime2), 3, N'vuong                                             ', N'Thêm', N'Phong', N'22', N'Thêm phòng số 10, loại Phòng đơn', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (47, CAST(N'2025-11-15T11:29:32.0000000' AS DateTime2), 3, N'vuong                                             ', N'Upsert', N'KhachHang', N'11', N'KH: Huỳnh Tấn Khang - 3245346563', NULL, N'Ten=Huỳnh Tấn Khang; CCCD=234543234; SDT=3245346563', 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (48, CAST(N'2025-11-15T11:29:32.0000000' AS DateTime2), 3, N'vuong                                             ', N'Sửa', N'DatPhong', N'20', N'Cập nhật đặt phòng | Phòng=10 | 15/11→26/11 | Cọc=200,000 | Thuế=2,200,000 | Trạng thái=Đang sử dụng', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
INSERT [dbo].[NhatKyHeThong] ([Id], [ThoiGian], [MaNV], [TenDangNhap], [HanhDong], [DoiTuong], [KhoaChinh], [MoTa], [DuLieuCu], [DuLieuMoi], [KetQua], [Loi], [DiaChiIP], [TenMay]) VALUES (49, CAST(N'2025-11-15T11:29:32.0000000' AS DateTime2), 3, N'vuong                                             ', N'Sửa', N'Phong', N'22', N'Cập nhật trạng thái phòng 10 = Đang sử dụng', NULL, NULL, 1, NULL, N'192.168.123.142', N'DESKTOP-LIASEBH')
SET IDENTITY_INSERT [dbo].[NhatKyHeThong] OFF
GO
SET IDENTITY_INSERT [dbo].[Phong] ON 

INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [LoaiPhong], [Gia], [TrangThai], [MaLoaiPhong]) VALUES (4, 4, NULL, NULL, N'Trống', 1)
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [LoaiPhong], [Gia], [TrangThai], [MaLoaiPhong]) VALUES (5, 5, NULL, NULL, N'Trống', 3)
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [LoaiPhong], [Gia], [TrangThai], [MaLoaiPhong]) VALUES (6, 6, NULL, NULL, N'Trống', 2)
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [LoaiPhong], [Gia], [TrangThai], [MaLoaiPhong]) VALUES (7, 7, NULL, NULL, N'Trống', 1)
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [LoaiPhong], [Gia], [TrangThai], [MaLoaiPhong]) VALUES (15, 1, NULL, NULL, N'Trống', 1)
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [LoaiPhong], [Gia], [TrangThai], [MaLoaiPhong]) VALUES (16, 2, NULL, NULL, N'Trống', 1)
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [LoaiPhong], [Gia], [TrangThai], [MaLoaiPhong]) VALUES (17, 3, NULL, NULL, N'Trống', 2)
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [LoaiPhong], [Gia], [TrangThai], [MaLoaiPhong]) VALUES (18, 8, NULL, NULL, N'Trống', 2)
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [LoaiPhong], [Gia], [TrangThai], [MaLoaiPhong]) VALUES (19, 9, NULL, NULL, N'Trống', 1)
INSERT [dbo].[Phong] ([MaPhong], [SoPhong], [LoaiPhong], [Gia], [TrangThai], [MaLoaiPhong]) VALUES (22, 10, NULL, NULL, N'Trống', 1)
SET IDENTITY_INSERT [dbo].[Phong] OFF
GO
SET IDENTITY_INSERT [dbo].[TaiKhoan] ON 

INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [Quyen], [MaNV]) VALUES (1, N'tem                                               ', N'123                                               ', 1, NULL)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [Quyen], [MaNV]) VALUES (2, N'dan                                               ', N'123                                               ', 1, 1)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [Quyen], [MaNV]) VALUES (3, N'anh                                               ', N'123                                               ', 1, 2)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [Quyen], [MaNV]) VALUES (4, N'vuong                                             ', N'123                                               ', 2, 3)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [Quyen], [MaNV]) VALUES (5, N'thinh                                             ', N'123                                               ', 2, 4)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [Quyen], [MaNV]) VALUES (6, N'huynh                                             ', N'123                                               ', 2, 5)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [Quyen], [MaNV]) VALUES (7, N'thm                                               ', N'123                                               ', 2, 6)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [Quyen], [MaNV]) VALUES (8, N'peak                                              ', N'123                                               ', 2, 7)
SET IDENTITY_INSERT [dbo].[TaiKhoan] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_KhachHang]    Script Date: 11/15/2025 12:02:03 PM ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [IX_KhachHang] UNIQUE NONCLUSTERED 
(
	[CCCD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Phong]    Script Date: 11/15/2025 12:02:03 PM ******/
ALTER TABLE [dbo].[Phong] ADD  CONSTRAINT [IX_Phong] UNIQUE NONCLUSTERED 
(
	[SoPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChiTietHoaDon] ADD  CONSTRAINT [DF_ChiTietHoaDon_SoLuong]  DEFAULT ((1)) FOR [SoLuong]
GO
ALTER TABLE [dbo].[DatPhong] ADD  CONSTRAINT [DF_DatPhong_TrangThai]  DEFAULT (N'Đã đặt') FOR [TrangThai]
GO
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF_HoaDon_NgayLap]  DEFAULT (sysutcdatetime()) FOR [NgayLap]
GO
ALTER TABLE [dbo].[NhatKyHeThong] ADD  CONSTRAINT [DF__NhatKyHeT__ThoiG__1D4655FB]  DEFAULT (sysutcdatetime()) FOR [ThoiGian]
GO
ALTER TABLE [dbo].[NhatKyHeThong] ADD  CONSTRAINT [DF__NhatKyHeT__KetQu__1E3A7A34]  DEFAULT ((1)) FOR [KetQua]
GO
ALTER TABLE [dbo].[Phong] ADD  CONSTRAINT [DF_Phong_TrangThai]  DEFAULT (N'Trống') FOR [TrangThai]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_HoaDon] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HoaDon] ([MaHD])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_HoaDon]
GO
ALTER TABLE [dbo].[DatPhong]  WITH CHECK ADD  CONSTRAINT [FK_DatPhong_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[DatPhong] CHECK CONSTRAINT [FK_DatPhong_KhachHang]
GO
ALTER TABLE [dbo].[DatPhong]  WITH CHECK ADD  CONSTRAINT [FK_DatPhong_Phong] FOREIGN KEY([MaPhong])
REFERENCES [dbo].[Phong] ([MaPhong])
GO
ALTER TABLE [dbo].[DatPhong] CHECK CONSTRAINT [FK_DatPhong_Phong]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_DatPhong] FOREIGN KEY([MaDat])
REFERENCES [dbo].[DatPhong] ([MaDat])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_DatPhong]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_NhanVien]
GO
ALTER TABLE [dbo].[LichSuHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_LichSuHoaDon_DatPhong] FOREIGN KEY([MaDat])
REFERENCES [dbo].[DatPhong] ([MaDat])
GO
ALTER TABLE [dbo].[LichSuHoaDon] CHECK CONSTRAINT [FK_LichSuHoaDon_DatPhong]
GO
ALTER TABLE [dbo].[LichSuHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_LichSuHoaDon_HoaDon] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HoaDon] ([MaHD])
GO
ALTER TABLE [dbo].[LichSuHoaDon] CHECK CONSTRAINT [FK_LichSuHoaDon_HoaDon]
GO
ALTER TABLE [dbo].[LichSuHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_LichSuHoaDon_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[LichSuHoaDon] CHECK CONSTRAINT [FK_LichSuHoaDon_NhanVien]
GO
ALTER TABLE [dbo].[NhatKyHeThong]  WITH CHECK ADD  CONSTRAINT [FK_NhatKyHeThong_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[NhatKyHeThong] CHECK CONSTRAINT [FK_NhatKyHeThong_NhanVien]
GO
ALTER TABLE [dbo].[Phong]  WITH CHECK ADD  CONSTRAINT [FK_Phong_LoaiPhong] FOREIGN KEY([MaLoaiPhong])
REFERENCES [dbo].[LoaiPhong] ([MaLoaiPhong])
GO
ALTER TABLE [dbo].[Phong] CHECK CONSTRAINT [FK_Phong_LoaiPhong]
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD  CONSTRAINT [FK_TaiKhoan_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[TaiKhoan] CHECK CONSTRAINT [FK_TaiKhoan_NhanVien]
GO
ALTER TABLE [dbo].[DatPhong]  WITH CHECK ADD  CONSTRAINT [CK_DatPhong] CHECK  (([NgayTraDuKien]>[NgayNhan]))
GO
ALTER TABLE [dbo].[DatPhong] CHECK CONSTRAINT [CK_DatPhong]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [CK_HoaDon_Loai] CHECK  (([LoaiHoaDon]=N'Lần 2' OR [LoaiHoaDon]=N'Lần 1'))
GO
