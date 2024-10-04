using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.ViewModels
{
    public partial class PdfViewerViewModel : BaseViewModel
    {
        [ObservableProperty]
        string uri;
        [ObservableProperty]
        Stream streamContent;

        public PdfViewerViewModel(string uri)
        {
            this.Uri = uri;
            StreamContent = StreamContent;
            SetPdfDocumentStream();
        }

        private async void SetPdfDocumentStream()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(Uri);

            StreamContent = await response!.Content.ReadAsStreamAsync();
            
        }
    }
}
