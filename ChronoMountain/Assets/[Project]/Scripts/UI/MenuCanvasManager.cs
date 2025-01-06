using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

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
            // canvasLevelSelection.SetActive(true);
            foreach (var item in levelDescriptorsList)
            {
                SetButtonState(item, false);
            }
            ActivateButtonPlayableLevel();
            // canvasLevelSelection.SetActive(false);
        }

        //! Avtive les button pour chaque niveau jouable
        public void ActivateButtonPlayableLevel()
        {
            // print("Activate Button");
            SetButtonState(levelDescriptorsList[0], true);

            for (int i = 1; i < levelDescriptorsList.Count; i++)
                SetButtonState(levelDescriptorsList[i], levelDescriptorsList[i].IsLevelWin());
        }

        void SetButtonState(LevelDescriptor descriptor, bool state)
        {
            // print($"{(state ? "Diable" : "Enable")} level button");
            descriptor.GetComponent<Button>().enabled = state;
            descriptor.GetComponent<Image>().color = state ? Color.white : Color.gray;
        }
    }
}
