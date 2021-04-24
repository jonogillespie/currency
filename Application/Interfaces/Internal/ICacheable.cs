namespace Application.Interfaces.Internal
{
    public interface ICacheable
    {
        public int LifetimeSeconds() => 60;
    }
}