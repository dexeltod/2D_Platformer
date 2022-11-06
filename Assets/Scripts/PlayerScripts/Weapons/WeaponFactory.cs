using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
	private Animator _animator;
	private AnimationHasher _animationHasher;

	public WeaponBase CreateWeapon(WeaponBase weaponBase, Transform parent, Animator animator, AnimationHasher animationHasher)
	{
		var instantiatedWeapon = Instantiate(weaponBase, parent);
		weaponBase.Initialize(animator, animationHasher);
		return instantiatedWeapon;
	}
}