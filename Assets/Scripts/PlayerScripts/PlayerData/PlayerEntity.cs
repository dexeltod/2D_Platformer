using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public int Health { get; private set; }

    [SerializeField] private Transform _eyePosition;
    public Transform EyePosition => _eyePosition;

    public void ApplyDamage(int damage)
    {
        Health -= damage;
    }
}
