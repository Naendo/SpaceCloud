namespace Coworking.Application.Interfaces
{
    public interface IInjector
    {
        TService InjectService<TService, TImplementation>() where TService : class
            where TImplementation : class;
    }
}