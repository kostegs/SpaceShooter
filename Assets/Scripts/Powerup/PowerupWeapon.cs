using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupWeapon : Powerup
    {
        [SerializeField] private TurretProperties _properties;

        protected override void OnPickedUp(PlayerSpaceShip ship) => ship.AssignWeapon(_properties);
    }
}

