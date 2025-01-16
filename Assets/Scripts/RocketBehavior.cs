using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
}
