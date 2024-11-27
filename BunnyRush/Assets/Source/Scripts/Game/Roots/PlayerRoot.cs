using UnityEngine;
using UnityEngine.UI;

public class PlayerRoot : CompositeRoot
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private MenuRoot _menuRoot;   

    private Player _currentPlayer;

    public override void Compose()
    {      
        PlayerInput input = new PlayerInput();

        int index = SaveRoot.Instance.PlayerSaveData.CurrentSkinIndex;
        _currentPlayer = _playerSpawner.SpawnPlayer(index);  
        if(_currentPlayer == null)
        {
            _currentPlayer = _playerSpawner.SpawnPlayer(0);
        }
        _currentPlayer.Initialize(input);
    }

    private void OnEnable()
    {
        _menuRoot.OnStartGameButtonClicked += StartGame;
    }

    private void OnDisable()
    {
        _menuRoot.OnStartGameButtonClicked -= StartGame;
    }

    public void StartGame()
    {
        _currentPlayer.StartGame();
    }

    public void ForceJump()
    {
        _currentPlayer.ForceJump();
    }
}
