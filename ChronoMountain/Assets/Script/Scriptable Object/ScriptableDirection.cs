using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    [CreateAssetMenu(fileName = "Direction", menuName = "ChronoMoutain/Direction")]
    public class ScriptableDirection : ScriptableObject
    {
        public Pointer direction;
        public Sprite arrowSprite;
        public Vector3 GetDirection()
        {
            Vector3 toReturn = new Vector3(0, 0, 0);
            switch (direction)
            {
                case Pointer.Up :
                    toReturn = new Vector3(0, 1, 0);
                break;

                case Pointer.Left :
                    toReturn = new Vector3(-1, 0, 0);
                break;

                case Pointer.Right :
                    toReturn = new Vector3(1, 0, 0);
                break;

                case Pointer.Down :
                    toReturn = new Vector3(0, -1, 0);
                break;
            }
            return toReturn;
        }
    }
}
