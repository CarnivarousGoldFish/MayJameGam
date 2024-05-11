using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarker : BaseMarker
{
    private int _startX, _startY;
    private int _destinationX, _destinationY;

    [SerializeField] private float _moveTime = 1.75f;
    private float _lastMove;
    private bool _canMove;
    private bool _hazardDetected;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!_canMove) return;

        if (Time.time - _lastMove > _moveTime)
        {
            MoveMarker();
            _lastMove = Time.time;

            if (transform.position.x == _destinationX && transform.position.y == _destinationY)
                _canMove = false;
        }
    }

    public void SetDestination(Vector3 destination)
    {
        Debug.Log("Destination set at: " + destination);

        _destinationX = Mathf.RoundToInt(destination.x);
        _destinationY = Mathf.RoundToInt(destination.y);

        _canMove = true;
    }

    private void MoveMarker()
    {
        if (transform.position.x < _destinationX)
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            transform.position += Vector3.right;

        } else if (transform.position.x > _destinationX)
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            transform.position += Vector3.left;

        } else if (transform.position.y  < _destinationY)
        {
            transform.localEulerAngles = new Vector3(0f, 0f, -90f);
            transform.position += Vector3.up;

        } else if (transform.position.y > _destinationY)
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 90f);
            transform.position += Vector3.down;

        }
    }

    private bool IsValidMove()
    {
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard Tile"))
        {
            Debug.Log("Hazard detected");
            _canMove = false;
            _hazardDetected = true;
        }
        else
        {
            Debug.Log("NO hazard detected");
        }
    }
}
