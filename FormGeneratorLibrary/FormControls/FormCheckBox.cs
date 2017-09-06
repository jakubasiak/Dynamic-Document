using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormGeneratorLibrary.FormControls
{
    public class FormCheckBox : AbstractFormElement
    {
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        private bool _isChecked;
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public FormCheckBox()
        {
        }
        public FormCheckBox(string expression, string name, string text)
        {
            Expression = expression;
            Name = name;
            Text = text;
            IsChecked = false;
        }

        public override event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string GetValue()
        {
            if(IsChecked)
            {
                return Text;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
