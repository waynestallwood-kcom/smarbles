using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour 
{
	public float timeLeft = 20f;
	public float speed;
	public GUIText scoreText;
	public GUIText winText;
	public GUIText loseText;
	public GUIText livesText;
	public GUIText timeText;
	public GUIText restartText;
	public AudioClip Badieimpact;
	public AudioClip Youlose;
	public AudioClip Youwin;
	public AudioClip Pickup;

	private int icons;
	private int lives;
	private int gameover;
	private int playerMoved;

	void Start ()
	{
		icons = 14;
		lives = 3;
		gameover = 0;
		playerMoved = 0;
		SetScoreText ();
		SetLivesText ();
		winText.text = "";
		loseText.text = "";
		restartText.text = "";

	}

	void Update () // Main updates here
	{
		if (Input.GetKeyDown (KeyCode.Return) && gameover == 1) {  
						Application.LoadLevel (0); 
						}  

		if (rigidbody.angularVelocity != Vector3.zero) {
						playerMoved = 1; // Set flag when player object moves for the first time
						}

		if (gameover == 0 && playerMoved == 1) {
						timeLeft -= Time.deltaTime; // don't start counting time until player has moved
					  	}
		if (timeLeft <= 0.0f && gameover == 0){
						// Player took too long.
						loseText.text = "You ran out of time";
						EndGame (); 
						audio.PlayOneShot(Youlose, 1F);
						}
		else
						{
						timeText.text = "Time left = " + (int)timeLeft + " seconds";
						}
	}

	void FixedUpdate () // Player controls here so they aren't bound to the framerate
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		if (lives > 0 && gameover == 0){
						rigidbody.AddForce (movement * speed * Time.deltaTime);
					   }
	}

	void OnTriggerEnter (Collider other) // Collisions with other objects
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

	void EndGame () // Stops all player motion and sets the gameover flag so countdown stops
	{
		gameover = 1;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.Sleep ();
		restartText.text = "Enter/Return to Play again";
	}

}




