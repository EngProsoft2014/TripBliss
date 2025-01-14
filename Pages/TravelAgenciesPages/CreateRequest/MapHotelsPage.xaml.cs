using GoogleApi.Entities.Maps.Directions.Response;
using GoogleApi.Entities.Translate.Common.Enums;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Syncfusion.Maui.Core.Carousel;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using TripBliss.Models;

namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class MapHotelsPage : ContentPage
{
    public delegate void MapHotelDelegte(HotelResponse HotelResponse);
    public event MapHotelDelegte MapHotelClose;

    HotelResponse response = new HotelResponse();
    ObservableCollection<HotelResponse> LstHotels = new ObservableCollection<HotelResponse>();

    public MapHotelsPage(ObservableCollection<HotelResponse> hotels)
    {
        InitializeComponent();

        LstHotels = hotels;
        comboBoxHoteles.ItemsSource = hotels;

        // 24.468177756649197, 39.61111177426711  // Al Masjid an Nabawi
        //21.422832377351494, 39.82615615606046   // Al Kaaba
        //21.525193073325497, 39.16714758945929   // Jeddah

        string name = hotels[0].LocationId == 1 ? "Al Masjid an Nabawi" : hotels[0].LocationId == 2 ? "Al Kaaba" : "Jeddah";

        var pinDefualt = new CustomPin
        {
            Label = name,
            MarkerId = name,
            Address = name,
            Type = PinType.Place,
            Location = hotels[0].LocationId == 1 ? new Location(24.468177756649197, 39.61111177426711) : hotels[0].LocationId == 2 ? new Location(21.422832377351494, 39.82615615606046) : new Location(21.525193073325497, 39.16714758945929),
            ImageSource = hotels[0].LocationId == 1 ? "nabawy.jpg" : hotels[0].LocationId == 2 ? "kaba.png" : ""
        };
        myMap.Pins.Add(pinDefualt);

        
        foreach (var hotel in hotels)
        {
            if(!string.IsNullOrEmpty(hotel.Lat))
            {
                var pin = new CustomPin();
                pin = new CustomPin
                {
                    Label = !string.IsNullOrEmpty(hotel.HotelNameLang) ? hotel.HotelNameLang : "No Name",
                    MarkerId = hotel.Id,
                    Address = hotel.Address,
                    Type = PinType.Place,
                    Location = new Location(new Location(double.Parse(hotel.Lat), double.Parse(hotel.Lng))),
                    ImageSource = "hotel.png"
                };
                myMap.Pins.Add(pin);

                pin.MarkerClicked += Pin_MarkerClicked;
            }
        }

        if (hotels[0].LocationId == 1)
        {
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(24.468177756649197, 39.61111177426711), Distance.FromMiles(3)));
        }
        else if (hotels[0].LocationId == 2)
        {
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(21.422832377351494, 39.82615615606046), Distance.FromMiles(3)));
        }
        else
        {
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(21.525193073325497, 39.16714758945929), Distance.FromMiles(3)));
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopAsync();
    }

    private void comboBoxHoteles_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            var obj = e.CurrentSelection[0] as HotelResponse;

            if (obj != null)
            {
                myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(!string.IsNullOrEmpty(obj.Lat) ? double.Parse(obj.Lat) : 21.422832377351494, !string.IsNullOrEmpty(obj.Lng) ? double.Parse(obj.Lng) : 39.82615615606046), Distance.FromMeters(200)));
            }
        }
    }

    private void Pin_MarkerClicked(object? sender, PinClickedEventArgs e)
    {
        var hotel = sender as CustomPin;
        if(hotel != null)
        {      
            //int id = 0;
            //int.TryParse(hotel.MarkerId?.ToString()!.Substring(1), out id);
            response = LstHotels?.FirstOrDefault(f => f.HotelName == hotel.Label)!;
            bdHotel.IsVisible = true;
            lblName.Text = hotel.Label;
            lblAddress.Text = hotel.Address;
        }   
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        MapHotelClose.Invoke(response);
        await App.Current!.MainPage!.Navigation.PopAsync();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        bdHotel.IsVisible = false;
    }

}