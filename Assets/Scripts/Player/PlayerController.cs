using SpeedJam4.SO;
using SpeedJam4.Timer;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpeedJam4.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { private set; get; }

        [SerializeField]
        private PlayerInfo _info;

        private Rigidbody2D _rb;
        private LineRenderer _lr;
        private SpriteRenderer _sr;

        private bool _isCharging;
        private float _chargeForce;

        private Vector2 _lastVel;

        private float _reloadTimer;

        private void Awake()
        {
            Instance = this;

            _rb = GetComponent<Rigidbody2D>();
            _lr = GetComponentInChildren<LineRenderer>();
            _sr = GetComponentInChildren<SpriteRenderer>();

            _lr.gameObject.SetActive(false);
        }

        private void Update()
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouse - (Vector2)transform.position).normalized;
            _lr.transform.position = transform.position + (Vector3)direction;
            _lr.transform.right = direction;

            if (_isCharging)
            {
                _chargeForce = Mathf.Clamp(_chargeForce + Time.deltaTime, 0f, _info.MaxChargeForce);
                _lr.SetPositions(new[] { transform.position, _lr.transform.position + (_lr.transform.right * _chargeForce) });
            }

            if (_reloadTimer > 0f)
            {
                _reloadTimer -= Time.deltaTime;
                if (_reloadTimer <= 0f)
                {
                    AllowFiring();
                }
            }
        }

        private void AllowFiring()
        {
            _reloadTimer = 0f;
            _sr.color = Color.white;
        }

        private void FixedUpdate()
        {
            if (_rb.velocity.magnitude < _info.MinVelocity)
            {
                _rb.velocity = Vector2.zero;
                TimeController.Instance.IsActive = false;
                AllowFiring();
            }

            _lastVel = _rb.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _rb.velocity = Vector2.Reflect(_lastVel, collision.contacts[0].normal) * _lastVel.magnitude / _info.WallBounceDamping;
        }

        public void OnFire(InputAction.CallbackContext value)
        {
            if (_reloadTimer <= 0f)
            {
                if (value.phase == InputActionPhase.Started)
                {
                    _isCharging = true;
                    _chargeForce = 0f;
                    _lr.gameObject.SetActive(true);

                    _rb.velocity = Vector2.zero;

                    TimeController.Instance.IsActive = false;
                }
                else if (value.phase == InputActionPhase.Canceled)
                {
                    TimeController.Instance.IsActive = true;
                    _sr.color = Color.gray;

                    _isCharging = false;
                    _lr.gameObject.SetActive(false);
                    _rb.velocity = _chargeForce * _info.PropulsionVelMultiplier * -_lr.transform.right;

                    _reloadTimer = _info.ReloadTime;
                }
            }
        }
    }
}
