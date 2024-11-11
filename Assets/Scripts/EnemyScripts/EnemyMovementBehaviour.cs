using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementBehaviour : MonoBehaviour
{
    protected Transform playerTransform;
    
    protected NavMeshAgent agent;
    protected Rigidbody rb;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected float enemyHeight = 0.5f;

    [SerializeField] protected float pathUpdateRate = 0.2f;
    protected float pathUpdateTimer = 0;

    [SerializeField] protected float defaultSpeed;
    [SerializeField] protected float defaultAngularSpeed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        defaultSpeed = agent.speed;
        defaultAngularSpeed = agent.angularSpeed;

        SetPlayerTransform();
    }

    void OnEnable() {
        GameStateHandler.onPlayerSpawn += SetPlayerTransform;
    }
    void OnDisable() {
        GameStateHandler.onPlayerSpawn -= SetPlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        // very crappy every-frame call. This may be an Event or GameStateHandler will call "SetPlayer" method on a EnemyDirector class, which will then call "SetPlayer" to every EnemyMovement script
        if (GameStateHandler.instance.player == null) {
            return;
        }
        
        Move();
    }

    public virtual void Move() {
        MoveToPlayer();
    }

    protected void MoveToPlayer() {
        // print("moving to player");
        if (pathUpdateTimer >= pathUpdateRate) {
            agent.destination = playerTransform.position;
            pathUpdateTimer = 0;
        } else {
            pathUpdateTimer += Time.deltaTime;
        }
    }

    public void UpdateSpeeds(float multiplier) {
        agent.speed = defaultSpeed * multiplier;
        agent.angularSpeed = defaultAngularSpeed * multiplier;
    }

    protected void SetPlayerTransform() {
        if (GameStateHandler.instance.player != null) {
            playerTransform = GameStateHandler.instance.player.transform;
        }
    }
}
