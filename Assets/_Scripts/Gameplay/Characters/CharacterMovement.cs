using System;
using _Scripts.Extentions;
using _Scripts.Gameplay.SpotSystem;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Characters
{
    public sealed class CharacterMovement : SpotObject
    {
        public int Direction
        {
            get => transform.localScale.x.Sign();
            set
            {
                if (value == 0)
                    return;
                transform.localScale = transform.localScale.WithX(value.Sign());
            }
        }

        private CharacterLife _life;
        public CharacterLife Life => _life ??= GetComponent<CharacterLife>();
        
        private void Awake()
        {
            Life.Died += LeaveCurrentSpot;
        }

        public void TurnAround()
        {
            if (Life.IsDead)
                return;
            Direction = -Direction;
        }

        public void TryGoForward()
        {
            if (Life.IsDead)
                return;
            GoToSpot(Coordinates + new Vector2Int(Direction, 0));
        }

        public void TryGoBackwards()
        {
            if (Life.IsDead)
                return;
            GoToSpot(Coordinates - new Vector2Int(Direction, 0));
        }

        public void TryAscend()
        {
            if (Life.IsDead)
                return;
            GoToSpot(Coordinates + new Vector2Int(0, 1));
        }

        public void TryDescend()
        {
            if (Life.IsDead)
                return;
            CurrentSpot.GetAdjacentSpot(new Vector2Int(0, -1))?.ForceLeave<CharacterMovement>();
            GoToSpot(Coordinates + new Vector2Int(0, -1));
        }

        public override void OnForceLeave()
        {
            Life.Kill();
        }

        public override void Restore()
        {
            base.Restore();
            Direction = 1;
        }
    }
}
