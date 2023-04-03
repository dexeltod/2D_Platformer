using Infrastructure.GameLoading;
using Infrastructure.Services.Factory;
using UnityEngine;

namespace View.UI_Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _height;
        [SerializeField] private float _closeness = -23;
        private Transform _player;

        private IPlayerFactory _playerFactory;

        private void Start()
        {
            _playerFactory = ServiceLocator.Container.GetSingle<IPlayerFactory>();
            _playerFactory.MainCharacterCreated += Initialize;
        }

        private void FixedUpdate() =>
            UpdateCameraPosition();

        private void Initialize()
        {
            _playerFactory.MainCharacterCreated -= Initialize;
            _player = _playerFactory.MainCharacter.transform;
        }

        private void UpdateCameraPosition()
        {
            if(_player == null)
                return;
		
            Vector3 position = new Vector3(_player.position.x, _player.position.y + _height, _closeness);
            transform.position = position;
        }
    }
}