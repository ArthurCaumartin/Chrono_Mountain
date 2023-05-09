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
        [SerializeField] UnityEvent onTimerComplete;
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

        void Start()
        {
            timerImage = GameObject.FindGameObjectWithTag("TimerImage").GetComponent<Image>();
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
                    InGameCanvasManager.manager.SetLose();
                    isRuning = false;
                    return;
                }

                currentTime += Time.deltaTime;
                //! set le fil de l'image
                timerImage.fillAmount = currentTime / timeToComplete;
                timerImage.color = ColorUtils.RemapColor(timerImage.fillAmount, oMin, oMax, curve);
            }
        }
<<<<<<< HEAD
=======

        //! Call par onMovementStart dans PlayerMovement
        public void PauseTimer()
        {
            isRuning = false;
        }

        public void ResetTimer()
        {
            currentTime = 0;
            timerImage.fillAmount = 0;
            isRuning = true;
        }
>>>>>>> origin/Dev
    }
}
