
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using project.ModelAndData;
using System.Collections.Generic;

namespace project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        SQLiteConnection db;
        string dbpath;
        public Login()
        {
            InitializeComponent();
            dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatebase.db");
            db = new SQLiteConnection(dbpath);
            db.CreateTable<UserInfroTable>();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var item = new UserInfroTable()
            {
              
                EnterName = EnterUserName.Text,
                UserPassword = EnterPassword.Text
                
            };

            if (db.Table<UserInfroTable>().Where(i => i.EnterName == item.EnterName).FirstOrDefault() == null)
            {
                db.Insert(item);
                AlertMsg.IsEnabled = true;
                AlertMsg.Text = "No such user, user created";
                AlertMsg.TextColor = Color.White;
            }
            else
            {
                if (db.Table<UserInfroTable>().Where(i => i.EnterName == item.EnterName && i.UserPassword == item.UserPassword).FirstOrDefault()!=null)
                {
                    AlertMsg.IsEnabled = true;
                    AlertMsg.Text = "Loading main page...";
                    AlertMsg.TextColor = Color.Green;
                    var setting = new List<GetSetting>();
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new MainPage(setting)));
                }
                else
                {
                    AlertMsg.IsEnabled = true;
                    AlertMsg.Text = "Password not correct";
                    AlertMsg.TextColor = Color.Red;
                }
            }
            

        }
    }
}