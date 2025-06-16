using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damage;
    private BulletPool _bulletPool;

    public void Initialize(BulletPool bulletPool)
    {
        _bulletPool = bulletPool;
    }
    private RaycastHit Hit(Vector3 lastPos, Vector3 newPos)
    {
        RaycastHit hitted;
        Physics.Linecast(lastPos, newPos, out hitted, _layerMask);
        return hitted;
    }
    public IEnumerator Shoot(Vector3 startPos, Vector3 endPos, float speed)
    {
        float distance = Vector3.Distance(startPos, endPos);
        float time = distance / speed;
        float startTime = Time.time;
        float startWeight = 0f;

        while (Time.time - startTime < time)
        {
            Vector3 lastFramePosition = transform.position;
            startWeight += Time.deltaTime / time;
            transform.position = Vector3.Lerp(startPos, endPos, startWeight);
            RaycastHit newHit = Hit(lastFramePosition, transform.position);
            if(newHit.collider != null)
            {
                if(newHit.collider.TryGetComponent(out IDamageble damageable))
                {
                    damageable.TakeDamage(_damage);
                    _bulletPool.Return(this);
                }
            }
            yield return null;
        }
        _bulletPool.Return(this);
    }
}
