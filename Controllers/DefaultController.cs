using BOOK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
<<<<<<< HEAD
using PagedList;
using PagedList.Mvc;
=======

>>>>>>> e6d7a8a624ea7d3de0dc4f2fa8432bb450a6a75c
namespace BOOK.Controllers
{
    public class DefaultController : Controller
    {

<<<<<<< HEAD
        DataTHB2Entities1 data = new DataTHB2Entities1();
=======
        DataTHB2Entities data = new DataTHB2Entities();
>>>>>>> e6d7a8a624ea7d3de0dc4f2fa8432bb450a6a75c
        // GET: Default

        private List<SACH> Laysachmoi(int count)
        {
            // Sort by descending update date and take the top 'count' records
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

<<<<<<< HEAD
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNum =(page ?? 1);

            // Retrieve the 5 latest books
            var sachmoi = Laysachmoi(20);

            return View(sachmoi.ToPagedList(pageNum, pageSize));
=======
        public ActionResult Index()
        {
            // Retrieve the 5 latest books
            var sachmoi = Laysachmoi(6);

            return View(sachmoi);
>>>>>>> e6d7a8a624ea7d3de0dc4f2fa8432bb450a6a75c
        }





        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }

        public ActionResult NhaXB()
        {
            var xb = from cd in data.NHAXUATBANs select cd;
            return PartialView(xb);
        }

        public ActionResult Details(int id)
        {
            var sach = data.SACHes.SingleOrDefault(s => s.Masach == id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }


        public ActionResult SPTheochude(int id)
        {
           
                var sach = from s in data.SACHes where s.MaCD == id select s;
                return View(sach);
            

        }
        public ActionResult SPTheoNhaXB(int id )
        {
                var sach = from s in data.SACHes where s.MaNXB == id select s;
                return View(sach);
            

        }
    }
}