using CasualServer.Models;
using DevExpress.Mvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CasualClient.ViewModels
{
    class StatisticsVM : BaseVM
    {
        private string _usersReg = "";
        public string Usersreg
        {
            get { return _usersReg; }
            set { _usersReg = value; RaisePropertyChanged("Usersreg"); }
        }

        private string _servicePay = "";
        public string ServicePay
        {
            get { return _servicePay; }
            set { _servicePay = value; RaisePropertyChanged("ServicePay"); }
        }

        private string _userPay = "";
        public string UserPay
        {
            get { return _userPay; }
            set { _userPay = value; RaisePropertyChanged("UserPay"); }
        }

        private void GetUsersReg()
        {

            //_categories = new ObservableCollection<string>();
            WebRequest request = WebRequest.Create("http://localhost:5000/statistics/usersreg");
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


                Usersreg = result;

            }
        }

        private void GetServicePay()
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/statistics/servicepay");
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

                foreach (var item in JsonConvert.DeserializeObject<List<Service>>(result))
                    ServicePay = item.ServiceName;

            }
        }

        private void GetUserPay()
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/statistics/userpay");
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


                UserPay = JsonConvert.DeserializeObject<User>(result).Nickname;

            }
        }


        public ICommand GetStatistic
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    GetUsersReg();
                    GetServicePay();
                    GetUserPay();

                });
            }
        }
    }
}
