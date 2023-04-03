using Infrastructure.Services;
using UnityEngine;

namespace View.UI_Scripts.Shop
{
    public class GameStopper : MonoBehaviour
    {
        private InputService _inputSystemReaderService;

        private void OnEnable()
        {
            _inputSystemReaderService.DisableInputs();
            const int PauseTime = 0;
            Time.timeScale = PauseTime;
        }

        private void OnDisable()
        {
            _inputSystemReaderService.EnableInputs();
            const int NormalTime = 1;
            Time.timeScale = NormalTime;
        }
    }
}