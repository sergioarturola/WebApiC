using WebApi1.Models.Dto;

namespace WebApi1.Data
{
    //clase que simula una base de datos
    public static class VillaStore 
    {

        public static List<VillaDto> villaList = new List<VillaDto>
        {
            //agregando objetos
            new VillaDto { Id = 1, Nombre = "Vista a la piscina", MetrosCuadrados = 12},
            new VillaDto {Id = 2, Nombre = "Vista a la playa", MetrosCuadrados = 10}
        };
    
    }
}
