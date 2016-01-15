using BlockNotas09.Factorias;
using Autofac;
using Xamarin.Forms;

namespace BlockNotas09.Modulo
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ViewFactory>().As<IViewFactory>().SingleInstance();

            builder.RegisterType<Navigator>().As<INavigator>().SingleInstance();

            builder.Register<INavigation>(ctx=> App.Current.MainPage.Navigation).SingleInstance();
        }
    }
}