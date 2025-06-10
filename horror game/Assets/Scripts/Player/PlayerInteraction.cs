using System.Collections;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _interactObject;
    private void Awake()
    {
        _camera = Camera.main;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Interaction();
        }
    }
    private void Interaction()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _distance, _interactObject))
        {
            if (hit.collider.TryGetComponent<IInteract>(out IInteract interact))
            {
               interact.Interact();
            }
        }
    }
}
