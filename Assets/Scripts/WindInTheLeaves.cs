using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;

public class WindInTheLeaves : MonoBehaviour {

    // Maximum turn rate in degrees per second.
    public float TurningRate = 10f;
    // Rotation we should blend towards.
    private Quaternion _targetRotation = Quaternion.identity;
    private float _jitterAmount = 1f;
    // Use this for initialization
    void Start () {
        _targetRotation = Quaternion.Euler(transform.rotation.eulerAngles);
        TimersManager.SetLoopableTimer(this, 0.1f, UpdateWind);
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, TurningRate * Time.deltaTime);
    }

    void UpdateWind()
    {
        var angles = new Vector3(
            transform.rotation.eulerAngles.x + Random.Range(-_jitterAmount, _jitterAmount), 
            transform.rotation.eulerAngles.y + Random.Range(-_jitterAmount, _jitterAmount), 
            transform.rotation.eulerAngles.z + Random.Range(-_jitterAmount, _jitterAmount));
        _targetRotation = Quaternion.Euler(angles);
    }
   

}
