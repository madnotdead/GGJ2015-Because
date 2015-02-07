using UnityEngine;
using System.Collections;

public class PlayerObjectiveTracker : MonoBehaviour {


    public UnityEngine.UI.Image TimerImage;
	// Use this for initialization
	void Start () {
        TimerImage = transform.FindChild("Canvas").FindChild("Timer").GetComponent<UnityEngine.UI.Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (TimerImage != null)
        {
            TimerImage = transform.FindChild("Canvas").FindChild("Timer").GetComponent<UnityEngine.UI.Image>();
        }
	}

    void UpdateObjective()
    {
        TimerImage.gameObject.SetActive(true);
        var objective = ObjectiveManager.instance.GetCurrenteObjective;
        TimerImage.fillAmount = ObjectiveManager.instance.currentTime / objective.time;
        Debug.Log("UpdateObjective" + TimerImage.enabled.ToString(), TimerImage);
    }
    void ObjectiveCompleted()
    {
        TimerImage.gameObject.SetActive(false);
        Debug.Log("ObjectiveCompleted" + TimerImage.enabled.ToString(), TimerImage);

    }

    void SetInactive()
    {
        TimerImage.gameObject.SetActive(false);
        Debug.Log("SetInactive" + TimerImage.enabled.ToString(), TimerImage);
    }
}
