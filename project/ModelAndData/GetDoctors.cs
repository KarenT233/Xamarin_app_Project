using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace project.ModelAndData
{
    public class GetDoctors 
    {
        public GetDoctors()
        {
        }
        public string DocName { get; set; }
        public string DocLocation { get; set; }
        public string DocOpenHr { get; set; }
        public string DocNO { get; set; }
        public ImageSource DocPhoto { get; set; }
        public string DocType { get; set; }
    }
   }
