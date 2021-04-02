using Xamarin.Essentials;
using Xamarin.Forms;

namespace Netflix.Helpers
{
    public class TemplateSelector : DataTemplateSelector
    {
        public DataTemplate BaseTemplate { get; set; }
        public DataTemplate SearchTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) => string.IsNullOrWhiteSpace(Preferences.Get("search", string.Empty)) ? BaseTemplate : SearchTemplate;

    }
}
