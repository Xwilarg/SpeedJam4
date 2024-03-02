using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpeedJam4.Timers
{
    public class TimeController : MonoBehaviour
    {
        public static TimeController Instance { get; private set; }

        bool timerActive;

        float timeInMilliseconds;
        float timeInSeconds;
        float timeInMinutes;

        string milliseconds = "00";
        string seconds = "00";
        string minutes = "00";

        [SerializeField] private TMP_Text timeText;

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;

            timerActive = false;

            timeInMilliseconds = 0;
            timeInSeconds = 0;
            timeInMinutes = 0;

            timeText.text = "00 : 00 : 00";
        }

        // Update is called once per frame
        void Update()
        {
            if (!timerActive)
                return;

            timeInMilliseconds += (Time.deltaTime * 1000)/10;
            
            if(timeInMilliseconds >= 100)
            {
                timeInMilliseconds = 0;
                timeInSeconds++;
                if(timeInSeconds >= 60)
                {
                    timeInSeconds = 0;
                    timeInMinutes += 1;
                }    
            }

            milliseconds = timeInMilliseconds < 10 ? "0" + ((int)timeInMilliseconds).ToString() : ((int)timeInMilliseconds).ToString();
            seconds = timeInSeconds < 10 ? "0" + timeInSeconds.ToString() :  timeInSeconds.ToString();
            minutes = timeInMinutes < 10 ? "0" + timeInMinutes.ToString() : timeInMinutes.ToString();



            timeText.text = minutes + " : " + seconds + " : " + milliseconds;


        }

        public void EnableTimer()
        {
            timerActive = true;
        }

        public void DisableTimer()
        {
            timerActive = false;
        }

        public void ResetTimer()
        {
            timeInMilliseconds = 0;
            timeInSeconds = 0;
            timeInMinutes = 0;
        }
    }
}