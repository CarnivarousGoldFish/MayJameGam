using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private CrossMarker _crossMarker;

    private CrossMarker _crossInst;
    private MapManager _manager;

    private bool _onTile;

    public BaseMarker CurrentMarker { get; private set; }

    public virtual void Initialize(MapManager manager)
    {
        _manager = manager;
        CurrentMarker = null;
    }

    private void Update()
    {
        if (!_onTile) return;

        if (Input.GetMouseButtonDown(0))
        {
            // if the tile has no marker spawned, update the player's current tile
            if (!CurrentMarker)
            {
                _manager.PlayerMark.SetDestination(transform.position);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // if tile has a marker on it and it is a cross, destroy
             if (CurrentMarker && CurrentMarker is CrossMarker)
                DestroyCrossMarker();
            // else, spawn
            else if (!CurrentMarker)
                SpawnCrossMarker();
        }
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
        _onTile = true;
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
        _onTile = false;
    }

    private void SpawnCrossMarker()
    {
        _crossInst = Instantiate(_crossMarker, transform.position, Quaternion.identity);
        CurrentMarker = _crossInst;
    }

    private void DestroyCrossMarker()
    {
        Destroy(_crossInst.gameObject);
    }

    public void SetMarker(BaseMarker marker)
    {
        marker.SetMarkerPosition(transform.position);
        CurrentMarker = marker;
        marker.CurrentTile = this;
    }
}
