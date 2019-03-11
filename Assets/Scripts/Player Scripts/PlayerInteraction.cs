using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
	public GameObject heldItemLocation;
    public GameObject progressBar;
    public Image progressBarImage;
    public Text progressBarText;
    public float interactionTime;

    public AudioSource audioSource;

    public delegate void InputDelegate();
	public ActionList useActionList;
	public ActionList dropActionList;
    //public event InputDelegate useEvent = delegate { };
    //public event InputDelegate dropEvent = delegate { };

    public event InputDelegate startChannelEvent = delegate { };
    public event InputDelegate stopChannelEvent = delegate { };

	public delegate void ItemChangeDelegate(GameObject g);
	public event ItemChangeDelegate itemPickupEvent = delegate { };
	public event ItemChangeDelegate itemDropEvent = delegate { };
	public event ItemChangeDelegate ingredientEatEvent = delegate { };

    private GameObject progressBarLocation;
    private GameObject heldItem;
    private HoldableItem heldItemInteraction;
    private bool performingAction;

    void Start()
    {
        performingAction = false;
        if (progressBarLocation == null && (progressBarLocation = transform.GetChild(0).gameObject) == null)
        {
            Debug.Log("Error progressBar couldn't be found");
        }
		useActionList = new ActionList();
		dropActionList = new ActionList();
    }

    void Update()
    {
        if (heldItem != null)
        {
            UpdateHoldablePosition();
            if (Input.GetButtonDown("Use"))
            {
                if (heldItemInteraction.Use(gameObject))
                {
                    audioSource.clip = AudioManager.instance.munchAudio;
                    audioSource.Play();
					ingredientEatEvent(heldItem);
                    ClearFields();
                }
            }
            else if (Input.GetButtonDown("Drop"))
            {
                heldItemInteraction.Drop(gameObject);
				itemDropEvent(heldItem);
                ClearFields();
            }
            return;
        }

        if (performingAction)
        {
            return;
        }
        if (Input.GetButtonDown("Use"))
        {
            StartCoroutine(PerformAction("Use", useActionList));
        }
        if (Input.GetButtonDown("Drop"))
        {
            StartCoroutine(PerformAction("Drop", dropActionList));
        }
    }

    IEnumerator PerformAction(string button, ActionList al)
    {
		bool? performable = al.GetAction()?.IsPerformable?.Invoke();//return true/false if the list isn't empty and the delegate is set, returns null otherwise
        if ( (performable.HasValue) ? !performable.Value : true) 	//return true/false if performable isn't null, return true if it is
        {															// 	if(al.GetAction() == null || al.GetAction().IsPerformable == null) 
            yield break;											// 	{	
        }															// 		performable = true;
        performingAction = true;									//	}
        float timer = 0f;											// 	else 
        progressBar.SetActive(true);								// 	{
        UpdateText(button);											// 		performable = al.GetAction().IsPerformable.Invoke();
																	//	}
        startChannelEvent();										
																	
        while (timer <= interactionTime)
        {
            timer += Time.deltaTime;
            if (!Input.GetButton(button))
            {
                progressBar.SetActive(false);
                performingAction = false;
                stopChannelEvent();
                yield break;
            }
            progressBarImage.fillAmount = timer / interactionTime;
            progressBar.transform.position = Camera.main.WorldToScreenPoint(progressBarLocation.transform.position);
            yield return null;
        }
        stopChannelEvent();
        al.GetAction().Action();
        progressBar.SetActive(false);
        performingAction = false;
    }

    void UpdateText(string button)
    {
        if (button.Equals("Use"))
        {
            progressBarText.text = "Cooking...";
        }
        if (button.Equals("Drop"))
        {
            progressBarText.text = "Emptying...";
        }
    }

    //will need to be updated
    //Probably want the item to be offset from the player in the direction they are facing
    void UpdateHoldablePosition()
    {
        heldItemInteraction.gameObject.transform.position = heldItemLocation.transform.position;
        heldItemInteraction.gameObject.transform.rotation = transform.rotation;
    }

    //Called when the item is used or dropped
    void ClearFields()
    {
        heldItem = null;
        heldItemInteraction = null;
    }

    //return true if you can hold something
    public bool TryToPickUp(GameObject g, HoldableItem i)
    {
        //Debug.Log("trying to pickup a " + g.name);
        if (heldItem == null)
        {
            heldItem = g;
            heldItemInteraction = i;
			itemPickupEvent(g);
            return true;
        }
        return false;
    }
}
