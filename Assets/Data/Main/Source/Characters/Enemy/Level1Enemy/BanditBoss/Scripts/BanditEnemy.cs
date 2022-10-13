using UnityEngine;

public class BanditEnemy : MonoBehaviour
{
    [SerializeField] private PlayerParams _player;
    [SerializeField] private float _radius;
    [Header("Set params enemy")]
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    Bounds _bounds;
    private Rigidbody2D _rb2d;

     public int Health { get; private set; }
    [SerializeField] public int Damage { get; private set; }

    private bool _canSee;
    private RaycastHit2D _hit;
    private Collider2D[] _hits;
    private Vector2 _eyePoint;
    [SerializeField] private float _eyePointHeight;

    

    
    public BanditEnemy()
    {
        Damage = _damage;
        Health = _health;
    }

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckDistanceBetweenCharacter();
    }

    public void ApplyDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"EnemyDetect health: {Health}");

        if (Health <= 0 && gameObject != null)
        {
            Debug.Log("EnemyDetect is dead");
            Destroy(gameObject);
        }
    }

    private void CheckDistanceBetweenCharacter()
    {
        _eyePoint = new Vector2(transform.position.x, transform.position.y + _eyePointHeight);
    }
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

}