using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.ModelAndData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace project.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Setting : ContentPage
    {
    bool CaseStep=true;
    bool CaseBMI = true;
    bool CaseMenstruation = true;
    bool CaseHeart_Rate = true;
    bool CaseBloodPressure = true;
    bool CaseMedicine_Reminder = true;
    public Setting()
        {
            InitializeComponent();
            if (RecordMenstruation.IsEnabled == false && RecordMenstruation.BackgroundColor == Color.Plum)
            {
                CaseMenstruation = false;
                RecordMenstruation.BackgroundColor = Color.White;
            }
   


        }

        private async void Set_Clicked(object sender, EventArgs e)
        {
           var setting = new List<GetSetting>();
            setting.Add(new GetSetting
            {

            CountStep_view = CaseStep,
             CountBMI_view=CaseBMI,
             BloodPressure_view=CaseBloodPressure,
             Heart_Rate_view=CaseHeart_Rate,
             Medicine_Reminder_view=CaseMedicine_Reminder,
             Menstruation_view=CaseMenstruation,
            });
       
            await Navigation.PushAsync(new MainPage(setting));
        }

        private void Count_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button == CountSteps&& CaseStep == true)
            {
                CaseStep = false;
                CountSteps.BackgroundColor = Color.White;
            }
          else  if (button == CountSteps && CaseStep != true) { 
                CaseStep = true;
                CountSteps.BackgroundColor = Color.Plum;
            }

            if (button ==CountBMI && CaseBMI == true)
            {
                CaseBMI = false;
                CountBMI.BackgroundColor = Color.White;
            }
            else if (button == CountBMI && CaseBMI != true)
            {
                CaseBMI = true;
                CountBMI.BackgroundColor = Color.Plum;
            }

            if (button == RecordHeartRate && RecordHeartRate.BackgroundColor == Color.Plum)
            {
                CaseHeart_Rate = false;
                RecordHeartRate.BackgroundColor = Color.White;
            }
            else if (button == RecordHeartRate && RecordHeartRate.BackgroundColor != Color.Plum)
            {
                CaseHeart_Rate = true;
                RecordHeartRate.BackgroundColor = Color.Plum;
            }

            if (button == RecordBloodPressure && RecordBloodPressure.BackgroundColor == Color.Plum)
            {
                CaseBloodPressure = false;
                RecordBloodPressure.BackgroundColor = Color.White;
            }
            else if (button == RecordBloodPressure && RecordBloodPressure.BackgroundColor != Color.Plum)
            {
                CaseBloodPressure = true;
                RecordBloodPressure.BackgroundColor = Color.Plum;
            }

         if (button == RecordMedicine_Reminder && button == RecordMedicine_Reminder && RecordMedicine_Reminder.BackgroundColor == Color.Plum)
            {
                CaseMedicine_Reminder = false;
                RecordMedicine_Reminder.BackgroundColor = Color.White;
            }
            else if (button == RecordMedicine_Reminder && RecordMedicine_Reminder.BackgroundColor != Color.Plum)
            {
                CaseMedicine_Reminder = true;
                RecordMedicine_Reminder.BackgroundColor = Color.Plum;
            }

            if (button ==RecordMenstruation && RecordMenstruation.BackgroundColor == Color.Plum)
            {
                CaseMenstruation = false;
                RecordMenstruation.BackgroundColor = Color.White;
            }
            else if (button == RecordMenstruation && RecordMenstruation.BackgroundColor != Color.Plum)
            {
                CaseMenstruation = true;
                RecordMenstruation.BackgroundColor = Color.Plum;
            }
        }

        private void Girl_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.IsChecked == false)
            {
                CaseMenstruation = false;
                RecordMenstruation.BackgroundColor = Color.White;
            }
        }
        private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}