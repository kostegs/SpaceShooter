using System.Diagnostics;

namespace SpaceShooter
{
    public class Timer
    {
        private float _currentTime;
        private float _startTime = 0;

        public float CurrentTime => _currentTime;

        public bool IsFinished => _currentTime <= 0;

        public void Start(float startTime)
        {
            _startTime = startTime;
            _currentTime = startTime;
        }

        public void Restart() => Start(_startTime);

        public void SubstractTime(float deltaTime)
        {
            if (_currentTime <= 0) 
                return;

            _currentTime -= deltaTime;
        }
    }
}
