using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelElementBase : MonoBehaviour
{
    public static List<LevelElementBase> levelObjects = new List<LevelElementBase>();

    public static LevelElementBase GetAt(Vector3 pos) {
        foreach(var l in levelObjects) {
            if(Vector3.Distance(l.transform.position, pos) < .5f ) { // should change for a box check
                return l;
            }
        }
        return null;
    }
    // Start is called before the first frame update
    protected virtual void OnEnable()
    {
        levelObjects.Add(this);
    }

    // Update is called once per frame
    protected virtual void OnDisable()
    {
        
        levelObjects.Remove(this);
    }

    public virtual void OnStep(System.Action callback) {
        print("no method implemented ...");
        if(callback != null) callback();
    }
}
