using System;
using Autofac;
using BlockNotas09.Service;
using Xamarin.Forms;

namespace BlockNotas09.Modulo
{
    public class BlockModulo:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServicioDatosImpl>().As<IServicioDatos>().SingleInstance();
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