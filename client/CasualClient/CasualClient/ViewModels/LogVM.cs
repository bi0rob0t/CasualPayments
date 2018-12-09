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
using System.Windows.Input;

namespace CasualClient.ViewModels
{
    class LogVM : BaseVM
    {
        private ObservableCollection<Log> _log = new ObservableCollection<Log>();
        public ObservableCollection<Log> Log
        {
            get { getLog(); return _log;  }
            set {   _log = value; RaisePropertyChanged("Log"); }
        }
        private void getLog()
        {
            _log.Clear();
            WebRequest request = WebRequest.Create($"http://localhost:5000/log/");
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

                foreach (var item in JsonConvert.DeserializeObject<List<Log>>(result))
                    _log.Add(item);
            }
        }
        public ICommand GetLog
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    //Log test = new Log { actionName = "test", logId = 1, serviceName = "test", Time = DateTime.Now, userName = "test" };
                    //_log.Add(test);
                    //getLog();
                });
            }
        }

    }
}
