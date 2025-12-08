using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlaySoundOnGrab : MonoBehaviour
{
    public AudioSource audioSource;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Hook events
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (audioSource != null)
            audioSource.Play();
    }
}