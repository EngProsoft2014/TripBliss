using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using TripBliss.Resources.Language;


namespace TripBliss.Extensions
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty(nameof(Name))]
    public class TranslateExtension : IMarkupExtension , INotifyPropertyChanged
    {

        [Obsolete]
        public TranslateExtension()
        {
            AppResources.Culture = CultureInfo.CurrentCulture;
        }

        [Obsolete]
        public static TranslateExtension Instance { get; } = new();

        public object this[string resourceKey]
            => AppResources.ResourceManager.GetObject(resourceKey, AppResources.Culture) ?? Array.Empty<byte>();

        public string Name { get; set; }

        [Obsolete]
        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            return new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $"[{Name}]",
                Source = TranslateExtension.Instance
            };
        }

        [Obsolete]
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetCulture(CultureInfo culture)
        {
            AppResources.Culture = culture;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}