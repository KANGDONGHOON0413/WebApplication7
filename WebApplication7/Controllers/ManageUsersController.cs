using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.DataContext;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class ManageUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //default값은 get방식이다
        //html값을 받아오는 것은 get방식으로 한다
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User model)
        {
            //validation check
            if (ModelState.IsValid)
            {
                using var db = new AspnetDBContext();
                db.Users.Add(model);    //메모리에 추가/적제
                db.SaveChanges();       //db로 추가/적제
                return RedirectToAction("Index", "Home");   //Home컨트롤러의 Index액션으로 넘기겠다는 의미이다.
            }
            
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
