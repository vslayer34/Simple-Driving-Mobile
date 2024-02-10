using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _playBtn;

    [SerializeField, Tooltip("Score Display section")]
    private TextMeshProUGUI _highScoreTextField;


    private void Start()
    {
        _playBtn.onClick.AddListener(() => 
        {
            SceneManager.LoadScene(1);
        });

        DisplayHighestScore();
    }


    private void DisplayHighestScore()
    {
        float currentHighScroe = PlayerPrefs.GetFloat("High Score", 0);
        _highScoreTextField.text = currentHighScroe.ToString("0000");
    }
}
