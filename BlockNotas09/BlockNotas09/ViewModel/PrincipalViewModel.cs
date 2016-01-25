using System.Collections.ObjectModel;
using System.Windows.Input;
using BlockNotas09.Factorias;
using BlockNotas09.Model;
using BlockNotas09.Service;
using BlockNotas09.Util;
using Xamarin.Forms;

namespace BlockNotas09.ViewModel
{
    public class PrincipalViewModel:GeneralViewModel
    {
        //La carga de la lista de blocks de un usuario la hacemos en el momento del login, no justo al
        //llamar aquí

        //Las listas en mvvm son siempre observablecollection, porque así se refrescan automáticamente
        private ObservableCollection<Block> _blocks;

        public ObservableCollection<Block> Blocks
        {
            get { return _blocks; }

            set { SetProperty(ref _blocks, value); }
        }

        public ICommand cmdNuevo { get; set; }

        public PrincipalViewModel(INavigator navigator, IServicioDatos servicio, Session session) :
            base(navigator, servicio, session)
        {
            cmdNuevo=new Command(NuevoBlock);
        }

        private async void NuevoBlock()
        {
            await _navigator.PushAsync<NuevoBlockViewModel>(viewModel =>
            {
                viewModel.Titulo = "Nuevo Block";
                viewModel.Blocks = Blocks;//Estás pasando la referencia, no se multiplica la información
            });
        }
    }
}