using System.ComponentModel.DataAnnotations;

namespace DatatableCRUD.Models
{
    [MetadataType(typeof(UsuariosMetadata))]
    public partial class Usuarios
    {

    }

    public class UsuariosMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor adicione o nome")]
        public string UsuarioNome { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor adicione o sobrenome")]
        public string UsuarioSobrenome { get; set; }
    }
}