using UnityEngine;
using System.Collections;

public class PlayerStateTracker : MonoBehaviour {


    public UnityEngine.UI.Image KeyImage;
	// Use this for initialization
	void Start () {
        KeyImage = transform.FindChild("Canvas").FindChild("Key").GetComponent<UnityEngine.UI.Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (KeyImage != null)
        {
            KeyImage = transform.FindChild("Canvas").FindChild("Key").GetComponent<UnityEngine.UI.Image>();
        }
	}

    void SetInactive()
    {
        KeyImage.gameObject.SetActive(true);
    }
    void SetActive()
    {
        KeyImage.gameObject.SetActive(false);
    }
}
