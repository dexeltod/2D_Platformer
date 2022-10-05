using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CooldownTest : MonoBehaviour
{
    [SerializeField ]private PlayerAttack _playerAttack;
    private float _colldownDelay;
    private bool _cooldownBool;

    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Update()
    {
        if(_playerAttack != null && _playerAttack.CurrentAttackDelay >= 0)
        {
            CountCooldown();
        }
        
    }

    private void CountCooldown()
    {
        _colldownDelay = _playerAttack.CurrentAttackDelay;
        _cooldownBool = _playerAttack.CurrentAttackState;
        if (_cooldownBool)
        {
            _textMeshPro.text += "CanAttack";
        }
        else
        {
            _textMeshPro.text += "CantAttack";
        }
        _textMeshPro.text = _colldownDelay.ToString();
    }
}
