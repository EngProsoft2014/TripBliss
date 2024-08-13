using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using TripBliss.Resources;
using TripBliss.Resources.Language;

namespace TripBliss.Helpers
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {


        readonly CultureInfo ci = null;
        const string ResourceId = "TripBliss.Resources.Language.AppResources";

        [Obsolete]
        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                ci = AppResources.Culture;// DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

            }
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {

            if (Text == null)
                return "";

            ResourceManager temp = new ResourceManager(ResourceId, typeof(AppResources).GetTypeInfo().Assembly);

            var translation = temp.GetString(Text, ci);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                          String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
                          "Text");
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}