using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//Se crea un viewmodel base, del que heredarán todos, aqui se ponen todas las propiedades que se quieren
//que tengan todas las vistas
namespace BlockNotas09.ViewModel.Base
{
    public class ViewModelBase:IViewModel
    {
        //Implementación by Luisito de cosecha propia
        /*Son properties que puedo usar en todas las pantallas.
        _isbusy se usa para que este a true cuando está pillada y false cuando haya terminada
        _opacity oscurece la pantalla
        _enabled desactiva la pantalla
        De esta forma se simula el circulito de "cargando" mientras la aplicación está haciendo algo,
        con la pantalla oscurecida e inactiva mientras _isbusy == true (está ocupado) y luego lo elimina
        Hay que desmarcar la opción "auto-propertie" de estas variables 
        (ratón encima, bombilla, encapsulate fiel y desmarcar el auto-propertie)
            */
        private bool _isBusy ;
        private double _opacity;
        private bool _enabled;


        //Manejador del cambio de propiedades.
        //Cada vez que cambie una propiedad se lanzará este handler
        public event PropertyChangedEventHandler PropertyChanged;

        //Inicializas la opacidad y el habilitado para que no haya problemas al arrancar
        public ViewModelBase()
        {
            Opacity = 1;
            Enabled = true;
        }

        public string Titulo { get; set; }

        public double Opacity
        {
            get { return _opacity; }
            set { SetProperty(ref _opacity, value); }
        }
        
        public bool Enabled
        {
            get { return _enabled; }
            set { SetProperty(ref _enabled, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (value)
                    Opacity = 0.5;
                else
                    Opacity = 1;

                Enabled = !value;

                SetProperty(ref _isBusy, value);
            }
        }

        /* Con esto se implementa el two-way. Como lo estás definiendo en el VM base,
        todos los demas que heredarán de él, ya tendrán esto definido y solo habrá que llamarlo
        para los elementos que queremos que sean two-way (por ejemplo una lista que se filtra)

        Recibe la variable por referencia, no la variable en si
        a continuacion el valor a fijar
        a continuacion el nombre de la variable que se indica con el callermembername
        
        El primer paso se comprueba si el elemento que se quiere hacer two-way ha cambiado su valor
            */
        protected virtual bool SetProperty<T>(ref T variable, T valor,
            [CallerMemberName] string nombre = null)
        {
            /*Si la variable(que como pasas por referencia es el valor que tenia
            y el valor nuevo son iguales, no se hace nada*/
            if (object.Equals(variable, valor))
            {
                return false;
            }
            variable = valor;
            OnPropertyChanged(nombre);
            return true;
        }
        //Notifica al interfaz que ha cambiado el valor y así este refresca
        protected void OnPropertyChanged([CallerMemberName] string nombre = null)
        {
            var handler = PropertyChanged;
            if (handler!=null)
            {
                //This es viewmodel base o quien herede de él que lanza el evento del cambio
                //this es el sender, el que ha lanzado el evento y property recibe la propiedad que ha cambiado
                handler(this,new PropertyChangedEventArgs(nombre));
            }
        }
        /*Es invocado y se llama al metodo action que esta definido internamente
        de esta forma tienes el objeto viewmodel que está trabajando*/
        public void SetState<T>(Action<T> action) where T : class, IViewModel
        {
            
            action?.Invoke(this as T);
        }
    }
}
