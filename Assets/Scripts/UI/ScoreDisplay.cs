using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField, Tooltip("Reference to the score text to update it")]
    private TextMeshProUGUI _scoreTextField;


    // score and score modifier to control how much it increases with time
    private float _score;

    [SerializeField, Tooltip("modifier to how mucg score increases with passing time")]
    private float _scoreModifier;

    private void Update()
    {
        _score += Time.deltaTime * _scoreModifier;

        _scoreTextField.text = _score.ToString("0000");
    }
}
