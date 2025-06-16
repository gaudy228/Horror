using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private float _hp;
    [SerializeField] private float _speed;
    [SerializeField, Range(0, 360)] private float _viewAngle;
    [SerializeField] private float _detectionDistance;
    [SerializeField] private float _distance;
    [SerializeField] private bool _runPlayer = false;
    [SerializeField] private float _timeAfterDiscoverPlayer;
    private bool _enemyLoseSight = true;
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
        DiscoverPlayer();
        Run();
    }
    private void DiscoverPlayer()
    {
        float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if(distanceToPlayer <= _detectionDistance || IsinView())
        {
            _runPlayer = true;
            _enemyLoseSight = false;
        }
        else
        {
            if (!_enemyLoseSight)
            {
                StartCoroutine(RunToPlayer());
            }
        }
        DrawViewState();
    }
    private bool IsinView()
    {
        float realAngle = Vector3.Angle(transform.forward, _player.transform.position - transform.position);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, _player.transform.position - transform.position, out hit, _distance))
        {
            if(realAngle < _viewAngle / 2f && hit.transform == _player.transform)
            {
                return true;
            }
        }
        return false;
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
    private IEnumerator RunToPlayer()
    {
        _enemyLoseSight = true;
        yield return new WaitForSeconds(_timeAfterDiscoverPlayer);
        _runPlayer = false;
    }
    public void Stun(float timeStun)
    { 
        StopAllCoroutines();
        StartCoroutine(TimeStun(timeStun));
    }
    private IEnumerator TimeStun(float timeStun)
    {
        _agent.speed = 0;
        yield return new WaitForSeconds(timeStun);
        _agent.speed = _speed;
    }
    private void DrawViewState()
    {
        Vector3 left = transform.position + Quaternion.Euler(new Vector3(0, _viewAngle / 2f, 0)) * (transform.forward * _distance);
        Vector3 right = transform.position + Quaternion.Euler(-new Vector3(0, _viewAngle / 2f, 0)) * (transform.forward * _distance);
        Debug.DrawLine(transform.position, left, Color.yellow);
        Debug.DrawLine(transform.position, right, Color.yellow);
    }
}
