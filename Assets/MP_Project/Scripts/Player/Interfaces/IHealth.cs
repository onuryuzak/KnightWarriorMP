namespace Core.Interfaces
{
    public interface IHealth
    {
        int CurrentHealth { get; }
        void TakeDamageServerRpc(int damage);
        void Heal(int amount);
    }
}
