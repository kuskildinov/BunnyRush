using UnityEngine;
using UnityEngine.UI;

public class PlayerRoot : CompositeRoot
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private MenuRoot _menuRoot;   

    private Player _currentPlayer;

    public Player Player => _currentPlayer;

    public override void Compose()
    {       
        SetNewPlayer();
    }

    private void OnEnable()
    {
        _menuRoot.OnStartGameButtonClicked += StartGame;
    }

    private void OnDisable()
    {
        _menuRoot.OnStartGameButtonClicked -= StartGame;
    }

    public void SetNewPlayer()
    {
        if (_currentPlayer != null)
            Destroy(_currentPlayer.gameObject);

        PlayerInput input = new PlayerInput();
        int index = SaveRoot.Instance.PlayerSaveData.CurrentSkinIndex;
        _currentPlayer = _playerSpawner.SpawnPlayer(index);
        if (_currentPlayer == null)
        {
            _currentPlayer = _playerSpawner.SpawnPlayer(0);
        }
        _currentPlayer.Initialize(input);
    }

    public void StartGame()
    {
        SetNewPlayer();
        _currentPlayer.StartGame();
    }

    public void ContinueGame()
    {
        _currentPlayer.ContinueGame();
    }

    public void ResumeGame()
    {
        SetNewPlayer();
        _currentPlayer.ResumeGame();
    }

    public void ForceJump()
    {
        _currentPlayer.ForceJump();
    }
}
