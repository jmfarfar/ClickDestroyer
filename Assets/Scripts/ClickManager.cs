using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ClickManager : MonoBehaviour
{
    public Action OnClick;

    public TextMeshProUGUI score;

    private int clickCounter;
    private Vector3 scalebkp;
    private GameManager gameManager;
    public ParticleSystem emiter;

    private void Awake()
    {
        OnClick += Click;
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clickCounter = 0;
        scalebkp = transform.localScale;
    }

    private void OnMouseDown()
    {
        OnClick.Invoke();
        transform.localScale = scalebkp - new Vector3(1f, 0f, 1f);
        emiter.Play();
        // looking for a way to interpolate the scaling
        // transform.localScale.x = Mathf.Lerp(scalebkp.x, scalebkp.x - 1f, 0.1f);
        // transform.localScale.z = Mathf.Lerp(scalebkp.z, scalebkp.z - 1f, 0.1f);
    }

    private void OnMouseUp()
    {
        transform.localScale = scalebkp;
    }

    public void Click()
    {
        clickCounter++;
        score.text = $"{clickCounter}";
    }

    private void OnDestroy()
    {
        OnClick -= Click;
    }
}