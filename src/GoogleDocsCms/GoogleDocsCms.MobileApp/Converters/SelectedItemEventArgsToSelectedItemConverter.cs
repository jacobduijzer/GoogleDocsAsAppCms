using System;
using System.Globalization;
using Xamarin.Forms;
using GoogleDocsCms.Shared.Models;

namespace GoogleDocsCms.MobileApp.Converters
{
    public class SelectedItemEventArgsToSelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as SelectedItemChangedEventArgs;
            var jobOffer = eventArgs.SelectedItem as JobOffer;

            return jobOffer;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
