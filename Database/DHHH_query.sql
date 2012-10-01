----------------------------------------------------------------------------------------------------------
--script for table Khoa
----------------------------------------------------------------------------------------------------------
--Insert
INSERT INTO Khoa(ID_khoa, Ma_khoa, Ten_khoa)
SELECT ID_khoa, Ma_khoa, Ten_khoa FROM DHHH.dbo.STU_Khoa k1
WHERE NOT EXISTS(SELECT ID_khoa FROM Khoa k2 WHERE k1.ID_khoa = k2.ID_khoa)
--Update
UPDATE k1 SET k1.Ma_khoa = k2.Ma_khoa, k1.Ten_khoa = k2.Ten_khoa
FROM Khoa k1 JOIN DHHH.dbo.STU_Khoa k2 on k1.ID_khoa = k2.ID_khoa
----------------------------------------------------------------------------------------------------------
--script for table HocKy
----------------------------------------------------------------------------------------------------------
--Insert
INSERT INTO HocKy(Ky_dang_ky, Dot, Hoc_ky, Nam_hoc, Chon_dang_ky)
SELECT Ky_dang_ky, Dot, Hoc_ky, Nam_hoc, Chon_dang_ky FROM DHHH.dbo.PLAN_HocKyDangKy_TC h2
WHERE NOT EXISTS(SELECT h1.Ky_dang_ky FROM HocKy h1 WHERE h1.Ky_dang_ky = h2.Ky_dang_ky)
--Update
UPDATE h1 SET h1.Dot = h2.Dot, h1.Hoc_ky = h2.Hoc_ky, h1.Nam_hoc = h2.Nam_hoc
, h1.Chon_dang_ky = h2.Chon_dang_ky
FROM HocKy h1 JOIN DHHH.dbo.PLAN_HocKyDangKy_TC h2 on h1.Ky_dang_ky = h2.Ky_dang_ky
----------------------------------------------------------------------------------------------------------
--script for table BoMon
----------------------------------------------------------------------------------------------------------
--Insert
INSERT INTO BoMon(ID_bm, Ma_bm, Ten_bm, ID_khoa)
SELECT ID_bm, Ma_bo_mon, Bo_mon, ID_khoa FROM DHHH.dbo.PLAN_BoMon_TC b1
WHERE NOT EXISTS(SELECT ID_bm FROM BoMon b2 WHERE b2.ID_bm = b1.ID_bm)
--Update
UPDATE b1 SET Ma_bm = b2.Ma_bo_mon, b1.Ten_bm = b2.Bo_mon, b1.ID_khoa = b2.ID_khoa
FROM BoMon b1 JOIN DHHH.dbo.PLAN_BoMon_TC b2
ON b1.ID_bm = b2.ID_bm
----------------------------------------------------------------------------------------------------------
--script for table MonHoc
----------------------------------------------------------------------------------------------------------
--Insert
INSERT INTO MonHoc(ID_mon, Ky_hieu,Ten_mon, ID_bm)
SELECT ID_mon, Ky_hieu,Ten_mon, ID_bm FROM DHHH.dbo.MARK_MonHoc_TC m1
WHERE NOT EXISTS(SELECT ID_mon FROM MonHoc m2 WHERE m2.ID_mon = m1.ID_mon)
--Update
UPDATE m1 SET m1.Ky_hieu = m2.Ky_hieu, m1.Ten_mon = m2.Ten_mon, m1.ID_bm = m2.ID_bm
FROM MonHoc m1 JOIN DHHH.dbo.MARK_MonHoc_TC m2
ON m1.ID_mon = m2.ID_mon
----------------------------------------------------------------------------------------------------------
--script for table SinhVien
----------------------------------------------------------------------------------------------------------
--Insert--
INSERT INTO SinhVien(ID_sv, Ma_sv, Ho_dem, Ten, Ngay_sinh, Lop)
SELECT ID_sv, Ma_sv
, LEFT(Ho_ten, LEN(Ho_ten) - CHARINDEX(' ', REVERSE(Ho_ten))+1) Ho_dem
, RIGHT(Ho_ten, CHARINDEX(' ', REVERSE(Ho_ten))-1) Ten
, Ngay_sinh, Lop FROM DHHH.dbo.STU_HoSoSinhVien sv1
WHERE NOT EXISTS(SELECT ID_sv FROM SinhVien sv2 WHERE sv2.ID_sv = sv1.ID_sv)
--Update--
UPDATE sv SET sv.Ma_sv = hs.Ma_sv
, sv.Ho_dem = LEFT(hs.Ho_ten, LEN(hs.Ho_ten) - CHARINDEX(' ', REVERSE(hs.Ho_ten))+1)
, sv.Ten = RIGHT(hs.Ho_ten, CHARINDEX(' ', REVERSE(hs.Ho_ten))-1)
, Ngay_sinh = hs.Ngay_sinh
, Lop = hs.Lop
FROM DHHH.dbo.STU_HoSoSinhVien hs JOIN SinhVien sv on sv.ID_sv = hs.ID_sv
----------------------------------------------------------------------------------------------------------
--script for table ThoiKhoaBieu
----------------------------------------------------------------------------------------------------------
--Insert
INSERT INTO ThoiKhoaBieu(ID_tkb, ID_mon, Ky_dang_ky, Ma_nhom, Ngay_bd, Ngay_kt)
SELECT ltc.ID_lop_tc AS ID_tkb, mtc.ID_mon AS ID_mon
 , mtc.Ky_dang_ky AS Ky_dang_ky
 ,'N' + RIGHT('0' + CAST(ltc.STT_lop AS VARCHAR(2)),2)AS Ma_nhom
 ,ltc.Tu_ngay AS Ngay_bd, ltc.Den_ngay AS Ngay_kt
