using System.Diagnostics;

namespace SpaceShooter
{
    public class Timer
    {
        private float _currentTime;
        public float CurrentTime => _currentTime;

        public bool IsFinished => _currentTime <= 0;

        public Timer(float startTime) => Start(startTime);
        
        public void Start(float startTime) => _currentTime = startTime;

        public void SubstractTime(float deltaTime)
        {
            if (_currentTime <= 0) 
                return;

            _currentTime -= deltaTime;
        }
    }
}
