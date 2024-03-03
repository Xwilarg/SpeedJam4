using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SpeedJam4.Player
{
    public class InputManager : MonoBehaviour
    {
        public void OnRestart(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                SceneManager.LoadScene("Main");
            }
        }

        public void OnFire(InputAction.CallbackContext value)
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.OnFire(value);
            }
        }
    }
}
