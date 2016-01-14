using System;
using System.ComponentModel;
// Se crea una interface, que luego se implementará en un viewmodel base, con las tareas que queremos que sean
//comunes a todos los viewmodel
namespace BlockNotas09.ViewModel.Base
{
    //Hereda de la clase que notifica cuando una propiedad de un elemento ha cambiado
    public interface IViewModel:INotifyPropertyChanged
    {
        /*Asi se crea un elemento para que todos los viewmodel tengan un titulo*/
        string Titulo { get; set; }
        //Se utiliza para fijar el estado del viewmodel, para ver si ha cambiado alguna propiedad de una clase
        //es un delegado ¿Ò.ó?
        //Encapsulas un objeto
        void SetState<T>(Action<T> action) where T : class, IViewModel;

    }
}