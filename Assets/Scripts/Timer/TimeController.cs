using UnityEngine;
using TMPro;

namespace SpeedJam4.Timer
{
    public class TimeController : MonoBehaviour
    {
        public static TimeController Instance { get; private set; }

        [SerializeField] private TMP_Text _text;

        public bool IsActive { set; get; }
        private float _timer;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (IsActive)
            {
                _timer += Time.deltaTime;
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            _text.text = $"{(int)_timer / 60}:{_timer % 60:00}.{(int)(_timer % 1 * 1000f):000}";
        }

        public void ResetTimer()
        {
            IsActive = false;
            _timer = 0f;
            UpdateUI();
        }
    }
}