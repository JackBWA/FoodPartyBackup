using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RequestManager : MonoBehaviour
{

    public string title
    {
        get
        {
            return _titleText.text;
        }
        set
        {
            _titleText.text = value;
        }
    }

    public string message
    {
        get
        {
            return _messageText.text;
        }
        set
        {
            _messageText.text = value;
        }
    }

    public string acceptButtonText
    {
        get
        {
            return _acceptButtonText.text;
        }
        set
        {
            _acceptButtonText.text = value;
        }
    }

    public string declineButtonText
    {
        get
        {
            return _declineButtonText.text;
        }
        set
        {
            _declineButtonText.text = value;
        }
    }

    [SerializeField]
    private TextMeshProUGUI _titleText;

    [SerializeField]
    private TextMeshProUGUI _messageText;

    [SerializeField]
    private Button _acceptButton;

    [SerializeField]
    private Button _declineButton;

    [SerializeField]
    private TextMeshProUGUI _acceptButtonText;

    [SerializeField]
    private TextMeshProUGUI _declineButtonText;

    public bool hasSubmittedRequest { get; private set; }

    public bool requestResult { get; private set; }

    private void Awake()
    {
        hasSubmittedRequest = false;
        _acceptButton.onClick.AddListener(delegate { SetRequestResult(true); });
        _declineButton.onClick.AddListener(delegate { SetRequestResult(false); });
    }

    private void SetRequestResult(bool value)
    {
        requestResult = value;
        hasSubmittedRequest = true;
    }
}
