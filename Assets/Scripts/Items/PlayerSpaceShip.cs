using System;
using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerSpaceShip : SpaceShip
    {
        [Header("UI")]
        [SerializeField] private UIArrow _UIArrow;
        [SerializeField] private GameObject _UIShields;
        [SerializeField] private GameObject _UIInvulnerabilityPanel;
        [SerializeField] private GameObject _UISpeedPanel;
        [SerializeField] private GameObject _speedometerIcon;
        [SerializeField] private GameObject _hud;

        /// <summary>
        /// Coroutines
        /// </summary>
        private IEnumerator _invulnerabilityCoroutine;
        private IEnumerator _speedChangingCoroutine;

        // Events
        public event EventHandler<TimerEventArgs> TimerInvulnerabilityChanged;
        public event EventHandler<TimerEventArgs> TimerSpeedChanged;
        public event EventHandler<EventArgs> OnEnergyChanged;
        public event EventHandler<EventArgs> OnAmmoChanged;


        #region PublicFields
        public override float PrimaryEnergy
        {
            get => _primaryEnergy;
            set
            {
                _primaryEnergy = value;
                OnEnergyChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion

        public override int SecondaryAmmo
        {
            get => _secondaryAmmo;
            set
            {
                _secondaryAmmo = value;
                OnAmmoChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void TuneHud(Transform target, Camera mainCamera) => _UIArrow?.TuneHud(target, mainCamera);

        public void HudVisibility(bool visibility) => _hud.SetActive(visibility);

        public void AddEnergy(int energy) => PrimaryEnergy = Mathf.Clamp(PrimaryEnergy + energy, 0, _maxEnergy);

        public void AddAmmo(int ammo) => SecondaryAmmo = Mathf.Clamp(SecondaryAmmo + ammo, 0, _maxAmmo);

        public void SetIndestructible(int interval)
        {
            if (_invulnerabilityCoroutine == null)
            {
                _invulnerabilityCoroutine = ShipInvulnerability(interval);
                StartCoroutine(_invulnerabilityCoroutine);
            }
            else
            {
                StopCoroutine(_invulnerabilityCoroutine);
                _invulnerabilityCoroutine = ShipInvulnerability(interval);
                StartCoroutine(_invulnerabilityCoroutine);
            }
        }

        public void SetIndestructibleState(bool state)
        {
            _UIInvulnerabilityPanel.SetActive(state);
            _UIShields.SetActive(state);
            _indestructible = state;
        }

        IEnumerator ShipInvulnerability(int interval)
        {
            SetIndestructibleState(true);
            yield return new WaitForSeconds(0.3f);
            TimerInvulnerabilityChanged?.Invoke(this, new TimerEventArgs(interval));

            int invTimer = interval;

            while (invTimer > 0)
            {
                TimerInvulnerabilityChanged?.Invoke(this, new TimerEventArgs(invTimer));
                invTimer--;
                yield return new WaitForSeconds(1);
            }

            SetIndestructibleState(false);
        }

        public void IncreaseSpeed(float speed)
        {
            if (_speedChangingCoroutine == null)
            {
                _speedChangingCoroutine = SpeedChanging(speed, 10);
                StartCoroutine(_speedChangingCoroutine);
            }
            else
            {
                StopCoroutine(_speedChangingCoroutine);
                _speedChangingCoroutine = SpeedChanging(speed, 10);
                StartCoroutine(_speedChangingCoroutine);
            }
        }

        IEnumerator SpeedChanging(float speedCoef, int timer)
        {
            _UISpeedPanel.SetActive(true);
            _speedometerIcon.SetActive(true);
            float savedThrust = _thrust;
            int invTimer = timer;

            _thrust += speedCoef;

            while (invTimer > 0)
            {
                TimerSpeedChanged?.Invoke(this, new TimerEventArgs(invTimer));
                invTimer--;
                yield return new WaitForSeconds(1);
            }

            _thrust = savedThrust;
            _UISpeedPanel.SetActive(false);
            _speedometerIcon.SetActive(false);
        }
    }
}
