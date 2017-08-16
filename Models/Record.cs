using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace proj
{
	public class Record : basemodel
	{
		public int RecordId{get;set;}

		public int Amount{get;set;}

		//[ForeignKey("UserId")]
		public int UserId{get;set;}
		public User User{get;set;}

		public DateTime CreatedAt{get;set;}
		public DateTime UpdatedAt{get;set;}

		public Record()
		{
			CreatedAt=DateTime.Now;
			UpdatedAt=DateTime.Now;
		}
	}
}
