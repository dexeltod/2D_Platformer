using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Transform _eyePosition;
    private Rigidbody2D _rigidbody;

    public int Health { get; private set; }

    public Transform EyePosition => _eyePosition;

    public void ApplyDamage(int damage)
    {
        Health -= damage;
    }
    public void OnPlayerBodyTypeSwitch()
    {
        if (_rigidbody.isKinematic == false)
            _rigidbody.isKinematic = true;
        else
        {
            _rigidbody.isKinematic = false;
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
}
