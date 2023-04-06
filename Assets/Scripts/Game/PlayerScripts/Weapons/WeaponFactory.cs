using Cysharp.Threading.Tasks;
using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Weapons.MeleeTrigger;
using UnityEngine;

namespace Game.PlayerScripts.Weapons
{
    public class WeaponFactory : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorFacade _animatorFacade;
        [SerializeField] private AnimationHasher _animationHasher;
        [SerializeField] private MeleeWeaponTrigger _meleeWeaponTrigger;

        public async UniTask<AbstractWeapon> CreateWeapon(AbstractWeapon weaponBase, Transform parent)
        {
            weaponBase.gameObject.SetActive(false);
            weaponBase.enabled = false;
		
            AbstractWeapon instantiatedAbstractWeapon = Instantiate(weaponBase, parent);
            await instantiatedAbstractWeapon.Initialize(_animator, _animatorFacade, _animationHasher, _meleeWeaponTrigger);
		
            instantiatedAbstractWeapon.gameObject.SetActive(true);
            instantiatedAbstractWeapon.enabled = true;
            return instantiatedAbstractWeapon;
        }
    }
}