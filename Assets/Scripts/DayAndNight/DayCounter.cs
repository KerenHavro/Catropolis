using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace WorldTime
{
    [RequireComponent(typeof(TMP_Text))]
    public class DayCounter : MonoBehaviour
    {
        [SerializeField]
        private WorldTime _worldTime;
  
        [SerializeField]
        private TMP_Text _Daytext;



        private void Awake()
        {
            
            _Daytext = GetComponent<TMP_Text>();


            _worldTime.WorldTimeChanged += OnWorldTimeChanged;

        }

        private void OnDestroy()
        {

            _worldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
         _Daytext.SetText(newTime.ToString(@"dd"));
        }
    }
}