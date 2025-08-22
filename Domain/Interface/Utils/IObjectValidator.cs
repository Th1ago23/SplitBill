using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Utils
{
    public interface IObjectValidator
    {
        public void Validate<T>(T obj) where T : class;
    }
}
