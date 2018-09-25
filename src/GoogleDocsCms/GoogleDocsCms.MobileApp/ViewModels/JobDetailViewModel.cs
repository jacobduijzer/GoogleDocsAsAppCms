using System;
using GoogleDocsCms.Shared.Models;
using MvvmHelpers;

namespace GoogleDocsCms.MobileApp.ViewModels
{
    public class JobDetailViewModel : BaseViewModel
    {
        public JobDetailViewModel(JobOffer jobOffer)
        => JobDetails = jobOffer;

        private JobOffer _jobDetails;
        public JobOffer JobDetails
        {
            get => _jobDetails;
            set => SetProperty(ref _jobDetails, value);
        }
    }
}
