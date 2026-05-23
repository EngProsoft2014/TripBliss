using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Places.Details.Request;
using Mopups.Services;
using TripBliss.Models;

namespace TripBliss.Pages.MainPopups;

public partial class HotelsPopup : Mopups.Pages.PopupPage
{
    public delegate void CustomDelegte(SuggestionAddressModel str);
    public event CustomDelegte DidClose;

    public static string Lan { get; set; }

    public HotelsPopup()
	{
		InitializeComponent();

        Lan = Preferences.Default.Get("Lan", "");
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }


    private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        countryListView.IsVisible = true;
        countryListView.BeginRefresh();

        try
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    List<SuggestionAddressModel> x = await GetPlacesAutocompleteAsync(searchBar.Text);
                    //string[] x = await GetPlacesAutocompleteAsync(searchBar.Text);
                    countryListView.ItemsSource = x;
                }
                catch (Exception err)
                {
                    System.Diagnostics.Debug.WriteLine("Exception with autocomplete: " + err.Message + " stack :" + err.StackTrace);
                }
            });
        }
        catch (Exception ex)
        {
            countryListView.IsVisible = false;

        }
        countryListView.EndRefresh();
    }

    private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is not SuggestionAddressModel listed) return;

        try
        {
            // ✅ Use hotel name directly
            searchBar.Text = listed.MainAddress;

            countryListView.IsVisible = false;
            ((ListView)sender).SelectedItem = null;

            DidClose?.Invoke(listed);
            await MopupService.Instance.PopAsync();
        }
        catch (Exception ex)
        {
            await App.Current!.MainPage!.DisplayAlert("Error", ex.Message, "OK");
        }
    }


    private async void OnImageNameTapped_CloseIcon(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }


    public static async Task<List<SuggestionAddressModel>> GetPlacesAutocompleteAsync(string search)
    {
        var request = new GoogleApi.Entities.Places.AutoComplete.Request.PlacesAutoCompleteRequest
        {
            Input = search,
            Key = "AIzaSyA6nc23HAg2fa8pGaolThKga40bUtZYpxE",
            Language = Lan == "ar" ? GoogleApi.Entities.Common.Enums.Language.Arabic
                       : GoogleApi.Entities.Common.Enums.Language.English,
            RestrictType = GoogleApi.Entities.Places.AutoComplete.Request.Enums.RestrictPlaceType.Establishment
        };

        var response = await GoogleApi.GooglePlaces.AutoComplete.QueryAsync(request, null);

        // Filter only hotels (lodging)
        var hotelPredictions = response.Predictions
            .Where(p => p.Types.Contains(PlaceLocationType.Lodging))
            .ToList();

        // Fetch details for each hotel prediction
        var detailsTasks = hotelPredictions.Select(prediction =>
        {
            var detailsRequest = new PlacesDetailsRequest
            {
                Key = request.Key,
                PlaceId = prediction.PlaceId.ToString(),
                Language = Lan == "ar" ? GoogleApi.Entities.Common.Enums.Language.Arabic
                                       : GoogleApi.Entities.Common.Enums.Language.English
            };

            return GoogleApi.GooglePlaces.Details.QueryAsync(detailsRequest);
        }).ToList();

        var detailsResponses = await Task.WhenAll(detailsTasks);

        var suggestions = detailsResponses.Select(detailsResponse =>
        {
            var addressComponents = detailsResponse.Result.AddressComponents;

            string[] ArrAdd = new string[7];

            foreach (var itemAdd in addressComponents)
            {
                switch (itemAdd.Types.FirstOrDefault())
                {
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Street_Number:
                        {
                            ArrAdd[0] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Route:
                        {
                            ArrAdd[1] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_3:
                        {
                            ArrAdd[2] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_2:
                        {
                            ArrAdd[3] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_1:
                        {
                            ArrAdd[4] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Country:
                        {
                            ArrAdd[5] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Postal_Code:
                        {
                            ArrAdd[6] = itemAdd.LongName;
                        }
                        break;
                }
            }

            return new SuggestionAddressModel
            {
                PalceId = detailsResponse.Result.PlaceId,
                Latitude = detailsResponse.Result.Geometry.Location.Latitude,
                Longitude = detailsResponse.Result.Geometry.Location.Longitude,
                FullAddress = ArrAdd[0] + " " + ArrAdd[1] + " " + ArrAdd[2] + " " + ArrAdd[3] + " " + ArrAdd[4] + " " + ArrAdd[5],
                FullAddressAr = ArrAdd[0] + " " + ArrAdd[1] + " " + ArrAdd[2] + " " + ArrAdd[3] + " " + ArrAdd[4] + " " + ArrAdd[5],

                MainAddress = detailsResponse.Result.Name, // hotel name 
                //MainAddress = ArrAdd[0] + " " + ArrAdd[1] + " " + ArrAdd[2],

                SubAddress = ArrAdd[3] + " " + ArrAdd[4],
                Street = ArrAdd[1],
                StreetAr = ArrAdd[1],
                Locality = ArrAdd[2],
                LocalityAr = ArrAdd[2],
                State = ArrAdd[3],
                StateAr = ArrAdd[3],
                City = ArrAdd[4],
                CityAr = ArrAdd[4],
                Country = ArrAdd[5],
                CountryAr = ArrAdd[5],
                Zip = ArrAdd[6],
            };
        }).ToList();

        return suggestions;
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        if (stkManuallyAddress.IsVisible == false)
        {
            stkManuallyAddress.IsVisible = true;
            searchBar.IsVisible = false;
            countryListView.IsVisible = false;
        }
        else
        {
            stkManuallyAddress.IsVisible = false;
            searchBar.IsVisible = true;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        SuggestionAddressModel Listed = new SuggestionAddressModel();
        Listed.MainAddress = entryHotelName.Text;// hotel name
        Listed.City = entryCity.Text;
        Listed.State = entryState.Text;
        Listed.Country = entryCountry.Text;
        Listed.FullAddress = entryHotelName.Text + " " + entryState.Text + " " + entryCity.Text + " " + entryCountry.Text;

        DidClose?.Invoke(Listed);

        await MopupService.Instance.PopAsync();
    }
}