using System;
using System.Threading.Tasks;
using BlockNotas09.ViewModel.Base;
using Xamarin.Forms;

namespace BlockNotas09.Factorias
{
    //Interfaz para manejar todas las operaciones de push y pop, para navegar por las pantallas
    public interface INavigator
    {
        //Estas tres primeras son para eliminar ventanas
        //Se les pasa el iviewmodel, porque con él sabemos que vista es
        Task<IViewModel> PopAsync();
        //Con este vuelves a la pantalla modal (que solo hay una por "momento" en la aplicación, 
        //pero puede variar si se llama a otra ventana y se establece como modal)
        Task<IViewModel> PopModalAsync();
        //Elimina todas las ventanas en memoria y vuelve a la pantalla principal
        Task PopToRootAsync();
        
        //Se puede hacer también push pasando un viewmodel concreto
        //lo del where indica que se obliga a que el tviewmodel sea un clase Y implemente el IViewModel
        Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> action = null) where TViewModel:class,IViewModel;
        Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel;

        //Push para pantallas "modales", que es la pantalla principal que aparece al abrir la aplicación
        Task<TViewModel> PushModalAsync<TViewModel>(Action<TViewModel> action = null) where TViewModel : class, IViewModel;
        Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel;


    }
}