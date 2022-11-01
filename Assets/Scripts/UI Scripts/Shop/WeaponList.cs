using System.Collections.Generic;
using UI_Scripts.Shop;
using UnityEngine;

public class WeaponList : MonoBehaviour
{
    [SerializeField] private ShopItemView _weaponPanelPrefab;
    [SerializeField] private List<ItemInfo> _weapons;

    private void Awake()
    {
        foreach (var weapon in _weapons)
        {
            var currentWeapon = Instantiate(_weaponPanelPrefab, transform);
            currentWeapon.Init(weapon.Description, weapon.Price, weapon.Sprite);
        }
    }
}
