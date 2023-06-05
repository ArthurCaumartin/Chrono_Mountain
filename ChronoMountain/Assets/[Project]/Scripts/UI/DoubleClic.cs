using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DoubleClic : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] UnityEvent onDoubleClic;
    bool isDoubleClic = false;
    Coroutine coroutineDelay;

    public void Clic()
    {
        if(isDoubleClic)
        {
            onDoubleClic.Invoke();
        }
        else if(coroutineDelay == null)
        {
            coroutineDelay = StartCoroutine(clicCD(delay));
        }
    }

    public void PrintStuff(string oeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheeoeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheergoiuhezrgoiuhezrgiouhehrgrioeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheergoiuhezrgoiuhezrgiouhehrgrioeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheergoiuhezrgoiuhezrgiouhehrgrioeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheergoiuhezrgoiuhezrgiouhehrgrioeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheergoiuhezrgoiuhezrgiouhehrgrirgoiuhezrgoiuhezrgiouhehrgri)
    {
        print(oeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheeoeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheergoiuhezrgoiuhezrgiouhehrgrioeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheergoiuhezrgoiuhezrgiouhehrgrioeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheergoiuhezrgoiuhezrgiouhehrgrioeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheergoiuhezrgoiuhezrgiouhehrgrioeiuhzergiuihzergoiuergoiuhezrgoiuhezrgroiuzheergoiuhezrgoiuhezrgiouhehrgrirgoiuhezrgoiuhezrgiouhehrgri);
    }

    IEnumerator clicCD(float delay)
    {
        isDoubleClic = true;
        yield return new WaitForSeconds(delay);
        isDoubleClic = false;
        coroutineDelay = null;
    }
}
