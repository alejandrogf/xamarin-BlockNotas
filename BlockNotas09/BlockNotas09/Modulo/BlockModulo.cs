using System;
using Autofac;
using BlockNotas09.Service;
using BlockNotas09.Util;
using BlockNotas09.View;
using BlockNotas09.ViewModel;
using Xamarin.Forms;

namespace BlockNotas09.Modulo
{
    public class BlockModulo:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServicioDatosImpl>().As<IServicioDatos>().SingleInstance();
            //Registras la sesión, como single prque queremos que solo haya una
            builder.RegisterType<Session>().SingleInstance();

            //Se registran las view y viewmodel
            builder.RegisterType<Login>();
            builder.RegisterType<Principal>();
            builder.RegisterType<Registro>();
            builder.RegisterType<NuevoBlockView>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<PrincipalViewModel>();
            builder.RegisterType<RegistroViewModel>();
            builder.RegisterType<NuevoBlockViewModel>();


            //Es una funcion como las de javascript de var = function.....
            builder.RegisterInstance<Func<Page>>(() =>
            {   /*Se busca primero si es masterdetailpage porque hay algunos tipos de páginas, 
                por ejemplo la de carrusel, que tiene una estructura rara, como de pagina dentro de página
                y tienes que navegar sobre la "segunda" página por tanto miras si la página en la que estás
                es de este tipo y si no es así, pillas toda la página en sí.*/
                var masterP = App.Current.MainPage as MasterDetailPage;
                var page = masterP != null ? masterP.Detail : App.Current.MainPage;
                var navigation = page as IPageContainer<Page>;
                return navigation != null ? navigation.CurrentPage : page;
            });
        }
         
    }
}