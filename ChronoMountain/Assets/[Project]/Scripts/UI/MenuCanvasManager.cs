using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Mwa.Chronomountain
{
    // [ExecuteInEditMode]
    public class MenuCanvasManager : MonoBehaviour
    {
        public GameObject canvasLevelSelection;
        [SerializeField] List<LevelDescriptor> levelDescriptorsList;

        // [ContextMenu("StartAMoi")]
        void Start()
        {
            RefreshButtonState();
        }

        public void RefreshButtonState()
        {
            // print("RefreshButtonState");
            canvasLevelSelection.SetActive(true);
            foreach(var item in levelDescriptorsList)
            {
                SetButtonState(item, false);
            }
            ActivateButtonPlayableLevel();
            canvasLevelSelection.SetActive(false);
        }

        //! Avtive les button pour chaque niveau jouable
        // [ContextMenu("ActivateButtonPlayableLevel")]
        public void ActivateButtonPlayableLevel()
        {
            // print("Activate Button");
            SetButtonState(levelDescriptorsList[0], true);

            if(levelDescriptorsList[0].IsLevelWin() == false)
                return;

            for(int i = 1; i < levelDescriptorsList.Count; i++)
            {
                print("levelDescriptors : " + levelDescriptorsList[i]);
                //! Si le niveau est win active le button
                if(levelDescriptorsList[i].IsLevelWin())
                {
                    SetButtonState(levelDescriptorsList[i], true);
                }
                else
                {
                    SetButtonState(levelDescriptorsList[i], true);
                    return;
                }
            }
        }

        void SetButtonState(LevelDescriptor descriptor, bool state)
        {
            descriptor.GetComponent<Button>().enabled = state;

            if(state)
                descriptor.GetComponent<Image>().color = Color.white;
            else
                descriptor.GetComponent<Image>().color = Color.gray;
        }
    }
}
