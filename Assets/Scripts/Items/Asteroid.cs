
namespace SpaceShooter
{
    public class Asteroid : Destructible
    {
        private protected override void OnDeath() => ObjectDestroyer.Instance.DestroyGameObject(gameObject, 1.5f, DestroyEffect, _spriteRenderer, base.OnDeath);
    }
}
