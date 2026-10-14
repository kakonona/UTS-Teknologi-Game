using UnityEngine;
using UnityEngine.InputSystem;

public class SpellCaster : MonoBehaviour
{
    public Transform firePoint;
    public GameObject slashPrefab;
    public GameObject hitPrefab;
    public GameObject activeVFX;        // aura/efek di pedang, drag dari Hierarchy
    public float damage = 20f;
    public float range = 30f;
    public float cooldown = 0.5f;

    [Header("Garis bidik")]
    public float aimLength = 10f;
    public Color aimColor = new Color(1f, 1f, 1f, 0.6f);

    Camera cam;
    LineRenderer line;
    Vector3 aimDir = Vector3.forward;
    float nextFire;

    void Start()
    {
        cam = Camera.main;
        if (activeVFX) activeVFX.SetActive(true);

        line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startWidth = line.endWidth = 0.25f;
        line.startColor = line.endColor = aimColor;
        line.positionCount = 2;
        line.numCapVertices = 4;
    }

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null || cam == null) return;

        Plane ground = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        Ray r = cam.ScreenPointToRay(mouse.position.ReadValue());
        if (ground.Raycast(r, out float d))
        {
            Vector3 dir = r.GetPoint(d) - transform.position;
            dir.y = 0;
            if (dir.sqrMagnitude > 0.05f) aimDir = dir.normalized;
        }

        Vector3 origin = firePoint ? firePoint.position : transform.position + Vector3.up * 0.5f;
        Vector3 flat = new Vector3(origin.x, transform.position.y + 0.05f, origin.z);
        line.SetPosition(0, flat);
        line.SetPosition(1, flat + aimDir * aimLength);

        if (mouse.leftButton.wasPressedThisFrame && Time.time >= nextFire)
        {
            nextFire = Time.time + cooldown;
            Fire(origin);
        }
    }

    void LateUpdate()
    {
        // menimpa rotasi dari script gerak, supaya kucing selalu menghadap mouse
        transform.rotation = Quaternion.LookRotation(aimDir);
    }

    void Fire(Vector3 origin)
    {
        if (slashPrefab)
            Destroy(Instantiate(slashPrefab, origin, Quaternion.LookRotation(aimDir)), 2f);

        if (activeVFX)
        {
            activeVFX.SetActive(false);
            activeVFX.SetActive(true);   // restart efek tiap tembak
        }

        if (Physics.Raycast(origin, aimDir, out RaycastHit hit, range, ~0, QueryTriggerInteraction.Ignore))
        {
            var hp = hit.collider.GetComponentInParent<EnemyHealth>();
            if (hp != null)
            {
                Debug.Log($"Kena Musuh pada jarak {hit.distance:F1} meter");
                hp.TakeDamage(damage);
                if (hitPrefab)
                    Destroy(Instantiate(hitPrefab, hit.point, Quaternion.LookRotation(-aimDir)), 2f);
            }
        }
    }
}