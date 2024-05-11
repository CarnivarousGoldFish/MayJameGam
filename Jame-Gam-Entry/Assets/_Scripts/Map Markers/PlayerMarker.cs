using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarker : BaseMarker
{
    private int _startX, _startY;
    private int _destinationX, _destinationY;

    [SerializeField] private float _moveTime = 1.75f;
    [SerializeField] private BoxCollider2D _collider;

    private float _lastMove;
    private bool _canMove;

    private bool _moveForward;
    private bool _moveBackward;

    private void Update()
    {
        if (!_canMove) return;

        if (Time.time - _lastMove > _moveTime)
        {
            /*
            if (_moveForward)
            {
                MoveMarker(_destinationX, _destinationY);
                Debug.Log("Forward");
                _lastMove = Time.time;
            }

            else if (_moveBackward)
            {
                MoveMarker(_startX, _startY);
                Debug.Log("Backward");
                _lastMove = Time.time;
            }
            */

            MoveMarker(_destinationX, _destinationY);
            _lastMove = Time.time;

            if (transform.position.x == _destinationX && transform.position.y == _destinationY)
            {
                _canMove = false;
            }
        }
    }

    public void SetDestination(Vector3 destination)
    {

        _startX = Mathf.RoundToInt(transform.position.x);
        _startY = Mathf.RoundToInt(transform.position.y);

        _destinationX = Mathf.RoundToInt(destination.x);
        _destinationY = Mathf.RoundToInt(destination.y);

        _canMove = true;
    }

    // This receives the UnityEvents invoke
    /*
     public void OnBehaviorChange()
    {
        Debug.Log("Hello");
        _canMove = true;

        if (LeverManager.Instance.CheckForward())
        {
            Debug.Log("Check F");
            _moveForward = true;
            _moveBackward = false;

            Debug.Log(_moveForward);
        }
        else if (LeverManager.Instance.CheckReverse())
        {
            Debug.Log("Check B");
            _moveForward = false;
            _moveBackward = true;
        }
        else if (LeverManager.Instance.CheckStop())
        {
            Debug.Log("Dumb bitch");
            _canMove = false;
        }
    }
    */

    private void MoveMarker(int targetX, int targetY)
    {
        var newOffset = _collider.offset;

        if (transform.position.x < targetX)
        {
            newOffset.x = -1;
            newOffset.y = 0;

            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            transform.position += Vector3.right;

        } 
        else if (transform.position.x > targetX)
        {
            newOffset.x = -1;
            newOffset.y = 0;

            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            transform.position += Vector3.left;

        } 
        else if (transform.position.y  < targetY)
        {
            newOffset.x = 0;
            newOffset.y = 1;

            transform.position += Vector3.up;

        } 
        else if (transform.position.y > targetY)
        {
            newOffset.x = 0;
            newOffset.y = -1;

            transform.position += Vector3.down;

        }

        _collider.offset = newOffset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard Tile"))
        {
            //Debug.Log("Hazard detected");
            _canMove = false;
        }
        else
        {
            Debug.Log("NO hazard detected");
        }
    }
}
