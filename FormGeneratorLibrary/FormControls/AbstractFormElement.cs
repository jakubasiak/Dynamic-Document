using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormGeneratorLibrary.FormControls
{
    public abstract class AbstractFormElement : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Expression { get; set; }

        public abstract event PropertyChangedEventHandler PropertyChanged;

        public abstract string GetValue();
    }
}
