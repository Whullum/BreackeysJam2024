﻿using _Scripts.Gameplay.SpotSystem;

namespace _Scripts.Gameplay.Props
{
    public class Prop : SpotObject
    {
        public override void OnForceLeave()
        {
            Destroy(gameObject);
        }
    }
}