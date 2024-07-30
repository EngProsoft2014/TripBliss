namespace TripBliss.Pages;

public partial class QrCodePage : ContentPage
{
	public QrCodePage()
	{
		InitializeComponent();
	}

    private void cameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {

    }
}