using System;
using _Scripts.Extentions;
using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyIntention[] _intentionLoop;

        public void PerformTurn(int turn)
        {
            int intentionIndex = turn.RepeatIndex(_intentionLoop.Length);
            
            switch (_intentionLoop[intentionIndex])
            {
                case EnemyIntention.Move:
                    Debug.Log($"{gameObject} enemy moves");
                    break;
                
                case EnemyIntention.Attack:
                    Debug.Log($"{gameObject} enemy attacks");
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum EnemyIntention
    {
        Move,
        Attack
    }
}