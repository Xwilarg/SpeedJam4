using SpeedJam4.Player;
using SpeedJam4.Timer;
using UnityEngine;

namespace SpeedJam4.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private const float SpeedMultiplier = 2f;
        private const float Range = 2f;
        private const float AttackDist = 2f;

        [SerializeField]
        private GameObject _hint;

        private GameObject _hintInstance;

        private Rigidbody2D _rb;
        private Animator _anim;

        private Vector2 _lastDir = Vector2.down;

        private int _attackState;
        private float _timer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponentInChildren<Animator>();

            _anim.SetFloat("X", _lastDir.x);
            _anim.SetFloat("Y", _lastDir.y);
        }

        private void Update()
        {
            if (!TimeController.Instance.IsActive) return;

            if (_attackState == 1)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    _attackState = 0;
                    Destroy(_hintInstance);
                }
            }
        }

        private void FixedUpdate()
        {
            if (TimeController.Instance.IsActive && _attackState == 0)
            {
                if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= AttackDist)
                {
                    _attackState = 1;
                    _hintInstance = Instantiate(_hint, transform.up * Range, Quaternion.identity);
                    _hintInstance.transform.localScale = Vector3.one;
                }
                else
                {
                    var dir = (PlayerController.Instance.transform.position - transform.position).normalized;

                    _rb.velocity = dir * SpeedMultiplier;
                    _lastDir = dir;

                    _anim.SetBool("IsMoving", true);
                    _anim.SetFloat("X", _lastDir.x);
                    _anim.SetFloat("Y", _lastDir.y);
                }
            }
            else
            {
                _rb.velocity = Vector2.zero;
                _anim.SetBool("IsMoving", false);
            }
        }
    }
}
