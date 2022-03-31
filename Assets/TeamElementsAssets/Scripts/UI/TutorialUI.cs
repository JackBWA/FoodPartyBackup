using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [HideInInspector]
    public string title;
    [HideInInspector]
    public string description;

    public List<Sprite> images = new List<Sprite>();

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private Image imagesHolder;
    public float maxImageDuration = 5f;

    private Coroutine displayImagesCo;

    private void OnEnable()
    {
        title = MiniGame.singleton.minigameName;
        description = MiniGame.singleton.minigameDescription;
        DisplayImages();
    }

    public void DisplayImages()
    {
        displayImagesCo = StartCoroutine(DisplayCo());
    }

    public void StopDisplayingImages()
    {
        StopCoroutine(displayImagesCo);
    }

    private IEnumerator DisplayCo()
    {
        yield return null;
    }
}
