using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;

    private float maxSize;
    private float growSpeed;
    private float shrinkSpeed;
    private float blackholeTimer;

    private bool canGrow = true;         // ºÚ¶´·Å´ó
    private bool canShrink;              // ºÚ¶´ËõÐ¡
    private bool canCreateHotKeys = true;

    private int amountOfAttacks;
    private float cloneAttackCooldown = .3f;
    private float cloneAttackTimer;
    private bool cloneAttackReleased;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createdHotKeys = new List<GameObject>();

    private bool playerCanDisapper = true;
    public bool playerCanExitState {  get; private set; }

    public void SetUpBlackhole(float _maxSize, float _growSpeed, float _shrinkSpeed, int _amoutOfAttacks, float _cloneAttackCooldown, float _blackholeDuration)
    {
        maxSize = _maxSize; 
        growSpeed = _growSpeed; 
        shrinkSpeed = _shrinkSpeed;
        amountOfAttacks = _amoutOfAttacks;
        cloneAttackCooldown = _cloneAttackCooldown;
        blackholeTimer = _blackholeDuration;
    }

    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        blackholeTimer -= Time.deltaTime;

        if(blackholeTimer < 0 )
        {
            blackholeTimer = Mathf.Infinity;

            if(targets.Count > 0)
            {
                ReleaseCloneAttack();
            }
            else
            {
                FinishBlackholeSkill();
            }

        }

        /*
         * R¼üÊÍ·Å¿ËÂ¡¹¥»÷
         */
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();
        }

        CloneAttackLogic();

        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);

            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent <Enemy>().FreezeTime(false);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (keyCodeList.Count <= 0) return;

        if (!canCreateHotKeys) return;


        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createdHotKeys.Add(newHotKey);

        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosenKey);

        Blackhole_Hotkey_Controller newHotKeyScript = newHotKey.GetComponent<Blackhole_Hotkey_Controller>();

        newHotKeyScript.SetUpHotKey(choosenKey, collision.transform, this);
    }

    private void DestroyHotKeys()
    {
        if (createdHotKeys.Count <= 0) return;

        for (int i = 0; i < createdHotKeys.Count; i++)
        {
            Destroy(createdHotKeys[i]);
        }
    }

    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);

    private void ReleaseCloneAttack()
    {
        if(targets.Count <= 0) return;

        DestroyHotKeys();
        cloneAttackReleased = true;
        canCreateHotKeys = false;

        if (playerCanDisapper)
        {
            playerCanDisapper = false;
            PlayerManager.instance.player.MakeTransparent(true);
        }
    }

    private void CloneAttackLogic()
    {
        if (cloneAttackTimer < 0 && cloneAttackReleased && amountOfAttacks > 0) 
        {
            cloneAttackTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);

            float xOffset;

            if (Random.Range(0, 100) > 50)
            {
                xOffset = 2;
            }
            else
            {
                xOffset = -2;
            }

            SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));

            amountOfAttacks--;

            if (amountOfAttacks <= 0)
            {
                Invoke("FinishBlackholeSkill", 1f);
            }
        }
    }

    private void FinishBlackholeSkill()
    {
        DestroyHotKeys();
        playerCanExitState = true;
        canShrink = true;
        cloneAttackReleased = false;
    }
}
