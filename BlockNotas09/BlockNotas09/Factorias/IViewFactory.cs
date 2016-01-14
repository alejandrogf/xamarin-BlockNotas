using System;
using BlockNotas09.ViewModel.Base;
using Xamarin.Forms;

/*
Hay que instalar el nuGet AutoFac que permite gestionar y automatizar la gestión de inyeccion de dependencias
Luego se crea este interface se encarga de construir vistas su trabajo es hacer que el contenedor de inyeccion 
de depencias sepa que vista va con que viewmodel y hacer todas las relaciones
*/

namespace BlockNotas09.Factorias
{
    public interface IViewFactory
    {
        /*Guardas en el contenedor la relación del viewmodel y el view
        Que tiene que ser el TviewModel un objeto(clase) y del tipo TViewModel
        Tview tiene que ser un objeto de tipo Page*/
        void Register<TViewModel, TView>() where TViewModel : class, IViewModel where TView : Page;
        /*Con el resolve se recuperan los objetos de la vista que está relacionada con el viewmodel
        Se puede hacer de tres fromas*/
        Page Resolve<TViewModel>(Action<TViewModel> action = null) where TViewModel : class, IViewModel;

        Page Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> action = null) 
                                 where TViewModel : class, IViewModel;

        Page Resolve<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel;
    }
}