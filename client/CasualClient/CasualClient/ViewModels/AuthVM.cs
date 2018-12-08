using CasualClient.Views;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CasualClient.ViewModels
{
    class AuthVM : BaseVM
    {
        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = value; RaisePropertyChanged("Login"); }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged("Password"); }
        }
        public DelegateCommand<Window> OpenMainWindow
        {
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    bool logPassIsValid = CheckLogPass();
                    if(logPassIsValid)
                    {
                        MainWindow mw = new MainWindow();
                        MainWindowVM mwVm = new MainWindowVM();
                        mw.DataContext = mwVm;
                        w.Close();
                        mw.Show();
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль введены неверно");
                    }
                });
            }
        }
        private bool CheckLogPass()
        {
            
            //////////////////////////////////////////////////////
            WebRequest request = WebRequest.Create("http://localhost:5000/users/check");
            request.Method = "GET";
            request.Headers.Add("login", Login);
            request.Headers.Add("password", Password);
            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/json";

            string result;
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            if (result == "true")
                return true;
            else
                return false;
        }

        public DelegateCommand<Window> OpenRegisterWindow
        {
            get
            {
                return new DelegateCommand<Window>((w) =>
                {

                    RegisterWindow rw = new RegisterWindow();
                    RegisterWindowVM rwVm = new RegisterWindowVM();
                    rw.DataContext = rwVm;
                    w.Close();
                    rw.Show();

                });
            }
        }


    }
}
