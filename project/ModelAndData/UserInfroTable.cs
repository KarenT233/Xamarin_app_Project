using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLite;
namespace project.ModelAndData
{
    public class UserInfroTable
    {
        public Guid UserID { get; set; }
        public string EnterName { get; set; }
        public string UserPassword { get; set; }
    }
  

}
