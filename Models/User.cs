using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace proj
{
	public class User : basemodel
	{
		//[Key, Column("UserId")]
		public int UserId{get;set;}

		public string Name{get;set;}

		public string Email{get;set;}

		public string Password{get;set;}

		public int Balance{get;set;}

		public List<Record> Activity{get;set;}

		public DateTime CreatedAt{get;set;}
		public DateTime UpdatedAt{get;set;}

		public User()
		{
			Activity = new List<Record>();
			CreatedAt = DateTime.Now;
			UpdatedAt = DateTime.Now;
		}

	}
}
