using Prism;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoPrism.Models;
using ToDoPrism.Repositories;

namespace ToDoPrism.ViewModels
{
    public class ItemPageViewModel : ViewModelBase, IAutoInitialize 
    {        
        #region Properties
        public DelegateCommand SaveItemCommand { get; set; }
        public DelegateCommand DeleteItemCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand SpeakCommand { get; set; }
        public DelegateCommand SpeechToTitleCommand { get; set; }
        public DelegateCommand SpeechToNotesCommand { get; set; }

        public string PageTitle
        {
            get { return _pageTitle; }
            set { SetProperty(ref _pageTitle, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }

        public bool Done
        {
            get { return _done; }
            set { SetProperty(ref _done, value); }
        }

        public DateTime Due
        {
            get { return _due; }
            set { SetProperty(ref _due, value); }
        }

        private TodoItem selectedItem;
        [AutoInitialize("ToDoItem")]
        public TodoItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value);
                SetFields();
            }            
        }

        private void SetFields()
        {
            Name = SelectedItem.Name;
            Notes = SelectedItem.Notes;
            Due = SelectedItem.Due;
            Done = SelectedItem.Done;
            _itemId = SelectedItem.Id;
        }
        #endregion


        #region Fields        
        private readonly ITodoItemRepository _toDoItemsRepository;        
        private readonly INavigationService _navigationService;        
        private string _pageTitle;
        private int _itemId = 0;
        private string _name;
        private string _notes;
        private bool _done;
        private DateTime _due = DateTime.Today;
        //private readonly IItemAnnouncementService _itemAnnouncementService;
        //private readonly IVoiceRecognitionService _voiceRecognitionService;
        #endregion

        public ItemPageViewModel(INavigationService navigationService, ITodoItemRepository todoItemRepository) : base(navigationService)
        {
            _navigationService = navigationService;
            _toDoItemsRepository = todoItemRepository;
            SaveItemCommand = new DelegateCommand(OnSaveItem);
        }

        private async void OnSaveItem()
        {
            var todoItem = new TodoItem()
            {
                Id = _itemId,
                Name = _name,
                Notes = _notes,
                Done = _done,
                Due = _due
            };

            await _toDoItemsRepository.AddOrUpdate(todoItem);
            await _navigationService.GoBackAsync();
        }

        //public void OnNavigatingTo(NavigationParameters parameters)
        //{
            
        //    if (parameters.ContainsKey("ToDoItem"))
        //    {
        //        var item = (TodoItem)parameters["ToDoItem"];

        //        _itemId = item.Id;
        //        Name = item.Name;
        //        Notes = item.Notes;
        //        Done = item.Done;
        //        Due = item.Due;
        //    }
        //}
    }
}
