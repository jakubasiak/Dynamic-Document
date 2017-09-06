using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormGeneratorLibrary.FormControls
{
    public class FormTextBox:AbstractFormElement
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
        public FormTextBox()
        {

        }
        public FormTextBox(string expression, string name)
        {
            Expression = expression;
            Name = name;
            Text = string.Empty;
        }
        public override event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public override string GetValue()
        {
            if (!string.IsNullOrEmpty(Text))
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
