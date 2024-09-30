using TMPro;
using UnityEngine;

public class Blackhole_Hotkey_Controller : MonoBehaviour
{
    private KeyCode myHotKey;
    private SpriteRenderer sr;
    private TextMeshProUGUI myText;

    private Transform myEnemy;
    private Blackhole_Skill_Controller blackhole;

    public void SetUpHotKey(KeyCode _myHotKey, Transform _myEnemy, Blackhole_Skill_Controller _myBlackhole)
    {
        sr = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myHotKey = _myHotKey;
        myEnemy = _myEnemy;
        blackhole = _myBlackhole;

        myText.text = myHotKey.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(myHotKey))
        {
            blackhole.AddEnemyToList(myEnemy);

            sr.color = Color.clear;
            myText.color = Color.clear;
        }
    }
}
