using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // timed aspect of the energy recharge
    [SerializeField, Tooltip("How much time it needs to refill enery bar")]
    private int _energyRefillRate;


    [SerializeField, Tooltip("Max number of energy")]
    private int _maxEnergy;


    private int _currentEnergy;


    private const string _enregyKey = "Energy";
    private const string _energyReadyKey = "Energy Ready";


    [SerializeField]
    private Button _playBtn;

    [SerializeField]
    private TextMeshProUGUI _playBtnTextField;


    [SerializeField, Tooltip("Score Display section")]
    private TextMeshProUGUI _highScoreTextField;


    private void Start()
    {
        _playBtn.onClick.AddListener(() => 
        {
            SceneManager.LoadScene(1);
        });

        DisplayHighestScore();
        CheckEnergy();
    }


    private void CheckEnergy()
    {
        _currentEnergy = PlayerPrefs.GetInt(_enregyKey, _maxEnergy);

        if (_currentEnergy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(_energyReadyKey, String.Empty);

            if (string.IsNullOrEmpty(_enregyKey))
            {
                return;
            }

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady)
            {
                _currentEnergy = _maxEnergy;
                PlayerPrefs.SetInt(_enregyKey, _currentEnergy);
            }
        }

        _playBtnTextField.text = $"Play ({_currentEnergy})";
    }

    private void DisplayHighestScore()
    {
        float currentHighScroe = PlayerPrefs.GetFloat("High Score", 0);
        _highScoreTextField.text = currentHighScroe.ToString("0000");
    }
}
