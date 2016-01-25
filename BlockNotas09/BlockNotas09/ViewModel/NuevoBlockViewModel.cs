using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlockNotas09.Factorias;
using BlockNotas09.Model;
using BlockNotas09.Service;
using BlockNotas09.Util;
using Xamarin.Forms;

namespace BlockNotas09.ViewModel
{
    public class NuevoBlockViewModel:GeneralViewModel
    {
        public ObservableCollection<Block> Blocks { get; set; }

        private Block _block;

        public Block Block
        {
            get { return _block; }
            set { SetProperty(ref _block, value); }
        }

        public ICommand cmdGuardar { get; set; }

        public NuevoBlockViewModel(INavigator navigator, IServicioDatos servicio, Session session) : 
            base(navigator, servicio, session)
        {
            _block=new Block();
            cmdGuardar=new Command(GuardarBlock);
        }

        private async void GuardarBlock()
        {
            Block.Fecha=DateTime.Now;
            Block.IdUsuario = (Session["usuario"] as Usuario).Id;
            Block.Icono = "";
            //añade a azure en la bbdd en la nube
            await _servicio.AddBloc(Block);
            //añade a local, a la colección para que se muestre automaticamente
            Blocks.Add(Block);
        }
    }
}
