using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PrgressBarWPF.Annotations;

namespace PrgressBarWPF
{
    public class PBar : INotifyPropertyChanged
    {
        public double _progressPercentage = 0;
        public double ProgressPercentage
        {
            get => _progressPercentage;
            set
            {
                if (Math.Abs(value - _progressPercentage) < -1) return;
                _progressPercentage = value;
                OnPropertyChanged(nameof(ProgressPercentage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
