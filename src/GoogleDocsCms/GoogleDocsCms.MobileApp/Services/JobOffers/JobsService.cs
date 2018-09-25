using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleDocsCms.MobileApp.Interfaces;
using GoogleDocsCms.Shared.Models;
using SheetToObjects.Lib;
using Xamarin.Forms;

namespace GoogleDocsCms.MobileApp.Handlers
{
    public class JobsService
    {
        private readonly ISecretService _secretService;

        public JobsService()
        {
            _secretService = DependencyService.Resolve<ISecretService>();
        }

        public async Task<List<JobOffer>> GetJobOffers()
        {
            var provider = new SheetToObjects.Adapters.GoogleSheets.ProtectedGoogleSheetAdapter();

            var filePath = _secretService.GetSecretFilePath();

            var sheetData = await provider.GetAsync(filePath,
                                                    "Job offers",
                                                    "1MlqUNhlcBEjx5V8IEa1MzJhQv9Bj-1eUECat1L8fgqM",
                                                    "A:G")
                                          .ConfigureAwait(false);

            var sheetMapper = new SheetMapper().AddConfigFor<JobOffer>(cfg => cfg
                                                                       .MapColumn(column => column.WithHeader("Title").MapTo(m => m.Title))
                                                                       .MapColumn(column => column.WithHeader("Introduction").MapTo(m => m.Introduction))
                                                                       .MapColumn(column => column.WithHeader("Text").MapTo(m => m.Text))
                                                                       .MapColumn(column => column.WithHeader("Tags").MapTo(m => m.Tags))
                                                                       .MapColumn(column => column.WithHeader("Color").MapTo(m => m.Color))
                                                                       .MapColumn(column => column.WithHeader("Date").MapTo(m => m.Date))
                                                                       .MapColumn(column => column.WithHeader("Published")
                                                                                  .WithDefaultValue<bool>(true)
                                                                                  .MapTo(m => m.Published)));

            var data = sheetMapper.Map<JobOffer>(sheetData);
            if (data.IsSuccess)
                return data.ParsedModels
                           .ToList()
                           .Select(x => x.ParsedModel)
                           .ToList();

            return null;
        }
    }
}