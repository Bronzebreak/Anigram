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
        MovementOneWay
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
