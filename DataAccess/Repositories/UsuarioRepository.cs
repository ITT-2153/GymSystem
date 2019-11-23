using DataAccess.Contracts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UsuarioRepository : LoginRepository, IUsuarioRepository
    {
        private readonly string select;
        private readonly string insert;
        private readonly string update;
        private readonly string delete;
        private readonly string login;

        public UsuarioRepository()
        {
            select = "SELECT Usuario.id, Usuario.nombre, apaterno, amaterno, apodo, pin, imgpath, correo, TipoUsuario.id, TipoUsuario.nombre, tipousuario FROM Usuario INNER JOIN TipoUsuario ON Usuario.tipousuario_id = TipoUsuario.id";
            insert = "INSERT INTO";
            update = "UPDATE";
            delete = "DELETE FROM Usuario WHERE id=@Id";
            login = "SELECT Usuario.id, Usuario.nombre, apaterno, amaterno, apodo, pin, imgpath, correo, TipoUsuario.id, TipoUsuario.nombre, tipousuario FROM Usuario INNER JOIN TipoUsuario ON Usuario.tipousuario_id = TipoUsuario.id WHERE Usuario.apodo =@Apodo AND Usuario.pin =@Pin";
        }
        public int Add(Usuario entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@Contrasena", entity.Pin),
                new SqlParameter("@Correo", entity.Correo),
                new SqlParameter("@IdTipoUsuario", entity.IdTipoUsuario)
            };
            return ExecuteNonQuery(insert);
        }

        public int Edit(Usuario entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@Contrasena", entity.Pin),
                new SqlParameter("@Correo", entity.Correo),
                new SqlParameter("@IdTipoUsuario", entity.IdTipoUsuario)
            };
            return ExecuteNonQuery(update);
        }

        public IEnumerable<Usuario> GetAll()
        {
            var tableResult = ExecuteReader(select);
            var listUsuarios = new List<Usuario>();
            foreach (DataRow item in tableResult.Rows)
            {
                listUsuarios.Add(new Usuario
                {
                    Id = Convert.ToInt32(item[0]),
                    Nombre = item[1].ToString(),
                    ApellidoPaterno = item[2].ToString(),
                    ApellidoMaterno = item[3].ToString(),
                    Apodo = item[4].ToString(),
                    Pin = item[5].ToString(),
                    ImgPath = item[6].ToString(),
                    Correo = item[7].ToString(),
                    IdTipoUsuario = Convert.ToInt32(item[8]),
                    NombreTipoUsuario = item[9].ToString(),
                    TipoUsuario = Convert.ToChar(item[10])

                });
            }
            return listUsuarios;
        }

        public bool Login(string apodo, string pin)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Apodo", apodo),
                new SqlParameter("@Pin", pin)
            };
            return Login(login);
        }

        public int Remove(int id)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            return ExecuteNonQuery(delete);
        }
    }
}
