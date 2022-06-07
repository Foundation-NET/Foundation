namespace Foundation.Annotations 
{
    public interface IFactoryBuilder
    {
        Object GetService(System.Object? type);
    }
}