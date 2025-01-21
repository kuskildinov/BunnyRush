using UnityEngine;
using UnityEngine.SceneManagement;


public class GameRoot : CompositeRoot
{
    public static GameRoot Instance;

    [SerializeField] private PlayerRoot _playerRoot;
    [SerializeField] private MenuRoot _menuRoot;
    [SerializeField] private RoadTilesRoot _roadRoot;
    [SerializeField] private CoinsCollector _coinsCollector;
    [SerializeField] private GameLevelRoot _gameDifficalty;
    [SerializeField] private GameScore _gameScore;
    [SerializeField] private PlayerGameUI _playerGameUI;
    [SerializeField] private LosePanel _losePanel;
    public CoinsCollector CoinsCollector => _coinsCollector;
    public GameScore GameScore => _gameScore;
    public PlayerRoot PlayerRoot => _playerRoot;
    public RoadTilesRoot RoadTilesRoot => _roadRoot;

    public override void Compose()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Time.timeScale = 1f;

        _gameDifficalty.Initialize();
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
        MyYandex.Instance.StartGameplay();

        _playerGameUI.StartGame();
        _coinsCollector.StartGame();
        _gameScore.StartGame();
    }

    public void PauseGame()
    {
        _gameScore.EndGame();
        Time.timeScale = 0f;

        MyYandex.Instance.StopGameplay();
    }

    public void ResumeGame()
    {
        MyYandex.Instance.StartGameplay();

        Time.timeScale = 1f;        
        _roadRoot.ResumeGame();
        _playerRoot.ContinueGame();
        _gameScore.ResumeGame();        
    }

    public void PlayerLose(DeadZone zone)
    {
        MyYandex.Instance.StopGameplay();
        zone.GetComponent<Collider>().isTrigger = false;

        _playerGameUI.PlayerLose();
        _roadRoot.EndGame();
        _gameScore.EndGame();
    }

    public void EndGame(bool getCoins)
    {
        _gameScore.EndGame();
        _playerGameUI.gameObject.SetActive(false);
        if(getCoins)
            SaveRoot.Instance.AddCoinsCount(_coinsCollector.GetCoinsCount());

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnCoinTaked()
    {
        SoundsRoot.Instance.PlayCoinTakeSound();
        _coinsCollector.OnCoinTaked();
    }

    public void BackToMenu()
    {
        MyYandex.Instance.StopGameplay();
        EndGame(false);
    }

    public void TryRestartGame()
    {
        AdvRoot.Instance.ShowRestartRewardAdv();
    }

    public void OpenRestartPanel()
    {
        _losePanel.ContinueGame();
    }

    public void RestartGame()
    {      
        _playerGameUI.StartGame();
        MyYandex.Instance.StartGameplay();

        Time.timeScale = 1f;
        _roadRoot.ResumeGame();
        _playerRoot.ResumeGame();
        _gameScore.ResumeGame();
    }

}
