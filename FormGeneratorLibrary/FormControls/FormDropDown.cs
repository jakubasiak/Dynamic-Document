using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormGeneratorLibrary.FormControls
{
    public class FormDropDown:AbstractFormElement
    {
        private List<string> _options;
        public List<string> Options
        {
            get
            {
                return _options;
            }
            set
            {
                _options = value;
                OnPropertyChanged("Options");
            }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (value >= 0 && value < Options.Count)
                {
                    _selectedIndex = value;
                    OnPropertyChanged("SelectedIndex");
                }
            }
        }
        public string SelectedValue
        {
            get { return Options[SelectedIndex]; }
        }

        public string this[int index]
        {
            get
            {
                return Options[index];
            }
            set
            {
                if (index >= 0 && index < Options.Count)
                {
                    Options[index] = value;
                    OnPropertyChanged("Options");
                }
            }
        }
        public FormDropDown()
        {
            Options = new List<string>();
        }
        public FormDropDown(string expression, string name, List<string> options)
        {
            Expression = expression;
            Name = name;
            Options = options;
            SelectedIndex = 0;
        }
        public override event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public override string ToString()
        {
            return Options[SelectedIndex];
        }

        public override string GetValue()
        {
            if (SelectedIndex >= 0)
            {
                return SelectedValue;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
