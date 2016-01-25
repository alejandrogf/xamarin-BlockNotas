using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockNotas09.Model;
using BlockNotas09.Util;
using Microsoft.WindowsAzure.MobileServices;

namespace BlockNotas09.Service
{
    public class ServicioDatosImpl:IServicioDatos
    {
        //Con esta clase se implementa azure mobile service
        private MobileServiceClient cliente;

        public ServicioDatosImpl()
        {
            cliente=new MobileServiceClient(Cadenas.UrlServicio,Cadenas.TokenServicio);
        }

        //Operaciones de USUARIO
        #region USUARIO

        public async Task<Usuario> Validar(Usuario usuario)
        {
            var tabla = cliente.GetTable<Usuario>();
            var data = await tabla.CreateQuery().
                Where(o => o.Login == usuario.Login && o.Password == usuario.Password).
                ToListAsync();
            if (data.Count == 0)
            {
                return null;
            }
            return data[0];
        }

        public async Task<Usuario> AddUsuario(Usuario usuario)
        {
            var tabla = cliente.GetTable<Usuario>();
            var data = await tabla.CreateQuery().Where(o => o.Login == usuario.Login).ToListAsync();

            if (data.Count > 0)
            {
                throw new Exception("Usuario ya registrado.");
            }

            try
            {
                await tabla.InsertAsync(usuario);
            }
            catch (Exception e)
            {
                throw new Exception("Error al registrar el usuario");
            }
            return usuario;
        }

        public Task<Usuario> UpdateUsuario(Usuario usuario, string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUsuario(string id)
        {
            throw new NotImplementedException();
        }

        #endregion

        //Operaciones de BLOCK
        #region BLOCK

        public async Task AddBloc(Block block)
        {
            //Con gettable recuperas el formato de la tabla que le indicas.
            var tabla = cliente.GetTable<Block>();
            //Insertas en la tabla formateada el objeto recibido
            await tabla.InsertAsync(block);
        }

        public async Task<List<Block>> GetBlocks(string usuario)
        {
            var tabla = cliente.GetTable<Block>();
            //Se recuperan los blocks del usuario recibido, con la expresión lambda y se guardan
            //en una lista
            var data = await tabla.CreateQuery().Where(o => o.IdUsuario == usuario).ToListAsync();
            return data;
        }
        //Como usamos azure mobile services hay que ceñirnos a su arquitectura, por eso el delete y update
        //lo haces pasando el block entero
        public async Task DeleteBlock(Block block)
        {
            //Para borrar, mejor pasar el block entero, no solo el ID
            var tabla = cliente.GetTable<Block>();
            await tabla.DeleteAsync(block);
        }

        public async Task UpdateBlock(Block block)
        {
            var tabla = cliente.GetTable<Block>();
            await tabla.UpdateAsync(block);
        }

        #endregion


    }
}
