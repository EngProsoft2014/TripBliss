using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Maui.ProgressBar;
using System.Collections.ObjectModel;
using TripBliss.Helpers;


namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    public partial class Tr_DocumentsViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<StepProgressBarItem> stepProgressItem = new ObservableCollection<StepProgressBarItem>();

        #region Servises
        IGenericRepository Rep;

        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_DocumentsViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            AddSteps();
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        void OnBackPressed()
        {
            App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion

        void AddSteps()
        {
            StepProgressItem.Add(new StepProgressBarItem() { PrimaryText = "Tax card" });
            StepProgressItem.Add(new StepProgressBarItem() { PrimaryText = "Commercial license" });
            StepProgressItem.Add(new StepProgressBarItem() { PrimaryText = "commercial register" });
        }
    }
}
