using CasualClient.Views;
using CasualServer.Models;
using DevExpress.Mvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CasualClient.ViewModels
{
    class MainWindowVM : BaseVM
    {

        private ObservableCollection<string> _categories = new ObservableCollection<string>();
        public ObservableCollection<string> Categories
        {
            get { return _categories; }
            set { _categories = value; RaisePropertyChanged("Services"); }
        }
        
        private ObservableCollection<string> _services = new ObservableCollection<string>();
        public ObservableCollection<string> Services
        {
            get { return _services; }
            set { _services = value; RaisePropertyChanged("Services"); }
        }

        private string _selectedCategory;

        public string SelectedCategory
        {
            get { OnSelected();  return _selectedCategory;  }
            set { _selectedCategory = value; RaisePropertyChanged("SelectedCategory");  }
        }

        private string _selectedService;

        public string SelectedService
        {
            get { return _selectedService; }
            set { _selectedService = value; RaisePropertyChanged("SelectedService"); }
        }
        private void OnSelected()
        {
            //MessageBox.Show("Test");
            _services.Clear();
            //_services = new ObservableCollection<string> { };
            WebRequest request = WebRequest.Create("http://localhost:5000/services/");
            request.Method = "GET";
            request.ContentType = "application/json";

            request.Headers.Add("desiredCategoryName", _selectedCategory);

            string result;
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
                foreach (var item in JsonConvert.DeserializeObject<List<Service>>(result))
                    _services.Add(item.ServiceName);
            }
        }


        public ICommand OnLoad
        {
            get
            {
                return new DelegateCommand(() =>
                {                    
                    getCategories();
                });
            }
        }
        private void getCategories()
        {
            _categories.Clear();
            //_categories = new ObservableCollection<string>();
            WebRequest request = WebRequest.Create("http://localhost:5000/categories/");
            request.Method = "GET";
            
            request.ContentType = "application/json";


            string result;
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
                
                    foreach (var item in JsonConvert.DeserializeObject<List<Category>>(result))
                        _categories.Add(item.CategoryName);
            }
        }

        public ICommand AddCategory
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    AddCategory ad = new AddCategory();
                    ad.ShowDialog();
                    getCategories();
                });
            }
        }

        public ICommand DeleteCategory
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (_selectedCategory != null)
                    {
                        try
                        {

                        
                        WebRequest request = WebRequest.Create($"http://localhost:5000/categories/{_selectedCategory}");
                        request.Method = "DELETE";
                        request.ContentType = "application/json";
                        request.ContentLength = 0;
                        WebResponse response = request.GetResponse();
                        getCategories();
                        }
                        catch
                        {
                            MessageBox.Show("Невозможно удалить категорию, в которой есть услуги");
                        }
                    }
                   

                });
            }
        }

        public ICommand AddService
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    AddService ad = new AddService();
                    ad.ShowDialog();
                    getCategories();
                });
            }
        }

        public ICommand DeleteService
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (_selectedService != null)
                    {
                        WebRequest request = WebRequest.Create($"http://localhost:5000/services/{_selectedService}");
                        request.Method = "DELETE";
                        request.ContentType = "application/json";
                        request.ContentLength = 0;
                        WebResponse response = request.GetResponse();
                        getCategories();
                    }


                });
            }
        }

        public ICommand OpenUsers
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Users ad = new Users();
                    ad.ShowDialog();                    
                });
            }
        }

        public ICommand OpenLog
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    LogView log = new LogView();
                    log.ShowDialog();
                });
            }
        }

        public ICommand OpenStatistics
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Statistics st = new Statistics();
                    st.ShowDialog();
                });
            }
        }



    }
    }
