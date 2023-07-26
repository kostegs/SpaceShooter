using UnityEngine;

namespace SpaceShooter
{
    public class PowerupStats : Powerup
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy,
            Invulnerability,
            IncreaseSpeed
        }

        [SerializeField] private EffectType _effectType;
        [SerializeField] private float _value;

        protected override void OnPickedUp(PlayerSpaceShip ship)
        {
            switch (_effectType)
            {
                case EffectType.AddAmmo:
                    ship.AddAmmo((int)_value);
                    break;
                case EffectType.AddEnergy:
                    ship.AddEnergy((int)_value);
                    break;
                case EffectType.Invulnerability:
                    ship.SetIndestructible((int)_value); 
                    break;
                case EffectType.IncreaseSpeed:
                    ship.IncreaseSpeed(_value);
                    break;
                  
            }            
        }
    }
}
