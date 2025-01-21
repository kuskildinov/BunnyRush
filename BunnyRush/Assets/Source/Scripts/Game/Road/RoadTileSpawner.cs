using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadTileSpawner : MonoBehaviour
{
    [SerializeField] private RoadTilesRoot _roadTileRoot;
    [SerializeField] private GameLevelRoot _gameLevelRoot;
    [SerializeField] private RoadTile _emptyRoad;
    [SerializeField] private Transform _tilesContainer;

    private Vector2Int _levelOneEasyLimits = new Vector2Int(0,300);
    private Vector2Int _levelOneHardLimits = new Vector2Int(300, 1000);

    private Vector2Int _levelTwoEasyLimits = new Vector2Int(1000, 1300);
    private Vector2Int _levelTwoHardLimits = new Vector2Int(1300, 2000);

    private Vector2Int _levelThreeEasyLimits = new Vector2Int(2000, 2300);
    private Vector2Int _levelThreeHardLimits = new Vector2Int(2300, 3000);

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
                GenerateLevelOneEmptyTile();
            }
            else
            {
                GenerateLevelOneEasyTiles();
            }     
        }
    }

    private void Update()
    {
        float score = (int)GameRoot.Instance.GameScore.CurrentLevelScore;
      
        if (_roadTileRoot.Tiles.Count < _maxCount)
        {
            if(score > _levelOneEasyLimits.x && score < _levelOneEasyLimits.y)
            {
                GenerateLevelOneEasyTiles();
            }
            else if (score >= _levelOneHardLimits.x && score < _levelOneHardLimits.y)
            {
                GenerateLevelOneHardTiles();
            }

            else if (score == _levelTwoEasyLimits.x)
            {
                GenerateLevelTwoEmptyTile();
            }
            if (score > _levelTwoEasyLimits.x && score < _levelTwoEasyLimits.y)
            {
                GenerateLevelTwoEasyTiles();
            }
            else if (score >= _levelTwoHardLimits.x && score < _levelTwoHardLimits.y)
            {
                GenerateLevelTwoHardTiles();
            }

            else if (score == _levelThreeEasyLimits.x)
            {
                GenerateLevelThreeEmptyTile();
            }
            else if (score > _levelThreeEasyLimits.x)
            {
                GenerateLevelThreeEasyTiles();
            }
        }
    }

    public void OnGameStart()
    {
        OnGameStarted?.Invoke();
    }

    public void SetNewTilesSpeed(float speed)
    {
        _speed = speed;
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

    #region Level 1
    private void GenerateLevelOneEmptyTile()
    {
        RoadTile newTile = _gameLevelRoot.GetEmptyTile(1f);
        GenerateTile(newTile);
    }

    private void GenerateLevelOneEasyTiles()
    {
        RoadTile newTile = _gameLevelRoot.GetRandomTile(1.1f);
        GenerateTile(newTile);
    }
    private void GenerateLevelOneHardTiles()
    {
        RoadTile newTile = _gameLevelRoot.GetRandomTile(1.2f);
        GenerateTile(newTile);
    }
    #endregion
    #region Level 2
    private void GenerateLevelTwoEmptyTile()
    {
        RoadTile newTile = _gameLevelRoot.GetEmptyTile(2f);
        GenerateTile(newTile);
    }

    private void GenerateLevelTwoEasyTiles()
    {
        RoadTile newTile = _gameLevelRoot.GetRandomTile(2.1f);
        GenerateTile(newTile);
    }
    private void GenerateLevelTwoHardTiles()
    {
        RoadTile newTile = _gameLevelRoot.GetRandomTile(2.2f);
        GenerateTile(newTile);
    }
    #endregion
    #region Level 3
    private void GenerateLevelThreeEmptyTile()
    {
        RoadTile newTile = _gameLevelRoot.GetEmptyTile(3f);
        GenerateTile(newTile);
    }

    private void GenerateLevelThreeEasyTiles()
    {
        RoadTile newTile = _gameLevelRoot.GetRandomTile(3.1f);
        GenerateTile(newTile);
    }
    private void GenerateLevelThreeHardTiles()
    {
        RoadTile newTile = _gameLevelRoot.GetRandomTile(3.2f);
        GenerateTile(newTile);
    }
    #endregion

}
