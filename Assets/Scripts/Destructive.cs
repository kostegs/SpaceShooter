using UnityEngine;

public class Destructive : MonoBehaviour
{
    [SerializeField] private DestructiveProperties _destructiveProperties;

    public float DamagePoints => _destructiveProperties.DamagePoints;
}
