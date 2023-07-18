
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NagaCRUD.Models.Domain;

namespace NagaCRUD.Controllers
{
    public class PersonController : Controller
    {
        private readonly DatabaseContext _ctx;
        public PersonController(DatabaseContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {
            ViewBag.Greeting = "You can Add Person on this website";
            ViewData["Greeting1"] = "You can Update person details on this website";
            TempData["Greeting"] = "You can delete person from this website";
            return View();
        }
        public IActionResult AddPerson()
        {
            return View();
        }
        [HttpPost]
            public IActionResult AddPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _ctx.Person.Add(person);
                _ctx.SaveChanges();
                TempData["msg"] = "Added Sucessfully!!";
                return RedirectToAction("AddPerson");
            }
            catch (Exception ex)
            {

                TempData["msg"] = "Could not Added!!";
                return View();
            }
        }
        public IActionResult DisplayPersons()
        {
            var persons = _ctx.Person.ToList();
            return View(persons);
        }


        public IActionResult DeletePerson(int id)
        {
            try
            {
                var person = _ctx.Person.Find(id);
                if (person != null)
                {
                    _ctx.Person.Remove(person);
                    _ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return RedirectToAction("DisplayPersons");
        }

        public IActionResult EditPerson(int id)
        {
            var person = _ctx.Person.Find(id);
            return View(person);
        }

        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _ctx.Person.Update(person);
                _ctx.SaveChanges();
                return RedirectToAction("DisplayPersons");
            }
            catch (Exception ex)
            {

                TempData["msg"] = "Could not Updated!!";
                return View();
            }
        }


        //[HttpPost]
        //public IActionResult AddPerson(Person person)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    TempData["msg"] = "Added";
        //    return View();
        //}

    }
}
