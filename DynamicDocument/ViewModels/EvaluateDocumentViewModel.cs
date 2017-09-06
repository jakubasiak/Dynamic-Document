using DynamicDocument.Views;
using FormGenerator;
using FormGeneratorLibrary.FormControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DynamicDocument.ViewModels
{
    public class EvaluateDocumentViewModel : INotifyPropertyChanged
    {
        public EvaluateDocumentViewModel(DocumentProcessor dp)
        {
            DocumentProcessor = dp;
            Text = dp.ConstructedText;
            AbstractFormElements = dp.ValuesFormElements;
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
        private string _evaluatedText;
        public string EvaluatedText
        {
            get
            {
                return _evaluatedText;
            }
            set
            {
                _evaluatedText = value;
                OnPropertyChanged("EvaluatedText");
                Counter++;
            }
        }
        private int _counter = 0;
        public int Counter
        {
            get
            {
                return _counter;
            }
            set
            {
                _counter = value;
                OnPropertyChanged("Counter");
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
                            //Page page = x as Page;
                            //BuildDocumentViewModel viewModel = new BuildDocumentViewModel(DocumentProcessor);
                            //BuildDocumentPage bdp = new BuildDocumentPage(viewModel);
                            //page.NavigationService.Navigate(bdp);
                            //page.NavigationService.RemoveBackEntry();
                        }
                        );

                return _goBackCommand;
            }
        }
        private ICommand _exportDocumentCommand;
        public ICommand ExportDocumentCommand
        {
            get
            {
                if (_exportDocumentCommand == null)
                    _exportDocumentCommand = new RelayCommand(
                        x =>
                        {
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = Path.GetFileName(DocumentProcessor.TemplatePath);
                            sfd.DefaultExt = Path.GetExtension(DocumentProcessor.TemplatePath);
                            sfd.Filter = Path.GetExtension(DocumentProcessor.TemplatePath).Replace(".",string.Empty) +" |*" +Path.GetExtension(DocumentProcessor.TemplatePath);
                            sfd.Title = "Save document";
                            sfd.InitialDirectory = string.IsNullOrEmpty(DocumentProcessor.ExportPath)?"":DocumentProcessor.ExportPath;

                            sfd.ShowDialog();
                            if (!string.IsNullOrEmpty(sfd.FileName))
                            {
                                DocumentProcessor.ExportPath = sfd.FileName;
                                DocumentProcessor.GenerateDocument();
                            }
                            try
                            {
                                System.Diagnostics.Process process = new System.Diagnostics.Process();
                                Uri uri = new Uri(sfd.FileName, UriKind.RelativeOrAbsolute);
                                process.StartInfo.FileName = uri.LocalPath;
                                process.Start();
                                process.WaitForExit();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("File opening failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }
                        );

                return _exportDocumentCommand;
            }
        }
        #endregion
        #region Reszta
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            if (name != "EvaluatedText" && name != "Counter")
                EvaluatedText = DocumentProcessor.EvaluateDocument(Text, AbstractFormElements.ToList()); //TODO mam wątpliwości
        }
        #endregion

    }
}
