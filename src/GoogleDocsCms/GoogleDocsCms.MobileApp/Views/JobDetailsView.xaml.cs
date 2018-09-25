using GoogleDocsCms.MobileApp.ViewModels;
using GoogleDocsCms.Shared.Models;
using Xamarin.Forms;

namespace GoogleDocsCms.MobileApp.Views
{
    public partial class JobDetailsView : ContentPage
    {
        public JobDetailsView(JobOffer jobOffer)
        {
            InitializeComponent();

            BindingContext = new JobDetailViewModel(jobOffer);
        }
    }
}