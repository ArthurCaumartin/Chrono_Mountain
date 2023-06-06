using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransitionPlayerAnimation : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform playerSprite;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform cameraFollower;
    [SerializeField] Transform cameraStoper;

    [Header("Animation :")]
    [SerializeField] float bumpSpeed;
    [SerializeField] float yGap;

    [Header("Tween Move :")]
    [SerializeField] float tweenSpeed;
    [SerializeField] List<Transform> targetList;
    Sequence movementSequence;
    int index;
    Vector3 currentFramePosition;
    Vector3 lastFramPosition;
    Vector3 newCamPosition;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        StartMoving();
    }

    [ContextMenu("StartMoving")]
    public void StartMoving()
    {
        // player.DOMove(targetList[index].position, speed)
        // .SetSpeedBased().OnComplete(() => 
        // {
        //     index++;
        //     if(index != targetList.Count)
        //     {
        //         StartMoving();
        //     }
        //     else
        //     {
        //         print("Fin de sequence !");
        //     }
        // });

        DOTween.To((time) =>
        {
            player.position = Vector3.Lerp(targetList[index].position, targetList[index + 1].position, time);
        },
        0, 1, Vector3.Distance(targetList[index].position, targetList[index + 1].position) * tweenSpeed)
        .SetEase(Ease.Linear).OnComplete(() =>
        {
            index++;
            //! -1 car pour fonctioner l'animation a besoin du n+1
            if(index == targetList.Count - 1)
            {
                print("Fin de sequence !");
                
            }
            else
            {
                StartMoving();
            }
        });
    }

    float newY;
    float distanceFactor;
    float sinIncrement;

    void Update()
    {
        //! Animation de boing boing
        cameraFollower.position = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth/2, Camera.main.pixelHeight));

        currentFramePosition = player.position;
        distanceFactor = Vector3.Distance(currentFramePosition, lastFramPosition);

        sinIncrement += Time.deltaTime;

        newY = (yGap * Mathf.Sin(sinIncrement * bumpSpeed)) * distanceFactor;
        playerSprite.localPosition = new Vector2(playerSprite.localPosition.x, newY);

        lastFramPosition = player.position;

        //! Condition pour le deplacement de la camera
        if(player.position.y >= cameraTransform.position.y && cameraFollower.position.y < cameraStoper.position.y)
        {
            newCamPosition = new Vector3(cameraTransform.position.x, player.position.y, cameraTransform.position.z);
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, newCamPosition, Time.deltaTime);
        }
    }
}
