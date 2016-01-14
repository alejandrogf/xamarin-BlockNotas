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

            if (data.Count>0)
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
    }
}
