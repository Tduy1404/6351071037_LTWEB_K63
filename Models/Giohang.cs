﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
<<<<<<< HEAD
using System.Web.DynamicData;
=======
>>>>>>> e6d7a8a624ea7d3de0dc4f2fa8432bb450a6a75c

namespace BOOK.Models
{
    public class Giohang
    {
        // Tạo đối tượng data chứa dữ liệu từ model dbBansach đã tạo.
<<<<<<< HEAD
        DataTHB2Entities1 data = new DataTHB2Entities1();
=======
        DataTHB2Entities data = new DataTHB2Entities();
>>>>>>> e6d7a8a624ea7d3de0dc4f2fa8432bb450a6a75c

        public int iMasach { get; set; }
        public string sTensach { get; set; }
        public string sAnhbia { get; set; }
        public double dDongia { get; set; }
        public int iSoluong { get; set; }

        public double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }

        // Khởi tạo giỏ hàng theo Masach được truyền vào với Soluong mặc định là 1
        public Giohang(int Masach)
        {
            iMasach = Masach;
            SACH sach = data.SACHes.Single(n => n.Masach == iMasach);
            sTensach = sach.Tensach;
            sAnhbia = sach.Anhbia;
            dDongia = double.Parse(sach.Giaban.ToString());
            iSoluong = 1;
        }
    }

}