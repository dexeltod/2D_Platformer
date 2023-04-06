using Game.PlayerScripts.Weapons;
using Game.PlayerScripts.Weapons.WeaponTypes;

namespace Game.Enemy
{
	public interface IWeaponVisitor
	{
		 void FistVisit(Fist fist);
		 void RangeWeaponVisit(RangeAbstractWeapon shotGun);
	}
}