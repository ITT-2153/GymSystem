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
    public class UsuarioModel
    {
        private readonly IUsuarioRepository usuarioRepository;
        private List<UsuarioModel> listUsuarios;

        private string id;
        private string nombre;
        private string apellidoPaterno;
        private string apellidoMaterno;
        private string apodo;
        private string pin;
        private string imgPath;
        private string correo;
        private string idTipoUsuario;
        private string nombreTipoUsuario;
        private string tipoUsuario;

        public EntityState EntityState { private get; set; }
        public string Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }
        public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }
        public string Apodo { get => apodo; set => apodo = value; }
        public string Pin { get => pin; set => pin = value; }
        public string ImgPath { get => imgPath; set => imgPath = value; }
        public string Correo { get => correo; set => correo = value; }
        public string IdTipoUsuario { get => idTipoUsuario; set => idTipoUsuario = value; }
        public string NombreTipoUsuario { get => nombreTipoUsuario; set => nombreTipoUsuario = value; }
        public string TipoUsuario { get => tipoUsuario; set => tipoUsuario = value; }

        public UsuarioModel()
        {
            usuarioRepository = new UsuarioRepository();
        }
        public string Savechanges()
        {
            string message = null;
            try
            {
                var usuarioDataModel = new Usuario
                {
                    Id = Convert.ToInt32(id),
                    Nombre = nombre,
                    ApellidoPaterno = apellidoPaterno,
                    ApellidoMaterno = apellidoMaterno,
                    Apodo = apodo,
                    Pin = pin,
                    ImgPath = imgPath,
                    Correo = correo,
                    IdTipoUsuario = Convert.ToInt32(idTipoUsuario),
                    NombreTipoUsuario = nombreTipoUsuario,
                    TipoUsuario = Convert.ToChar(tipoUsuario)
                };

                switch (EntityState)
                {
                    case EntityState.Added:
                        usuarioRepository.Add(usuarioDataModel);
                        message = "Registro agregado.";
                        break;
                    case EntityState.Modified:
                        usuarioRepository.Edit(usuarioDataModel);
                        message = "Registro editado.";
                        break;
                    case EntityState.Deleted:
                        usuarioRepository.Remove(Convert.ToInt32(id));
                        message = "Registro eliminado.";
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex is System.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 2627)
                    message = "Registro repetido.";
                else
                    message = ex.ToString();
            }
            return message;
        }
        public List<UsuarioModel> GetAll()
        {
            var tipoUsuarioDataModel = usuarioRepository.GetAll();
            listUsuarios = new List<UsuarioModel>();
            foreach (Usuario item in tipoUsuarioDataModel)
            {
                listUsuarios.Add(new UsuarioModel
                {
                    id = item.Id.ToString(),
                    nombre = item.Nombre,
                    apellidoPaterno = item.ApellidoPaterno,
                    apellidoMaterno = item.ApellidoMaterno,
                    apodo = item.Apodo,
                    pin = item.Pin,
                    imgPath = item.ImgPath,
                    correo = item.Correo,
                    idTipoUsuario = item.IdTipoUsuario.ToString(),
                    nombreTipoUsuario = item.NombreTipoUsuario,
                    tipoUsuario = item.TipoUsuario.ToString()
                });
            }
            return listUsuarios;
        }
        public bool LoginUser(string user, string pin)
        {
            return usuarioRepository.Login(user, pin);
        }
        public IEnumerable<UsuarioModel> FindBy(string filter)
        {
            return listUsuarios.FindAll(e => e.id.Contains(filter) ||
                                             e.nombre.Contains(filter) ||
                                             e.ApellidoPaterno.Contains(filter) ||
                                             e.apellidoMaterno.Contains(filter) ||
                                             e.apodo.Contains(filter) ||
                                             e.pin.Contains(filter) ||
                                             e.correo.Contains(filter) ||
                                             e.idTipoUsuario.Contains(filter) ||
                                             e.nombreTipoUsuario.Contains(filter) ||
                                             e.tipoUsuario.Contains(filter));
        }
    }
}
