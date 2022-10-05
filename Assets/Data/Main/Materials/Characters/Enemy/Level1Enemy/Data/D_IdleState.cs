
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data Data/Idle state")]
public class D_IdleState : ScriptableObject
{
    public float MinIdleTime = 1f;
    public float MaxIdleTime = 3f;
}
