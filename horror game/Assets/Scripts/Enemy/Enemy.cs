using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private float _hp;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private bool _runPlayer = false;
    private NavMeshAgent _agent;
    private GameObject _player;
    private Transform _targetPoint;
    [SerializeField] private Transform[] _patrolPoints;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        _agent.speed = _speed;
        _targetPoint = _patrolPoints[UnityEngine.Random.Range(0, _patrolPoints.Length)];
    }
    private void Update()
    {
        DisableWalkToPlayer();
        Run();
    }
    private void DisableWalkToPlayer()
    {
        float d = Vector3.Dot((_player.transform.position - transform.position).normalized, transform.forward);
        RaycastHit hit;
        if(d > 0.2f)
        {
            if (Physics.Raycast(transform.position, _player.transform.position - transform.position, out hit, _distance))
            {
                if (hit.transform.gameObject == _player)
                {
                    StartCoroutine(RunToPlayer());
                }
                Debug.DrawRay(transform.position, _player.transform.position - transform.position);
            }
        }
    }
    public void TakeDamage(float damage)
    {
        _hp -= damage;
        if(_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Run()
    {
        if(_runPlayer)
        {
            _agent.SetDestination(_player.transform.position); 
        }
        else
        {
            WalkToNewPoint();
        }
    }
    private void WalkToNewPoint()
    {
        if(transform.position.x == _targetPoint.position.x && transform.position.z == _targetPoint.position.z)
        {
            _targetPoint = _patrolPoints[UnityEngine.Random.Range(0, _patrolPoints.Length)];
        }
        _agent.SetDestination(_targetPoint.position);
    }
    public IEnumerator RunToPlayer()
    {
        _runPlayer = true;
        yield return new WaitForSeconds(5);
        _runPlayer = false;
    }
    public void Stun(float timeStun)
    { 
        StartCoroutine(TimeStun(timeStun));
    }
    public IEnumerator TimeStun(float timeStun)
    {
        _agent.speed = 0;
        yield return new WaitForSeconds(timeStun);
        _agent.speed = _speed;
    }
}
