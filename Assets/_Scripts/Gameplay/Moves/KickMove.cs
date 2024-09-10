﻿using _Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Scripts.Gameplay.Moves
{
    [CreateAssetMenu(menuName = "Moves/Kick")]
    public class KickMove : Move
    {
        public override void Execute(PlayerMarker player)
        {
            player.Strikes.Kick();
        }
    }
}