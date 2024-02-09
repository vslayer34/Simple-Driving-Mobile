using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField, Tooltip("The speed of the car")]
    private float _speed;

    [SerializeField, Tooltip("speed increase per second")]
    private float _accerelationRate;

    [SerializeField, Tooltip("Turn rate of the car")]
    private float _turnRate;

    private float _steerDirection;


    private void Update()
    {
        _speed += _accerelationRate * Time.deltaTime;
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        SteerCar(_steerDirection);
        Debug.Log(_steerDirection);
    }


    /// <summary>
    /// Apply rotation to the car transform
    /// </summary>
    private void SteerCar(float direction)
    {
        transform.Rotate(0.0f, direction * _turnRate * Time.deltaTime, 0.0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }
    }


    public float SteerDirection { set => _steerDirection = value; }
}
