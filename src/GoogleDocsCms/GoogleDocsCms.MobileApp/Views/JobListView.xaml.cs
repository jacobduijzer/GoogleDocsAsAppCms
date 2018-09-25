
using GoogleDocsCms.MobileApp.ViewModels;
using Xamarin.Forms;

namespace GoogleDocsCms.MobileApp.Views
{
    public partial class JobListView : ContentPage
    {
        public JobListView()
        {
            InitializeComponent();

            BindingContext = new JobListViewModel(this.Navigation);
        }
    }
}