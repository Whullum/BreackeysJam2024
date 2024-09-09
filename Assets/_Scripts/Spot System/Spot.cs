using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// We have 7 positions in an X axis (-3, -2, -1, 0 (initial position), 1, 2, 3).
/// We can have one object or person (enemy or player) in each position.
/// </summary>
public class Spot : MonoBehaviour
{
    [Inject]
    private SpotManager _spotManager;
    
    private Character currentCharacter;
    public bool IsOccupied => currentCharacter != null;

    private void Start()
    {
        _spotManager.AddSpot(this);
    }
    
    public void Occupy(Character character)
    {
        if (IsOccupied)
        {
            Debug.LogError("Spot is already occupied");
            return;
        }

        currentCharacter = character;
    }
    
    
    
    public void Leave()
    {
        currentCharacter = null;
    }
    
}
