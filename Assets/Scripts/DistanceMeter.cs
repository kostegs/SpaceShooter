using UnityEngine;

public class DistanceMeter : MonoBehaviour
{
    [SerializeField] private Transform _firstObject;
    [SerializeField] private Transform _secondObject;

    public float GetDistance()
    {
        Vector2 vect = _secondObject.position - _firstObject.position;
        return vect.magnitude;
    }
}
