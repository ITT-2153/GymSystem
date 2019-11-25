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
    public class RutinaRepository : MasterRepository, IRutinaRepository
    {
        private readonly string select;
        private readonly string insert;
        private readonly string update;
        private readonly string delete;
        public RutinaRepository()
        {
            select = "SELECT * FROM Rutina";
            insert = "INSERT INTO Rutina VALUES (@Nombre,@APaterno,@AMaterno,@Apodo,@Pin,@ImgPath,@Correo,@FNacimiento,@Peso,@Estatura,@Genero)";
            update = "UPDATE Rutina SET nombre=@Nombre,apaterno=@APaterno,amaterno=@AMaterno,apodo=@Apodo,pin=@Pin,imgpath=@ImgPath,correo=@Correo,fnacimiento=@FNacimiento,peso=@Peso,estatura=@Estatura,genero=@Genero WHERE id=@Id";
            delete = "DELETE FROM Rutina WHERE id=@Id";
        }
        public int Add(Rutina entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@Dia", entity.Dia),
                new SqlParameter("@Repeticiones", entity.Repeticiones),
                new SqlParameter("@Peso", entity.Peso),
                new SqlParameter("@IdEjercicio", entity.IdEjercicio),
                new SqlParameter("@IdUsuario",entity.IdUsuario)
            };
            return ExecuteNonQuery(insert);
        }

        public int Edit(Rutina entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@Dia", entity.Dia),
                new SqlParameter("@Repeticiones", entity.Repeticiones),
                new SqlParameter("@Peso", entity.Peso),
                new SqlParameter("@IdEjercicio", entity.IdEjercicio),
                new SqlParameter("@IdUsuario",entity.IdUsuario)
            };
            return ExecuteNonQuery(update);
        }

        public IEnumerable<Rutina> GetAll()
        {
            var tableResult = ExecuteReader(select);
            var listRutinas= new List<Rutina>();
            foreach (DataRow item in tableResult.Rows)
            {
                listRutinas.Add(new Rutina
                {
                    Id = Convert.ToInt32(item[0]),
                    Nombre = item[1].ToString(),
                    Dia = item[2].ToString(),
                    Repeticiones = Convert.ToInt32(item[3]),
                    Peso = Convert.ToDecimal(item[4]),
                    IdEjercicio = Convert.ToInt32(item[5]),
                    IdUsuario = Convert.ToInt32(item[6])
                }); ;
            }
            return listRutinas;
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
