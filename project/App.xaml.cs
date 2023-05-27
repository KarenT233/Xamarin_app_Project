using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;
using project.ModelAndData;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace project
{
    public partial class App : Application
    {

          /*  static Database database;

            public static Database Database
            {
                get
                {
                    if (database == null)
                    {
                        database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people.db3"));
                    }
                    return database;
                }
            */

            public App()
        {
            
            InitializeComponent();
            var setting = new List<GetSetting>();
            MainPage = new NavigationPage(new MainPage(setting));
            

        }
       
        protected override void OnStart()
        {

        }

         protected override void OnSleep()
        {
    
            
        }

        protected override void OnResume()
        {
        }
         
    }
}
