
using Domain.Interface.Utils;

namespace Application.Utils.Validator
{
    public class ObjectValidator: IObjectValidator
    {
        public void Validate<T>(T obj) where T : class{

            if (obj == null) throw new ArgumentNullException(nameof(obj), "Objeto nulo");
        }
    }
}
