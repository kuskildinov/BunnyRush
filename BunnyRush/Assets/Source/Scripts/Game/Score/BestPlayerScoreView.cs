using UnityEngine;
using UnityEngine.UI;

public class BestPlayerScoreView : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _scoreText;

    public void SetNewBestPlayerScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void SetNewBestPlayerName(string name)
    {
        _nameText.text = name;
    }

    public void SetNewBestPlayerIcon(Texture icon)
    {
        _nameText.text = name;
    }
}
