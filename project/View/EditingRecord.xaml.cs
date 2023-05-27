using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.AudioRecorder;
using project.ModelAndData;
using Plugin.Media;
using System.IO;

namespace project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class EditingRecord : ContentPage
    {
        private readonly AudioRecorderService audioRecorderService = new AudioRecorderService();
        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        List<GetSetting> setting = new List<GetSetting>();
        List<DataRecord> dataRecords = new List<DataRecord>();
        string Time="No";
        float height=0;
        float weight=0;
        string text_Rec= "Haven't Record";
        object image_Rec;
        object voice_Rec;
        string Bpm_record = "Haven't Record";
        string bp= "Haven't Record";
        string med_Type = " No type yet";
        int eat_times = 0;
        string eat_feq="Days";
        bool menstP=false;
        byte[] bytes;
        Stream stream;
        List<DateTime> dateT=new List<DateTime>();
        StreamImageSource StreamImage;
        public EditingRecord(List<GetSetting> m)
        {
            InitializeComponent();
            Date.Text = GetCanlendar.SelectedDate.ToString();
            VoiceOut.Maximum = audioRecorderService.TotalAudioTimeout.TotalSeconds;
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
            catch (Exception)
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
           
            BMI.IsVisible = setting[0].CountBMI_view;
            BloodPressure.IsVisible = setting[0].BloodPressure_view;
            HeartRate.IsVisible = setting[0].Heart_Rate_view;
            MedicineReminder.IsVisible = setting[0].Medicine_Reminder_view;
            Menstruation.IsVisible=setting[0].Menstruation_view;
            
        }

        private async void RecordButton_Clicked(object sender, EventArgs e)
        {
            ImageButton Type = sender as ImageButton;
            if (Type == Audio)
            {
                TextRecord.IsVisible = false;
                PhotoRecord.IsVisible =false;
                VoiceOut.IsVisible=true;
              
                var status = await Permissions.RequestAsync<Permissions.Microphone>();
                if (status != PermissionStatus.Granted) { return; }
                if (audioRecorderService.IsRecording)
                {
                   await audioRecorderService.StopRecording();
                    audioPlayer.Play(audioRecorderService.GetAudioFilePath());
                    if (audioRecorderService.IsRecording == false)
                    {
                        for (double i = 0; i <= VoiceOut.Maximum; i=i+0.1)
                        {
                            VoiceOut.Value = VoiceOut.Value + 0.1;
                        }
                    }
                    else { VoiceOut.Value = 0; }

                }
                else
                {
                   await audioRecorderService.StartRecording();
                }
            }
            if (Type==Text)
            {
                TextRecord.IsVisible = true;
                PhotoRecord.IsVisible = false;
                VoiceOut.IsVisible = false;
            }
            if (Type== Photo)
            {

                TextRecord.IsVisible = false;
                PhotoRecord.IsVisible = true;
                VoiceOut.IsVisible = false;
                if (MediaPicker.IsCaptureSupported == true)
                {
                    var Photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Take a photo"
                    });
                    if (Photo != null)
                    {
                        var Steam = await Photo.OpenReadAsync();
                      
                        stream = Steam;
                        try
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {

                                stream.CopyTo(ms);
                                
                                bytes = ms.ToArray();
                                ms.Dispose();
                            }
                        }
                      catch(Exception)
                        {
                        }
                        Phototaken.Source =ImageSource.FromStream(() =>
                        {
                            var ms = new MemoryStream(bytes);
                            return ms;
                        });
                    }
                }

            }
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked == true&& times.SelectedIndex!=0)
            {
                /*var check = times.SelectedIndex;*/
               times.SelectedIndex-= 1;
            }
            
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            eat_times = times.SelectedIndex;
            eat_feq = Length.SelectedItem.ToString();
            menstP = Switch_Color.IsToggled;
            voice_Rec = VoiceOut;
            image_Rec = PhotoRecord;
            try
            {
                if (Height.Text != null)
                {
                    height = float.Parse(Height.Text) / 100;
                }
                if (Weight.Text != null)
                {
                    weight = float.Parse(Weight.Text);
                }
                if (TextRecord.Text != null)
                {
                    text_Rec = TextRecord.Text;
                }
               

                if (HeartRateBPM.Text != null)
                {
                    Bpm_record = HeartRateBPM.Text;
                }
                if (BloodPressureSystolic.Text != null && BloodPressureDiastolic.Text != null)
                {
                    bp = String.Format(" {0}/{1}mm Hg", BloodPressureSystolic.Text, BloodPressureDiastolic.Text);
                }
                if (MedicineType.Text != null)
                {
                    med_Type = MedicineType.Text;
                }
                

            }
            catch (Exception) { };
            try
            {
               
                dataRecords.Add(new DataRecord
                {
                    Height_var = height,
                    Weight_var = weight,
                    TimeSet = Time,
                    text = text_Rec,
                    /* image = image_Rec,
                     voice = voice_Rec,*/
                    BP = bp,
                    bpm_record = Bpm_record,
                    MedType = med_Type,
                    Eat_times = eat_times,
                    Eat_feq = eat_feq,
                    MenstP = menstP,
                    dateTimes = dateT,
                    vs = bytes
                }) ;
            }
            catch (Exception)
            {
               
            }
            await Navigation.PushAsync(new RecordViewPage(setting,dataRecords, audioRecorderService));
        }

        private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            var result = await DisplayAlert("Back to Home", "If you exit this page, the input data will be clear", "Leave","Stay");
            if (result == true) { await Navigation.PopToRootAsync(); }
           
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
           
            if (radioButton == Morning)
            {
                Time = "Morning";
            }
            if (radioButton == Lunch)
            {
                Time = "Lunch";
            }
            if (radioButton == Dinner)
            {
                Time = "Dinner";
            }
            if (radioButton == Extra)
            {
                Time = "Extra";
            }

        }

        private void GetCanlendar_DateClicked(object sender, XamForms.Controls.DateTimeEventArgs e)
        {
            
            if (Switch_Color.IsToggled == true)
            {
                dateT.Add(e.DateTime);
                GetCanlendar.SpecialDates = new List<XamForms.Controls.SpecialDate>();
                for (int i = 0; i < dateT.Count; i++)
                {
                    GetCanlendar.SpecialDates.Add(new XamForms.Controls.SpecialDate(new DateTime(dateT[i].Ticks)) { BackgroundColor = Switch_Color.OnColor, TextColor = Color.Accent, BorderColor = Color.Lime, BorderWidth = 8, Selectable = true });
                }

            }
        }
    }
}