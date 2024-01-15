using CRUP_with_Image_MVC.Models;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUP_with_Image_MVC.Controllers
{
    public class HomeController : Controller
    {
        MVC_DBEntities db=new MVC_DBEntities();
        public ActionResult Index()
        {
            var data=db.Employees.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Create()

        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee obj)
        {
            if (ModelState.IsValid==true)
            {
                string fileName = Path.GetFileNameWithoutExtension(obj.Imagefile.FileName);
                string extension = Path.GetExtension(obj.Imagefile.FileName);
                HttpPostedFileBase postedfile = obj.Imagefile;
                int lenght = postedfile.ContentLength;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png") {
                    if(lenght <= 1000000)
                    {
                        fileName = fileName + extension;
                        obj.image_path = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        obj.Imagefile.SaveAs(fileName);
                        db.Employees.Add(obj);
                       int a= db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["sucess"] = "<script> alert('data inserted ') </script>";
                            ModelState.Clear();
                        }
                        else
                        {
                            TempData["error"] = "<script>alert('data not inserted')</script>";

                        }
                    }
                    else
                    {
                        TempData["sizeMessage"] = "<script>alert('Image Size Not Support !')</script>";

                    }
                }
                else
                {
                    TempData["extensionMessage"] = "<script> alert('format not supported')</script>";
                }
               
                
            }
            

            return View();

        }
        public ActionResult Delete(int id)
        {
            
            var data = db.Employees.Find(id);
            string path = Server.MapPath(data.image_path);
            FileInfo file = new FileInfo(path);
            if (file.Exists)//check file exsit or not  
            {
                file.Delete();
                
            }
            db.Employees.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");


        }
        public ActionResult Edit(int id)
        {
            
            var Rowdata = db.Employees.Where(a=>a.id==id).FirstOrDefault();
            Session["imagepath"] = Rowdata.image_path;
            return View(Rowdata);

        }
        [HttpPost]
        public ActionResult Edit( Employee obj)
        {
            if (ModelState.IsValid == true)
            {
                if (obj.Imagefile != null)
                {


                    string fileName = Path.GetFileNameWithoutExtension(obj.Imagefile.FileName);
                    string extension = Path.GetExtension(obj.Imagefile.FileName);
                    HttpPostedFileBase postedfile = obj.Imagefile;
                    int lenght = postedfile.ContentLength;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (lenght <= 1000000)
                        {
                            fileName = fileName + extension;
                            obj.image_path = "~/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            obj.Imagefile.SaveAs(fileName);
                            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                TempData["sucess"] = "<script> alert('data Updated ') </script>";
                                ModelState.Clear();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["error"] = "<script>alert('data not Updated')</script>";

                            }
                        }
                        else
                        {
                            TempData["sizeMessage"] = "<script>alert('Image Size Not Support !')</script>";

                        }
                    }
                    else
                    {
                        TempData["extensionMessage"] = "<script> alert('format not supported')</script>";
                    }
                }
                else
                {
                    obj.image_path = Session["imagepath"].ToString();
                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["sucess"] = "<script> alert('data Updated ') </script>";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "<script>alert('data not Updated')</script>";

                    }
                }

            }
            

            return View();
        }
    }
}