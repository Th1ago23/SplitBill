namespace Domain.Interface.Utils
{
    public interface IObjectValidator
    {
        public void Validate<T>(T obj) where T : class;
    }
}
