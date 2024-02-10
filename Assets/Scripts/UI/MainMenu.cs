using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Notifications
    [SerializeField, Tooltip("Reference to the notification handler")]
    private AndroidNotificationHandler _andoridNotification;


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
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool isFocused)
    {
        if (!isFocused)
        {
            return;
        }

        CancelInvoke();
        DisplayHighestScore();
        CheckEnergy();

        _playBtn.onClick.AddListener(() => 
        {
            if (_currentEnergy < 1)
            {
                return;
            }

            _currentEnergy--;
            PlayerPrefs.SetInt(_enregyKey, _currentEnergy);

            if (_currentEnergy <= 0)
            {
                DateTime energyReady = DateTime.Now.AddMinutes(_energyRefillRate);
                PlayerPrefs.SetString(_energyReadyKey, energyReady.ToString());
                #if UNITY_ANDROID
                _andoridNotification.SchedualGameReadyNotification(energyReady);
                #endif
            }

            SceneManager.LoadScene(1);
        });
    }


    private void CheckEnergy()
    {
        _currentEnergy = PlayerPrefs.GetInt(_enregyKey, _maxEnergy);

        if (_currentEnergy <= 0)
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
            else
            {
                _playBtn.interactable = false;
                Invoke(nameof(RefreshScreen), (energyReady - DateTime.Now).Seconds);
            }
        }

        _playBtnTextField.text = $"Play ({_currentEnergy})";
    }


    private void RefreshScreen()
    {
        _currentEnergy = _maxEnergy;
        PlayerPrefs.SetInt(_enregyKey, _currentEnergy);
        _playBtn.interactable = true;
        _playBtnTextField.text = $"Play ({_currentEnergy})";
    }

    private void DisplayHighestScore()
    {
        float currentHighScroe = PlayerPrefs.GetFloat("High Score", 0);
        _highScoreTextField.text = currentHighScroe.ToString("0000");
    }
}
