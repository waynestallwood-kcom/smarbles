using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour 
{
	public float timeLeft = 50f;
	public float speed;
	public GUIText scoreText;
	public GUIText winText;
	public GUIText loseText;
	public GUIText livesText;
	public GUIText timeText;
	public AudioClip Badieimpact;
	public AudioClip Youlose;
	public AudioClip Youwin;
	public AudioClip Pickup;

	private int icons;
	private int lives;
	private int gameover;

	void Start ()
	{
		icons = 14;
		lives = 3;
		gameover = 0;
		SetScoreText ();
		SetLivesText ();
		winText.text = "";
		loseText.text = "";

	}

	void Update ()
	{
		if (gameover == 0) {
						timeLeft -= Time.deltaTime;
					  }
		if (timeLeft <= 0.0f)
		{
			// Player took too long.
			loseText.text = "You ran out of time";
			EndGame ();
			timeLeft = 0.1f; // FIXME: Yuck 
			audio.PlayOneShot(Youlose, 1F);
		}
		else
		{
			timeText.text = "Time left = " + (int)timeLeft + " seconds";
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		if (lives > 0 && gameover == 0){
						rigidbody.AddForce (movement * speed * Time.deltaTime);
					   }
	}

	void OnTriggerEnter (Collider other) 
	{
		if (other.gameObject.tag == "PickUp") {
			other.gameObject.SetActive (false);
			icons = icons - 1;
			SetScoreText ();
			audio.PlayOneShot(Pickup, 1F);
	}
	
		if (other.gameObject.tag == "Badie") {
			lives = lives - 1;
			SetLivesText ();
			audio.PlayOneShot(Badieimpact, 1F);
	}
	}

	void SetScoreText ()
	{
		scoreText.text = "Icons Left: " + icons.ToString();
		if (icons <= 0) {
						winText.text = "You Win !!!";
						EndGame ();
						audio.PlayOneShot(Youwin, 1F);
						}

	}
	void SetLivesText ()
	{
		livesText.text = "Lives: " + lives.ToString ();
		if (lives <= 0) {
						loseText.text = "Hugged the Tin too much, You Lose !!!";
						EndGame ();
						audio.PlayOneShot(Youlose, 1F);
						}
	}

	void EndGame ()
	{
		gameover = 1;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.Sleep ();
	}

}



