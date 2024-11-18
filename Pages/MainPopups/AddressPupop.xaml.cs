using TripBliss.Models;
using Mopups.Services;
using TripBliss.ViewModels;
using System.Reactive.Joins;

namespace TripBliss.Pages.MainPopups;

public partial class AddressPupop : Mopups.Pages.PopupPage
{
    public delegate void CustomDelegte(SuggestionAddressModel str);
    public event CustomDelegte DidClose;

    public static string Lan {  get; set; }
    public AddressPupop()
	{
        InitializeComponent();

        Lan = Preferences.Default.Get("Lan", "");
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }

    [Obsolete]
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

    private async void ListView_OnItemTapped(Object sender, ItemTappedEventArgs e)
    {
        //EmployeeListView.IsVisible = false;  
        SuggestionAddressModel Listed = e.Item as SuggestionAddressModel;

        var _request2 = new GoogleApi.Entities.Places.Details.Request.PlacesDetailsRequest
        {
            //Key = "AIzaSyCAFJVJ3OVKxJHSKPBljAlHTah2QzWUcBY",
            Key = Device.iOS == "iOS" ? "AIzaSyDY-9LWg_lY41hlxBA2-ngBydMGYaXxKA4" : "AIzaSyAW5B8h96IXYo_0ZL_bSGlDBVExVOAHa6w",//AutoCompleteKey(ios-Android)
            PlaceId = Listed.PalceId,
            Language = Lan == "ar" ? GoogleApi.Entities.Common.Enums.Language.Arabic : GoogleApi.Entities.Common.Enums.Language.English
        };

        var _response2 = await GoogleApi.GooglePlaces.Details.QueryAsync(_request2);

        string[] ArrAdd = new string[7];

        foreach (var itemAdd in _response2.Result.AddressComponents)
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

        searchBar.Text = Listed.FullAddress;
        countryListView.IsVisible = false;

        ((ListView)sender).SelectedItem = null;

        //DidClose?.Invoke(searchBar.Text);
        DidClose?.Invoke(Listed);

        await MopupService.Instance.PopAsync();
    }

    private async void OnImageNameTapped_CloseIcon(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }


    public static async Task<List<SuggestionAddressModel>> GetPlacesAutocompleteAsync(string search)
    {
        GoogleApi.Entities.Places.AutoComplete.Request.PlacesAutoCompleteRequest request = new GoogleApi.Entities.Places.AutoComplete.Request.PlacesAutoCompleteRequest();
        request.Input = search;
        request.Key = Device.iOS == "iOS" ? "AIzaSyDY-9LWg_lY41hlxBA2-ngBydMGYaXxKA4" : "AIzaSyAW5B8h96IXYo_0ZL_bSGlDBVExVOAHa6w";
        request.Language = Lan == "ar" ? GoogleApi.Entities.Common.Enums.Language.Arabic : GoogleApi.Entities.Common.Enums.Language.English;

        var response = await GoogleApi.GooglePlaces.AutoComplete.QueryAsync(request, null);

        List<Task<GoogleApi.Entities.Places.Details.Response.PlacesDetailsResponse>> detailsTasks = new List<Task<GoogleApi.Entities.Places.Details.Response.PlacesDetailsResponse>>();

        foreach (var prediction in response.Predictions)
        {
            var detailsRequest = new GoogleApi.Entities.Places.Details.Request.PlacesDetailsRequest
            {
                Key = request.Key,
                PlaceId = prediction.PlaceId.ToString(),
                Language = Lan == "ar" ? GoogleApi.Entities.Common.Enums.Language.Arabic : GoogleApi.Entities.Common.Enums.Language.English
            };

            detailsTasks.Add(GoogleApi.GooglePlaces.Details.QueryAsync(detailsRequest));
        }

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
                MainAddress = ArrAdd[0] + " " + ArrAdd[1] + " " + ArrAdd[2],
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
        Listed.Street = entryStreet.Text;
        Listed.City = entryCity.Text;
        Listed.State = entryState.Text;
        Listed.Zip = entryPostalCode.Text;
        Listed.Country = entryCountry.Text;
        Listed.FullAddress = entryStreet.Text + " " + entryState.Text + " " + entryCity.Text + " " + entryCountry.Text + " " + entryPostalCode.Text;

        DidClose?.Invoke(Listed);

        await MopupService.Instance.PopAsync();
    }

}