using UnityEngine;

public class LightTracker : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacles;
    [SerializeField] private bool _lightF = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            Vector3 Player = transform.position;
            Vector3 Light = other.transform.position;
            bool ObstaclesBeforLight = Physics.Linecast(Player, Light, _obstacles.value);
            if (!ObstaclesBeforLight)
            {
                _lightF = true;
            }
            else
            {
                _lightF = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Light"))
        {
            _lightF = false;
        }
    }
}
