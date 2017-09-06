using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormGeneratorLibrary.FormControls
{
    public class FormDate:AbstractFormElement
    {
        private DateTime _date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        public FormDate()
        {

        }
        public FormDate(string expression, string name)
        {
            Expression = expression;
            Name = name;
        }
        public override event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string GetValue()
        {
            if(Date != null)
            {
                return Date.ToString("dd.MM.yyyy");
            }
            else
            {
                return DateTime.Now.ToString("dd.MM.yyyy");
            }
        }
    }
}
