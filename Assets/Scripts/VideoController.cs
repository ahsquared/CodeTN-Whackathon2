using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    private MovieTexture _movie;

	// Use this for initialization
	void Start ()
	{
	    _movie = gameObject.GetComponent<Image>().mainTexture as MovieTexture;
        _movie.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
