using System.Collections.Generic;
using UI_Scripts.Shop;
using UnityEngine;

public class WeaponList : MonoBehaviour
{
    [SerializeField] private ShopItemView _weaponPanelPrefab;
    [SerializeField] private List<ItemInfo> _weapons;

    private void Awake()
    {
        Initialize();
    }


    private void Initialize()
    {
        for (var i = 0; i < _weapons.Count; i++)
        {
            var weapon = _weapons[i];
            var currentWeapon = Instantiate(_weaponPanelPrefab, transform);
            currentWeapon.Initialize(weapon.Title, weapon.Description, weapon.Price, weapon.Sprite);
        }
    }
}
