using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldTime
{


    public class WorldTime : MonoBehaviour
    {
        public EventHandler<TimeSpan> WorldTimeChanged;

        [SerializeField]
        private float _dayLength;

        private TimeSpan _currentTime;
        private TimeSpan _currentDay;
        private float _minuteLength=> _dayLength/ WorldTimeConstants.MinutesInDay;

        private void Start()
        {
           StartCoroutine(AddMinute());
            StartCoroutine(AddDay());
        }




        private IEnumerator AddMinute()
        {
            _currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this, _currentTime);
            yield return new WaitForSeconds(_minuteLength);
            StartCoroutine(AddMinute());
        }
        private IEnumerator AddDay()
        {
            _currentTime += TimeSpan.FromDays(1);
            WorldTimeChanged?.Invoke(this, _currentDay);
            yield return new WaitForSeconds(_dayLength);
            StartCoroutine(AddMinute());
        }

    }
}
