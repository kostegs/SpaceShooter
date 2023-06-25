using UnityEngine;

namespace SpaceShooter
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _numberOfLives;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private MovementController _movementController;

        private SpaceShip _spaceShip;

        public int NumberOfLives
        {
            get => _numberOfLives;
            set => _numberOfLives = value < 0 ? 0 : value;
        }

        private void OnSpaceShipDestruct(object sender, System.EventArgs e) => _numberOfLives--;        

        private void SubscribeToSpaceShip() => _spaceShip.OnDestruct += OnSpaceShipDestruct;

        public void SetTarget(SpaceShip ship)
        {
            _spaceShip = ship;
            SubscribeToSpaceShip();
            _cameraController.SetTarget(_spaceShip.transform);
            _movementController.SetTarget(_spaceShip);
        } 
    }
}
