using System.Collections.ObjectModel;
using System.Windows.Input;
using BlockNotas09.Factorias;
using BlockNotas09.Model;
using BlockNotas09.Service;
using BlockNotas09.Util;
using Xamarin.Forms;

namespace BlockNotas09.ViewModel
{
    /*Hay que manejar la referencia al navegador y al servicio*/
    public class LoginViewModel:GeneralViewModel
    {
        //Los eventos se capturan con el command
        public ICommand cmdLogin { get; set; }
        public ICommand cmdAlta { get; set; }

        public LoginViewModel(INavigator navigator, IServicioDatos servicio, Session session) : 
            base(navigator, servicio, session)
        {
            cmdLogin=new Command(IniciarSesion);
            cmdAlta=new Command(NuevoUsuario);
            Titulo = "Blocks Powah!";
        }

        public string TituloIniciar { get { return "Iniciar sesión"; } }
        public string TituloRegistro { get { return "Nuevo Usuario"; } }
        
        public string TituloLogin { get { return "Nombre de usuario"; } }
        public string TituloPassword { get { return "Contraseña"; } }

        private Usuario _usuario=new Usuario();
        public Usuario Usuario
        {
            get { return _usuario; }
            set { SetProperty(ref _usuario, value); }
        }

        private async void IniciarSesion()
        {
            try
            {
                IsBusy = true;
                var us = await _servicio.Validar(_usuario);
                if (us!=null)
                {
                    Session["usuario"] = us;//Guardo el usuario
                    var listadoblocks = await _servicio.GetBlocks(us.Id);
                    
                    await _navigator.PopToRootAsync();
                    await _navigator.PushAsync<PrincipalViewModel>(viewModel =>
                    {
                        //Para pasar objetos a otro viewmodel(y su vista asociada) es aquí
                        //Si están definidos en el viewmodel destino
                        viewModel.Titulo = "Pantalla Principal";
                        viewModel.Blocks=new ObservableCollection<Block>(listadoblocks);    
                    });
                }
                else
                {
                    var xxx = "";//para poner un break y ver como funciona
                }
                //TODO: Aquí navegariamos a la pantalla principal o dariamos error
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void NuevoUsuario()
        {
            await _navigator.PopToRootAsync();
            await _navigator.PushModalAsync<RegistroViewModel>(viewModel =>
            {
                viewModel.Titulo = "Registro de Nuevo Usuario";
            });
        }
    }
}