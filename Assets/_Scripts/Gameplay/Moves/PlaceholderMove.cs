﻿using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/Placeholder")]
    public class PlaceholderMove : Move
    {
        public override void Execute(PlayerMarker _)
        {
            Debug.Log($"{Name} move executed");
        }
    }
}