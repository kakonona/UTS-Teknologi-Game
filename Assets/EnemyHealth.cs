using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHP = 100f;
    public Image hpFill;                 // Image Filled di Canvas HP (opsional)
    public float CurrentHP { get; private set; }

    Camera cam;

    void Awake()
    {
        CurrentHP = maxHP;
        cam = Camera.main;
        UpdateBar();
    }

    public void TakeDamage(float dmg)
    {
        if (CurrentHP <= 0) return;
        CurrentHP -= dmg;
        Debug.Log($"{name} HP: {Mathf.Max(CurrentHP, 0)}/{maxHP}");
        UpdateBar();
        if (CurrentHP <= 0) Destroy(gameObject);
    }

    void UpdateBar()
    {
        if (hpFill) hpFill.fillAmount = Mathf.Clamp01(CurrentHP / maxHP);
    }

    void LateUpdate()
    {
        if (hpFill && cam) hpFill.canvas.transform.rotation = cam.transform.rotation;
    }
}