using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public PlayerCat player;
    public Image fill;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (player == null) player = GetComponentInParent<PlayerCat>();
    }

    void LateUpdate()
    {
        if (player == null || fill == null) return;
        fill.fillAmount = player.Stamina01;
        fill.color = Color.Lerp(Color.red, Color.green, player.Stamina01);
        if (cam) transform.rotation = cam.transform.rotation;  // selalu menghadap kamera, tidak ikut muter kucing
    }
}