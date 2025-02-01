using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlexHyperCasualGames;

public class CameraFollowSc : MonoBehaviour
{
    public Transform target;

    public bool isCustomOffset;
    public Vector3 offset;

    public float smoothSpeed = 0.1f;

    private void Awake()
    {
        GameControllerSc.Instance.mainCam = Camera.main;
    }

    private void Start()
    {
        // You can also specify your own offset from inspector
        // by making isCustomOffset bool to true
        if (!isCustomOffset)
        {
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        SmoothFollow();
    }

    public void SmoothFollow()
    {


        if (GameControllerSc.Instance.CurrentGameState != Constants.GameState.StartUIAnim &&  GameControllerSc.Instance.CurrentGameState != Constants.GameState.Shopping)
        {
            Vector3 targetPos = new Vector3(target.position.x, 0f, 0f) + offset;
            Vector3 smoothFollow = Vector3.Lerp(transform.position,
            targetPos, smoothSpeed);

            transform.position = smoothFollow;
        }
        else
        {
            Vector3 targetPos = target.position + offset;
            Vector3 smoothFollow = Vector3.Lerp(transform.position,
            targetPos, smoothSpeed);

            transform.position = smoothFollow;
        }
       

        
        //transform.LookAt(target);
    }
}
