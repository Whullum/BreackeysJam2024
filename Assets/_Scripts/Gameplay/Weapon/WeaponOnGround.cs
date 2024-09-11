namespace _Scripts.Gameplay.Weapon
{
    public class WeaponOnGround : SpotObject
    {
        public WeaponType Type { get; private set; }

        public void Init(WeaponType type)
        {
            if (Type != null)
                return;
            Type = type;
        }

        public void PickUp()
        {
            Destroy(gameObject);
            LeaveCurrentSpot();
        }

        public override void OnForceLeave()
        {
            Destroy(gameObject);
        }
    }
}