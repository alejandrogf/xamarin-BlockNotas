using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockNotas09.ViewModel.Base;
using Xamarin.Forms;

namespace BlockNotas09.Factorias
{
    public class Navigator:INavigator
    {
        //navigation es el que da xamarin. navigator el que he hecho yo
        /*
        lazy se encarga del lazyloading
        _page permite saber y cargar y recuperar la página
        _factory se encarga de la relación de vistas y viewmodel
            */
        private readonly Lazy<INavigation> _navigation;
        private readonly IViewFactory _viewFactory;
        //private readonly IPage _page;

        public Navigator(Lazy<INavigation> navigation, IViewFactory viewFactory, IPage page)
        {
            _navigation = navigation;
            _viewFactory = viewFactory;
            //_page = page;
        }

        public INavigation Navigation { get { return _navigation.Value; } }

        //Llama al metodo popasync del objeto navigation y devuelve el viewmodel que tiene relacionado
        public async Task<IViewModel> PopAsync()
        {
            Page vista = await Navigation.PopAsync();
            return vista.BindingContext as IViewModel;
        }

        public async Task<IViewModel> PopModalAsync()
        {
            Page vista = await Navigation.PopModalAsync();
            return vista.BindingContext as IViewModel;
        }

        public async Task PopToRootAsync()
        {
            await Navigation.PopToRootAsync();
        }

        public async Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> action = null) where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            //OUT es un objeto de salida, sive para tener dos objetos de salida (uno con return y otro el out)
            //Es un objeto solo de escritura que es forzado a que se escriba en él
            var vista = _viewFactory.Resolve<TViewModel>(out viewModel, action);
            await Navigation.PushAsync(vista);
            return viewModel;
        }

        public async Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            var vista = _viewFactory.Resolve<TViewModel>(out viewModel);
            await Navigation.PushAsync(vista);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(Action<TViewModel> action = null) where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var vista = _viewFactory.Resolve<TViewModel>(out viewModel, action);
            await Navigation.PushModalAsync(vista);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            var vista = _viewFactory.Resolve<TViewModel>(out viewModel);
            await Navigation.PushModalAsync(vista);
            return viewModel;
        }
    }
}
