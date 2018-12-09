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

namespace CasualClient.ViewModels
{
    class AddServiceVM : BaseVM
    {
        private ObservableCollection<string> _categories = new ObservableCollection<string>();

        public ObservableCollection<string> Categories
        {
            get {getCategories(); return _categories; }
            set { _categories = value; RaisePropertyChanged("Categories"); }
        }
        private string _serviceName;

        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; RaisePropertyChanged("ServiceName"); }
        }

        private string _selectedCategory;

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; RaisePropertyChanged("SelectedCategory"); }
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

        public DelegateCommand<Window> AddService
        {
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    WebRequest request = WebRequest.Create("http://localhost:5000/services/");
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = 0;
                    request.Headers.Add("serviceName", _serviceName);
                    request.Headers.Add("categoryName", _selectedCategory);

                    WebResponse response = request.GetResponse();



                    w.Close();                    
                });
            }
        }
    }
}
