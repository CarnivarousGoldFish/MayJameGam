using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarker : BaseMarker
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private float _moveTime = 1.75f;

    private MapManager _manager;
    private CameraShake _camShake;

    private int _destinationX, _destinationY;
    private MapManager _manager;

    private float _lastMove;

    private bool _canMove;

    public void Initialize(MapManager manager, CameraShake camShake)
    {
        _manager = manager;
        _camShake = camShake;
    }

    public void Initialize(MapManager manager)
    {
        _manager = manager;
        _camShake = camShake;
    }

    private void Update()
    {
        if (!_canMove) return;

        if (Time.time - _lastMove > _moveTime)
        {
            // move the player marker and update _lastMove
            MoveMarker(_destinationX, _destinationY);
            _lastMove = Time.time;

            // player has reached destination tile, stop moving
            if (transform.position.x == _destinationX && transform.position.y == _destinationY)
            {
                _canMove = false;
            }
        }
    }

    public void SetDestination(Vector3 destination)
    {
        _destinationX = Mathf.RoundToInt(destination.x);
        _destinationY = Mathf.RoundToInt(destination.y);

        _canMove = true;
    }

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
        _manager.SetPlayerTile();
        CheckIfGoalTile();
    }

    private void CheckIfGoalTile()
    {
        if (CurrentTile.CompareTag("Goal Tile"))
        {
            // un-comment and then add the string for the happy sfx
            // AudioManager.Instance.PlaySFX();

            // add scene transition to Credits
            // put here

        }
        else if (CurrentTile.CompareTag("Hazard Tile"))
        {
            _canMove = false;

            // screen shake
            StartCoroutine(_camShake.ScreenShake(0.2f, 0.25f));

            // reset
            // put here
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard Tile"))
        {
            AudioManager.Instance.PlaySFX("Signal Ping");
        }
        else if (collision.CompareTag("Goal Tile"))
        {
            CurrentTile = collision.GetComponent<MapTile>();
            Debug.Log(CurrentTile._isGoal);
        }
         
    }
}
