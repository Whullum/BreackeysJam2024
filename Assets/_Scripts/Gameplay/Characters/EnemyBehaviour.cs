using System;
using _Scripts.Extentions;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Characters
{
    public class EnemyBehaviour : MonoBehaviour, IRestorable
    {
        [SerializeField] private EnemyIntention[] _intentionLoop;

        private CharacterAttack _attack;
        private CharacterMovement _movement;
        private CharacterLife _life;
        
        private CharacterAttack Attack => _attack ??= GetComponent<CharacterAttack>();
        private CharacterMovement Movement => _movement ??= GetComponent<CharacterMovement>(); 
        private CharacterLife Life => _life ??= GetComponent<CharacterLife>(); 
        
        private int DesiredDistance => Attack.CurrentWeaponType?.MaxRange ?? 1;

        private int DeltaToPlayer => Player.Movement.CurrentSpot.Coordinates.x - Movement.CurrentSpot.Coordinates.x;
        
        private int DistanceToPlayer => Mathf.Abs(DeltaToPlayer);

        private int DirectionToPlayer => DeltaToPlayer.Sign();

        public int Turn { get; private set; }

        public EnemyIntention NextTurnIntention => _intentionLoop[Turn.RepeatIndex(_intentionLoop.Length)];
        
        [Inject]
        private PlayerMarker Player { get; set; }

        public void PerformNextTurn()
        {
            if (Life.IsDead)
                return;
            
            if (Movement.IsInAir)
            {
                Movement.TryDescend();
                Turn++;
                return;
            }
            
            int intentionIndex = Turn.RepeatIndex(_intentionLoop.Length);
            
            switch (_intentionLoop[intentionIndex])
            {
                case EnemyIntention.Move:
                    PerformMove();
                    break;
                
                case EnemyIntention.Attack:
                    PerformAttack();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Turn++;
        }

        public void ApplyLoop(EnemyIntention[] loop) => _intentionLoop = loop;

        private void PerformMove()
        {
            Movement.Direction = DirectionToPlayer;

            int desiredDelta = (DistanceToPlayer - DesiredDistance).Sign();
            
            if (desiredDelta == 1)
                Movement.TryGoForward();
            else if (desiredDelta == -1)
                Movement.TryGoBackwards();
        }

        private void PerformAttack()
        {
            if (DeltaToPlayer > DesiredDistance)
                return;
            
            if (Attack.CurrentWeaponType != null)
                Attack.UseWeapon();
            else
                Attack.Punch();
        }

        public void Restore()
        {
            Turn = 0;
        }
    }

    public enum EnemyIntention
    {
        Move,
        Attack
    }
}