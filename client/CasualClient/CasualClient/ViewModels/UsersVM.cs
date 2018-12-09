using CasualClient.Models;
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
using System.Windows.Input;

namespace CasualClient.ViewModels
{
    class UsersVM : BaseVM
    {
        private string _selectedUserName = "";
        public string SelectedUserName
        {
            get {  return _selectedUserName; }
            set {    _selectedUserName = value; LoadLogPass(); LoadUserServices(); RaisePropertyChanged("SelectedUserName"); }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; RaisePropertyChanged("Login"); }
        }

        private string _password = "";
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged("Password"); }
        }

        private ObservableCollection<string> _userServices = new ObservableCollection<string>();

        public ObservableCollection<string> UserServices
        {
            get { return _userServices; }
            set { _userServices = value; RaisePropertyChanged("UserServices"); }
        }

        private ObservableCollection<string> _users = new ObservableCollection<string>();
        public ObservableCollection<string> Users
        {
            get { UpdateUsers(); return _users; }
            set { _users = value; RaisePropertyChanged("Users"); }
        }

        public ICommand LoadUsers
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    UpdateUsers();
                });
            }
        }
        private void UpdateUsers()
        {
            //Users.Clear();
            List<User> u = new List<User>();
            WebRequest request = WebRequest.Create("http://localhost:5000/users/");
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

                foreach (var item in JsonConvert.DeserializeObject<List<User>>(result))
                    _users.Add(item.Nickname);
            }
        }

        private void LoadLogPass()
        {
            User u = new User();
            WebRequest request = WebRequest.Create("http://localhost:5000/users/nickname");
            request.Method = "GET";
            request.Headers.Add("username", _selectedUserName);
            request.ContentType = "application/json";


            string result;
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }                
                u = JsonConvert.DeserializeObject<User>(result);
                
            }
            Login = u.Login;
            Password = u.Password;
        }

        private void LoadUserServices()
        {
            _userServices.Clear();
            WebRequest request = WebRequest.Create($"http://localhost:5000/userservices/{_selectedUserName}");
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

                foreach (var item in JsonConvert.DeserializeObject<List<JsonPafseServiceName>>(result))
                    _userServices.Add(item.serviceName);
            }
        }

    }
}
