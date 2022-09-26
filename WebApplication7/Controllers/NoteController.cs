using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.DataContext;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class NoteController : Controller
    {
        //게시물 리스트 출력
        public IActionResult Index()
        {
            if (!(HttpContext.Session.GetInt32("USER_LOGIN_KEY") is null)){
                using (var db = new AspnetDBContext())
                {
                    var list = db.Notes.ToList();
                    return View(list);
                }
            }
            ModelState.AddModelError(string.Empty, "로그인을 확인하세요");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Note model)
        {
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString()); // userno를 세션에서 받아다가 작성한다
            if (ModelState.IsValid)
            {
                using(var db = new AspnetDBContext())
                {
                    db.Notes.Add(model);
                    if(db.SaveChanges() > 0) return RedirectToAction("Index", "Note");

                    ModelState.AddModelError(string.Empty, "게시물 저장 오류");
                }
            }
            return View(model);
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Detail(int noteNo)
        {
            using(var db = new AspnetDBContext())
            {
                var notecon = db.Notes.FirstOrDefault(A => A.NoteNo.Equals(noteNo));

                return View(notecon);
            }
           
        }
    }
}
