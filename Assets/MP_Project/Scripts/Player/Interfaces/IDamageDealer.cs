namespace Core.Interfaces
{
    public interface IDamageDealer
    {
        int Damage { get; }
        void DealDamage(IHealth target);
    }
}