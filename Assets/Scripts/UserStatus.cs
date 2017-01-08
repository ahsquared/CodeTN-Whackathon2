using System;
using UnityEngine;
using UnityEngine.UI;

public class UserStatus : AudioEvents, KinectGestures.GestureListenerInterface
{

    [Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    private int _playerIndex;
    public string PlayerName;

    private GameManager _gameManager;
    private AvatarController _avatarController;
    // Use this for initialization
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _avatarController = gameObject.GetComponent<AvatarController>();
        _playerIndex = _avatarController.playerIndex;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UserDetected(long userId, int userIndex)
    {
        if (userIndex != _playerIndex)
            return;

        if (PlayerName != null && _gameManager != null)
        {
            _gameManager.UpdatePlayer(PlayerName, true);
        }
    }

    public void UserLost(long userId, int userIndex)
    {
        if (userIndex != _playerIndex)
            return;

        if (PlayerName != null && _gameManager != null)
        {
            _gameManager.UpdatePlayer(PlayerName, false);
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
