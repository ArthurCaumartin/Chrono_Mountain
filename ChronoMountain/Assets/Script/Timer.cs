using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Mwa.Chronomountain
{
    public class Timer : MonoBehaviour
    {
        [Header("Timer Data :")]
        [SerializeField] float timeToComplete;
        [SerializeField] UnityEvent onComplete;
        float currentTime;
        bool isRuning = false;

        [Header("Timer Image :")]
        [SerializeField] Image timerImage;
        [SerializeField] Color oMin;
        [SerializeField] Color oMax;
        [SerializeField] AnimationCurve curve;

        void Awake()
        {
            StartTimer();
        }

        public void StartTimer()
        {
            isRuning = true;
        }

        void Update()
        {
            if(isRuning)
            {
                if(currentTime >= timeToComplete)
                {
                    onComplete.Invoke();
                    isRuning = false;
                    return;
                }

                currentTime += Time.deltaTime;
                //! set le fil de l'image
                timerImage.fillAmount = currentTime / timeToComplete;
                ColorUtils.RemapColor(timerImage.fillAmount, oMin, oMax, curve);
            }
        }
    }
}
