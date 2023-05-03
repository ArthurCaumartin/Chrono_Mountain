using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Mwa.Chronomountain
{
    public class CanvasManager : MonoBehaviour
    {
        public static CanvasManager manager;
        [Header("End Level")]
        [SerializeField] UnityEvent onWin;
        [SerializeField] UnityEvent onLose;

        [Header("Direction Button")]
        [SerializeField] List<GameObject> directionButton;

        [Header("Arrow Direction")]
        [SerializeField] GameObject arrowObjectPrefab;
        [SerializeField] Transform initialPosition;
        [SerializeField] float gap;
        [SerializeField] List<GameObject> arrowPrefabList;
        int arrowNumber = 0;

        void Awake()
        {
            manager = this;    
        }
#region Arrow UI
        //! Ajoute une interface, set le sprite en avec celui de la direction
        public void AddArrow(ScriptableDirection direction)
        {
            Vector3 posToInstantiate = new Vector3(initialPosition.position.x + (gap * arrowNumber), initialPosition.position.y, initialPosition.position.z);
            GameObject newArrow = Instantiate(arrowObjectPrefab, posToInstantiate, Quaternion.identity);

            newArrow.GetComponent<SpriteRenderer>().sprite = direction.arrowSprite;
            arrowPrefabList.Add(newArrow);
            arrowNumber++;
        }
        
        public void CollorArrow(int indextoColor)
        {
            arrowPrefabList[indextoColor].GetComponent<SpriteRenderer>().color = Color.cyan;
        }

        //! reset le compteur et clear la list
        public void ClearArrow()
        {
            arrowNumber = 0;
            foreach(var item in arrowPrefabList)
            {
                Destroy(item);
            }
            arrowPrefabList.Clear();
        }
#endregion

        public void SetActiveButtonDirection(bool value)
        {
            foreach (var item in directionButton)
            {
                item.SetActive(value);
            }
        }

        public void SetWin()
        {
            onWin.Invoke();
        }

        public void SetLose()
        {
            onLose.Invoke();
        }
    }
}
