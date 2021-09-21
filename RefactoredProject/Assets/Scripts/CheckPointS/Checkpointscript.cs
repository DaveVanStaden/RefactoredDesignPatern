using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpointscript : MonoBehaviour
{
    public TrackCheckpoints trackCheckpoints;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        Hide();
    }
    //when it collides with the player it will cycle towards the next checkpoint using the PlayerThroughCheckpoint function inside the main checkpoint script.
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            trackCheckpoints.PlayerThroughCheckpoint(this);
        }
    }

    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }
    public void Show()
    {
        meshRenderer.enabled = true;
    }
    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}
