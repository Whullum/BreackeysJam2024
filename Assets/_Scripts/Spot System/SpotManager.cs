using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SpotManager
{

    [SerializeField]
    private List<Spot> spots;
    
    private SpotManager _spotManager;
    private List<Spot> spotsList;

    [Inject]
    // Inject SpotManager through constructor injection
    public void Construct(SpotManager spotManager)
    {
        _spotManager = spotManager;
    }

    public void InstantiateCharacter(Character c)
    {
        if (spotsList == null)
        {
            Debug.LogError("No spots available");
            return;
        }
        
        if (c == null)
        {
            Debug.LogError("Character is null");
            return;
        }
        
        //Find the first available spot
        foreach (var spot in spotsList)
        {
            if (spot.IsOccupied != false) continue;
            spot.Occupy(c);
            c.SetSpot(spot);
            return;
        }
    }

    public void AddSpot(Spot spot)
    {
        spotsList ??= new List<Spot>();
        spotsList.Add(spot);
    }

    public Spot GetRandomAvailableSpot()
    {
        if (spotsList == null)
        {
            Debug.LogError("No spots available");
            return null;
        }
        
        //Find the first available spot
        return spotsList.FirstOrDefault(spot => spot.IsOccupied == false);
    }
}
