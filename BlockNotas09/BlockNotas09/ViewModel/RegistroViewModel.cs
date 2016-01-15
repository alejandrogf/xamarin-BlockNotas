using System.Windows.Input;
using BlockNotas09.Factorias;
using BlockNotas09.Model;
using BlockNotas09.Service;
using Xamarin.Forms;

namespace BlockNotas09.ViewModel
{
    public class RegistroViewModel:GeneralViewModel
    {
        //Para que el usuario ejecute la acción
        public ICommand cmdAlta { get; set; }

        public Usuario Usuario
        {
            get { return _usuario; }
            set { SetProperty(ref _usuario, value); }
        }

        private Usuario _usuario;
        public RegistroViewModel(INavigator navigator, IServicioDatos servicio) : base(navigator, servicio)
        {
            
        }

        private async void GuardarUsuario()
        {
            try
            {
                IsBusy = true;
                var r = await _servicio.AddUsuario(_usuario);
                if (r!=null)
                {
                   await  _navigator.PushModalAsync<PrincipalViewModel>();
                }
                else
                {
                    var a = "";//para controlar si va bien, en teoria aqui va un dialogo
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}