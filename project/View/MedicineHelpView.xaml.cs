using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.ModelAndData;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace project
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class MedicineHelpView : ContentPage
    {
   
        public MedicineHelpView()
        {

            InitializeComponent();
            DoctersResults.ItemsSource= LoadData();
        }
        #region LoadData();
        protected IList<GetDoctors> LoadData()
        {
            var doctors = new List<GetDoctors>();
            doctors.Add(new GetDoctors
            {
                
                DocName = "Wrong Tim Yi",
                DocLocation = "Tung Chung",
                DocOpenHr = "1100 - 2300  \nclosed on Mon &Fri &Sun",
                DocNO = "+852 - 71100010",
                DocPhoto = ImageSource.FromFile("Dr1.jpg"),
                DocType = " ophthalmology / Internal Medicine"
            });
            doctors.Add(new GetDoctors
            {
                DocName = "Alexsder Iris",
                DocLocation = "Tai Ko",
                DocOpenHr = "1300 - 2000  \n closed on weekend",
                DocNO = "+852 - 23302010",
                DocPhoto = ImageSource.FromFile("Dr1.jpg"),
                DocType = " ophthalmology / Internal Medicine"
            });
            doctors.Add(new GetDoctors
            {
                DocName = "Eric Kin",
                DocLocation = "Jordon",
                DocOpenHr = "1800 - 0000  \n closed on Monday",
                DocNO = "+852 - 29999910",
                DocPhoto = ImageSource.FromFile("Dr1.jpg"),
                DocType = " Psychiatry"
            });
            doctors.Add(new GetDoctors
            {
                DocName = "Jess Yau",
                DocLocation = "Sha Tin",
                DocOpenHr = "0000 - 0800  \n closed on Monday",
                DocNO = "+852 - 27779910",
                DocPhoto = ImageSource.FromFile("Dr2.jpg"),
                DocType = " Tuberculosis and Chest "
            });
            return doctors;
        }
        #endregion

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Keyword = FindingBar.Text.ToLower();
            DoctersResults.ItemsSource = LoadData().Where(name => name.DocName.ToLower().Contains(Keyword) || name.DocLocation.ToLower().Contains(Keyword));
        }

        private void DoctersResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var number = e.SelectedItemIndex;
            var doctors = LoadData();
            PhoneDialer.Open(doctors[number].DocNO.ToString());
        }
        private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            var result = await DisplayAlert("Back to Home", "If you exit this page, the input data will be clear", "Leave", "Stay");
            if (result == true) { await Navigation.PopToRootAsync(); }

        }
    }
    /*protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if(BindingContext != null)

    }*/




}

