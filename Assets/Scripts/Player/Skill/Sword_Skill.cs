using System;
using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class Sword_Skill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;       // 投掷力
    [SerializeField] private float swordGravity;        // Regular剑的重力
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;

    private Vector2 finalDir;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dots;

    [Header("Bounce info")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;       // Bounce剑的重力
    [SerializeField] private float bounceSpeed;

    [Header("Pierce info")]
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;       // Pierce剑的重力

    [Header("Spin info")]
    [SerializeField] private float maxTravelDistance;
    [SerializeField] private float spinDuration;
    [SerializeField] private float spinGravity;         // Spin剑的重力
    [SerializeField] private float hitCooldown;         // Spin剑的攻击冷却

    protected override void Start()
    {
        base.Start();

        GenerateDots();
    }

    protected override void Update()
    {
        SetUpSwordGravity();

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }

    #region Aim
    /*
     * 剑投掷方向
     */
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        // 距离公式 x = v * t + 1/2 * g * t^2
        Vector2 pos = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t +
            .5f * (Physics2D.gravity * swordGravity) * (t * t);

        return pos;
    }
    #endregion

    public void CreateSword()
    {
        // 实例化一个GameObject命名为newSword
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        // 访问Sword_Skill_Controller中的组件
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();

        if(swordType == SwordType.Bounce)
        {
            newSwordScript.SetUpBounce(true, bounceAmount,bounceSpeed);
        }
        else if(swordType == SwordType.Pierce)
        {
            newSwordScript.SetUpPierce(pierceAmount);
        }
        else if(swordType == SwordType.Spin)
        {
            newSwordScript.SetUpSpin(true, maxTravelDistance, spinDuration, hitCooldown);
        }

        newSwordScript.SetUpSword(finalDir, swordGravity, player, freezeTimeDuration,returnSpeed);

        player.AssignNewSword(newSword);

        DotsActive(false);
    }

    private void SetUpSwordGravity()
    {
        if(swordType == SwordType.Bounce)
        {
            swordGravity = bounceGravity;
        }
        else if(swordType == SwordType.Pierce)
        {
            swordGravity = pierceGravity;
        }
        else if (swordType == SwordType.Spin)
        {
            swordGravity = spinGravity;
        }
    }

}
