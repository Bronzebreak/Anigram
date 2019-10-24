using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class S_Path : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetLocation = Vector3.zero;

    [Range(1.0f, 15.0f), SerializeField]
    private float moveDuration = 1.0f;

    [SerializeField]
    private Ease moveEase=Ease.Linear;

    [SerializeField]
    private DoTweenType doTweenType = DoTweenType.MovementOneWay;
    private enum DoTweenType 
    { 
        MovementOneWay,
        MovementTwoWay
    };
    // Start is called before the first frame update
    void Start()
    {
        if(doTweenType == DoTweenType.MovementOneWay)
        {
            if (targetLocation == Vector3.zero)
            {
                targetLocation = transform.position;
            }
            transform.DOMove(targetLocation, moveDuration).SetEase(moveEase);
        }
        if (doTweenType == DoTweenType.MovementTwoWay)
        {
            if (targetLocation == Vector3.zero)
            {
                targetLocation = transform.position;
            }
            StartCoroutine(MovementWithBothWays());
        }
    }

    private IEnumerator MovementWithBothWays()
    {
        Vector3 originalLocation = transform.position;
        transform.DOMove(targetLocation, moveDuration).SetEase(moveEase);
        yield return new WaitForSeconds(moveDuration);
        transform.DOMove(originalLocation, moveDuration).SetEase(moveEase);
    }
}
