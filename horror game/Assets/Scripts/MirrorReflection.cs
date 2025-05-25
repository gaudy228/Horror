using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    [SerializeField] private int _reflections;
    [SerializeField] private float _maxLength;
    private LineRenderer _lineRenderer;
    private Ray _ray;
    private RaycastHit _hit;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        _ray = new Ray(transform.position, transform.forward);
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);
        float remainingLength = _maxLength;
        for (int i = 0; i < _reflections; i++)
        {
            if (Physics.Raycast(_ray.origin, _ray.direction, out _hit, remainingLength))
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _hit.point);
                remainingLength -= Vector3.Distance(_ray.origin, _hit.point);
                _ray = new Ray(_hit.point, Vector3.Reflect(_ray.direction, _hit.normal));
                if (_hit.collider.tag != "Mirror")
                    break;
            }
            else
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _ray.origin + _ray.direction * remainingLength);
            }
        }
    }
}
