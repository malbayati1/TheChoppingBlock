using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject progressBar;
    public Image progressBarImage;
    public Text progressBarText;
    public float interactionTime;

    public delegate void InputDelegate();
    public event InputDelegate useEvent = delegate { };
    public event InputDelegate dropEvent = delegate { };

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
    }

    void Update()
    {
        if (heldItem != null)
        {
            UpdateHoldablePosition();
            if (Input.GetButtonDown("Use"))
            {
                heldItemInteraction.Use(gameObject);
                ClearFields();
            }
            else if (Input.GetButtonDown("Drop"))
            {
                heldItemInteraction.Drop(gameObject);
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
            StartCoroutine(PerformAction("Use", useEvent));
        }
        if (Input.GetButtonDown("Drop"))
        {
            StartCoroutine(PerformAction("Drop", dropEvent));
        }
    }

    IEnumerator PerformAction(string button, InputDelegate f)
    {
        if (f.GetInvocationList().Length <= 1)
        {
            yield break;
        }
        performingAction = true;
        float timer = 0f;
        progressBar.SetActive(true);
        UpdateText(button);
        while (timer <= interactionTime)
        {
            timer += Time.deltaTime;
            if (!Input.GetButton(button))
            {
                progressBar.SetActive(false);
                performingAction = false;
                yield break;
            }
            progressBarImage.fillAmount = timer / interactionTime;
            progressBar.transform.position = Camera.main.WorldToScreenPoint(progressBarLocation.transform.position);
            yield return null;
        }
        f();
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
        heldItemInteraction.gameObject.transform.position = transform.position + CameraController.instance.forwardDirection.normalized;
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
            return true;
        }
        return false;
    }
}
