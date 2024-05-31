using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MarcaTento.Extensions
{
    /*
        Isso é uma classe de extensão. O que significa que ela
    extende uma classe padrão do APS.NET, porém implementando
    novos recursos. 
        A principal função dessa classe é utilizada dentro dos
    controllers para mostrar badrequests.
        A palavra reservada "this" indica que todos modelStates
    irão recever o novo método. Ela vem antes do parâmetro da
    função.
     */
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState) 
        {
            var result = new List<string>();

            foreach (var item in modelState.Values)
                result.AddRange(item.Errors.Select(error => error.ErrorMessage));
    
            return result;
        }
    }
}
