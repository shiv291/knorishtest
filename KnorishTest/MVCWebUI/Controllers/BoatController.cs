using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using Inventory.BusinessLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MVCWebUI.Models;

namespace MVCWebUI.Controllers
{
    public class BoatController : Controller
    {
        IBoatOperation obj = new BoatOperation();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterBoat()
        {
            ViewBag.msg = "";
            return View();
        }

        [HttpPost]
        public IActionResult SaveBoat(BoatRegistration model)
        {
            ViewBag.msg = "";
            try
            {
               
                if (model.BoatImage != null)
                {
                    if (model.BoatImage.Length > 0)
                    {

                        var fileName = Path.GetFileName(model.BoatImage.FileName);

                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        var fileExtension = Path.GetExtension(fileName);
                        var newFileName = String.Concat(myUniqueFileName, fileExtension);
                        string basicPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");
                        var filepath = new PhysicalFileProvider(basicPath).Root + $@"\{newFileName}";
                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            model.BoatImage.CopyTo(fs);
                            fs.Flush();
                        }

                        Boats boatObj = new Boats();
                        boatObj.CreatedDate = DateTime.UtcNow;
                        boatObj.HourRate = model.HourRate;
                        boatObj.IsActive = true;
                        boatObj.Name = model.BoatName;
                        boatObj.ImageUrl = basicPath + "\\" + newFileName;
                        var d = obj.SaveBoat(boatObj);
                        ViewBag.msg = "Boat Number is " + d.ToString();

                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            
            return View("RegisterBoat");
        }

        public IActionResult RentToCustomer()
        {
            ViewBag.msg = "";
            return View();
        }

        [HttpPost]
        public IActionResult SaveRentToCustomer(RentedToCustomerModel model)
        {
            ViewBag.msg = "";
            try
            {
                RentBoatToCustomer r = new RentBoatToCustomer();
                r.CustomerName = model.CustomerName;
                r.BoatId = model.BoatId;
                r.IsReturn = false;
                var result=obj.RentToCUstomer(r);
                ViewBag.msg = "Your booking id :" + result.Id;
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }

            return View("RentToCustomer");
        }

        public IActionResult ReturnBoat()
        {
            ViewBag.msg = "";
            return View();
        }

        [HttpPost]
        public IActionResult returnBoatMethod(string boatNumber)
        {
            ViewBag.msg = "";
            if (string.IsNullOrEmpty(boatNumber))
                return View("ReturnBoat");
            try
            {                
                obj.ReturnBoat(Convert.ToInt64(boatNumber));
                ViewBag.msg = "Boat has been returned";
            }
            catch(Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View("ReturnBoat");
        }

        public IActionResult RemoveBoat()
        {
            ViewBag.msg = "";
            return View();
        }

        [HttpPost]
        public IActionResult removeBoatMethod(string boatNumber)
        {
            ViewBag.msg = "";
            if (string.IsNullOrEmpty(boatNumber))
                return View("RemoveBoat");
            try
            {
                obj.UnregisterBoat(Convert.ToInt64(boatNumber));
                ViewBag.msg = "Boat has been remove";
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View("RemoveBoat");
        }
    }
}