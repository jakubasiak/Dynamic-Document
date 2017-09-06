using DynamicDocument.Views;
using FormGenerator;
using FormGeneratorLibrary.FormControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace DynamicDocument.ViewModels
{
    public class BuildDocumentViewModel : INotifyPropertyChanged
    {
        public BuildDocumentViewModel(DocumentProcessor dp)
        {
            DocumentProcessor = dp;
            Text = dp.OrginalText;
            AbstractFormElements = dp.ConstructingFormElements;
            foreach (var item in AbstractFormElements)
            {
                item.PropertyChanged += (sender, eventArgs) => OnPropertyChanged("AbstractFormElements");
            }
        }

        public DocumentProcessor DocumentProcessor { get; set; }
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
            }
        }
        private string _builtText;
        public string BuiltText
        {
            get
            {
                return _builtText;
            }
            set
            {
                _builtText = value;
                OnPropertyChanged("BuiltText");
            }
        }

        private List<AbstractFormElement> _abstractFormElements;
        public List<AbstractFormElement> AbstractFormElements
        {
            get
            {
                return _abstractFormElements;
            }
            set
            {
                _abstractFormElements = value;
                OnPropertyChanged("AbstractFormElements");
            }
        }
        #region Commands
        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                    _goBackCommand = new RelayCommand(
                        x =>
                        {
                            Page page = x as Page;
                            if (page.NavigationService.CanGoBack)
                            {
                                page.NavigationService.GoBack();
                            }
                        }
                        );

                return _goBackCommand;
            }
        }
        private ICommand _evaluateCommand;
        public ICommand EvaluateCommand
        {
            get
            {
                if (_evaluateCommand == null)
                    _evaluateCommand = new RelayCommand(
                        x =>
                        {
                            DocumentProcessor.ConstructedText = BuiltText.Replace("[b]",string.Empty).Replace("[/b]", string.Empty);
                            //Tu jest błąd po jednokrotnym przypisaniu wartości text, w drugim przpisaniu nie ma już czego podmienić. Trzeba przechowywać orginalną wartość
                            DocumentProcessor.MapEvaluationFormElements();
                            EvaluateDocumentViewModel viewModel = new EvaluateDocumentViewModel(DocumentProcessor);
                            EvaluateDocumentPage edp = new EvaluateDocumentPage(viewModel);
                            Page page = x as Page;
                            page?.NavigationService.Navigate(edp);                           
                        }
                        );

                return _evaluateCommand;
            }
        }
        //TODO przypisz do dp.Text wartość z BuiltText
        #endregion
        #region Reszta
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            if (name != "BuiltText")
                BuiltText = DocumentProcessor.BuildDocument(Text, AbstractFormElements.ToList());
        }
        #endregion

    }
}
