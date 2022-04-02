using Makale.BusinessLayer;
using Makale.Entities;
using Makale.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCProject_Makale.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            NoteYonet ny = new NoteYonet();

            return View(ny.NotListesi().OrderByDescending(x=>x.ModifiedDate).ToList());
        }
        public ActionResult Kategori(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KategoriYonet ky = new KategoriYonet();

            Kategori kat = ky.KategoriBul(id.Value);

            return View("Index",kat.Notes);
        }

        public ActionResult Populer()
        {
            NoteYonet ny = new NoteYonet();

            return View("Index",ny.NotListesi().OrderByDescending(x=>x.LikeCount).Take(6).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                KullaniciYonet ky = new KullaniciYonet();
                BusinessLayerResult<Kullanici> sonuc = ky.LoginUser(model);

                if(sonuc.hata.Count>0)
                {
                    sonuc.hata.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                Session["login"] = sonuc.Nesne;
                return RedirectToAction("Index");

            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {       

                KullaniciYonet ky = new KullaniciYonet();
                BusinessLayerResult<Kullanici> sonuc=ky.KullaniciKaydet(model);
          
                if(sonuc.hata.Count>0)
                {
                    sonuc.hata.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);                    
                }

                return RedirectToAction("RegisterOK");

            }
            return View();
        }
        KullaniciYonet ky = new KullaniciYonet();
        public ActionResult RegisterOK()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult UserActivate(Guid id)
        {
           
           BusinessLayerResult<Kullanici> sonuc= ky.ActivateUser(id);

            if(sonuc.hata.Count>0)
            {
                TempData["error"] = sonuc.hata;
                return RedirectToAction("UserActivateHata");
            }

            return RedirectToAction("UserActivateOK");
        }
        public ActionResult UserActivateOK()
        {
            return View();
        }
        public ActionResult UserActivateHata()
        {
            List<string> hatalar = new List<string>();
            if(TempData["error"]!=null)
            {
                hatalar =(List<string>)TempData["error"];
            }
            return View(hatalar);
        }

        public ActionResult ProfilGoster()
        {
            Kullanici user =(Kullanici)Session["login"];
           BusinessLayerResult<Kullanici> sonuc= ky.KullaniciGetir(user.ID);

            if(sonuc.hata.Count>0)
            {
                //Hata varsa kullanıcıyı başka sayfaya yönlendir.
            }

            return View(sonuc.Nesne);
        }

        public ActionResult ProfileDuzenle()
        {
            Kullanici user = (Kullanici)Session["login"];
            BusinessLayerResult<Kullanici> sonuc = ky.KullaniciGetir(user.ID);

            if (sonuc.hata.Count > 0)
            {
                //Hata varsa kullanıcıyı başka sayfaya yönlendir.
            }

            return View(sonuc.Nesne);
        }

        [HttpPost]
        public ActionResult ProfileDuzenle(Kullanici user,HttpPostedFileBase profileimage)
        {
            ModelState.Remove("ModifiedUsername");

            if(ModelState.IsValid)
            {
                if(profileimage != null && (profileimage.ContentType=="image/png"|| profileimage.ContentType=="image/jpg" || profileimage.ContentType=="image/jpeg" ))
                {
                    string dosyaadi = $"user_{user.ID}.{profileimage.ContentType.Split('/')[1]}";//user_1.jpg
                    profileimage.SaveAs(Server.MapPath($"~/Image/{dosyaadi}"));
                    user.ProfileImageFile = dosyaadi;
                }

                BusinessLayerResult<Kullanici> sonuc = ky.KullaniciUpdate(user);

                if(sonuc.hata.Count>0)
                {
                    //işlemler return
                }

                Session["login"] = sonuc.Nesne;

                return RedirectToAction("ProfilGoster");

            }
         return View(user);

        }

        public ActionResult ProfileSil()
        {
            Kullanici kullanici =(Kullanici)Session["login"];

           BusinessLayerResult<Kullanici> sonuc= ky.KullaniciSil(kullanici.ID);

            if(sonuc.hata.Count>0)
            {
                // hata sayfasına yönlendirme
            }
            Session.Clear();
            return RedirectToAction("Index");
        }


    }
}