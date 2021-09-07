using System;
using System.Collections.Generic;
using System.Text;

namespace App1.ViewModels
{
    public class TabViewModel : BaseViewModel
    {
        public TabViewModel() : base()
        {
            IsSelected = false;
        }

        private bool _IsSelected { get; set; }
        public bool IsSelected
        {
            get => _IsSelected; set
            {
                _IsSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                OnPropertyChanged(nameof(NameOpacity));
            }
        }

        public float NameOpacity { get { return IsSelected ? 1 : 0.6f; } }

        public string Name { get; set; }


    }
}
