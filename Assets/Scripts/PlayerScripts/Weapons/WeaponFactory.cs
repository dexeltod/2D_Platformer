using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private Animator _animator;
    private AnimationHasher _animationHasher;

    public Weapon CreateWeapon(Transform parent, Animator animator, AnimationHasher animationHasher)
    {
        var weapon = Instantiate(_weapon, parent);
        weapon.transform.position = parent.position;
        weapon.Initialize(animator,animationHasher);
        return weapon;
    }
}
