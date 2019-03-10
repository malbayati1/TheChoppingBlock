using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents the actual object in the scene of the ingredient

public class Weapon : HoldableItem
{
	public Vector3 heldPositionOffset = new Vector3(.5f, .5f, -.5f);
	public Vector3 heldRotationOffset = new Vector3(0f, 180f, 0f);

	public Vector3 swungRotationOffset = new Vector3(-30, 30, 90);
	public Vector3 swungPositionOffset = new Vector3(.75f, .75f, -.75f);

	[HideInInspector]
	public bool canHit = false;

	private bool swinging = false;

	public float knockbackModifier = 1f;

	public int damageModifier = 1;
	
	private float rotationDegreesPerSecond = 180f;

	private Transform modelChild;

	private AudioSource audioSource;
	private AudioSource impactAudioSource;

	void Awake()
	{
		modelChild = transform.GetChild(0);

		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.spatialBlend = 1;
		impactAudioSource = gameObject.AddComponent<AudioSource>();
		impactAudioSource.spatialBlend = 1;
	}

	void Update()
	{
		if(isHeld)
		{
			return;
		}
		modelChild.localPosition = new Vector3(0, 0.5f, 0);
		transform.RotateAround(transform.position, Vector3.up, rotationDegreesPerSecond * Time.deltaTime);
	}

    public override bool Use(GameObject user)
	{
		if (!swinging)
			StartCoroutine(Swing());

		return false;
	}
	
	protected IEnumerator Swing()
	{
		swinging = true;

		canHit = true;

		audioSource.clip = AudioManager.instance.swingAudio;
		audioSource.Play();


		iTween.MoveTo(modelChild.gameObject, iTween.Hash("position", swungPositionOffset, "easetype", "easeInQuad", "time", .25f, "isLocal", true));

		iTween.MoveTo(modelChild.gameObject, iTween.Hash("position", heldPositionOffset, "easetype", "easeOutQuad", "time", .15f, "delay", .25f, "isLocal", true));


		iTween.RotateTo(modelChild.gameObject, iTween.Hash("rotation", swungRotationOffset, "easetype", "easeInQuad", "time", .25f, "isLocal", true));

		iTween.RotateTo(modelChild.gameObject, iTween.Hash("rotation", heldRotationOffset, "easetype", "easeOutQuad", "time", .15f, "delay", .25f, "isLocal", true));

		yield return new WaitForSeconds(.5f);

		canHit = false;

		swinging = false;
		
	}

    public override void Drop(GameObject from)
	{
		base.Drop(from);

		audioSource.clip = AudioManager.instance.dropKnifeAudio;
		audioSource.Play();
	}

	protected override void OnTriggerEnter(Collider col)
    {
		base.OnTriggerEnter(col);

		if (isHeld && canHit && col.gameObject.CompareTag("Enemy"))
		{
			Unit unit = col.transform.parent.gameObject.GetComponent<Unit>();
			if (unit != null)
			{
				PlayerStats stats = heldBy.GetComponent<PlayerStats>();
				int damage = Mathf.RoundToInt(stats.strength.Value) * damageModifier;
				float knockback = knockbackModifier * stats.strength.Value;
				Vector3 direction = transform.forward.normalized;

				unit.GetHit(damage, direction, knockback);

				impactAudioSource.clip = AudioManager.instance.juicyImpactAudio;
				impactAudioSource.Play();
			}
		}
	}

	protected override void GetPickedUp(Collider col)
	{
		base.GetPickedUp(col);
		
		iTween.MoveTo(modelChild.gameObject, iTween.Hash("position", heldPositionOffset, "easetype", "easeOutQuad", "time", .02f, "isLocal", true));

		iTween.RotateTo(modelChild.gameObject, iTween.Hash("rotation", heldRotationOffset, "easetype", "easeOutQuad", "time", .02f, "isLocal", true));
	
		audioSource.clip = AudioManager.instance.pickUpKnifeAudio;
		audioSource.Play();
	}
}
