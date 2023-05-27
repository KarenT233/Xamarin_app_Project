using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace project.ModelAndData
{
   public class DataRecord: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DataRecord()
        {
        }
        
        public string TimeSet { get; set; }
        public float Height_var { get; set; }
        public float Weight_var { get; set; }
        public string text { get; set; }
      /*  public object image { get; set; }
        public object voice { get; set; }*/
        public string bpm_record { get; set; }
        public string BP { get; set; }
        public string MedType { get; set; }
        public int Eat_times { get; set; }
        public string Eat_feq { get; set; }
        public bool MenstP { get; set; }
        public List<DateTime> dateTimes{ get; set; }
        public byte[] vs;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

}

