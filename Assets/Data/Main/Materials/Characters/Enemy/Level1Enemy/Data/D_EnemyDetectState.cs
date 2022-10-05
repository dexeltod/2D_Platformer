using UnityEngine;


[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data Data/Detect state")]
public class D_EnemyDetectState : ScriptableObject
{
    public float RunSpeed = 1f;
    public float MaxDistanceToFollow = 5f;
}
