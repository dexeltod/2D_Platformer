using Game.Animation.AnimationHashes.Characters;
using UnityEngine;

namespace Game.PlayerScripts.Weapons
{
    public class WeaponFactory : MonoBehaviour
    {
        [SerializeField] private MeleeTrigger.MeleeWeaponTriggerInformant _meleeWeaponTrigger;
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationHasher _animationHasher;

        public AbstractWeapon CreateWeapon(AbstractWeapon weaponBase, Transform parent)
        {
            weaponBase.gameObject.SetActive(false);
            weaponBase.enabled = false;
		
            AbstractWeapon instantiatedAbstractWeapon = Instantiate(weaponBase, parent);
            instantiatedAbstractWeapon.Initialize(_animator, _animationHasher, _meleeWeaponTrigger);
		
            instantiatedAbstractWeapon.gameObject.SetActive(true);
            instantiatedAbstractWeapon.enabled = true;
            return instantiatedAbstractWeapon;
        }
    }
}