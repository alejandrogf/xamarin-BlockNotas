using BlockNotas09.Factorias;
using BlockNotas09.Service;
using BlockNotas09.ViewModel.Base;

namespace BlockNotas09.ViewModel
{
    public class GeneralViewModel:ViewModelBase
    {
        //Todos los servicios que se quieren que estén en todas las pantallas es mejor
        //ponerlas aquí para no tener que repetirlas por todas partes

        protected INavigator _navigator;
        protected IServicioDatos _servicio;

        public GeneralViewModel(INavigator navigator, IServicioDatos servicio)
        {
            _navigator = navigator;
            _servicio = servicio;
        }
    }
}