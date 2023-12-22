using Lab1_Architecture_IS.Models;
using NetController;
using NetProtocol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows.Input;

namespace ViewWPF
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public IClient<CommandType, Command> client = new Client(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8808));
        // Коллекция элементов для отображения в DataGrid
        private ObservableCollection<CSVModel> _items;
        public ObservableCollection<CSVModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        private Stack<CSVModel> _modelStack = new Stack<CSVModel>();

        // Команда для добавления нового элемента
        public ICommand AddItemCommand { get; }
        // Добавить команду для удаления выбранного элемента
        public ICommand DeleteItemCommand { get; }
        public ICommand Update { get; }
        public ICommand Save { get; }

        // Конструктор модели представления
        public MainViewModel()
        {
            // Инициализация коллекции элементов
            Items = new ObservableCollection<CSVModel>();
            client.OnReceive += OnReceiveHandler;
            // Инициализация команды для добавления нового элемента
            AddItemCommand = new RelayCommand(AddItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            Update = new RelayCommand(UpdateItems);
            Save = new RelayCommand(SaveChange);
        }

        private void SaveChange(object obj)
        {
            var command = new Command();
            command.CommandType = CommandType.Save;
            client.Send(command);
        }

        private void UpdateItems(object obj)
        {
            var command = new Command();
            command.CommandType = CommandType.TransferAll;
            client.Send(command);
        }

        private void OnReceiveHandler(Command command)
        {
             if (command.CommandType == CommandType.TransferAll)
              {
                var models = command.Data.Get<CSVModel[]>("Models");

                App.Current.Dispatcher.Invoke(() =>
                {
                    Items = new ObservableCollection<CSVModel>();
                    foreach (var model in models)
                    {
                        Items.Add(model);
                    }
                });
              }   
        }

        // Добавить свойство для выбранного элемента в модели представления
        private CSVModel _selectedItem;
        public CSVModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private void DeleteItem(object parameter)
        {
            // Проверить, что выбранный элемент не null
            if (SelectedItem != null)
            {
                var command = new Command();
                command.CommandType = CommandType.Delete;
                command.Data.Add("Id", SelectedItem.Id);
                client.Send(command);


                Items.Remove(SelectedItem);

                // Сбросить выбранный элемент в null
                SelectedItem = null;
            }
        }


        // Метод для добавления нового элемента в коллекцию
        private void AddItem(object parameter)
        {
            // Создание нового элемента с произвольными данными
            var newItem = new CSVModel()
            {
                Id = Items.Count + 1,
                Name = "Item " + (Items.Count + 1),
                Type = "Type " + (Items.Count + 1),
                Volume = (float)(new Random().NextDouble() * 100),
                IsInteractive = new Random().Next(2) == 0
            };
            Items.Add(newItem);
            // Добавление нового элемента в коллекцию
            var command = new Command();
            command.CommandType = CommandType.Add;
            command.Data.Add("Model", newItem);
            client.Send(command);
        }

        // Реализация интерфейса INotifyPropertyChanged для обновления свойств в представлении
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
