using BlockNotas09.Factorias;
using BlockNotas09.Service;
using BlockNotas09.Util;
using BlockNotas09.ViewModel.Base;

namespace BlockNotas09.ViewModel
{
    public class GeneralViewModel:ViewModelBase
    {
        //Todos los servicios que se quieren que estén en todas las pantallas es mejor
        //ponerlas aquí para no tener que repetirlas por todas partes

        //De esta forma ya tengo un objeto sesión en la memoria del dispositivo, ya que se construye
        //Aquí que es de el que heredan todos los viewmodel

        protected INavigator _navigator;
        protected IServicioDatos _servicio;
        protected Session Session { get; set; }

        public GeneralViewModel(INavigator navigator, IServicioDatos servicio, Session session)
        {
            _navigator = navigator;
            _servicio = servicio;
            Session = session;
        }
    }
}