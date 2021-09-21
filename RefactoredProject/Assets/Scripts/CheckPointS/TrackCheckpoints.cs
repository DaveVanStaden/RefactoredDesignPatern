using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TrackCheckpoints : MonoBehaviour
{
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;

    private List<Checkpointscript> checkpointList;
    private int nextCheckpointIndex;
    private bool NextLap;
    public bool Victory;
    private int laps = 0;
    public Text lapText;
    [SerializeField] private GameObject VictoryReset;
    private void Awake()
    {
        //find all checkpoints in the game, put them inside a list so you can check what checkpoint would be next using an integer.
        Transform checkPointsTransform = transform.Find("Checkpoints");

        checkpointList = new List<Checkpointscript>();
        foreach(Transform checkpointTransform in checkPointsTransform)
        {
            Checkpointscript checkpoint = checkpointTransform.GetComponent<Checkpointscript>();
            checkpoint.SetTrackCheckpoints(this);
            checkpointList.Add(checkpoint);
        }
        nextCheckpointIndex = 0;
    }
    private void Update()
    {
        //checks the lapcount to identify if the race is over and prints this to a text object.
        lapText.text = "Lapcount: " + laps;
        if(NextLap)
        {
            if(nextCheckpointIndex == 1)
            {
                laps += 1;
                NextLap = false;
            }
        }
        if(laps == 4)
        {
            VictoryReset.SetActive(true);
            Victory = true;
            laps = 3;
        }
    }
    public void PlayerThroughCheckpoint(Checkpointscript checkpoint)
    {
        //gets the index of the checkpoint that is currently active inside the list, if they equal,
        //it will hide the object incase the wrong checkpoint was encountered first, will increase an integer that counts what checkpoint you are on.
        //Will call an event that will dissable the previous checkpoint if it was wrong. 
        if (checkpointList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            Checkpointscript correctCheckpoint = checkpointList[nextCheckpointIndex];
            correctCheckpoint.Hide();

            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointList.Count;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
            NextLap = true;
        }else{
            //If its the wrong checkpoint, it will invoke an event that will show the correct one.
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
            Checkpointscript correctCheckpoint = checkpointList[nextCheckpointIndex];
            correctCheckpoint.Show();
        }
    }
}
