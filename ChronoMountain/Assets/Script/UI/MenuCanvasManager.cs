using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Mwa.Chronomountain
{
    // [ExecuteInEditMode]
    public class MenuCanvasManager : MonoBehaviour
    {
        [SerializeField] List<LevelDescriptor> levelDescriptorsList;

        // [ContextMenu("StartAMoi")]
        void Start()
        {
            foreach (var item in levelDescriptorsList)
            {
                print(item);
                SetButtonState(item, false);
            }
            ActivateButtonPlayableLevel();
        }

        //TODO Boucle a ajuster
        //! Avtive les button pour chaque niveau jouable

        // [ContextMenu("ActivateButtonPlayableLevel")]
        public void ActivateButtonPlayableLevel()
        {
            // print("Activate Button");
            SetButtonState(levelDescriptorsList[0], true);

            if(levelDescriptorsList[0].IsLevelWin() == false)
                return;

            for (int i = 1; i < levelDescriptorsList.Count - 1; i++)
            {

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
