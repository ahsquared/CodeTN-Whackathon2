using System;
using UnityEngine;
using UnityEngine.UI;

public class UserDetection : AudioEvents, KinectGestures.GestureListenerInterface
{

    [Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    public int PlayerIndex = 0;

    [Tooltip("GUI-Text to display gesture-listener messages and gesture information.")]
    public Text GestureInfo;

    public string AudioEvent;

    public void UserDetected(long userId, int userIndex)
    {
        Debug.Log("user detected: " + userId + ", index: " + userIndex);
        if (userIndex != PlayerIndex)
            return;

        //// as an example - detect these user specific gestures
        //KinectManager manager = KinectManager.Instance;
        //manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
        //manager.DetectGesture(userId, KinectGestures.Gestures.Squat);

        //manager.DetectGesture(userId, KinectGestures.Gestures.LeanLeft);
        //manager.DetectGesture(userId, KinectGestures.Gestures.LeanRight);
        //manager.DetectGesture(userId, KinectGestures.Gestures.LeanForward);
        //manager.DetectGesture(userId, KinectGestures.Gestures.LeanBack);

        ////manager.DetectGesture(userId, KinectGestures.Gestures.Run);

        if (AudioEvent != null)
        {
            PlayEvent(AudioEvent, gameObject);
        }

        if (GestureInfo != null)
        {
            GestureInfo.text = "User " + PlayerIndex + " found.";
        }
    }

    public void UserLost(long userId, int userIndex)
    {
        if (userIndex != PlayerIndex)
            return;

        if (AudioEvent != null)
        {
            StopEvent(AudioEvent, gameObject, 0);
        }

        if (GestureInfo != null)
        {
            GestureInfo.text = "User " + PlayerIndex + " lost.";
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (userIndex != PlayerIndex)
            return;

        return;
    }                    
                         
    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint, Vector3 screenPos)
    {                    
        if (userIndex != PlayerIndex)
            return false;
                         
                         
                         
        return true;     
    }                    
                         
    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint)
    {                    
        if (userIndex != PlayerIndex)
            return false;

       
        return true;
    }

}
