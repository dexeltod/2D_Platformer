using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    public Bullet Create(Transform parent = null)
    {
        return Instantiate(_bulletPrefab, parent);
    }
}