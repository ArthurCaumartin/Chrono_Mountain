using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndLevelAnimation : MonoBehaviour
{
    [SerializeField] Sprite winSprite;
    [SerializeField] Sprite loseSprite;
    [SerializeField] float duration;
    public void StartAnimation(bool isWin, System.Action callBack)
    {
        
    }

    IEnumerator Wait()
    {
        
    }
}
