using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class MakeRealSize : MonoBehaviour
{
    const float ratio = 2.1225f * 2f;
    
    void Start() {
        Image img = GetComponent<Image>();
        Rasterize(img);
        // DestroyImmediate(this);
    }
    
#if UNITY_EDITOR
    static int frame;
    [MenuItem("GameObject/Rasterize")]
    static void Rasterize() {
        if(frame == Time.frameCount) return;
        frame = Time.frameCount;

        Debug.Log("Doing Something...");
        foreach(Transform t in Selection.transforms) {
            if(t != null) {
                Rasterize(t.GetComponent<Image>());
            }
        }
    }
#endif

    static void Rasterize(Image img) {
        if(!img) return;

        float width = img.sprite.rect.width;    
        float height = img.sprite.rect.height;

        RectTransform rt = (RectTransform)img.transform;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width * ratio);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height * ratio);
    }
}
