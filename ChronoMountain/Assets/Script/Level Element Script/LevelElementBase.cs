using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class LevelElementBase : MonoBehaviour
    {
        public static List<LevelElementBase> levelObjects = new List<LevelElementBase>();

        //! return l'element a la poses demande en parametre
        //! avec une tolerence de .5f de distance
        public static LevelElementBase GetAt(Vector3 pos)
        {
            foreach(var l in levelObjects)
            {
                //TODO should change for a box check
                if(Vector3.Distance(l.transform.position, pos) < .5f )
                { 
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

        //! C'est pour sa que l'heritage c ouf
        public virtual void OnStep(System.Action callback) 
        {
            print("no method implemented ... pfff u nul");
            if(callback != null) callback();
        }
    }
}