using System.Collections;
using UnityEngine;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    // Movement Code GameProg 3
    [Header("Movement")]
    [Tooltip("Horizontal Speed")]
    [SerializeField] private float moveSpeed;
    [Tooltip("Rate of Change for Movement Speed")]
    [SerializeField] private float acceleration;
    [Tooltip("Deceleration Rate When No Input")]
    [SerializeField] private float deceleration;
    
    [Header("Controls")] 
    [Tooltip("Use Keys to Move")]
    [SerializeField] private KeyCode forwardKey = KeyCode.W;
    [SerializeField] private KeyCode backwardKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    
    private Vector3 _inputVector;
    private float _currentSpeed;
    private CharacterController _charController;
    private float _initialYPosition;
    
    // Fire Rocket Applied Math
    [Header("Rocket")]
    [SerializeField] private GameObject rocket;
    [SerializeField] private float rocketCooldown = 5f;
    [SerializeField] private int rocketCount = 4;
    [SerializeField] private float rocketLifetime = 3f;
    [SerializeField] private TMP_Text maxRocketText;

    
    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
        _initialYPosition = _charController.transform.localPosition.y;
    }
    
    private void Start()
    {
        StartCoroutine(FireRocketsRoutine());
    }
    
    void Update()
    {
        HandleInput();
        Move(_inputVector);
    }
    
    private IEnumerator FireRocketsRoutine()
    {
        while (true)
        {
            InsantiateRocket();
            yield return new WaitForSeconds(rocketCooldown);
        }
    }
    
    private void HandleInput()
    {
        float xInput = 0;
        float zInput = 0;
        
        if (Input.GetKey(forwardKey)) zInput++;
        else if (Input.GetKey(backwardKey)) zInput--;
        if (Input.GetKey(leftKey)) xInput--;
        else if (Input.GetKey(rightKey)) xInput++;
        
        _inputVector = new Vector3(xInput, 0, zInput);
    }
    
    private void Move(Vector3 _inputVector)
    {
        if (_inputVector == Vector3.zero)
        {
            if (_currentSpeed > 0)
            {
                _currentSpeed -= deceleration * Time.deltaTime;
                _currentSpeed = Mathf.Max(_currentSpeed, 0);
            }
        }
        else
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, moveSpeed, Time.deltaTime * acceleration);
        }
        
        Vector3 movement = _inputVector.normalized * _currentSpeed * Time.deltaTime;
        _charController.Move(movement);
        transform.position = new Vector3(transform.position.x, _initialYPosition, transform.position.z);
    }

    private void InsantiateRocket()
    {
        float angleIncrement = (2 * Mathf.PI) / rocketCount; // Divide 360 degrees (2π radians) by the rocket count

        for (int i = 0; i < rocketCount; i++)
        {
            float angle = (i / (float)rocketCount) * 2 * Mathf.PI;
            
            float x = Mathf.Cos(angle); 
            float z = Mathf.Sin(angle); 
            
            Vector3 spawnPosition = transform.position + Vector3.up * 0.8f; 
            Quaternion rotation = Quaternion.Euler(0, angle * Mathf.Rad2Deg, 0); 
            GameObject newRocket = Instantiate(rocket, spawnPosition, rotation); 

            // Destroy the rocket after the specified lifetime
            Destroy(newRocket, rocketLifetime);
        }
    }
    
    public void IncreaseRocketCount(int amount)
    {
        rocketCount = Mathf.Min(rocketCount + amount, 8);
        if (rocketCount == 8) maxRocketText.gameObject.SetActive(true); 
        else maxRocketText.gameObject.SetActive(false);
    }
    
    public int RocketCount
    {
        get { return rocketCount; }
    }
}
