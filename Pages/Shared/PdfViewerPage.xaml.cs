using CommunityToolkit.Maui.Alerts;
using Maui.PDFView;
using Microsoft.Maui.Storage;
using System;

namespace TripBliss.Pages.Shared;

public partial class PdfViewerPage : Controls.CustomControl
{
	public PdfViewerPage(string url)
	{
		InitializeComponent();

        SetPdfDocumentStream(url);
    }

    private async void SetPdfDocumentStream(string url)
    {
        string fileName = "downloadedFile.pdf";  // You can customize the file name
        string filePath = await DownloadPdfFromUrlAsync(url, fileName);

        if (!string.IsNullOrEmpty(filePath))
        {
            await OpenPdfAsync(filePath);
        }
        else
        {
            var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Failed_to_download_the_PDF, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
            await toast.Show();
        }
    }

    public async Task<string> DownloadPdfFromUrlAsync(string pdfUrl, string fileName)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Fetch the PDF content from the URL
                var pdfBytes = await client.GetByteArrayAsync(pdfUrl);

                // Get the platform-specific local directory to save the file
                string localPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                // Save the byte array as a file
                File.WriteAllBytes(localPath, pdfBytes);

                // Return the path to the saved PDF
                return localPath;
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions (e.g., network issues)
            await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.error, $"{TripBliss.Resources.Language.AppResources.Error_downloading_PDF} {ex.Message}", TripBliss.Resources.Language.AppResources.OK);
            return null;
        }
    }

    public async Task OpenPdfAsync(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                // Open the PDF using the platform's default app
                await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
            else
            {
                // Handle file not found case
                await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.error, TripBliss.Resources.Language.AppResources.PDF_file_not_found_at_path + filePath, TripBliss.Resources.Language.AppResources.OK);
            }
        }
        catch (Exception ex)
        {
            // Handle any errors during the opening of the file
            await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.error, $"{TripBliss.Resources.Language.AppResources.Error_opening_PDF} {ex.Message}", TripBliss.Resources.Language.AppResources.OK);
        }
    }
}