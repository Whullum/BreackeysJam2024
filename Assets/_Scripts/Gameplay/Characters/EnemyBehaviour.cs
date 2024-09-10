using System;
using _Scripts.Extentions;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Characters
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyIntention[] _intentionLoop;

        private CharacterAttack _attack;
        private CharacterMovement _movement;

        private CharacterAttack Attack => _attack ??= GetComponent<CharacterAttack>();
        private CharacterMovement Movement => _movement ??= GetComponent<CharacterMovement>(); 
        
        private int DesiredDistance => Attack.CurrentWeaponType?.MaxRange ?? 1;

        private int DeltaToPlayer => Player.Movement.CurrentSpot.IndexOnMap - Movement.CurrentSpot.IndexOnMap;
        
        private int DistanceToPlayer => Mathf.Abs(DeltaToPlayer);

        private int DirectionToPlayer => DeltaToPlayer.Sign();
        
        [Inject]
        private PlayerMarker Player { get; set; }

        public void PerformTurn(int turn)
        {
            int intentionIndex = turn.RepeatIndex(_intentionLoop.Length);
            
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
        }

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
    }

    public enum EnemyIntention
    {
        Move,
        Attack
    }
}