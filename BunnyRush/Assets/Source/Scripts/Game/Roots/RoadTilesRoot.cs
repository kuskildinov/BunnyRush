using System.Collections.Generic;
using UnityEngine;

public class RoadTilesRoot : CompositeRoot
{
    [SerializeField] private RoadTileSpawner _spawner;
    [SerializeField] private float _speed;
    [SerializeField] private int _maxCount;
    [SerializeField] private MenuRoot _menuRoot;

    [SerializeField] private List<RoadTile> _tiles;

    private bool _isGameStarted;

    public bool IsGameStarted => _isGameStarted;
    public List<RoadTile> Tiles { get => _tiles; set => _tiles = value; }

    public override void Compose()
    {       
        _spawner.Initialize(_speed, _maxCount);
    }

    public void StartGame()
    {
        _spawner.OnGameStart();
        _isGameStarted = true;       
    }

    public void ResumeGame()
    {
        StartGame();
    }

    public void EndGame()
    {
        _isGameStarted = false;
        StopRoad();
    }

    public void SetNewTilesSpeed(float speed)
    {
        _spawner.SetNewTilesSpeed(speed);
    }

    public void StartRoad()
    {
        foreach (RoadTile tile in _tiles)
        {
            tile.ActivateTile();
        }
    }

    public void StopRoad()
    {
        foreach (RoadTile tile in _tiles)
        {
            tile.DeactivateTile();
        }
    }

    private void OnEnable()
    {
        _menuRoot.OnStartGameButtonClicked += StartGame;
    }

    private void OnDisable()
    {
        _menuRoot.OnStartGameButtonClicked -= StartGame;
    }
}
