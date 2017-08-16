using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace proj
{
	public class projController:Controller
	{
		//private readonly DbConnector _dbConnector;
		//private readonly UserFactory userFactory;
		private projContext _context;

		public projController(projContext context)//DbConnector connect)
		{
			//_dbConnector = connect;
			//userFactory = new UserFactory();
			_context = context;
		}

		[HttpGet]
		[Route("default")]
		public IActionResult Default()
		{
			return View();
		}

		[HttpGet]
		[Route("")]
		public IActionResult Index()
		{
			//ViewBag.Users = userFactory.FindAll();
			string result = HttpContext.Session.GetString("Email");
			if(result=="Null")
			{
				ViewBag.Msg = "Login failed!";
				return View();
			}
			if(result == null)
				return View();
			
			return RedirectToAction("Activity");
		}

		[HttpGet]
		[Route("activity")]
		public IActionResult Activity()
		{
			string email = HttpContext.Session.GetString("Email");
			if(email==null)
				return RedirectToAction("");

			User currUser = _context.Users.SingleOrDefault(user=>user.Email == email);

			ViewBag.User = currUser;
			ViewBag.Activity = _context.Records.Where(rec => rec.UserId == currUser.UserId).ToList();

			return View();
		}

		[HttpPost]
		[Route("act")]
		public IActionResult Act(int amount)
		{
			User user = _context.Users.SingleOrDefault(u=>u.Email==HttpContext.Session.GetString("Email"));
			if(amount > 0 || user.Balance + amount > 0)
			{
				Record NewRec = new Record
				{
					Amount = amount,
					UserId = user.UserId
				};
				_context.Add(NewRec);
				user.Balance += amount;
			}
			_context.SaveChanges();
			return Redirect("Activity");
		}

		[HttpGet]
		[Route("register")]
		public IActionResult Register()
		{ return View(); }
		[HttpPost]
		[Route("reg")]
		public IActionResult Reg(RegisterUser model)
		{
			if(ModelState.IsValid)
			{
				User NewUser = new User
				{
					Name = model.Name,
					Email = model.Email,
					Password = model.Password,
					Balance = 0
				};
				_context.Add(NewUser);
				_context.SaveChanges();
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		[Route("login")]
		public IActionResult Login(string email, string password)
		{
			User loggeduser = _context.Users.SingleOrDefault(user => user.Email == email);
			HttpContext.Session.SetString("Email", "Null");
			if(loggeduser != null)
				if(loggeduser.Password == password)
					HttpContext.Session.SetString("Email", email);
			
			return RedirectToAction("Index");
		}	

	}
}
