using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NickBillboard : MonoBehaviour
{

    public TextMeshProUGUI nickField;

    public void SetNick(string nick)
    {
        nickField.text = nick;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
