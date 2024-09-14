using _Scripts.Gameplay.SpotSystem;

namespace _Scripts.Gameplay.Props
{
    public class Prop : SpotObject
    {
        protected override bool ReturnToHomeSpotOnDiscard => false;

        public override void OnForceLeave()
        {
            Destroy(gameObject);
        }
    }
}