using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageble
{
    [Header("Enemy View")]

    [SerializeField, Range(0, 360)] private float _viewAngle;
    [SerializeField] private float _detectionDistance;
    [SerializeField] private float _distance;
    [SerializeField] private float _timeAfterDiscoverPlayer;
    [SerializeField] private float _stopDistance;
    [SerializeField] private Vector3[] _patrolPoints;
    private Vector3 _targetPoint;
    private bool _runPlayer = false;
    private bool _enemyLoseSight = true;
    
    [Header("Enemy Attack")]
    
    [SerializeField] private float _damage;
    [SerializeField] private float _rangeAttack;
    [SerializeField] private Transform _pointAttack;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _timeBetweenAttack;
    private bool _canAttak = true;

    [Header("Enemy Other")]

    [SerializeField] private float _hp;
    [SerializeField] private float _speed;
    private bool _isStun;
    private NavMeshAgent _agent;
    private GameObject _player;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        _agent.speed = _speed;
        _agent.stoppingDistance = 0;
        _targetPoint = _patrolPoints[UnityEngine.Random.Range(0, _patrolPoints.Length)];
    }
    private void Update()
    {
        DiscoverPlayer();
        Run();
        if(Vector3.Distance(_player.transform.position, transform.position) <= _stopDistance && _runPlayer && !_isStun)
        {
            Attack();
        }
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
            _agent.stoppingDistance = _stopDistance;
        }
        else
        {
            WalkToNewPoint();
            _agent.stoppingDistance = 0;
        }
    }
    private void WalkToNewPoint()
    {
        if(transform.position == _targetPoint)
        {
            _targetPoint = _patrolPoints[UnityEngine.Random.Range(0, _patrolPoints.Length)];
        }
        _agent.SetDestination(_targetPoint);
    }
    private void Attack()
    {
        if (_canAttak)
        {
            Collider[] _enemy = Physics.OverlapSphere(_pointAttack.position, _rangeAttack, _enemyLayer);
            for (int i = 0; i < _enemy.Length; i++)
            {
                if (_enemy[i].TryGetComponent(out PlayerHP player))
                {
                    player.TakeDamage(_damage / 2);
                }
            }
            StartCoroutine(ReloadAttack());
        }
    }
    private IEnumerator ReloadAttack()
    {
        _canAttak = false;
        yield return new WaitForSeconds(_timeBetweenAttack);
        _canAttak = true;
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
        _isStun = true;
        yield return new WaitForSeconds(timeStun);
        _agent.speed = _speed;
        _isStun = false;
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(_pointAttack.position, _rangeAttack);
    //    DrawViewState();
    //}
    private void DrawViewState()
    {
        Vector3 left = transform.position + Quaternion.Euler(new Vector3(0, _viewAngle / 2f, 0)) * (transform.forward * _distance);
        Vector3 right = transform.position + Quaternion.Euler(-new Vector3(0, _viewAngle / 2f, 0)) * (transform.forward * _distance);
        Debug.DrawLine(transform.position, left, Color.yellow);
        Debug.DrawLine(transform.position, right, Color.yellow);
    }
}
