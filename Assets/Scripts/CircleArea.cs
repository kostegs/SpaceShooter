using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CircleArea : MonoBehaviour
{
    [SerializeField] private float _radius;

    public float Radius => _radius;

    public Vector2 GetRandomInsideZone() => (Vector2) transform.position + (Vector2) Random.insideUnitSphere * _radius;


#if UNITY_EDITOR
    private static Color _gizmoColor = new Color(0, 1, 0, 0.3f);

    private void OnDrawGizmosSelected()
    {
        Handles.color = _gizmoColor;
        Handles.DrawSolidDisc(transform.position, transform.forward, _radius);
    }
#endif

}
