using App1.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class AboutViewModel : BaseViewModel, IConverterEvents
    {
        public AboutViewModel()
        {
            Title = "Ionix rock \\,,/";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }


        //Com o converter vai ter um looping infinito pegar a mudança pelo set... melhor pegar pelo IConverterEvents.UnfocusEvent
        public decimal DecimalBinder { get; set; }

        public async Task UnfocusEvent()
        {
            //executa alguma alteração na tela ou chamar um NotifyPropertyChanged
        }
    }
}