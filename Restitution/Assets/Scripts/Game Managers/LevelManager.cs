using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;
    public GameObject replayButton;
    public GameObject player;
    public Image fade;
    public GameObject pauseText;
    public string nextLevel;
    public bool paused;

    private David david;

	private void Awake() {
        instance = this;
	}

	private void Start() {
        david = player == null ? null : player.GetComponent<David>();
	}

	private void Update() {
		if (david != null && !david.caught && Input.GetKeyDown(KeyCode.P)) {
            paused = !paused;
            Time.timeScale = paused ? 0 : 1;
            pauseText.SetActive(paused);
		}
	}

	public void AdvanceLevel() {
        if (player != null)
            player.GetComponent<David>().enabled = false;
        fade.color = new Color(0, 0, 0, 0);
        fade.gameObject.SetActive(true);
        StartCoroutine(TransitionScene(fade));
	}

    IEnumerator TransitionScene(Image mask) {
        Color c = mask.color;
        for (float alpha = 0f; alpha <= 1f; alpha += 0.04f) {
            c.a = alpha;
            mask.color = c;
            yield return new WaitForSeconds(0.01f);
		}
        SceneManager.LoadScene(nextLevel);
	}
}
