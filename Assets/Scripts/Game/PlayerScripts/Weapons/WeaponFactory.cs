using PlayerScripts.Weapons;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
	[SerializeField] private MeleeWeaponTriggerInformant _meleeWeaponTrigger;
	[SerializeField] private Animator _animator;
	[SerializeField] private AnimationHasher _animationHasher;

	public WeaponBase CreateWeapon(WeaponBase weaponBase, Transform parent)
	{
		weaponBase.gameObject.SetActive(false);
		weaponBase.enabled = false;
		
		WeaponBase instantiatedWeapon = Instantiate(weaponBase, parent);
		instantiatedWeapon.Initialize(_animator, _animationHasher, _meleeWeaponTrigger);
		
		instantiatedWeapon.gameObject.SetActive(true);
		instantiatedWeapon.enabled = true;
		return instantiatedWeapon;
	}
}