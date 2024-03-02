using LootLocker.Requests;
using UnityEngine;

namespace SpeedJam4
{
    public class LootLockerManager : MonoBehaviour
    {
        private void Awake()
        {
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (!response.success)
                {
                    Debug.Log($"Error starting LootLocker session: {response.text}");

                    return;
                }

                Debug.Log("Successfully started LootLocker session");
            });
        }
    }
}
