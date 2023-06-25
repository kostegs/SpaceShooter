using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour
{
    [SerializeField] private GameObject _uiTextPressButton;

    public event EventHandler<EventArgs> PlatformStartAnimation;

    private bool _animationStarted;

    private void OnTriggerEnter2D(Collider2D other) => _uiTextPressButton.SetActive(true);

    private void OnTriggerExit2D() => _uiTextPressButton.SetActive(false);

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_animationStarted == false)
        {
            if (Input.GetKey(KeyCode.E))
            {
                _animationStarted = true;
                _uiTextPressButton.SetActive(false);
                PlatformStartAnimation?.Invoke(this, new EventArgs());
            }
        }
    }
}
