using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BreedingManager : Singleton<BreedingManager>
{
    List<BreedingCouple> _couples = new();

    public void AddCouple(AnimalStateMachine parentA, AnimalStateMachine parentB)
    {
        _couples.Add(new BreedingCouple() { ParentA = parentA, ParentB = parentB });
    }

    public void DestroyCouple(AnimalStateMachine parentA, AnimalStateMachine parentB)
    {
        _couples.RemoveAll(x => x.ParentA.ID == parentA.ID && x.ParentB.ID == parentB.ID);
    }

    public void DestroyCouple(AnimalStateMachine parent)
    {
        _couples.RemoveAll(x => x.ParentA.ID == parent.ID || x.ParentB.ID == parent.ID);
    }

    public void DestroyCouple(BreedingCouple couple)
    {
        _couples.Remove(couple);
    }

    public void ClearCouples()
    {
        _couples.Clear();
    }

    public bool IsInAnyCouple(Guid id) => _couples.Any(x => x.ParentA.ID == id || x.ParentB.ID == id);

    public AnimalStateMachine GetOtherParentASM(Guid id)
    {
        var couple = _couples.FirstOrDefault(x => x.ParentA.ID == id || x.ParentB.ID == id);

        if (couple.ParentA.ID == id)
            return couple.ParentB;

        return couple.ParentA;
    }

    public void ParentReachedDestination(Guid id)
    {
        var couple = _couples.FirstOrDefault(x => x.ParentA.ID == id || x.ParentB.ID == id);

        if (couple.ParentA.ID == id)
            couple.ParentAReached = true;
        else 
            couple.ParentBReached = true;

        if (couple.ParentAReached && couple.ParentBReached)
        {
            GameEvent<AnimalTypes, Vector3>.Call(Event.SpawnAnimal, 
                couple.ParentA.AnimalType, couple.ParentA.transform.position);

            couple.ParentA.BirthedChild();
            couple.ParentB.BirthedChild();

            DestroyCouple(couple);
        }
    }

    public bool RequireReachUpdate(Guid id)
    {
        var couple = _couples.FirstOrDefault(x => x.ParentA.ID == id || x.ParentB.ID == id);

        return couple.ParentA.ID == id ? !couple.ParentAReached : !couple.ParentBReached;
    }
}

public class BreedingCouple
{
    public AnimalStateMachine ParentA { get; set; }
    public AnimalStateMachine ParentB { get; set; }

    public bool ParentAReached { get; set; } = false;
    public bool ParentBReached { get; set; } = false;
}
