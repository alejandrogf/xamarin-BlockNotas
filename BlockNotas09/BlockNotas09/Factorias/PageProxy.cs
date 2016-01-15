using System;
using Xamarin.Forms;
//Necesitamos un mecanismo que nos devuelva el contexto de navegacion para la página actual.
namespace BlockNotas09.Factorias
{
    public class PageProxy:IPage
    {
        readonly Func<Page> _page;

        public PageProxy(Func<Page> page)
        {
            _page = page;
        }

        public INavigation Navigation { get { return _page().Navigation; } }
    }
}