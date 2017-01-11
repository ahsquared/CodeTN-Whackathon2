using UnityEngine;
using System.Collections;

public class Ball : AudioEvents {

    [Range(0,5)]
    public float ForceMultiplier = 1f;

    private Material _mat;
    private Rigidbody _rigidbody;
    private bool _hasBeenStruck;

    // Use this for initialization
    void Start()
    {
        iTween.FadeTo(gameObject, iTween.Hash("delay", 5f, "alpha", 1f, "time", 1f, "oncomplete", "Remove"));
    }

    private void Remove () {
        Destroy(gameObject);
    }
    
    // Update is called once per frame
    void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "Byron" ||
            collision.gameObject.name == "Smokey" || 
            collision.gameObject.name == "LeftHand" || 
            collision.gameObject.name == "RightHand" || 
            collision.gameObject.name == "LeftFoot" ||
            collision.gameObject.name == "RightFoot")
        {
            // make a sound
            PlayEvent("AcornHits");
            // if we haven't used this one yet color it and update the score
            if (_hasBeenStruck)
            {
                return;
            }
            //Debug.Log("Collided with: " + collision.gameObject.name);
            //mat.SetColor("_EmissionColor", new Color(0.964f, 0.8f, 0.262f));
            _mat = gameObject.GetComponentInChildren<MeshRenderer>().material;
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _mat.SetTexture("_EmissionMap", new Texture2D(1,1));
            _mat.SetColor("_EmissionColor", new Color(0.897f, 0.664f, 0.066f));
            DynamicGI.SetEmissive(gameObject.GetComponent<Renderer>(), new Color(0.897f, 0.664f, 0.066f) * 5);
            _rigidbody.AddForce(collision.impulse, ForceMode.Impulse);
            GameObject.Find("GameManager").GetComponent<GameManager>().UpdateScore(collision.gameObject.name);
            _hasBeenStruck = true;
        }
    }
    
}
