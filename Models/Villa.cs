using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi1.Models
{
    /*Esta clase representa el modelo en la base de datos, cada propiedad es una columna
     * en la base de datos esto con ayuda de ENITY FRAMEWORK, para ello instalamos los
     * siguientes paquetes:
     * - Microsoft.EntityFrameworkCore.SqlServer -> con instalar ese paquete ya tenemos "enity framework" y las "relaciones"
     * -Microsoft.EntityFrameworkCore.Tools
     * 
     * En este caso vamos a usar "CODE FIRST" que significa que no necesitamos que esten creadas en la base de datos las TABLAS
     * sino que las tablas se crean en base a los modelos que vayamos creando.
     * 
     * La cadena de conexion a la base de datos la colocamos en aspssetings.json le podemos poner el nombre que deseemos 
     * en nuestro caso se llama "DefaultConnection"
     */
    public class Villa
    {
        [Key] //anotacion para que "Id" sea la llave primaria en la base de datos 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//esto es para que cada ID se genere de manera automatica y se vaya autoincrementando
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public double MetrosCuadrados { get; set; }
        public  string ImgUrl { get; set; }
        public string Amenidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
