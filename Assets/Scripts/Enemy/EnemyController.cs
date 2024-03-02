using SpeedJam4.Player;
using SpeedJam4.Timer;
using UnityEngine;

namespace SpeedJam4.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Animator _anim;

        private Vector2 _lastDir = Vector2.down;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponentInChildren<Animator>();

            _anim.SetFloat("X", _lastDir.x);
            _anim.SetFloat("Y", _lastDir.y);
        }

        private void FixedUpdate()
        {
            if (TimeController.Instance.IsActive)
            {
                var dir = (PlayerController.Instance.transform.position - transform.position).normalized;

                _rb.velocity = dir * 2f;
                _lastDir = dir;

                _anim.SetBool("IsMoving", true);
                _anim.SetFloat("X", _lastDir.x);
                _anim.SetFloat("Y", _lastDir.y);
            }
            else
            {
                _rb.velocity = Vector2.zero;
                _anim.SetBool("IsMoving", false);
            }
        }
    }
}
