using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin;
using project.ModelAndData;
using System.Windows.Input;
using Plugin.AudioRecorder;
using System.IO;

namespace project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class RecordViewPage : ContentPage
    {
        List<GetSetting> setting = new List<GetSetting>();
        List<DataRecord> dataRecords = new List<DataRecord>();
        private readonly AudioRecorderService audioRecorderService = new AudioRecorderService();
        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        SensorSpeed speed = SensorSpeed.UI;
        public RecordViewPage(List<GetSetting> m,List<DataRecord>k, AudioRecorderService audioRecorderService1)
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
            catch (NullReferenceException)
            {
                setting.Add(new GetSetting
                {

                    CountStep_view = true,
                    CountBMI_view = true,
                    BloodPressure_view = true,
                    Heart_Rate_view = true,
                    Medicine_Reminder_view = true,
                    Menstruation_view = true,
                });
            }
            
            Date.Text = DateTime.Now.ToString("dd MMMM yyyy");
          
           
           View_Steps.IsVisible = setting[0].CountStep_view;
           BMI.IsVisible = setting[0].CountBMI_view;
            View_BloodPressure.IsVisible = setting[0].BloodPressure_view;
            View_HeartRate.IsVisible = setting[0].Heart_Rate_view;
            View_MedicineReminder.IsVisible = setting[0].Medicine_Reminder_view;

            try
            {
              
                    GetCanlendar.SpecialDates = new List<XamForms.Controls.SpecialDate>();
                for (int i = 0; i < k[0].dateTimes.Count; i++)
                {
                    GetCanlendar.SpecialDates.Add(new XamForms.Controls.SpecialDate(new DateTime(k[0].dateTimes[i].Ticks)) { BackgroundColor = Color.Yellow, TextColor = Color.Accent, BorderColor = Color.Lime, BorderWidth = 8, Selectable = true });
                    
                }
                BMI.Text = String.Format("BMI:{0}", (k[0].Weight_var / (k[0].Height_var * k[0].Height_var)));
                if (k[0].TimeSet == "Morning")
                {
                    Morning_view.IsVisible = true;
                    Morning_Text.Text= k[0].text;
                    Morning_Photo.Source= ImageSource.FromStream(() =>
                    {
                        var ms = new MemoryStream(k[0].vs);
                        return ms;
                    });
                    audioRecorderService = audioRecorderService1;
                }
                if (k[0].TimeSet == "Lunch")
                {
                    Lunch_view.IsVisible = true;
                    Lunch_Text.Text = k[0].text;
                    Lunch_Photo.Source = ImageSource.FromStream(() =>
                    {
                        var ms = new MemoryStream(k[0].vs);
                        return ms;
                    });
                    audioRecorderService = audioRecorderService1;
                }
                if (k[0].TimeSet == "Dinner")
                {
                    Dinner_view.IsVisible = true;
                    Dinner_Text.Text = k[0].text;
                    Dinner_Photo.Source = ImageSource.FromStream(() =>
                    {
                        var ms = new MemoryStream(k[0].vs);
                        return ms;
                    });
                    audioRecorderService = audioRecorderService1;
                } 
                if (k[0].TimeSet == "Extra")
                {
                    Extra_view.IsVisible = true;
                    Extra_Text.Text = k[0].text;
                    Extra_Photo.Source = ImageSource.FromStream(() =>
                    {
                        var ms = new MemoryStream(k[0].vs);
                        return ms;
                    });
                    audioRecorderService = audioRecorderService1;
                }
                View_HeartRate_content.Text = string.Format("{0}", k[0].bpm_record);
                View_BloodPressure_content.Text = string.Format("{0}", k[0].BP);
                View_MedicineReminder_content.Text = string.Format("{0} still remain {1} times a {2}", k[0].MedType,k[0].Eat_times,k[0].Eat_feq);

            }
            catch (Exception) { }
        }

 

        XamForms.Controls.Calendar GetCalendar = new XamForms.Controls.Calendar()
        {
            WidthRequest = 300,
            HeightRequest = 300
        };

        async void Button_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new EditingRecord(setting));
        }


        /* void Accelerometer_ShakeDetected(object sender, EventArgs e)
         {
             // Process shake event
             MainThread.BeginInvokeOnMainThread(() => {
                 var data = e.GetType(Acceleration.Y);


             });
         }*/
        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var step = e.Reading.Acceleration.Length();
           
            MainThread.BeginInvokeOnMainThread(() =>
            {
               /* int i = 0;
                if (step != 1) 
                {*/
                    Steps.Text = $"Steps: {step}";
                 /*   i++;
                    }
              */
            });
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            /*   Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;*/
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.Game);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
            Accelerometer.Stop();
        }

        private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private void voice_Clicked(object sender, EventArgs e)
        {
            audioPlayer.Play(audioRecorderService.GetAudioFilePath());
        }
    }
}