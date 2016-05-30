using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

    [SerializeField]
    private Slider hpBar;

	// Use this for initialization
	void Start () {
        hpBar.maxValue = hpBar.value = GetComponent<StatManager>().health;
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.value = Mathf.Lerp(hpBar.value, GetComponent<StatManager>().health, Time.deltaTime * 10);
	}
}
