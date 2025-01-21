using UnityEngine;
using UnityEngine.UI;

public class RestartPanel : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(ContinueGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveAllListeners();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
   
    private void ContinueGame()
    {
        GameRoot.Instance.RestartGame();
    }
}
