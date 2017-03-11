using LightWS;
using LightWS.Cms.Model;
using LightWS.Models;
using Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LightWS.Areas.Cms.Controllers
{
    
    public class SystemKeyController : Controller
    {
        MarketingOnlineDBEntities _db = new MarketingOnlineDBEntities();

        // GET: CMS/SystemKey
        public ActionResult Index(SystemKeyViewModel model)
        {            
            return View(model);
        }
        public ActionResult GetList()
        {
            ViewBag.List = _db.SystemKeys.ToList();
            return PartialView("_records", new SystemKeyViewModel());
        }
        public ActionResult DetailForEdit(int id)
        {
            if (id == 0)
            {
                return PartialView("_Insert", new SystemKeyViewModel { });
            }
            else
            {
                var model = _db.SystemKeys.FirstOrDefault(p => p.Id == id);
                return PartialView("_Update", Mapper.MapFrom(model));
            }
        }
        [ValidateInput(false)]
        public JsonResult Insert(SystemKeyViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var model_copy = Mapper.MapFrom(model);
                    _db.SystemKeys.Add(model_copy);
                    _db.SaveChanges();

                    return GetStatus(true);
                }
                catch (Exception ex)
                {
                    return GetStatus(false,ex);
                }
            }
            else
            {
                
                return GetStatus(false);
            }
        }
        [ValidateInput(false)]
        public JsonResult Edit(SystemKeyViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = _db.SystemKeys.FirstOrDefault(p => p.Id == model.Id);
                    item = Mapper.MapFrom(model, item);
                    _db.Entry(item).State = EntityState.Modified;
                    _db.SaveChanges();
                    return GetStatus(true);
                }
                catch (Exception ex)
                {
                    return GetStatus(false, ex);
                }
            }
            else
            {

                return GetStatus(false);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var item = _db.SystemKeys.FirstOrDefault(p => p.Id == id);
                _db.SystemKeys.Remove(item);
                _db.SaveChanges();
                return GetStatus(true);
            }
            catch (Exception ex)
            {
                return GetStatus(false,ex);
            }
            
        }
        public ActionResult _Index()
        {
            return PartialView("_Index", _db.SystemKeys.ToList());
        }
        public JsonResult GetStatus(bool IsSuccess,Exception ex=null)
        {
            if(IsSuccess)
                return Json(new { status = 1, title = "Success", text = "Success", obj = "" }, JsonRequestBehavior.AllowGet);
            if(ex!=null)
                return Json(new { status = -1, title = "Error", text = ex.Message, obj = "" }, JsonRequestBehavior.AllowGet);
            return Json(new { status = -2, title = "Error", text = "Erorr", obj = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}