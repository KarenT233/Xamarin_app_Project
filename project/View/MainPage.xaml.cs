using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using project.ModelAndData;
using System.IO;
using project.View;
using Plugin.AudioRecorder;
using Xamarin.Essentials;

namespace project
{

    public partial class MainPage : MasterDetailPage
    {
        private readonly AudioRecorderService audioRecorderService = new AudioRecorderService();
        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        bool CaseStep = true;
        bool CaseBMI = true;
        bool CaseMenstruation = true;
        bool CaseHeart_Rate = true;
        bool CaseBloodPressure = true;
        bool CaseMedicine_Reminder = true;
        Image image;
        List<GetSetting> setting = new List<GetSetting>();
        List<DataRecord> dataRecords = new List<DataRecord>();
        public MainPage(List<GetSetting> m)
        {
           
            InitializeComponent();
         
            try
            {
                setting.Add(new GetSetting
                {

                    CountStep_view = m[0].CountStep_view,
                    CountBMI_view = m[0].CountBMI_view,
                    BloodPressure_view = m[0].BloodPressure_view,
                    Heart_Rate_view = m[0].Heart_Rate_view,
                    Medicine_Reminder_view = m[0].Medicine_Reminder_view,
                    Menstruation_view = m[0].Menstruation_view,
                });

            }
            catch (Exception) { info.Text = string.Format("deflaut setting");
                setting.Add(new GetSetting
                {

                    CountStep_view = CaseStep,
                    CountBMI_view = CaseBMI,
                    BloodPressure_view = CaseBloodPressure,
                    Heart_Rate_view = CaseHeart_Rate,
                    Medicine_Reminder_view = CaseMedicine_Reminder,
                    Menstruation_view = CaseMenstruation,
                });
            }
            Notice.IsVisible = setting[0].Medicine_Reminder_view;

        }

        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
          
            var param = ((TappedEventArgs)e).Parameter;
            if (param.ToString() == "Record")
            {
                
                await Navigation.PushAsync(new RecordViewPage(setting,dataRecords,audioRecorderService));
            }

            if(param.ToString()== "Medicine Help")
            {
                await Navigation.PushAsync(new MedicineHelpView());
            }

        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (IsPresented == false) { IsPresented = true; }
            if (IsPresented == true) { IsPresented = false; }
        }
        
        async protected override void OnAppearing()
        {
            base.OnAppearing();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatebase.db");
            var db = new SQLiteConnection(dbpath);
            try
            {
                var data = db.Table<UserInfroTable>(); //Call Table  
                foreach (var item in data)
                {
                    UserName.Text = item.EnterName;
                    UserGivenID.Text = item.UserID.ToString();

                }
            }
            catch (SQLiteException ex)
            {
                await Navigation.PushAsync(new Login()); 
            }


        }

        private async void Test_ID_Logout_Not_true_Clicked(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatebase.db");
            var db = new SQLiteConnection(dbpath);
            db.Table<UserInfroTable>().Delete(x=>x.EnterName== UserName.Text); //Call Table  
            
               await Navigation.PushAsync(new Login());

                /*  Task<int> DeleteItemAsync(UserInfroTable table) => db.DeleteAsync(data);*/
        }

        private async void Setting_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Setting());
        }

        private async void TapGestureRecognizer_Tapped_Icon(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "please pick a photo" });
                var stream = await photo.OpenReadAsync();
                Icon.Source = ImageSource.FromStream(() => stream);


            }
            catch (Exception)
            {
                // Feature is not supported on the device

            }
        }
    }
}
