using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BadCupboard : BaseCupboard
{
    [SerializeField] private AlarmIncreaser _alarm;
}
