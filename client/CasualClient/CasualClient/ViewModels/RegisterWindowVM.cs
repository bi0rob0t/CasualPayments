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
    class RegisterWindowVM : BaseVM
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

        private string _nickname;
        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; RaisePropertyChanged("Nickname"); }
        }

        public DelegateCommand<Window> AddUser
        {
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    WebRequest request = WebRequest.Create("http://localhost:5000/users/");
                    request.Method = "POST";
                    request.Headers.Add("nickname", Nickname);
                    request.Headers.Add("login", Login);
                    request.Headers.Add("password", Password);
                    request.ContentType = "application/json";
                    request.ContentLength = 0;

                    string result;
                    WebResponse response = request.GetResponse();
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                    if (result == "false")
                        MessageBox.Show("Пользователь с таким логином или никнеймом уже существует");
                    else
                    {
                        // Тут должно быть добавление Action и потом добавление его в Log
                        AuthWindow aw = new AuthWindow();
                        w.Close();
                        aw.Show();
                    }

                });
            }
        }
    }
}
