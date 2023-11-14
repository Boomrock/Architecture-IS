using Lab1_Architecture_IS.Models;
using NetController;
using NetProtocol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        // Конструктор модели представления
        public MainViewModel()
        {
            // Инициализация коллекции элементов
            Items = new ObservableCollection<CSVModel>();
            client.OnReceive += OnReceiveHandler;
            // Инициализация команды для добавления нового элемента
            AddItemCommand = new RelayCommand(AddItem);
        }

        private void OnReceiveHandler(Command command)
        {
             if (command.CommandType == CommandType.TransferAll)
            {
                var models = command.Data.Get<CSVModel[]>("Models");

                App.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var model in models)
                    {
                        Items.Add(model);
                    }
                });
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
