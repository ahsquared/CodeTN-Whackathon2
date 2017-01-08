using UnityEngine;
using System.Collections;

public class Ball : AudioEvents {

    [Range(0,5)]
    public float ForceMultiplier = 1f;

    // Use this for initialization
    void Start()
    {
        float size = Random.Range(0.4f, 1f);

        iTween.FadeTo(gameObject, iTween.Hash("delay", 5f, "alpha", 1f, "time", 1f, "oncomplete", "Remove"));
        //iTween.ScaleTo(gameObject, iTween.Hash("delay", 5f, "scale", new Vector3(0, 0, 0), "time", 4f, "oncomplete", "Remove", "easeType", "easeInOutQuad"));

        //gameObject.GetComponent<Transform>().localScale = new Vector3(size, size, size);
        //gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //StartCoroutine(LaunchBall(1.5f));
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
            //Debug.Log("Collided with: " + collision.gameObject.name);
            //gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            gameObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0.964f, 0.8f, 0.262f));
            gameObject.GetComponent<Rigidbody>().AddForce(collision.impulse, ForceMode.Impulse);
            GameObject.Find("GameManager").GetComponent<GameManager>().UpdateScore(collision.gameObject.name);
            PlayEvent("AcornHits");
        }
    }

    private IEnumerator LaunchBall(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f) * ForceMultiplier, 0, Random.Range(-1f, 1f) * ForceMultiplier), ForceMode.Impulse);

    }
}
