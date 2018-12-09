using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CasualClient.ViewModels
{
    class AddCategoryVM : BaseVM
    {
        private string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; RaisePropertyChanged("CategoryName"); }
        }

        public DelegateCommand<Window> AddCategory
        {
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    WebRequest request = WebRequest.Create($"http://localhost:5000/categories/{_categoryName}");
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = 0;
                    WebResponse response = request.GetResponse();
                    w.Close();
                });
            }
        }

        
        }
}
