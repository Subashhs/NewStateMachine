using UnityEngine;
using UnityEngine.AI;

public class StatePatternEnemy : MonoBehaviour
{
    public float searchDuration; // How long we search in Alert state
    public float searchTurnSpeed; // How fast we turn in Alert State
    public float sightRange; // How far enemy can see
    public float attackRange; // How close the player needs to be to enter HuntState
    public Transform[] waypoints;
    public Transform eye;
    public MeshRenderer indicator;
    public GameObject bulletPrefab; // Bullet prefab for hunting
    public Transform shootPoint; // Where to instantiate bullets from
    public float bulletSpeed = 20f; // Speed at which bullets are thrown

    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public HuntState huntState; // Add HuntState
    [HideInInspector] public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);
        huntState = new HuntState(this); // Initialize HuntState
    }

    // Start is called once before the first execution of Update
    void Start()
    {
        currentState = patrolState; // Start in PatrolState
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
}
