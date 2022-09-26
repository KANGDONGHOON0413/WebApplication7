using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.DataContext;
using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class ManageUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        #region 회원가입
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
        #endregion

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginCheck model)
        {
            if (ModelState.IsValid)
            {
                using(var db = new AspnetDBContext())
                {
                    //Linq-메서드 체이닝 식(메서드 뒤에 메서드를 다는것) FirstOrDefault 은
                    //하나만 검색할 때 쓰는 방식으로, 처음값 또는 디폴트값만을 검색
                    //var user = db.Users.FirstOrDefault(A => A.UserID == model.UserID && A.UserPW == model.UserPW); ->메모리를 비효율적으로 사용하게 됨
                    var user = db.Users.FirstOrDefault(A => A.UserID.Equals(model.UserID) && A.UserPW.Equals(model.UserPW));
                    if (!(user is null))
                    {   //로그인에 성공했을 경우
                        //세션에 사용자 정보를 담아서 전송
                        // HttpContext.Session.SetInt32(key, value);
                        HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNo);
                        return RedirectToAction("LoginSuccess", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "사용자 정보가 올바르지 않습니다");
                        return View();
                    }
                }
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("USER_LOGIN_KEY");
            return RedirectToAction("Index", "Home");
        }

    }
}
