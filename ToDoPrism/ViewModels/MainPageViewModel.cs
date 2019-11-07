using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoPrism.Models;
using ToDoPrism.Repositories;

namespace ToDoPrism.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IPageLifecycleAware
    {

        #region Commands
        public DelegateCommand AddToDoItemCommand { get; private set; }
        //public DelegateCommand<TodoItem> SelectedItem { get; private set; }
        #endregion

        public TodoItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                if(_selectedItem == null)
                {
                    return;
                }

                OnEditItem();

                SelectedItem = null;
            }
        }

        

        private readonly ITodoItemRepository repository;
        private readonly INavigationService _navigationService;
        private bool _isListViewRefreshing;
        private TodoItem _selectedItem;

        private ObservableCollection<TodoItem> items;
        public ObservableCollection<TodoItem> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }

        public MainPageViewModel(INavigationService navigationService, ITodoItemRepository todoItemRepository)
            : base(navigationService)
        {
            Title = "Main Page";
            _navigationService = navigationService;
            this.repository = todoItemRepository;
            AddToDoItemCommand = new DelegateCommand(OnAddItem);

            //Items = new ObservableCollection<TodoItem>();
        }

        private async void OnEditItem()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("ToDoItem", SelectedItem);
            navParameters.Add("PageTitle", "Edit item");

            await _navigationService.NavigateAsync("ItemPage", navParameters);
        }

        private async void OnAddItem()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("PageTitle", "Create new item");
            await _navigationService.NavigateAsync("ItemPage", navParameters);
        }

        public async void OnAppearing()
        {
            await initializePageData();
        }

        private async Task<bool> initializePageData()
        {
            bool received = false;
            var getItems = await repository.GetItems();
            Items = new ObservableCollection<TodoItem>(getItems);
            return received;
        }

        public void OnDisappearing()
        {
            Console.WriteLine("OnDisappering fired");
        }
    }
}
