using Microsoft.EntityFrameworkCore;
using WebApi1.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi1.Data
{
    /*
     * RECUERDA QUE ESTO ES EL PRINCIPIO " C O D E  F I R S T "
     * Esta clase que hereda de "DbContext" nos permite configurar las tablas (entidades) asi como
     * acceder a los datos mediante el codigo
     * 
     * Para migrar los datos (y si es la primera vez se crea la base y la tabla) usamos 2 comandos
     * 
     *  -> ADD-MIGRATION & UPDATE-DATABASE <-
     *  (estos comandos deben de ser en la consola de "administrador de paquetes")
     *  Si ya existe la base de datos ya no la vuelve a crear
     *  Si hay nuevos modelos en el DbContext los crea como tablas en la base de datos
     *  
     *  Cuando se realiza la migracion le podemos poner el nombre que sea para que sea algo descriptivo
     *  PM> add-migration AgregarBaseDatos
     *  
     *  Cuando termine se crea la carpeta "Migrations" el archivo "20230801062549_AgregarBaseDatos" es un script
     *  que se creo con el comando
     *  AHORA NECESITAMOS EJECUTAR ESE SCRIPT para que se vea reflejado en la base de datos (update-database)
     */
    public class AplicationDboContext : DbContext
    {
        //constructor
        public AplicationDboContext(DbContextOptions<AplicationDboContext> options) : base(options)
        {
        }
            
        //PROPIEDAD para colocar "items" en las tablas (entidades)
        public DbSet<Villa> Villas {  get; set; }

        //vamos a llenar las tablas de la BD y darle algunos valores por default a la tabla
        //para ello vamos a sobreescribir un metodo de la clase DbContext y en el con la propiedad
        //HasData 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(new Villa()
            {
                Id = 1,
                Nombre = "Villa del real tecamac",
                Detalle ="casita chida",
                ImgUrl="",
                Ocupantes=6,
                MetrosCuadrados=30,
                Tarifa=1234,
                Amenidad="muchas",
                FechaCreacion=DateTime.Now,
                FechaActualizacion=DateTime.Now

            },
            new Villa()
            {
                Id = 2,
                Nombre = "heroes de tecamac",
                Detalle = "privada exclusiva",
                ImgUrl = "",
                Ocupantes = 3,
                MetrosCuadrados = 40,
                Tarifa = 7890,
                Amenidad = "2 pisos",
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now

            }
         
            );

            //para que este model builder se agrege a la base de datos nuevamente tenemos que hacer una migracion y despues actualizar
            //como la primera vez podemos darle el nombre que queramos en este ejemplo
            //  add-migration AlimentarTablaVilla
            //NOTA QUE EL "AlimentarTablaVilla" se ve reflejado en los archivos de la carptea "Migrations" ahi es donde podemos
            //ver los scripts generados por las migraciones
            //ya para que se vea reflejado en la BD ejecutamos update-database

            //POR ULTIMO ESTA CLASE SERA INYECTADA EN EL CONTROLADOR "VillaController" asi que checa bien ahi



        }

    }
}
