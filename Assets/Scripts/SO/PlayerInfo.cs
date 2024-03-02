using UnityEngine;

namespace SpeedJam4.SO
{
    [CreateAssetMenu(fileName = "PlayerInfo", menuName = "SpeedJam4/PlayerInfo")]
    public class PlayerInfo : ScriptableObject
    {
        [Tooltip("Size and time to reach max charge force")] public float MaxChargeForce = 2f;
        [Tooltip("Multiplier added to the propulsion force when firing")] public float PropulsionVelMultiplier = 10f;
        [Tooltip("Speed reduction when hitting a wall, it is too low it'll increase it instead")] public float WallBounceDamping = 10f;
        [Tooltip("Velocity under which we just stop the player and wait for next shoot")] public float MinVelocity = .1f;
    }
}