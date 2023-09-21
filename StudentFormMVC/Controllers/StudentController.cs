using StudentFormMVC.DAL;
using StudentFormMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StudentFormMVC.Controllers
{
    public class StudentController : Controller
    {
        private StudentDBContext data = new StudentDBContext();
        // GET: Student
        public ActionResult Index()
        {
            //list k student model a raksi
            var db = data.Students.ToList();
            return View(db);
        }

        //Create
        //create=action method
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //The[Bind] attribute will let you specify the exact properties of a model should include or exclude in binding.
        ////Students connectd to model ,students=parameter
      public ActionResult Create([Bind(Include = "Id,Name,Email,Phone,Address,Gender,DateOfBirth , IsActive")] Students students)
        {
            try
            {
                //ModelState is a collection of name and value pairs that are submitted to the server during a POST
                if (ModelState.IsValid)
                {
                    data.Students.Add(students);
                    data.SaveChanges();
                    //Redirects to the specified action using the action name, controller name, and route dictionary.
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to Change");
            }
            return View(students);
        }
        
        //Details
        public ActionResult Details(long? id)
        {
            if(id==null)
            {
                //Initializes a new instance of the HttpStatusCodeResult class using a status code.
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Students students = data.Students.Find(id);
            if(students==null)
            {
                return HttpNotFound();
            }
            return View(students);

        }

        //Delete
        //get
        public ActionResult Delete(long? id) {
            if(id==null)
            {
                //Initializes a new instance of the HttpStatusCodeResult class using a status code.
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Students students = data.Students.Find(id);
            if(students==null)
            {
                return HttpNotFound();
            }
            return View(students);
        }
        //delete post
        [HttpPost]
        public ActionResult Delete(long id)
        {
           
            Students students= data.Students.Find(id);
            data.Students.Remove(students);
            data.SaveChanges();

            return RedirectToAction("Index");
        }

        //Edit//get
        public ActionResult Edit(long? id) { 
        if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        Students students= data.Students.Find(id);
            if(students==null)
            {
                return HttpNotFound();
            }
            return View(students);  
            }
        //edit//post
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,Address,Gender,DateOfBirth,IsActive")] Students student)
        {
            if (ModelState.IsValid)
            {
                data.Entry(student).State = EntityState.Modified; ;
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }
    }
}