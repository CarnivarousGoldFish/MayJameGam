using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map Parameters")]
    [SerializeField] private int _mapWidth;
    [SerializeField] private int _mapHeight;

    [SerializeField] private MapTile _clearTile;
    [SerializeField] private MapTile _hazardTile;
    [SerializeField] private MapTile _goalTile;

    [SerializeField] private List<Vector2> _hazardVectors = new List<Vector2>();
    private Dictionary<Vector2, MapTile> _tiles;

    [SerializeField] private Transform _camera;
    [SerializeField] private PlayerMarker _playerPrefab;

    private LeverManager _leverManager;

    public PlayerMarker PlayerMark { get; private set; }

    private void Start()
    {
        _leverManager = GetComponent<LeverManager>();
        // GenerateMapGrid();

    }

    public void GenerateMapGrid()
    {
        _tiles = new Dictionary<Vector2, MapTile>();

        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y  = 0; y < _mapHeight; y++)
            {
                foreach (Vector2 position in _hazardVectors)
                {
                    if (position.x == y && position.y == x)
                    {
                        var hazardTile = Instantiate(_hazardTile, new Vector3(x, y), Quaternion.identity);
                        HandleTileSpawn(hazardTile, x, y);
                    }
                }

                if (x == 2 && y == 2)
                {
                    var goalTile = Instantiate(_goalTile, new Vector3(x, y), Quaternion.identity);
                    HandleTileSpawn(goalTile, x, y);
                } 
                else
                {
                    var clearTile = Instantiate(_clearTile, new Vector3(x, y), Quaternion.identity);
                    HandleTileSpawn(clearTile, x, y);
                }
            }
        }

        SpawnPlayerMarker();
        _leverManager.SetUp(_mapWidth * 1.5f, _mapHeight / 2f);

        _camera.transform.position = new Vector3((float)_mapWidth / 1.125f - 0.5f, (float)_mapHeight / 2.25f - 0.5f, -10f);
    }

    private void HandleTileSpawn(MapTile tileType, int xPos, int yPos)
    {
        Debug.Log(tileType);

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

    public void SetPlayerTile()
    {
        MapTile newTile = _tiles.Where(t => t.Key.x == PlayerMark.transform.position.x && t.Key.y == PlayerMark.transform.position.y).First().Value;
        PlayerMark.CurrentTile = newTile;
    }

    private void SpawnPlayerMarker()
    {
        if (PlayerMark != null) return;

        PlayerMark = Instantiate(_playerPrefab);
        var spawnPoint = SetPlayerSpawn();

        spawnPoint.SetMarker(PlayerMark);
        Debug.Log("Spawned at: " + spawnPoint.transform.position);
        PlayerMark.Initialize(this);
    }
}
