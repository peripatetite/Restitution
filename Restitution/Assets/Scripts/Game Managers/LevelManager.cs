using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;
    public GameObject player;
    public Image fade;
    public string nextLevel;

	private void Awake() {
        instance = this;
	}

    public void AdvanceLevel() {
        player.GetComponent<David>().enabled = false;
        fade.color = new Color(0, 0, 0, 0);
        fade.gameObject.SetActive(true);
        StartCoroutine(TransitionScene(fade));
	}

    IEnumerator TransitionScene(Image mask) {
        Color c = mask.color;
        for (float alpha = 0f; alpha <= 1f; alpha += 0.02f) {
            c.a = alpha;
            mask.color = c;
            yield return new WaitForSeconds(0.1f);
		}
        SceneManager.LoadScene(nextLevel);
	}
}
