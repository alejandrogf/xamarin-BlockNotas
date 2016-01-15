using Autofac;
using BlockNotas09.Factorias;
using BlockNotas09.View;
using BlockNotas09.ViewModel;

namespace BlockNotas09.Modulo
{
    public class Startup:AutofacBootstraper
    {   
        private readonly App _application;

        public Startup(App application)
        {
            _application = application;
        }

        protected override void RegisterViews(IViewFactory viewFactory)
        {
            //Aquí registras los viewmodel y los view, relcionandolos
            viewFactory.Register<LoginViewModel, Login>();
            viewFactory.Register<RegistroViewModel, Registro>();
            viewFactory.Register<PrincipalViewModel, Principal>();
        }

        protected override void ConfigureApplication(IContainer container)
        {
            var main = container.Resolve<Login>();
            _application.MainPage = main;
        }
    }
}