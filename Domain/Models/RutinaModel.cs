using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RutinaModel
    {
        private readonly IRutinaRepository rutinaRepository;
        private List<RutinaModel> listRutinas;

        private string id;
        private string nombre;
        private string dia;
        private string repeticiones;
        private string peso;
        private string idEjercicio;
        private string idUsuario;

        public EntityState EntityState { private get; set; }
        public string Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Dia { get => dia; set => dia = value; }
        public string Repeticiones { get => repeticiones; set => repeticiones = value; }
        public string Peso { get => peso; set => peso = value; }
        public string IdEjercicio { get => idEjercicio; set => idEjercicio = value; }
        public string IdUsuario { get => idUsuario; set => idUsuario = value; }

        public RutinaModel()
        {
            rutinaRepository = new RutinaRepository();
        }
        public string Savechanges()
        {
            string message = null;
            try
            {
                var rutinaDataModel = new Rutina
                {
                    Id = Convert.ToInt32(id),
                    Nombre = nombre,
                    Dia = dia,
                    Repeticiones = Convert.ToInt32(repeticiones),
                    Peso = Convert.ToDecimal(peso),
                    IdEjercicio = Convert.ToInt32(idEjercicio),
                    IdUsuario = Convert.ToInt32(idUsuario)
                };

                switch (EntityState)
                {
                    case EntityState.Added:
                        rutinaRepository.Add(rutinaDataModel);
                        message = "Rutina agregada.";
                        break;
                    case EntityState.Modified:
                        rutinaRepository.Edit(rutinaDataModel);
                        message = "Rutina agregada.";
                        break;
                    case EntityState.Deleted:
                        rutinaRepository.Remove(Convert.ToInt32(id));
                        message = "Rutina agregada.";
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex is System.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 2627)
                    message = "Rutina repetida.";
                else
                    message = ex.ToString();
            }
            return message;
        }
        public List<RutinaModel> GetAll()
        {
            var rutinaDataModel = rutinaRepository.GetAll();
            listRutinas = new List<RutinaModel>();
            foreach (Rutina item in rutinaDataModel)
            {
                listRutinas.Add(new RutinaModel
                {
                    id = item.Id.ToString(),
                    nombre = item.Nombre,
                    dia = item.Dia,
                    repeticiones = item.Repeticiones.ToString(),
                    peso = item.Peso.ToString(),
                    idEjercicio = item.IdEjercicio.ToString(),
                    idUsuario = item.IdUsuario.ToString()

                });
            }
            return listRutinas;
        }
        public IEnumerable<RutinaModel> FindBy(string filter)
        {
            return listRutinas.FindAll(e => e.id.Contains(filter) ||
                                             e.nombre.Contains(filter) ||
                                             e.dia.Contains(filter) ||
                                             e.repeticiones.Contains(filter) ||
                                             e.peso.Contains(filter) ||
                                             e.idEjercicio.Contains(filter) ||
                                             e.idUsuario.Contains(filter));
        }
    }
}
