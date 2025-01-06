using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevelPlayerPref : MonoBehaviour
{
    [ContextMenu("Reset Pref")]
    public void ResetPref()
    {
        PlayerPrefs.DeleteAll();
    }
}
