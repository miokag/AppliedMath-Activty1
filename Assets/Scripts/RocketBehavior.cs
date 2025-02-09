using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector3 direction;

    void Start()
    {
        direction = transform.forward;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}