FROM DHHH.dbo.PLAN_LopTinChi_TC ltc
JOIN DHHH.dbo.PLAN_MonTinChi_TC mtc ON mtc.ID_mon_tc = ltc.ID_mon_Tc
JOIN DHHH.dbo.MARK_MonHoc mh ON mh.ID_mon = mtc.ID_mon
WHERE ltc.ID_lop_lt=0 AND NOT EXISTS
(SELECT ID_tkb FROM ThoiKhoaBieu tkb WHERE tkb.ID_tkb = ltc.ID_lop_tc) 
UNION
SELECT l2.ID_lop_tc AS ID_tkb, mtc.ID_mon AS ID_mon
 ,mtc.Ky_dang_ky AS Ky_dang_ky
 ,'N' + RIGHT('0' + CAST(l1.STT_lop AS VARCHAR(2)),2) + '.TH' + CAST(l2.STT_Lop AS VARCHAR) AS Ma_nhom
 ,l2.Tu_ngay AS Ngay_bd, l2.Den_ngay AS Ngay_kt
FROM DHHH.dbo.PLAN_LopTinChi_TC l1 JOIN DHHH.dbo.PLAN_LopTinChi_TC l2 ON l1.ID_lop_tc = l2.ID_lop_lt
JOIN DHHH.dbo.PLAN_MonTinChi_TC mtc ON mtc.ID_mon_tc = l1.ID_mon_Tc
JOIN DHHH.dbo.MARK_MonHoc mh ON mh.ID_mon = mtc.ID_mon 
WHERE l2.ID_lop_lt<>0 AND NOT EXISTS
(SELECT ID_tkb FROM ThoiKhoaBieu tkb WHERE tkb.ID_tkb = l2.ID_lop_tc)
--Update lop ly thuyet
UPDATE t1 SET t1.ID_mon = t2.ID_mon, t1.Ky_dang_ky = t2.Ky_dang_ky, t1.Ma_nhom = t2.Ma_nhom
, t1.Ngay_bd = t1.Ngay_bd, t1.Ngay_kt = t2.Ngay_kt
FROM ThoiKhoaBieu t1 JOIN 
(SELECT ltc.ID_lop_tc AS ID_tkb, mtc.ID_mon AS ID_mon
 ,mtc.Ky_dang_ky AS Ky_dang_ky
 ,'N' + RIGHT('0' + CAST(ltc.STT_lop AS VARCHAR(2)),2)AS Ma_nhom
 ,ltc.Tu_ngay AS Ngay_bd, ltc.Den_ngay AS Ngay_kt
FROM DHHH.dbo.PLAN_LopTinChi_TC ltc
JOIN DHHH.dbo.PLAN_MonTinChi_TC mtc ON mtc.ID_mon_tc = ltc.ID_mon_Tc
JOIN DHHH.dbo.MARK_MonHoc mh ON mh.ID_mon = mtc.ID_mon
WHERE ltc.ID_lop_lt = 0) AS t2
ON t1.ID_tkb = t2.ID_tkb
--Update lop thuc hanh
UPDATE t1 SET t1.ID_mon = t2.ID_mon, t1.Ky_dang_ky = t2.Ky_dang_ky, t1.Ma_nhom = t2.Ma_nhom
, t1.Ngay_bd = t1.Ngay_bd, t1.Ngay_kt = t2.Ngay_kt
FROM ThoiKhoaBieu t1 JOIN
(SELECT l2.ID_lop_tc AS ID_tkb, mtc.ID_mon AS ID_mon
 ,mtc.Ky_dang_ky AS Ky_dang_ky
 ,'N' + RIGHT('0' + CAST(l1.STT_lop AS VARCHAR(2)),2) + '.TH' + CAST(l2.STT_Lop AS VARCHAR) AS Ma_nhom
 ,l2.Tu_ngay AS Ngay_bd, l2.Den_ngay AS Ngay_kt
FROM DHHH.dbo.PLAN_LopTinChi_TC l1 JOIN DHHH.dbo.PLAN_LopTinChi_TC l2 ON l1.ID_lop_tc = l2.ID_lop_lt
JOIN DHHH.dbo.PLAN_MonTinChi_TC mtc ON mtc.ID_mon_tc = l1.ID_mon_Tc
JOIN DHHH.dbo.MARK_MonHoc mh ON mh.ID_mon = mtc.ID_mon 
WHERE l2.ID_lop_lt<>0) AS t2
ON t1.ID_tkb = t2.ID_tkb
----------------------------------------------------------------------------------------------------------
--script for table DangKy
----------------------------------------------------------------------------------------------------------
--Insert
INSERT INTO DangKy(ID_dk, ID_sv, ID_tkb)
SELECT ID, ID_sv, ID_lop_tc FROM DHHH.dbo.STU_DanhSachLopTinChi ds
WHERE NOT EXISTS
(SELECT ID_dk FROM DangKy dk WHERE dk.ID_dk = ds.ID)
--Update
UPDATE dk SET dk.ID_sv = ds.ID_sv, dk.ID_tkb = ds.ID_lop_tc
FROM DangKy dk JOIN DHHH.dbo.STU_DanhSachLopTinChi ds
ON dk.ID_dk = ds.ID
----------------------------------------------------------------------------------------------------------
--script for table ?
----------------------------------------------------------------------------------------------------------