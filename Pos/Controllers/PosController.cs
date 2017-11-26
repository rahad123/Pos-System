using System;
using Pos.Models;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;

namespace Pos.Controllers
{
    public class POSController : Controller
    {
        //
        // GET: /POS/

        public ApplicationDbContext data;
        public POSController()
        {
           
            data = new ApplicationDbContext();
        }
        public ActionResult Login()
        {
            Session["userId"] = Guid.NewGuid();
            return View();
        }

        public ActionResult Admin()
        {
            var items = data.Items.ToList();
            return View(items);
        }

        public ActionResult AddItem(Item item)
        {
            if(item.Name==null || item.Price==0)
            {
                Session["msg"] = "Please fill-up all field";
                return RedirectToAction("Add", "Pos");
            }
            data.Items.Add(item);
            data.SaveChanges();
            var items = data.Items.ToList();
            return View(items);
        }

        public ActionResult Add()
        {

            return View();
        }

        public ActionResult Edit(int itemId)
        {
            var items = GetItem(itemId);
            return View(items);
            
        }

        public ActionResult Update(Item item)
        {
            var dataitem = GetItem(item.Id);
            dataitem.Name = item.Name;
            dataitem.Price = item.Price;
            data.SaveChanges();
            return RedirectToAction("Admin");
        }

        public ActionResult Delete(int itemId)
        {
            var deleteData = data.Items.FirstOrDefault(m => m.Id == itemId);
            return View(deleteData);
        }

        public ActionResult DeleteItem(int itemId)
        {
            var deleteData = GetItem(itemId);
                data.Items.Remove(deleteData);
                data.SaveChanges();
                var dataList = data.Items.ToList();
            return RedirectToAction("Admin",dataList);
        }

        public ActionResult Customer()
        {
            var items = data.Items.ToList();
            return View(items);
        }
        public ActionResult Buy(int Id, int Quantity)
        {
            var Item = GetItem(Id);
            var boughtItem = new BoughtItem()
            {
                Quantity = Quantity,
                UserId = (Guid)Session["userId"],
                Item = Item,
                ItemId = Id
            };
            data.BoughtItems.Add(boughtItem);
            data.SaveChanges();
            
            return View(boughtItem);
        }

        public ActionResult ParchasesList()
        {
            var UserId = (Guid) Session["userId"];
            var boughtItemList = GetBoughtItemList(UserId);
            return View(boughtItemList);
        }

        public Item GetItem(int itemId)
        {
            return data.Items.FirstOrDefault(m=>m.Id == itemId);
        }

        public List<BoughtItem> GetBoughtItemList(Guid userId)
        {
            return data.BoughtItems.Where(m => m.UserId == userId).Include("Item").ToList();
        }

        public BoughtItem GetBoughtItem(int itemId)
        {
            return data.BoughtItems.FirstOrDefault(m => m.ItemId == itemId);
        }
    }
}