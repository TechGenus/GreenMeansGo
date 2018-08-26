using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
	public float moveSpeed;
	[Range(1, 10)]
	public float jumpSpeed;
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;
	public GameObject deathParticles;
	public Text deathCountText;

	private Vector3 input;

	private Rigidbody rb;
	private Transform t;
	private Animator anim;

	private Vector3 spawnPosition;
	private Vector3 newRotation;

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody>();
		t = GetComponent<Transform>();
		anim = GetComponent<Animator>();
		spawnPosition = t.position;
		deathCountText.text = "Deaths: " + GameManager.deathCount;
	}

	// Update is called once per frame
	void Update() {
		if (t.position.y < spawnPosition.y - 2f) Die();

		input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		rb.velocity = new Vector3(input.x * moveSpeed, rb.velocity.y, input.z * moveSpeed);

		if (input.magnitude != 0) {
			newRotation = new Vector3(0, 90 - Mathf.Atan2(input.z, input.x) * Mathf.Rad2Deg, 0);
			t.rotation = Quaternion.Euler(newRotation);
			anim.SetInteger("state", 1);
		} else {
			anim.SetInteger("state", 0);
		}

		Jump();
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Enemy") Die();
	}

	private void OnCollisionStay(Collision collision) {
		if (collision.collider.tag == "Red" 
			&& (input.magnitude != 0 
			|| Input.GetAxisRaw("Jump") > 0))
			Die();
	}

	void OnTriggerEnter(Collider other) {
        if (other.tag == "Goal")
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            if (currentScene != 6) SceneManager.LoadScene(currentScene + 1);
            else GameManager.CompleteLevel();
        }
            


    }

	void Die() {
		Instantiate(deathParticles, t.position, Quaternion.LookRotation(Vector3.up));
		t.position = spawnPosition;
		t.rotation = Quaternion.LookRotation(Vector3.forward);
		deathCountText.text = "Deaths: " + ++GameManager.deathCount;
	}

	bool isGrounded() {
		return Physics.Raycast(t.position + new Vector3(0, 1f, 0), Vector3.down, 1.1f);
	}
	
	void Jump() {
		if (Input.GetAxisRaw("Jump") > 0 && isGrounded()) {
			rb.velocity = Vector3.up * jumpSpeed;
		} else if (!isGrounded()) {
			Fall();
			anim.SetInteger("state", 2);
		}
	}

	void Fall() {
		if (rb.velocity.y < 0) {
			rb.velocity += Vector3.up.y * Physics.gravity * (fallMultiplier - 1) * Time.deltaTime;
		} else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
			rb.velocity += Vector3.up.y * Physics.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}
}
