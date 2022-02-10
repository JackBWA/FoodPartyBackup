using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureScriptTest : MonoBehaviour
{

    public GameObject gObj;

    private void Start()
    {
        if(gObj != null)
        {
            StartCoroutine(Move());
        }
    }

    public IEnumerator Move()
    {
        while(Vector3.Distance(transform.position, gObj.transform.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, gObj.transform.position, 0.05f);
            yield return new WaitForSeconds(0.1f);
        }
        transform.position = gObj.transform.position;
    }
}
