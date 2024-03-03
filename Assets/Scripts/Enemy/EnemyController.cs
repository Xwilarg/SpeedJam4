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
        private const float AttackWaitTime = 1f;

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
                    var coll = new Collider2D[1];
                    Physics2D.OverlapCircleNonAlloc(transform.position + ((Vector3)_lastDir * Range), Range, coll, 1 << LayerMask.NameToLayer("Player"));
                    if (coll[0] != null)
                    {
                        coll[0].transform.parent.GetComponent<PlayerController>().Die();
                    }

                    _attackState = 0;
                    Destroy(_hintInstance);
                    _hintInstance = null;
                }
            }
        }

        private void FixedUpdate()
        {
            if (TimeController.Instance.IsActive && _attackState == 0 && PlayerController.Instance != null)
            {
                if (Physics2D.Linecast(transform.position, PlayerController.Instance.transform.position, 1 << LayerMask.NameToLayer("Map")).collider == null // Somehow doesn't work
                    && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= AttackDist)
                {
                    _attackState = 1;
                    _hintInstance = Instantiate(_hint, transform.position + ((Vector3)_lastDir * Range), Quaternion.identity);
                    _hintInstance.transform.localScale = Vector3.one * Range;
                    _timer = AttackWaitTime;
                    _rb.velocity = Vector2.zero;
                    _anim.SetBool("IsMoving", false);
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

        private void OnDestroy()
        {
            if (_hintInstance != null)
            {
                Destroy(_hintInstance);
            }
        }
    }
}
