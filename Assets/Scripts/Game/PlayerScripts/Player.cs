using Game.PlayerScripts.Move;
using Game.PlayerScripts.StateMachine;
using UI_Scripts.Shop;
using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerScripts
{
    [RequireComponent( typeof(PhysicsMovement))]
    public class Player : MonoBehaviour
    {
        private PhysicsMovement _physicsMovement;
        private float _lookDirection = -1;
        public float LookDirection => _lookDirection;

        private StateService _stateService;

        public event UnityAction<int, ItemScriptableObject> Bought;

        private void Awake()
        {
            _physicsMovement = GetComponent<PhysicsMovement>();
        }

        private void OnEnable()
        {
            _physicsMovement.Rotating += SetLookDirection;
        }

        public void TryBuyWeapon(ItemScriptableObject itemScriptableObject) =>
            Bought?.Invoke(itemScriptableObject.Price, itemScriptableObject);

        private void OnDisable()
        {
            _physicsMovement.Rotating -= SetLookDirection;
        }

        private void SetLookDirection(bool direction)
        {
            int rotateDirection = direction ? 0 : 180;
            transform.rotation = Quaternion.Euler(0, rotateDirection, 0);
        }
    }
}