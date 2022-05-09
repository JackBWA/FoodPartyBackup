using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoCanvas : MonoBehaviour
{

    public string title
    {
        get
        {
            return _title.text;
        }
        set
        {
            _title.text = value;
        }
    }
    [SerializeField]
    private TextMeshProUGUI _title;

    public string description
    {
        get
        {
            return _description.text;
        }
        set
        {
            _description.text = value;
        }
    }
    [SerializeField]
    private TextMeshProUGUI _description;

    private BoardEntity interactor;

    public event Action<BoardEntity> onOpen;
    public void Open(BoardEntity interactor)
    {
        onOpen?.Invoke(interactor);
        this.interactor = interactor;
        gameObject.SetActive(true);
    }

    public event Action<BoardEntity> onClose;
    public void Close()
    {
        onClose?.Invoke(interactor);
        Destroy(gameObject);
    }
}
