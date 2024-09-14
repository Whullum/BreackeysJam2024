using UnityEngine;

namespace _Scripts.Gameplay.Props
{
    public class ElectricPole : Prop
    {
        [SerializeField]
        private int _startingDurability;

        public int Durability { get; private set; }

        public bool IsBroken => Durability == 0;

        private void Awake()
        {
            RestoreDurability();
        }

        public void TakeHit()
        {
            Debug.Log("Hit");
            Durability--;
            Durability = Mathf.Max(Durability, 0);
            Debug.Log(Durability);
        }

        public override void Discard()
        {
            base.Discard();
            RestoreDurability();
        }

        private void RestoreDurability()
        {
            Durability = _startingDurability;
        }
    }
}