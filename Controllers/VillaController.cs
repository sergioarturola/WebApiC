using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi1.Data;
using WebApi1.Models;
using WebApi1.Models.Dto;
/*
 * En este controlador usamos "db context" con "code first" para que se arreglen 
 */
namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class VillaController : ControllerBase // clase controlador
                                                  // 
    {
        //vamos a crear un "logging" con ayuda de la interfaz para que pueda ser inyectado en el constructor
        //de este controlador
        //1ro creamos una variable privada y que solo pueda ser modificada en el constructor
        private readonly ILogger<VillaController> _logger;

        //para poder inyectar el AplicationDboContext y trabajar con los datos que ahora si son persistentes creamos la variable
        private readonly AplicationDboContext _db;

        //inyectamos
        public VillaController(ILogger<VillaController> logger, AplicationDboContext db)

        {
            //Esto es con el fin de poder mandar mensajes o ver que esta pasando al momento
            //de las peticiones http como se ve en las diferentes funciones, estos mensajes
            //son visibles en la consola
            _logger = logger;

            //inyectando el modelo de la base esto nos servira en los metodos
            _db = db;

        }

        /*  M E T O D O S  H T T P              */

        //GET-----------------------------------------------------------------------------------------------------
        [HttpGet]//especificando el verbo http en este caso GET
        [ProducesResponseType(StatusCodes.Status200OK)]//documentando el codigo de estado
        //un ActionResult es lo que retorna 
        public ActionResult<IEnumerable<VillaDto>> GetVillas() //endpoint que retorna una coleccion de objetos del tipo "Villa" llamado GetVillas
        {
            //retornando correcto
            _logger.LogInformation("Obtener villas");
            //return Ok(VillaStore.villaList); ahora lo retornamos con los datos de la base de datos y no de la clase
            //objeto + clase entidad es como decir "select * from Villas"
            return Ok(_db.Villas.ToList());

        }

       
        //tambien destacar que los endpoints pueden tener nombre para poder indentificarlos o para redireccionar informacion a ellos
        [HttpGet("id : int", Name = "GetVilla")]
        //documentando los codigos de respuesta
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDto> GetVilla(int id) //endpoint que retorna villa especifica, para diferenciarlo del anterior le pasamos el parametro "id"
        {
            if(id == 0)//si esta mal el id por ejemplo si se pone una letra
            {
                _logger.LogError("Error al traer la villa solicitada: " + id);
                return BadRequest();//retorna el codigo 400
            }

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id); usando la clase quje simulaba la base
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if(villa == null)
            {
                return NotFound();//si es nulo retorna el codigo 404
            }

            return Ok(villa);//si encontro reigstro retorna codigo 200
        }

        //POST-----------------------------------------------------------------------------------------------------

        [HttpPost]
        //configurando los codigos de estado
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        /*
         * Le indiciamos que recibe como parametro una entrada de datos "FromBody" del tipo VillaDto
         */
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villadto)//endpoint para crear nuevos registros
        {

            //con las anotaciones del modelo vamos a validar que este correcto, el ModelState
            //lo toma de lo que este declarado en el ActionResult (en este caso es <VillaDto>
            //en este caso si un campo no es valido (como estar en blanco o tener mas caracteres
            //mandamos un BadRequest asi entra al if y se evita que continue las demas lineas de codigo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //si ya existe un registro entonces evitamos que se sobreescriba
            //antes (VillaStore.villaList.FirstOrDefault
            if (_db.Villas.FirstOrDefault(v => v.Nombre.ToLower() == villadto.Nombre.ToLower()) != null)
            {
                //para poder agregar el modelo personalizado (que basicamente es un JSON que nosotros mismos creamos)
                //recibe 2 parametros 
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);

            }

            //realizando una validacion antes de ingresar datos
            if (villadto == null)
            {
                return BadRequest(villadto);
            }
            /*
             * le decimos que mayor a cero genere el error porque en el body va a tener el id 0 y cuando se mande el dato
             * a ese cero se le va a asignar el ultimo valor de la lista ordenada (OrderByDescending) mas UNO asi obtendreomos
             * siempre al agregar un item el ultimo "id"
             */

            if (villadto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            /*
             * si no entra en los if entonces el dato esta correcto y le asignamos un id
             * 
             
            villadto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villadto);
            YA NO LO USAMOS PORQUE AHORA ES USADO POR LA BASE DE DATOS
            
            En cambio vamos a usar un M O D E L O para que guarde los datos
            */

            Villa modelo = new()
            {
                // Id = villadto.Id,no lo necesitamos porque se genera y autoincrementa de manera automatica
                Nombre = villadto.Nombre,
                Detalle = villadto.Detalle,
                ImgUrl = villadto.ImgUrl,
                Ocupantes = villadto.Ocupantes,
                Tarifa = villadto.Tarifa,
                MetrosCuadrados = villadto.MetrosCuadrados,
                Amenidad = villadto.Amenidad

            };

            //agregandolo a la base (como cuando decimos insert into Villas (campo1, campo2, campo3) values(valor1, valor2, valor3)
            //y despues guardamos (como el equivalente a decir "execute update")
            _db.Villas.Add(modelo);
            _db.SaveChanges();

            //en lugar de que retorne el item agregado con codigo 200 lo dirigimos al endpoint
            return CreatedAtRoute("GetVilla", new {id= villadto.Id}, villadto);
            

            
        }



        //DELETE-----------------------------------------------------------------------------------------------------
        [HttpDelete("id : int")]//indicando que es un endpoint para borrar mediante un id
        //documentando los codigos de respuesta
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        //en este caso no necesitamos el modelo como tal porque no retornaremos ningun contenido
        public IActionResult DeleteVilla(int id)
        {
            //es necesario verificar que el id sea valido
            if(id == 0)
            {
                return BadRequest();    
            }

            //en una variable vamos a guardar (si es que coincidio) el resultado de la busqueda
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            //en caso de que regrese null retornamos "not found"
            if(villa == null) 
            {
                return NotFound();
            }

            //en caso de que todo este correcto y no haya entrado en los if de validacion entonces
            //procedemos a eliminarlo (como es una lista usamos el metodo Remove) como "villa" ya tiene
            ////ese item pues se lo pasamos
            //VillaStore.villaList.Remove(villa); ya no lo usamos porque estamos usando la bd

            //ahora le pasamos la variable villa al metodo REMOVE
            _db.Villas.Remove(villa);
            _db.SaveChanges(); //para que se guarden los cambios
            

            //como estamos trabajando con delete pues no retornamos nada (es solo una accion que estamos realizando)
            //entonces no retornamos ningun contenido

            return NoContent();
        }

        //PUT-----------------------------------------------------------------------------------------------------
        [HttpPut("id : int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        //como parametro recibe el id para poder indentificar que "villa" queremos actualizar
        //despues de recibir el objeto que queremos actualizar del tipo VillaDto llamado villaDto
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villadto)
        {
            //validando que la peticion sea correcta
            if (villadto == null || id != villadto.Id)//si el id que esgtoy recibiendo es diferente al que hay en el objeto
            {
                return BadRequest();
            }
            /*
            //si no entra en el if entonces en una variable podemos guardar el item
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //de villa ahora vamos a actualizar todas sus propiedades (porque asi trabaja put reemplaza todo el objeto)
            //de las que llegen del cuerpo de villaDto
            villa.Nombre = villaDto.Nombre;
            villa.Amenidad = villaDto.Amenidad;

            YA NO USAMOS ESTO AHORA DE IGUAL MANERA PODEMOS CREAR UN MODELO PARA GESTIONAR "Patch" para poder
            llenar las propiedades
            */

            Villa modelo = new()
            {
                Id = villadto.Id,
                Nombre = villadto.Nombre,
                Detalle = villadto.Detalle,
                ImgUrl = villadto.ImgUrl,
                Ocupantes = villadto.Ocupantes,
                Tarifa = villadto.Tarifa,
                MetrosCuadrados = villadto.MetrosCuadrados,
                Amenidad = villadto.Amenidad

            };

            //actualizando la base de datos
            _db.Villas.Update(modelo);
            _db.SaveChanges();


            //como es una actualizacion no retornamos nada, asi que no regresmaos contenido
            return NoContent();

        }

        //PATCH-----------------------------------------------------------------------------------------------------

        /*
         *  I M P O R T A N T E
         *  para poder realizar patch necesitamos instalar los siguientes paquetes nuget
         *  -Microsoft.AspNetCore.JsonPatch
         *  -Microsoft.AspNetCore.MVC.NewtonsoftJson
         */

        [HttpPatch("id : int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        //como parametro recibe el id para poder indentificar que "villa" queremos actualizar
        //despues de recibir el objeto en vez de recibir el cuerpo invocamos al json patch
        //le decimos que es del tipo "VillaDto" y de nombre se llama "patchDto"
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            //validando que la peticion sea correcta
            if (patchDto == null || id == 0)//si el id que esgtoy recibiendo es diferente al que hay en el objeto
            {
                return BadRequest();
            }

            /*
                si no entra en el if entonces en una variable podemos guardar el item
                var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                en vez de recibir el objeto (como en put) vamos a recibir el estado del modelo 
                patchDto.ApplyTo(villa, ModelState);

                En vez de hacer lo anterior vamos a capturar el primer registro que encuentre con el respectivo ID

                Es importante ponerle "AsNoTracking" porque sino EnityFramework no sabe que hacer con el "modelo anterior" ya
                que ambos tienen el mismo "ID" entonces asi le cerramos la pista del anterior y asi evitamos problemas de "tracking"
            */
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

            //ahora procedemos a "parchear" el modelo de la sig manera (no le llamamos aqui modelo)
            VillaDto villadto = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                ImgUrl = villa.ImgUrl,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad

            };


            if(villa == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(villadto, ModelState);//AQUI ME QUEDE

            //verificando que el model state sea valido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //AHORA SI creando el modelo despues de haberlo "parchado" esto con el fin de 
            //que se actualizen solo las propiedades que se modificaron
            Villa modelo = new()
            {
                Id = villadto.Id,
                Nombre = villadto.Nombre,
                Detalle = villadto.Detalle,
                ImgUrl = villadto.ImgUrl,
                Ocupantes = villadto.Ocupantes,
                Tarifa = villadto.Tarifa,
                MetrosCuadrados = villadto.MetrosCuadrados,
                Amenidad = villadto.Amenidad

            };

            //finalmente actualizamos
            _db.Update(modelo);

            //y guardamos los cambios
            _db.SaveChanges();

            //como es una actualizacion no retornamos nada, asi que no regresmaos contenido
            return NoContent();



            /* En Swagger se ve asi 
             * [
                 {
                    "path": "/nombre", -> es la propiedad a actualizar
                     "op": "replace", -> la operacion que vamos a realizar (reemplazar)
                     "value": "Parchando endpoint" ->el nuevo valor
                    }
                ]
             */

        }



    }
}
