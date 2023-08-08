using System.ComponentModel.DataAnnotations;

namespace WebApi1.Models.Dto
    //Esta clase nos ayuda a no trabajar tan directamente con el modelo como su nombre lo indica (dto)
    //va a transferir los datos entre el CONTROLADOR y el MODELO
    //LA USAMOS CUANDO SE CREA UN NUEVO REGISTRO
{
    public class VillaCreateDto
    {
        //le quitamos el ID porque ese se crea de manera automatica
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
