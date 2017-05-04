using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private XViewModel _xModel;
       private X1ViewModel _x1Model;
       private MoreViewModel _moreModel;
       private X2ViewModel _x2Model;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = XModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public XViewModel XModel
        {
            get { return _xModel ?? (_xModel = new XViewModel()); }
        }
 
        public X1ViewModel X1Model
        {
            get { return _x1Model ?? (_x1Model = new X1ViewModel()); }
        }
 
        public MoreViewModel MoreModel
        {
            get { return _moreModel ?? (_moreModel = new MoreViewModel()); }
        }
 
        public X2ViewModel X2Model
        {
            get { return _x2Model ?? (_x2Model = new X2ViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            XModel.ViewType = viewType;
            X1Model.ViewType = viewType;
            MoreModel.ViewType = viewType;
            X2Model.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadDataAsync(bool forceRefresh = false)
        {
            var loadTasks = new Task[]
            { 
                XModel.LoadItemsAsync(forceRefresh),
                X1Model.LoadItemsAsync(forceRefresh),
                MoreModel.LoadItemsAsync(forceRefresh),
                X2Model.LoadItemsAsync(forceRefresh),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadDataAsync(true);
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
