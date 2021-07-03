using System;

namespace QMS_BenhVien.Model
{
    public class DatHenModel
    {
        public int BenhNhan_Id { get; set; }
        public int PhongKham_Id { get; set; }
        public int DichVu_Id { get; set; }
        public int BacSi_Id { get; set; }
        public string MaBenhNhan { get; set; }
        public string TenBenhNhan { get; set; }
        public string MaPhongKham { get; set; }
        public string TenPhongKham { get; set; }
        public string MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public string TenBacSi { get; set; }
        public string MaLoaiCuocHen { get; set; }
        public string TenLoaiCuocHen { get; set; }
        public string TrangThai { get; set; }
        public DateTime ThoiGianHen_BatDau { get; set; }
        public DateTime ThoiGianHen_KetThuc { get; set; }
    }
}
