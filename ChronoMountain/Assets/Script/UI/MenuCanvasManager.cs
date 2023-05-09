using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class MenuCanvasManager : MonoBehaviour
    {
        [SerializeField] List<LevelDescriptor> levelDescriptorsList;

        void Start()
        {
            foreach (var item in levelDescriptorsList)
            {
                SetButtonState(item, false);
            }
            ActivateButtonPlayableLevel();
        }

        //TODO Boucle a ajuster
        //! Avtive les button pour chaque niveau jouable
        public void ActivateButtonPlayableLevel()
        {
            SetButtonState(levelDescriptorsList[0], true);

            for (int i = 0; i < levelDescriptorsList.Count; i++)
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
