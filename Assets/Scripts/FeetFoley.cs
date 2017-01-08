using System;
using UnityEngine;
using UnityEngine.UI;

public class FeetFoley : AudioEvents, KinectGestures.GestureListenerInterface
{

    [Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    private int _playerIndex;

    [Tooltip("GUI-Text to display gesture-listener messages and gesture information.")]
    public Text GestureInfo;

    public string AudioEvent;
    private AvatarController _avatarController;
    // Use this for initialization
    void Start()
    {
        _avatarController = gameObject.GetComponent<AvatarController>();
        _playerIndex = _avatarController.playerIndex;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UserDetected(long userId, int userIndex)
    {
        Debug.Log("user detected: " + userId + ", index: " + userIndex);
        if (userIndex != _playerIndex)
            return;

        if (AudioEvent != null)
        {
            PlayEvent(AudioEvent, gameObject);
        }

        if (GestureInfo != null)
        {
            GestureInfo.text = "User " + _playerIndex + " found.";
        }
       
    }

    public void UserLost(long userId, int userIndex)
    {
        if (userIndex != _playerIndex)
            return;

        if (AudioEvent != null)
        {
            StopEvent(AudioEvent, gameObject, 0);
        }

        if (GestureInfo != null)
        {
            GestureInfo.text = "User " + _playerIndex + " lost.";
        }
       
    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (userIndex != _playerIndex)
            return;

        return;
    }

    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (userIndex != _playerIndex)
            return false;



        return true;
    }

    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint)
    {
        if (userIndex != _playerIndex)
            return false;


        return true;
    }

}
