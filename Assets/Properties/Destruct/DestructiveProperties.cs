using UnityEngine;

[CreateAssetMenu]
public class DestructiveProperties : ScriptableObject
{
    [SerializeField] private float _damagePoints;

    public float DamagePoints => _damagePoints;
}
