using DataAccess.Contracts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ClienteRepository : MasterRepository, IClienteRepository
    {
        private readonly string select;
        private readonly string insert;
        private readonly string update;
        private readonly string delete;
        public ClienteRepository()
        {
            select = "SELECT Usuario.id, Usuario.nombre, apaterno, amaterno, apodo, pin, imgpath, correo, TipoUsuario.id, TipoUsuario.nombre, tipousuario FROM Usuario INNER JOIN TipoUsuario ON Usuario.tipousuario_id = TipoUsuario.id";
            insert = "INSERT INTO Usuario VALUES (@Nombre,@APaterno,@AMaterno,@Apodo,@Pin,@ImgPath,@Correo,@IdTipoUsuario)";
            update = "UPDATE Usuario SET nombre=@Nombre,apaterno=@APaterno,amaterno=@AMaterno,apodo=@Apodo,pin=@Pin,imgpath=@ImgPath,correo=@Correo,tipousuario_id@IdTipoUsuario WHERE id=@Id";
            delete = "DELETE FROM Usuario WHERE id=@Id";
        }
        public int Add(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public int Edit(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
