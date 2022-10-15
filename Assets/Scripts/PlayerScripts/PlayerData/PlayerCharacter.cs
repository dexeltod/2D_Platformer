using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Transform _eyePosition;

    public int Health { get; private set; }
    public Transform EyePosition => _eyePosition;

    public void ApplyDamage(int damage)
    {
        Health -= damage;
    }
}
