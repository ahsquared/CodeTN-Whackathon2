using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetFoley : AudioEvents
{


    private int _usersSaved = 0;
	// Use this for initialization
	void Start ()
	{

    }
	
	// Update is called once per frame
	void Update ()
	{
        KinectManager manager = KinectManager.Instance;
        if (manager && manager.IsInitialized())
        {
            int usersNow = manager.GetUsersCount();

            if (usersNow > _usersSaved)
            {
                PlayEvent("Play_Feet");
            }
            if (usersNow < _usersSaved)
            {
                StopEvent("Play_Feet", 0);
            }

            _usersSaved = usersNow;
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("foot collided with: " + collision.gameObject.name);
    //    if (collision.gameObject.name == "FLOORPLANE")
    //    {
    //        Debug.Log("Collided with: " + collision.gameObject.name);
    //        PlayEvent("Play_Feet");
    //    }
    //}
}
