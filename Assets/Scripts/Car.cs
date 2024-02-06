using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField, Tooltip("The speed of the car")]
    private float _speed;

    [SerializeField, Tooltip("speed increase per second")]
    private float _accerelationRate;


    private void Update()
    {
        _speed += _accerelationRate * Time.deltaTime;
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
