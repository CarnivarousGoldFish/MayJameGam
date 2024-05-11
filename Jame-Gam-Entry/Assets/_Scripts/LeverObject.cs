using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverObject : MonoBehaviour
{
    [SerializeField] GameObject _highlight;
    [SerializeField] private float _forwardPos = 7.5f;
    [SerializeField] private float _stopPos = 4f;
    [SerializeField] private float _reversePos = 0.5f;

    private LeverManager _manager;

    private bool _isDragging;
    private bool _onLever;

    private Vector3 _mousePos;
    private float _offset;

    private float _lastPos;

    //public MarkerBehavior _currentBehavior;

    public void Initialize(LeverManager manager)
    {
        if (_manager) return;

        _manager = manager;

        _lastPos = _stopPos;
    }

    private void Update()
    {
        //if (!_onLever) return;

        /*
         if (Input.GetMouseButtonDown(0))
        {
            _manager.ChangeBehavior();
        }
        */

        
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (_isDragging)
        {
            transform.position = new Vector3(transform.position.x, _mousePos.y + _offset, transform.position.z);
            var leverPos = transform.position;

            if (transform.position.y >= _forwardPos)
                leverPos.y = _forwardPos;

            else if (transform.position.y <= _reversePos)
                leverPos.y = _reversePos;

            transform.position = leverPos;
        }
        
    }

    private void PositionHandler()
    {
        if (CheckDistance(_forwardPos))
        {
            SetLeverPosition(_forwardPos);
            _manager.ChangeForward();

        }
        else if (CheckDistance(_stopPos))
        {
            SetLeverPosition(_stopPos);
            _manager.ChangeStop();

        }
        else if (CheckDistance(_reversePos))
        {
            SetLeverPosition(_reversePos);
            _manager.ChangeReverse();

        }
        else
        {
            SetLeverPosition(_lastPos);
        }
    }

    private bool CheckDistance(float target)
    {
        return (Vector2.Distance(transform.position, new Vector3(transform.position.x, target, transform.position.z)) < 1.5f);
    }

    private void SetLeverPosition(float target)
    {
        var newPos = transform.position;
        newPos.y = target;
        transform.position = newPos;

        _lastPos = target;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
        _onLever = true;
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
        _onLever = false;
    }

     private void OnMouseDown()
    {
        if (!_onLever) return;

        var calc = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _offset = calc.y;
        _isDragging = true;
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        PositionHandler();
    }
}
