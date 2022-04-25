using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoasterTargetSelector : MonoBehaviour
{
    public CoasterTarget prefab;

    public BoardEntity interactor;

    public List<CoasterTarget> selectors = new List<CoasterTarget>();

    public Coaster result;

    private void Awake()
    {
        result = null;
    }

    public void CreateSelectors()
    {
        foreach(Coaster next in interactor.currentCoaster.next)
        {
            CoasterTarget instance = Instantiate(prefab);
            instance.selector = this;
            instance.transform.forward = next.transform.position - interactor.transform.position;
            instance.transform.position = interactor.transform.position + Vector3.up + (instance.transform.forward.normalized * 2.5f);
            instance.target = next;

            selectors.Add(instance);
        }
    }

    public void SetResult(Coaster coaster)
    {

        //result = coaster;
        // Notify.
        interactor.StartCoroutine(interactor.Move(coaster, true));
        //StartCoroutine(interactor.Move(coaster, true));
        foreach(CoasterTarget cT in selectors)
        {
            Destroy(cT.gameObject);
        }
        Destroy(gameObject);
    }
}
