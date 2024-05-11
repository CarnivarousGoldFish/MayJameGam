using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverManager : MonoBehaviour
{

    public static LeverManager Instance;
    public UnityEvent movementChange;

    [SerializeField] LeverObject _leverPrefab;
    private LeverObject _leverInst;

    public MovementBehavior CurrentBehavior { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetUp(float xPos, float yPos)
    {
        _leverInst = Instantiate(_leverPrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
        _leverInst.Initialize(this);
    }

    public void ChangeForward()
    {
        CurrentBehavior = MovementBehavior.Forward;
        movementChange?.Invoke();
    }

    public bool CheckForward()
    {
        if (CurrentBehavior == MovementBehavior.Forward)
            return true;

        return false;
    }

    public void ChangeStop()
    {
        CurrentBehavior = MovementBehavior.Stop;
        movementChange?.Invoke();
    }

    public bool CheckStop()
    {
        if (CurrentBehavior == MovementBehavior.Stop)
            return true;

        return false;
    }

    public void ChangeReverse()
    {
        CurrentBehavior = MovementBehavior.Reverse;
        movementChange?.Invoke();
    }

    public bool CheckReverse()
    {
        if (CurrentBehavior == MovementBehavior.Reverse)
            return true;

        return false;
    }

    public MovementBehavior GetMovementBehavior()
    {
        return CurrentBehavior;
    }
}

public enum MovementBehavior
{
    Forward,
    Stop,
    Reverse
}