﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BallState
{
    Middle, Right, X, Left
}

public class Ball : MonoBehaviour
{
    [SerializeField] private Sprite[] lanePositionSprites;
    [SerializeField] private Image lanePositionImage;

    private readonly Vector3 middleLane = Vector3.zero;
    private readonly Vector3 rightLane = new Vector3(0, 0, -1.6f);
    private readonly Vector3 leftLane = new Vector3(0, 0, 1.6f);

    private float smoothing = 3.0f;

    private BallState ballState;

    private void Start() {
        SetRandomBallPos();
    }

    public void SetRandomBallPos() {
        int rnd = Random.Range(0, 3);
        Debug.Log(rnd);
        switch (rnd) {
            case 0:
                ballState = BallState.Middle;
                lanePositionImage.sprite = lanePositionSprites[0];
                break;
            case 1:
                ballState = BallState.Right;
                lanePositionImage.sprite = lanePositionSprites[1];
                break;
            case 2:
                ballState = BallState.Left;
                lanePositionImage.sprite = lanePositionSprites[2];
                break;
        }
    }

    private IEnumerator MoveBall(BallState lane) {
        Vector3 target = SetNextBallPos(lane);
        while (Vector3.Distance(transform.position, target) > 0.05f) {
            transform.position = Vector3.Lerp(transform.position, target, smoothing * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);
    }

    private Vector3 SetNextBallPos(BallState state) {

        switch (state) {
            case BallState.Middle:
                return middleLane;
                
            case BallState.Right:
                return rightLane;

            case BallState.Left:
                return leftLane;

            case BallState.X:
                return middleLane;

            default:
                return middleLane;
        }
    }

    //DEBUG
    public void MoveLeft()
    {
        StartCoroutine(MoveBall(BallState.Left));
    }

    public void MoveRight()
    {
        StartCoroutine(MoveBall(BallState.Right));
    }

    public void MoveMiddle()
    {
        StartCoroutine(MoveBall(BallState.Middle));
    }
}
