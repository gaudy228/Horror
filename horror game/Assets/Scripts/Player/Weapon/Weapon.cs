using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private float _damage;

    [Header("Ray")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField, Min(0)] private float _distance = Mathf.Infinity;
    [SerializeField, Min(0)] private float _shotCount = 1;

    [Header("Spread")]
    [SerializeField] private bool _useSpread;
    [SerializeField, Min(0)] private float _spreadFactor;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PreformAttack();
        }
    }
    private void PreformAttack()
    {
        for(var i  = 0; i < _shotCount; i++)
        {
            PreformRaycast();
        }
    }
    private void PreformRaycast()
    {
        Vector3 direction = _useSpread ? transform.forward + CalculateSpread() : transform.forward;
        Ray ray = new Ray(transform.position, direction);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _layerMask))
        {
            var hitCollider = hitInfo.collider;
            if(hitCollider.TryGetComponent(out IDamageble damageable))
            {
                damageable.ApplyDamage(_damage);
            }
        }
    }
    private Vector3 CalculateSpread()
    {
        return new Vector3
        {
            x = Random.Range(-_spreadFactor, _spreadFactor),
            y = Random.Range(-_spreadFactor, _spreadFactor),
            z = Random.Range(-_spreadFactor, _spreadFactor)
        };
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out var hitInfo, _distance, _layerMask))
        {
            DrawRay(ray, hitInfo.point, hitInfo.distance, Color.red);
        }
        else
        {
            var hitPosition = ray.origin + ray.direction * _distance;
            DrawRay(ray, hitPosition, _distance, Color.green);
        }
    }
    private static void DrawRay(Ray ray, Vector3 hitPosition, float distance, Color color)
    {
        const float hitPointRadius = 0.15f;
        Debug.DrawRay(ray.origin, ray.direction * distance, color);
        Gizmos.color = color;
        Gizmos.DrawSphere(hitPosition, hitPointRadius);
    }
#endif
}
