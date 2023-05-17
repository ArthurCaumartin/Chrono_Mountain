using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
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
        
        protected virtual void OnEnable()
        {
            levelObjects.Add(this);
        }

        protected virtual void OnDisable()
        {
            levelObjects.Remove(this);
        }

        public virtual void OnStep(System.Action callback) {
            print("no method implemented ... pfff u nul");
            if(callback != null) callback();
        }
    }
}
