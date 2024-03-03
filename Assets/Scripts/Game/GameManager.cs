using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpeedJam4.Game
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            SceneManager.LoadScene($"Level{StaticData.NextLevel:00}", LoadSceneMode.Additive);
        }
    }
}
