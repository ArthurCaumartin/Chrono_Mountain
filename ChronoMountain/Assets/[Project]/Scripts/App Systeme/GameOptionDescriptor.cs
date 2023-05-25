using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class GameOptionDescriptor : MonoBehaviour
    {
        public ScriptableGameOption gameOption;

        public void SwitchTimerActivation()
        {
            gameOption.isTimerOn = !gameOption.isTimerOn;
        }
    }
}
