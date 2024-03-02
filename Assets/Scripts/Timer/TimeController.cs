using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpeedJam4.Timers
{
    public class TimeController : MonoBehaviour
    {
        float timeInMilliseconds;
        float timeInSeconds;
        float timeInMinutes;

        [SerializeField] private TMP_Text timeText;

        // Start is called before the first frame update
        void Start()
        {
            timeInMilliseconds = 0;
            timeInSeconds = 0;
            timeInMinutes = 0;
        }

        // Update is called once per frame
        void Update()
        {
            timeInMilliseconds += (Time.deltaTime * 1000)/100;
            
            if(timeInMilliseconds >= 10)
            {
                timeInMilliseconds = 0;
                timeInSeconds++;
                if(timeInSeconds >= 60)
                {
                    timeInSeconds = 0;
                    timeInMinutes += 1;
                }    
            }
        }
    }
}