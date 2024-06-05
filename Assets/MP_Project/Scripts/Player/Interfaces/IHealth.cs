namespace Core.Interfaces
{
    public interface IHealth
    {
        int CurrentHealth { get; }
        void TakeDamageServerRpc(int damage, ulong attackerId);
        void Heal(int amount);
    }
}