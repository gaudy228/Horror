using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damage;
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
                if(newHit.collider.TryGetComponent<IDamageble>(out IDamageble damageable))
                {
                    damageable.TakeDamage(_damage, newHit.point, newHit.normal);
                    StopAllCoroutines();
                    Destroy(gameObject, 0.25f);
                }
            }
            yield return null;
        }
        StopAllCoroutines();
        Destroy(gameObject, 0.25f);
    }
}
