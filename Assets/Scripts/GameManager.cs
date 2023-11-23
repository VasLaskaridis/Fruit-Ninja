using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public Image fadeImage;

    private int score;
    private Blade blade;
    private Spawner spawner;

    public void IncreaseScore(){
        score++;
        scoreText.text = score.ToString();
    }

    private void NewGame(){
        score = 0;
        scoreText.text = score.ToString();
        Time.timeScale = 1f;
        blade.enabled = true;
        spawner.enabled = true;
        ClearScene();
    }

    private void ClearScene(){
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach(Fruit fruit in fruits){
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach(Bomb bomb in bombs){
            Destroy(bomb.gameObject);
        }
    }

    public void Explode(){
        blade.enabled = false;
        spawner.enabled = false;
        StartCoroutine(ExplodeSequence());
    }

    private void Awake(){
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ExplodeSequence(){
        float elapsed = 0f;
        float duration = 0.5f;
        while(elapsed < duration){
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);
            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;
            yield return null; 
        }

        yield return new WaitForSecondsRealtime(1f); 


        elapsed = 0f;
        while(elapsed < duration){
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);
            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;
            yield return null; 
        }
        NewGame();

    }
}
