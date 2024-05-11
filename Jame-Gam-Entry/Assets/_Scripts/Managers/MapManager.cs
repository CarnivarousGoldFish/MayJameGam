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

    private Dictionary<Vector2, MapTile> _tiles;

    [SerializeField] private Transform _camera;
    [SerializeField] private PlayerMarker _playerPrefab;

    public PlayerMarker PlayerMark { get; private set; }

    private void Start()
    {
        GenerateMapGrid();
    }

    private void GenerateMapGrid()
    {
        _tiles = new Dictionary<Vector2, MapTile>();

        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y  = 0; y < _mapHeight; y++)
            {
                var randomTile = Random.Range(0, 50) == 1 ? _hazardTile : _clearTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);

                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.Initialize(this);

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        SpawnPlayerMarker();
        _camera.transform.position = new Vector3((float)_mapWidth / 1.25f - 0.5f, (float)_mapHeight / 2f - 0.5f, -10f);
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

        spawnPoint.SetMarker(PlayerMark);
        Debug.Log("Spawned at: " + spawnPoint.transform.position);
    }
}
