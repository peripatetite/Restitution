using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;
    public GameObject replayButton;
    public GameObject player;
    public Image fade;
    public GameObject pauseText;
    public GameObject returnToStartButton;
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
            returnToStartButton.SetActive(paused);
            david.frozen = paused;
		}
	}

	public void AdvanceLevel() {
        if (player != null)
            player.GetComponent<David>().enabled = false;
        StartCoroutine(TransitionScene(fade, nextLevel));
	}

    public void GoToStart() {
		if (player != null)
			player.GetComponent<David>().enabled = false;
		SceneManager.LoadScene("StartMenu");
	}

	public void ExitGame() {
        Application.Quit();
	}

    IEnumerator TransitionScene(Image mask, string scene) {
		mask.color = new Color(0, 0, 0, 0);
		mask.gameObject.SetActive(true);
		Color c = mask.color;
        for (float alpha = 0f; alpha <= 1f; alpha += 0.04f) {
            c.a = alpha;
            mask.color = c;
            yield return new WaitForSeconds(0.01f);
		}
        SceneManager.LoadScene(scene);
	}
}
