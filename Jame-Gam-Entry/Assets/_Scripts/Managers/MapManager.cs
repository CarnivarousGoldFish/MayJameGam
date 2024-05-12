using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map Parameters")]
    [SerializeField] private int _mapWidth;
    [SerializeField] private int _mapHeight;

    [Header("Tile References")]
    [SerializeField] private MapTile _clearTile;
    [SerializeField] private MapTile _hazardTile;
    [SerializeField] private MapTile _goalTile;

    [Header("Hazard Positions")]
    [SerializeField] private List<Vector2> _hazardVectors = new List<Vector2>();
    private Dictionary<Vector2, MapTile> _tiles;

    [Header("Miscellaneous")]
    [SerializeField] private Transform _camera;
    [SerializeField] private PlayerMarker _playerPrefab;
    [SerializeField] private CameraShake _camShake;
    [SerializeField] private GameObject _overlayCanvas;
    [SerializeField] private Transform _tileParent;

    public PlayerMarker PlayerMark { get; private set; }

    private void Start()
    {
        GenerateMapGrid();
        TurnOnOverlay(false);
    }

    public void GenerateMapGrid()
    {
        _tiles = new Dictionary<Vector2, MapTile>();

        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                if (_hazardVectors.Contains(new Vector2(y, x)))
                {
                    var hazardTile = Instantiate(_hazardTile, new Vector3(x, y), Quaternion.identity, _tileParent);
                    HandleTileSpawn(hazardTile, x, y);
                }
                else if (x == 2 && y == 2)
                {
                    var goalTile = Instantiate(_goalTile, new Vector3(x, y), Quaternion.identity, _tileParent);
                    HandleTileSpawn(goalTile, x, y);
                }
                else
                {
                    var clearTile = Instantiate(_clearTile, new Vector3(x, y), Quaternion.identity, _tileParent);
                    HandleTileSpawn(clearTile, x, y);
                }
            }
        }

        SpawnPlayerMarker();
        _camera.transform.position = new Vector3((float)_mapWidth / 1.125f - 0.5f, (float)_mapHeight / 2.25f - 0.5f, -10f);
    }

    private void HandleTileSpawn(MapTile tileType, int xPos, int yPos)
    {
        tileType.name = $"Tile {xPos} {yPos}";
        tileType.Initialize(this);

        _tiles[new Vector2(xPos, yPos)] = tileType;
    }

    private MapTile SetPlayerSpawn()
    {
        return _tiles
            .Where(t => t.Key.x == Mathf.RoundToInt(_mapWidth / 2) && t.Key.y == _mapHeight - 1)
            .OrderBy(t => Random.value).First().Value;
    }

    private void SpawnPlayerMarker()
    {
        if (PlayerMark != null) return;

        PlayerMark = Instantiate(_playerPrefab);
        var spawnPoint = SetPlayerSpawn();
        PlayerMark.transform.SetParent(_tileParent);
        spawnPoint.SetMarker(PlayerMark);
        PlayerMark.Initialize(this, _camShake);
    }

    public void TurnOnOverlay(bool value)
    {
        _overlayCanvas.SetActive(value);
    }

    public void SetPlayerTile()
    {
        MapTile newTile = _tiles.Where(t => t.Key.x == PlayerMark.transform.position.x && t.Key.y == PlayerMark.transform.position.y).First().Value;
        PlayerMark.CurrentTile = newTile;
    }
}
