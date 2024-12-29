using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float sprintMultiplier = 1.5f; // Multiplier for sprinting
    [SerializeField] float rotationSpeed = 15f;
    [SerializeField] LayerMask groundLayer;

    [SerializeField]
    [Tooltip("Input action for moving fatser")]
    InputAction moveFaster = new InputAction("moveFaster", type: InputActionType.Button, "<Keyboard>/shift");

    private NavMeshAgent agent;
    private Camera mainCamera;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;

        // Enable the InputAction
        moveFaster.Enable();
    }

    private void OnEnable()
    {
        moveFaster.Enable();
    }

    void OnDisable()
    {
        // Disable the InputAction
        moveFaster.Disable();
    }

    void Start()
    {
        // Set the initial speed of the NavMeshAgent
        agent.speed = moveSpeed;
    }

    void Update()
    {
        HandleMovement();
        HandleSprint();
        RotateTowardsMouse();
    }

    void HandleMovement()
    {
        if (Input.GetMouseButton(0)) // Left-click to move
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    void HandleSprint()
    {
        // Check if the sprint action is active
        if (moveFaster.IsPressed())
        {
            agent.speed = moveSpeed * sprintMultiplier;
        }
        else
        {
            agent.speed = moveSpeed; // Reset to normal speed
        }
    }

    void RotateTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPoint = hit.point;
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    Vector3 GetDirectionTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f;
            return direction.normalized;
        }

        return Vector3.zero;
    }
}
