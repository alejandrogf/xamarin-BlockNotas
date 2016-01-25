using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlockNotas09.ViewModel.Base;
using Xamarin.Forms;

namespace BlockNotas09.Factorias
{
    public class ViewFactory:IViewFactory
    {
        /*En el diccionario guardamos todas las referencias de los objetos
        se compone como clave=valor
        Como clave siempre es el viewmodel y el valor = view
        El register se hace sobre _map*/
        readonly IDictionary<Type,Type> _map=new Dictionary<Type, Type>();
        
        /*Es el contexto de ejecución de autofac*/
        private readonly IComponentContext _componentContext;

        public ViewFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        /*En el diccionario que se ha creado se registra, para cada elemento de tipo TViewModel (la clave)
        un elemento de tipo TView (el valor)*/
        public void Register<TViewModel, TView>() where TViewModel : class, IViewModel where TView : Page
        {
            _map[typeof (TViewModel)] = typeof (TView);
        }

        public Page Resolve<TViewModel>(Action<TViewModel> action = null) where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            return Resolve<TViewModel>(out viewModel, action);

        }

        public Page Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> action = null) where TViewModel : class, IViewModel
        {
            //Se recupera el tipo de viewmodel ya que se le ha pasado vacío.
            viewModel = _componentContext.Resolve<TViewModel>();
            //como tengo el viewmodel, recupero el tipo de view que tiene relacionado (loginView, altaView, etc)
            var tipoVista = _map[typeof (TViewModel)];
            //Ahora obtengo el objeto, que no el tipo, ya que es lo que necesito xq tiene los datos
            var vista = _componentContext.Resolve(tipoVista) as Page;
            //action = onclick creo... La acción que se hace en ese momento 
            //(darse de alta cuando pulsas el boton darse de alta)
            if (action!=null)
            {
                viewModel.SetState(action);
            }
            //Relacionas la vista y el viewmodel, que es de donde obtiene los datos para esa vista concreta
            vista.BindingContext = viewModel;
            return vista;
        }

        public Page Resolve<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            var tipoVista = _map[typeof (TViewModel)];
            var vista = _componentContext.Resolve(tipoVista) as Page;
            vista.BindingContext = viewModel;
            return vista;
        }
    }
}
