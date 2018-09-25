using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleDocsCms.MobileApp.Handlers;
using GoogleDocsCms.MobileApp.Views;
using GoogleDocsCms.Shared.Models;
using MvvmHelpers;
using Nito.Mvvm;
using Xamarin.Forms;

namespace GoogleDocsCms.MobileApp.ViewModels
{
    public class JobListViewModel : BaseViewModel
    {
        private readonly JobsService _jobsService;
        private readonly INavigation _navigation;

        public JobListViewModel(INavigation navigation)
        {
            _navigation = navigation;

            _jobsService = new JobsService();

            GetJobsNotifier = NotifyTask.Create(GetJobsAsync);
        }

        public NotifyTask GetJobsNotifier { get; protected set; }

        public IAsyncCommand RefreshCommand => new AsyncCommand(GetJobsAsync);

        public Command<JobOffer> ViewDetailsCommand 
        => new Command<JobOffer>(async (JobOffer jobOffer) => await DoViewDetails(jobOffer).ConfigureAwait(false));

        private List<JobOffer> _jobOffers;
        public List<JobOffer> JobOffers
        {
            get => _jobOffers;
            set => SetProperty(ref _jobOffers, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private async Task GetJobsAsync()
        {
            IsRefreshing = true;
           
            var data = await _jobsService.GetJobOffers()
                             .ConfigureAwait(false);

            if (data != null && data.Any())
                JobOffers = data.Where(x => x.Published).ToList();

            IsRefreshing = false;
        }

        private async Task DoViewDetails(JobOffer jobOffer) => await _navigation.PushAsync(new JobDetailsView(jobOffer))
                                                                                .ConfigureAwait(false);
    }
}