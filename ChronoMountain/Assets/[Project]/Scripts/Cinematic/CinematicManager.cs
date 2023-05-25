using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CinematicManager : MonoBehaviour
{
    [Header("Scene Object :")]
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] Image background;

    [Header("Camera Settings :")]
    [SerializeField] GameObject onlyOneCameraCanLiveOnThisPlanetImSorryBrother; //! c la kaméra !!!!
    [SerializeField] Transform cameraStartMovePoint;
    [SerializeField] Transform camerandMovePoint;
    [Header("Texte Settings :")]
    [SerializeField] float lettresDelais;
    [Space]
    [SerializeField] List<ScriptableCinematic> cinematicList;
    int cinematicIndex;
    Vector3 cameraPositionBackup;

    void Start()
    {
        cameraPositionBackup = onlyOneCameraCanLiveOnThisPlanetImSorryBrother.transform.position;
    }

    //! Initialise Background / image + text
    void StartNextCinematic()
    {
        //! reset la position de la cam
        onlyOneCameraCanLiveOnThisPlanetImSorryBrother.transform.position = cameraPositionBackup;

        //! set les elements de la scene
        background.sprite = cinematicList[cinematicIndex].backGround;

        StartCoroutine(PrintTexte());
    }

    IEnumerator PrintTexte()
    {
        mainText.text = " ";
        string textToSet = cinematicList[cinematicIndex].mainText;
        for (int i = 0; i < textToSet.Length; i++)
        {
            mainText.text += textToSet[i];
            yield return new WaitForSeconds(0.05f);
        }
    }
}
