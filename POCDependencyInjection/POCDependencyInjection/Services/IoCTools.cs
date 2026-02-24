namespace POCDependencyInjection.Services
{
    public interface IGuidGenerator
    {
        Guid Guid { get; init; }


    }

    public interface ISingleton: IGuidGenerator { }
    public interface IScoped : IGuidGenerator { }
    public interface ITransient: IGuidGenerator { }

    public class Singleton : ISingleton
    {
        public Guid Guid { get; init ; }
        public Singleton()
        {
            Guid = Guid.NewGuid();
        }
    }

    public class Scoped: IScoped
    {
        public Guid Guid { get; init; }
        public Scoped()
        {
            Guid = Guid.NewGuid();
        }
    }

    public class Transient: ITransient
    {
        public Guid Guid { get; init; }
        public Transient()
        {
            Guid = Guid.NewGuid();
        }
    }

    public class GuidService
    {
        public ISingleton Singleton { get; set; }
        public IScoped Scoped { get; set; }
        public ITransient Transient { get; set; }

        public GuidService(ISingleton singleton, IScoped scoped, ITransient transient)
        {
            Singleton = singleton;
            Scoped = scoped;
            Transient = transient;
        }
    }
}
