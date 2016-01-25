using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockNotas09.Model;

namespace BlockNotas09.Service
{
    
    public interface IServicioDatos
    {
        #region Usuario
        //En tareas asyncronas siempre devuelven un objeto TASK. Si no se indica un tipo, devuelve un void.
        //Devuelven un TASK y un RESULT, el task puede ser de un tipo concreto o vacio (void)
        Task<Usuario> Validar(Usuario usuario);
        Task<Usuario> AddUsuario(Usuario usuario);
        Task<Usuario> UpdateUsuario(Usuario usuario, String id);
        Task DeleteUsuario(String id);

        #endregion

        #region Block

        Task AddBloc(Block block);
        Task<List<Block>> GetBlocks(String usuario); //Recuperas todos los blocks de un usuario
        Task DeleteBlock(Block block);
        Task UpdateBlock(Block block);

        #endregion

    }
}
