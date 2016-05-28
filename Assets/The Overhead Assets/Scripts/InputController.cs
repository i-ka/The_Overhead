using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMoveController))]
public class InputController : MonoBehaviour {

    private CharacterMoveController player;
    private bool jump=false;
    // Use this for initialization

    void Start () {
        player = GetComponent<CharacterMoveController>();
        GameManager.instance.Setup(gameObject);
    }

	void FixedUpdate()
    {
        player.Move(Input.GetAxis("Horizontal"),jump);
        jump = false;
    }

	// Update is called once per frame
	void Update () {
        if (!jump)
        {
            jump = Input.GetButtonDown("Jump");
        }
        player.Attack(Input.GetButton("Fire1"));
	}
}
