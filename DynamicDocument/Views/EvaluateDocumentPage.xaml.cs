using DynamicDocument.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DynamicDocument.Views
{
    /// <summary>
    /// Interaction logic for EvaluateDocumentPage.xaml
    /// </summary>
    public partial class EvaluateDocumentPage : Page
    {
        public EvaluateDocumentPage(EvaluateDocumentViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();

            WPFControlsBuilder wpfControlsBuilder = new WPFControlsBuilder();

            foreach (var item in wpfControlsBuilder.BuildUIElements((DataContext as EvaluateDocumentViewModel).AbstractFormElements.ToList()))
            {
                container.Children.Add(item);
            }
        }
    }
}
