using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class EnemyIntentionView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField]
        private EnemyBehaviour _model;
            
        private void Update()
        {
            _spriteRenderer.enabled = _model.NextTurnIntention == EnemyIntention.Attack;
        }
    }
}