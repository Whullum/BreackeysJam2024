using System;
using _Scripts.Extentions;
using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyIntention[] _intentionLoop;

        public Action GetActionForTunr(int turn)
        {
            int intentionIndex = turn.RepeatIndex(_intentionLoop.Length);
            return GetActionForIntention(_intentionLoop[intentionIndex]);
        }

        private Action GetActionForIntention(EnemyIntention intention)
        {
            return intention switch
            {
                EnemyIntention.Move => () => Debug.Log($"{gameObject} enemy moves"),
                EnemyIntention.Attack => () => Debug.Log($"{gameObject} enemy attacks"),
                _ => throw new ArgumentOutOfRangeException(nameof(intention), intention, null)
            };
        }
    }

    public enum EnemyIntention
    {
        Move,
        Attack
    }
}