using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float _timeToDestruct;
    [SerializeField] private float _damage;
    [SerializeField] private float _startPointOfDamageReduction;
    [SerializeField] private float _finalDamageInPrecent;
    [SerializeField] AnimationCurve _damageReductionGraph;
    [SerializeField] private float _startSpeed;
    [SerializeField] private GameObject _particleHit;
    private Rigidbody _rb;
    private Vector3 _previousStep;
    private float _startTime;
    private float _currentDamage;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.TransformDirection(Vector3.forward * _startSpeed);

        _previousStep = transform.position;

        _startTime = Time.time;
        _currentDamage = _damage;
    }
    private void Start()
    {
        Destroy(gameObject, _timeToDestruct);
    }
    private void FixedUpdate()
    {
        Quaternion currentStep = transform.rotation;
        transform.LookAt(_previousStep, transform.up);
        RaycastHit hit = new RaycastHit();
        float distance = Vector3.Distance(_previousStep, transform.position);
        if(Physics.Raycast(_previousStep, transform.TransformDirection(Vector3.back), out hit, distance) && (hit.transform.gameObject != gameObject))
        {
            if (hit.collider.TryGetComponent(out IDamageble damageable))
            {
                damageable.TakeDamage(_damage /** GetDamageCoefficient()*/);
                //Debug.Log(hit.collider.name);
                Destroy(gameObject);
            }
            //Debug.Log(hit.collider.name);
            Destroy(gameObject);
        }
        gameObject.transform.rotation = currentStep;
        _previousStep = gameObject.transform.position;
    }
    private float GetDamageCoefficient()
    {
        float value = 1f;
        float currentTime = Time.time - _startTime;
        value = _damageReductionGraph.Evaluate(currentTime / _timeToDestruct);
        return value;
    }
}
