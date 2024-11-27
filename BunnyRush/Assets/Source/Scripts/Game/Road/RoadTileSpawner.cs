using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadTileSpawner : MonoBehaviour
{
    [SerializeField] private RoadTilesRoot _roadTileRoot;
    [SerializeField] private RoadTile _emptyRoad;
    [SerializeField] private List<RoadTile> _tilePrefabs;
    [SerializeField] private Transform _tilesContainer;
   

    private float _speed;
    private int _maxCount;

    public event Action OnGameStarted;

    public void Initialize(float speed, int maxCount)
    {
        _speed = speed;
        _maxCount = maxCount;

        GenerateTile(_emptyRoad);
        _roadTileRoot.Tiles.First().Initialize(this, _speed);

        for (int i = 0; i < _maxCount; i++)
        {
            if(i <= 2)
            {
                GenerateTile(_emptyRoad);
            }
            else
            {
                RoadTile newTile = GetRandomTile();
                GenerateTile(newTile);
            }     
        }
    }

    private void Update()
    {
        if (_roadTileRoot.Tiles.Count < _maxCount)
        {
            RoadTile newTile = GetRandomTile();
            GenerateTile(newTile);
        }
    }

    public void OnGameStart()
    {
        OnGameStarted?.Invoke();
    }

    public void DestroyTile(RoadTile tile)
    {
        _roadTileRoot.Tiles.Remove(tile);
            Destroy(tile.gameObject);
    }

    private void GenerateTile(RoadTile tilePrefab)
    {
        RoadTile newTile = Instantiate(tilePrefab, _roadTileRoot.Tiles.Last().transform.position + Vector3.right * tilePrefab.transform.localScale.x * 20, Quaternion.identity);
        newTile.Initialize(this,_speed);
        newTile.transform.SetParent(_tilesContainer);
        _roadTileRoot.Tiles.Add(newTile);
        if(_roadTileRoot.IsGameStarted)
        {
            newTile.ActivateTile();
        }
    }

    private RoadTile GetRandomTile()
    {
        var randonIndex = UnityEngine.Random.Range(0, _tilePrefabs.Count);
        return _tilePrefabs[randonIndex];
    }
}
