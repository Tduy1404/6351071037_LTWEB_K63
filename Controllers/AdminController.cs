using BOOK.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using PagedList.Mvc;
using System.Web;
using System.IO;
using System.Data.Entity.Infrastructure.Design;
using System.Data.Entity.Migrations;
namespace BOOK.Controllers
{
    public class AdminController : Controller
    {
        DataTHB2Entities1 db = new DataTHB2Entities1();

        // GET: Admin
        public ActionResult Index()
        {
            // Trang chủ sau khi đăng nhập thành công
            return View();
        }
        public ActionResult Sach(int? page)
        {

            int pageNum = (page ?? 1);
            int pageSize = 3;

            // Retrieve the 5 latest books


            //return View(sachmoi.ToPagedList(pageNum, pageSize));
            // Trang chủ sau khi đăng nhập thành công
            return View(db.SACHes.ToList().OrderBy(n => n.Masach).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult Login(string UserName, string Password)
        {
            // Kiểm tra nếu URL có tham số UserName và Password
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                // Tìm kiếm thông tin Admin trong cơ sở dữ liệu
                Admin ad = db.Admins.SingleOrDefault(n => n.UsreAdmin == UserName && n.Password == Password);
                if (ad != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["UserAdmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tedn = collection["UserName"];
            var matkhau = collection["Password"];

            if (String.IsNullOrEmpty(tedn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                // Tìm kiếm thông tin Admin trong cơ sở dữ liệu
                Admin ad = db.Admins.SingleOrDefault(n => n.UsreAdmin == tedn && n.Password == matkhau);
                if (ad != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["UserAdmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult ThemMoiSach()
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");


            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiSach(SACH sach, HttpPostedFileBase fileupload)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {

                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Hinhsanpham"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sach.Anhbia = fileName;
                    db.SACHes.Add(sach);

                    db.SaveChanges();

                }
                return RedirectToAction("Sach");
            }


        }
        //public ActionResult ChiTietSP()
        //{
        // return View();
        //}
        //[HttpPost]
        public ActionResult ChiTietSP(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);

            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        public ActionResult Xoasach(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost, ActionName("Xoasach")]
        public ActionResult XacNhanXoa(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SACHes.Remove(sach);
            db.SaveChanges();

            return RedirectToAction("Sach");
        }
        [HttpGet]
        public ActionResult SuaSach(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);

            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", sach.MaCD);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSach(SACH sach, HttpPostedFileBase fileupload)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", sach.MaCD);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);

            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View(sach);
            }
            else
            {

                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Hinhsanpham"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        return View(sach);
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sach.Anhbia = fileName;
                    db.SACHes.AddOrUpdate(sach);
                    //UpdateModel(sach);

                    db.SaveChanges();

                }
                return RedirectToAction("Sach");
            }


        }

        public ActionResult ThongKeSach()
        {
            var booksByCategory = db.SACHes
  .GroupBy(s => s.CHUDE.TenChuDe)  // Assuming `Tenchude` is the category name in CHUDE
  .Select(g => new { Category = g.Key, Count = g.Count() })
  .ToList();

            ViewBag.ChartLabels = booksByCategory.Select(b => b.Category).ToArray();
            ViewBag.ChartData = booksByCategory.Select(b => b.Count).ToArray();

            return View();
        }
    }
}


