using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoasterSpawner : MonoBehaviour
{
    public Coaster.CoasterType type;
    public bool random;

    private List<string> randomIgnore = new List<string>(){ "Initial", "Finish", "Teleport" };

    public List<CoasterSpawner> next = new List<CoasterSpawner>();

    public Coaster coaster;

    public Coaster SpawnCoaster()
    {
        Coaster spawnable;
        string coasterObjName;
        if (random)
        {
            List<string> types = new List<string>();
            foreach(string s in Enum.GetNames(typeof(Coaster.CoasterType)))
            {
                if (!randomIgnore.Contains(s))
                {
                    types.Add(s);
                }
            }
            coasterObjName = types[UnityEngine.Random.Range(0, types.Count)];
            spawnable = Resources.Load<Coaster>($"Coasters/{coasterObjName}_Coaster");
        } else
        {
            coasterObjName = type.ToString();
            spawnable = Resources.Load<Coaster>($"Coasters/{coasterObjName}_Coaster");
        }
        Coaster spawnedCoaster = Instantiate(spawnable);
        spawnedCoaster.gameObject.name = coasterObjName;
        spawnedCoaster.transform.position = transform.position;
        spawnedCoaster.transform.parent = transform.parent;
        coaster = spawnedCoaster;
        return spawnedCoaster;
    }
}