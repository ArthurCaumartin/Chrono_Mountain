using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransitionPlayerAnimation : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform playerSprite;

    [Header("Animation :")]
    [SerializeField] float speed;
    [SerializeField] float yGap;

    [Header("Tween Move :")]
    [SerializeField] float tweenSpeed;
    [SerializeField] AnimationCurve scaleCurve;
    [SerializeField] List<Transform> targetList;
    Sequence movementSequence;
    int index;

    //! scale change
    Vector3 currentFramePosition;
    Vector3 lastFramPosition;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        StartMoving();
    }

    [ContextMenu("StartMoving")]
    public void StartMoving()
    {
        DOTween.To((time) =>
        {
            player.position = Vector3.Lerp(targetList[index].position, targetList[index + 1].position, time);
        },
        0, 1, Vector3.Distance(targetList[index].position, targetList[index + 1].position) * tweenSpeed)
        .SetEase(Ease.Linear).OnComplete(() =>
        {
            index++;
            // if(index < targetList.Count)
            // {
                StartMoving();
            // }

        });
    }

    float newY;
    float distanceFactor;
    float sinIncrement;

    void Update()
    {
        currentFramePosition = player.position;
        distanceFactor = Vector3.Distance(currentFramePosition, lastFramPosition);

        sinIncrement += Time.deltaTime;

        newY = (yGap * Mathf.Sin(sinIncrement * speed)) * distanceFactor;
        playerSprite.localPosition = new Vector2(playerSprite.localPosition.x, newY);

        lastFramPosition = player.position;
    }
}
