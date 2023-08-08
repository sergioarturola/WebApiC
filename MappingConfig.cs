using AutoMapper;
using WebApi1.Models;
using WebApi1.Models.Dto;

namespace WebApi1

    /*
     * Esta clase nos va a ayudar con el mapeo de los objetos para asi convertir de
     * un tipo a otro para ellos necesitamos instalar 2 paquetes
     * -AutoMapper
     * -AutoMapper.InyectionDependency
     * NOTA: Este servicio se debe de agregar en Program.cs
     */
{
    //para configurar la clase de manera correcta necesitamos que herede de 
    //"Profile"
    public class MappingConfig : Profile
    {
        //creando los mapeos

        public MappingConfig()
        {
            //entre las <> es necesario indicar la fuente y el destino
            //es necesario realizar las operaciones al derecho y al revez
            //por cada modelo crearemos un mapa

            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();

            //podemos ahorrar las lineas anteriores de la siguiente manera
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
        }
    }
}
