using System.ComponentModel.DataAnnotations;

namespace WebApi1.Models.Dto
    //Esta clase nos ayuda a no trabajar tan directamente con el modelo como su nombre lo indica (dto)
    //va a transferir los datos entre el CONTROLADOR y el MODELO
{
    public class VillaUpdateDto
    {
        [Key] //anotacion para que "Id" sea la llave primaria en la base de datos 
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public double MetrosCuadrados { get; set; }
        public string ImgUrl { get; set; }
        public string Amenidad { get; set; }

        //no colocamos los datos DATETIME porque eso es mas cuando se graba en la base de datos
        //asi que lo omitimos aqui
    }
}
