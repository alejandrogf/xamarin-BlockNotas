using Autofac;
using BlockNotas09.Factorias;
using BlockNotas09.View;
using BlockNotas09.ViewModel;
using Xamarin.Forms;

namespace BlockNotas09.Modulo
{
    public class Startup:AutofacBootstraper
    {   
        private readonly App _application;

        public Startup(App application)
        {
            _application = application;
        }

        public override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
            builder.RegisterModule<BlockModulo>();
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
            var viewFactory = container.Resolve<IViewFactory>();
            var main = viewFactory.Resolve<LoginViewModel>();
            var navpage=new NavigationPage(main);
            _application.MainPage = navpage;
        }
    }
}