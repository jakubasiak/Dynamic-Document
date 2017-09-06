using DynamicDocument.Views;
using FormGenerator;
using FormGeneratorLibrary.FormControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

namespace DynamicDocument.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        #region Constructor
        public MainPageViewModel()
        {
            List<string> list = DeserializeFilesList();
            if (list != null)
            {
                ObservableCollection<FileInfo> files = new ObservableCollection<FileInfo>();

                foreach (var item in list)
                {
                    files.Add(new FileInfo(item));
                }
                Files = files;
            }
            else
            {
                Files = new ObservableCollection<FileInfo>();
            }
        }
        #endregion
        #region Properties
        private string HelpFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Resources\help.pdf";
        private ObservableCollection<FileInfo> _files;
        public ObservableCollection<FileInfo> Files
        {
            get
            {
                return _files;
            }
            set
            {
                _files = value;
                OnPropertyChanged("Files");
            }
        }
        private int _filesSelectedItem;
        public int FilesSelectedIndex
        {
            get
            {
                return _filesSelectedItem;
            }
            set
            {
                _filesSelectedItem = value;
                OnPropertyChanged("FilesSelectedIndex");
            }
        }
        #endregion
        #region Commands
        private ICommand _addTemplateCommand;
        public ICommand AddTemplateCommand
        {
            get
            {
                if (_addTemplateCommand == null)
                    _addTemplateCommand = new RelayCommand(
                        x =>
                        {
                            OpenFileDialog atd = new OpenFileDialog();
                            atd.Multiselect = true;
                            atd.DefaultExt = ".rtf";
                            atd.Filter = "RTF files|*.rtf|Text files|*.txt";
                            atd.Title = "Add template";

                            atd.ShowDialog();
                            string[] openPaths = atd.FileNames;

                            foreach (var item in openPaths)
                            {
                                if (File.Exists(item) && _files.Where(c => c.FilePath == item).Count() < 1)
                                {
                                    Files.Add(new FileInfo(item));
                                }
                            }
                        }
                        );

                return _addTemplateCommand;
            }
        }
        private ICommand _showTemplateCommand;
        public ICommand ShowTemplateCommand
        {
            get
            {
                if (_showTemplateCommand == null)
                    _showTemplateCommand = new RelayCommand(
                        x =>
                        {
                            if (FilesSelectedIndex >= 0)
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start(Files[FilesSelectedIndex].FilePath);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("File opening failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                        );

                return _showTemplateCommand;
            }
        }
        private ICommand _removeTemplateCommand;
        public ICommand RemoveTemplateCommand
        {
            get
            {
                if (_removeTemplateCommand == null)
                    _removeTemplateCommand = new RelayCommand(
                        x =>
                        {
                            if (FilesSelectedIndex >= 0)
                            {
                                Files.RemoveAt(FilesSelectedIndex);
                            }
                        }
                        );

                return _removeTemplateCommand;
            }
        }
        private ICommand _deleteTemplateCommand;
        public ICommand DeleteTemplateCommand
        {
            get
            {
                if (_deleteTemplateCommand == null)
                    _deleteTemplateCommand = new RelayCommand(
                        x =>
                        {
                            if (FilesSelectedIndex >= 0)
                            {
                                try
                                {
                                    MessageBoxResult result = MessageBox.Show($"You want to delete: {Files[FilesSelectedIndex].FileName}. Are you sure?", "Delete confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                                    if (result == MessageBoxResult.Yes)
                                    {
                                        File.Delete(Files[FilesSelectedIndex].FilePath);
                                        Files.RemoveAt(FilesSelectedIndex);
                                    }
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("File delete failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                        );

                return _deleteTemplateCommand;
            }
        }
        private ICommand _showHelpFileCommand;
        public ICommand ShowHelpFileCommand
        {
            get
            {
                if (_showHelpFileCommand == null)
                    _showHelpFileCommand = new RelayCommand(
                        x =>
                        {
                            if (FilesSelectedIndex >= 0)
                            {
                                try
                                {
                                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                                    Uri uri = new Uri(HelpFilePath, UriKind.RelativeOrAbsolute);
                                    process.StartInfo.FileName = uri.LocalPath;
                                    process.Start();
                                    process.WaitForExit();
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("File opening failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                        );

                return _showHelpFileCommand;
            }
        }
        private ICommand _aboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (_aboutCommand == null)
                    _aboutCommand = new RelayCommand(
                        x =>
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("Author: Jakub Kubasiak");
                            sb.AppendLine();
                            sb.Append("e-mail: kubakubasiak@gmail.com");
                            sb.AppendLine();
                            sb.Append("website: http://kubakubasiak.wixsite.com/home");
                            sb.AppendLine();
                            MessageBox.Show(sb.ToString(), "About", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        );

                return _aboutCommand;
            }
        }

        private ICommand _windowClosing;
        public ICommand WindowClosing
        {
            get
            {
                if (_windowClosing == null)
                    _windowClosing = new RelayCommand(
                        x =>
                        {
                            try
                            {
                                SerializeFilesList();

                            }
                            catch (Exception)
                            {
                                MessageBox.Show("File delete failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        );

                return _windowClosing;
            }
        }
        private ICommand _createDocumentCommand;
        public ICommand CreateDocumentCommand
        {
            get
            {
                if (_createDocumentCommand == null)
                    _createDocumentCommand = new RelayCommand(
                        x =>
                        {
                            if (x != null)
                            {
                                Page page = x as Page;
                                SerializeFilesList();
                                DocumentProcessor dp = new DocumentProcessor(Files[FilesSelectedIndex].FilePath);
                                dp.MapBuildingFormElements();

                                if (dp.ConstructingFormElements != null && dp.ConstructingFormElements.Count > 0)
                                {
                                    BuildDocumentViewModel viewModel = new BuildDocumentViewModel(dp);
                                    BuildDocumentPage bdp = new BuildDocumentPage(viewModel);
                                    page.NavigationService.Navigate(bdp);
                                }
                                else
                                {
                                    dp.ConstructedText = dp.OrginalText;
                                    dp.MapEvaluationFormElements();
                                    if (dp.ValuesFormElements != null && dp.ValuesFormElements.Count > 0)
                                    {
                                        EvaluateDocumentViewModel viewModel = new EvaluateDocumentViewModel(dp);
                                        EvaluateDocumentPage edp = new EvaluateDocumentPage(viewModel);
                                        page.NavigationService.Navigate(edp);
                                    }
                                    else
                                    {
                                        MessageBox.Show("The template does not contain dynamic elements or is invalid", "Template Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }
                            }
                        }
                        );

                return _createDocumentCommand;
            }
        }
        private void SerializeFilesList()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Save.xml");
            List<string> list = new List<string>();

            foreach (var item in Files)
            {
                list.Add(item.FilePath);
            }
            string xml = "";
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
                xmlSerializer.Serialize(stringWriter, list);

                xml = stringWriter.ToString();
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, false))
            {
                file.Write(xml);
            }
        }
        private List<string> DeserializeFilesList()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Save.xml");
            if (File.Exists(path))
            {
                List<string> list = new List<string>();
                try
                {
                    var serializer = new XmlSerializer(typeof(List<string>));
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        StreamReader stream = new StreamReader(fs, Encoding.UTF8);
                        list = (List<string>)serializer.Deserialize(new XmlTextReader(stream));
                        return list as List<string>;
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return null;
        }
        #endregion
        #region Reszta
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
