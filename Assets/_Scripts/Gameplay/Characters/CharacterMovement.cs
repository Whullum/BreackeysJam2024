using _Scripts.Extentions;
using UnityEngine;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        public int Direction => transform.localScale.x.Sign();
        
        public void MoveForward()
        {
            transform.position += new Vector3(Direction * Grid.GridSize, 0, 0);
        }

        public void TurnAround()
        {
            transform.localScale = transform.localScale.WithX(transform.localScale.x * -1);
        }
    }
}