using UnityEngine;

namespace SpaceShooter
{
    public class Player : MonoSingleton<Player>
    {
        [SerializeField] private int _numberOfLives;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private MovementController _movementController;

        private SpaceShip _spaceShip;

        public SpaceShip ActiveShip => _spaceShip;

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

        #region Score

        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill() => NumKills++;

        public void AddScore(int num) => Score += num;

        #endregion
    }
}
