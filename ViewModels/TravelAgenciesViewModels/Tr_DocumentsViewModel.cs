using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TripBliss.Helpers;


namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    public partial class Tr_DocumentsViewModel : BaseViewModel
    {

        #region Servises
        IGenericRepository Rep;

        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_DocumentsViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        void OnBackPressed()
        {
            App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion


    }
}
