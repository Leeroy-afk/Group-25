using UnityEngine;

public class EnemyEncounterManager : MonoBehaviour
{
   private EnemyEncounterZone activeEncounter;

    public bool TryActivateEncounter(EnemyEncounterZone encounter)
    {
        if(activeEncounter != null && activeEncounter != encounter) 
            return false;

        activeEncounter = encounter;
        return true;

    }

    public void ReleaseEncounter(EnemyEncounterZone encounter)
    {
        if (activeEncounter == encounter)
        {
            activeEncounter = null;
        }
    }

    public bool IsEncounterActive()
    {
        return activeEncounter != null;
    }
}
