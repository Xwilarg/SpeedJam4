using SpeedJam4.Timers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpeedJam4
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private LineRenderer _lr;

        private bool _isCharging;
        private float _chargeForce;
        private const float MaxChargeForce = 2f;

        private bool _didStart;

        private Vector2 _lastVel;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _lr = GetComponentInChildren<LineRenderer>();
            _lr.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!_isCharging)
            {
                Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mouse - (Vector2)transform.position).normalized;
                _lr.transform.position = transform.position + (Vector3)direction;
                _lr.transform.right = direction;
            }
            else
            {
                _chargeForce = Mathf.Clamp(_chargeForce + Time.deltaTime, 0f, MaxChargeForce);
                _lr.SetPositions(new[] { transform.position, _lr.transform.position + (_lr.transform.right * _chargeForce) });
            }
        }

        private void FixedUpdate()
        {
            _lastVel = _rb.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _rb.velocity = Vector2.Reflect(_lastVel, collision.contacts[0].normal) * _lastVel.magnitude / 10f;
        }

        public void OnFire(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Started)
            {
                _isCharging = true;
                _chargeForce = 0f;
                _lr.gameObject.SetActive(true);
            }
            else if (value.phase == InputActionPhase.Canceled)
            {
                if (!_didStart)
                {
                    _didStart = true;
                    TimeController.Instance.IsActive = true;
                }

                _isCharging = false;
                _lr.gameObject.SetActive(false);
                _rb.velocity = -_lr.transform.right * _chargeForce * 10f;
            }
        }
    }
}
