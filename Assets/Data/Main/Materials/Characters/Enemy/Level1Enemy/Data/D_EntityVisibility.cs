using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data Data/Base Data")]
public class D_EntityVisibility : ScriptableObject
{
    [Header("Settings for detect enemy")]
    public float VisibilityRange = 4f;
    public float AngleOfVisibility = 20f;

    [Header("Detect ground")]
    public float WallCheckDistance = 0.2f;
    public float LedgeCheckDistance = 0.4f;
    public LayerMask WhatIsTouched;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsEnemy;
}


