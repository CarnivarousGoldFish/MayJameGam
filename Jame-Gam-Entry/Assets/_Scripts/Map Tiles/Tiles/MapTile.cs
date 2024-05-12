using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MapTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private CrossMarker _crossMarker;

    public BaseMarker CurrentMarker { get; private set; }
    private CrossMarker _crossInst;

    private MapManager _manager;
    private bool _onTile;

    public bool _isHazard;

    public virtual void Initialize(MapManager manager)
    {
        _manager = manager;
        CurrentMarker = null;
    }

    private void Update()
    {
        if (!_onTile) return;
        if (SceneManagement.isPaused) return;
        if (Input.GetMouseButtonDown(0))
        {

            if (!CurrentMarker)
            {
                _manager.PlayerMark.SetDestination(transform.position);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
             if (CurrentMarker && CurrentMarker is CrossMarker)
                DestroyCrossMarker();
            else if (!CurrentMarker)
                SpawnCrossMarker();
        }
    }

    private void OnMouseEnter()
    {
        if (SceneManagement.isPaused) return;
        _highlight.SetActive(true);
        _onTile = true;
    }

    private void OnMouseExit()
    {
        if (SceneManagement.isPaused) return;
        _highlight.SetActive(false);
        _onTile = false;
    }

    private void SetPlayerDestination()
    {

    }

    private void SpawnCrossMarker()
    {
        _crossInst = Instantiate(_crossMarker, transform.position, Quaternion.identity);
        CurrentMarker = _crossInst;
    }

    private void DestroyCrossMarker()
    {
        Debug.Log("A cross is here! Deleting it!");
        Destroy(_crossInst.gameObject);
    }

    public void SetMarker(BaseMarker marker)
    {
        //if (marker.CurrentTile != null) marker.CurrentTile.CurrentMarker = null;

        marker.SetMarkerPosition(transform.position);
        CurrentMarker = marker;
        marker.CurrentTile = this;
    }
}
