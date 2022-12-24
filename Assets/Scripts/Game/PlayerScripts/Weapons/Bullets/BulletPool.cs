using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BulletFactory))]
public class BulletPool : MonoBehaviour
{
    private const int Capacity = 30;

    private BulletFactory _bulletFactory;
    private List<Bullet> _bullets = new List<Bullet>(Capacity);

    private void Awake()
    {
        _bulletFactory = GetComponent<BulletFactory>();
        Initialize();
    }

    public Bullet Get(Transform parent)
    {
        Bullet bullet = _bullets.FirstOrDefault(bullet => bullet.gameObject.activeSelf == false);
        bullet.gameObject.SetActive(true);
        bullet.transform.position = parent.position;

        if (bullet != null)
            return bullet;

        return null;
    }

    private void Initialize()
    {
        for (int i = 0; i < Capacity; i++)
        {
            _bullets.Add(_bulletFactory.Create());
            _bullets[i].gameObject.SetActive(false);
        }
    }
}