using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// We have 7 positions in an X axis (-3, -2, -1, 0 (initial position), 1, 2, 3).
/// We can have one object or person (enemy or player) in each position.
/// </summary>
public class Spot : MonoBehaviour
{
    
    private Character currentCharacter;
    public bool IsOccupied => currentCharacter != null;
}
