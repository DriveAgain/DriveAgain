using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    [Header("Speed (km/h)")]
    [SerializeField] float maxSpeedKmh = 50f;       // מהירות מותרת קדימה
    [SerializeField] float maxReverseKmh = 25f;     // מהירות מותרת אחורה (חדש)
    [SerializeField] float accelKmhPerSec = 12f;    // כמה מהר מאיצים כשמחזיקים ↑
    [SerializeField] float brakeKmhPerSec = 25f;    // כמה מהר בולמים כשמחזיקים ↓
    [SerializeField] float reverseAccelKmhPerSec = 10f; // כמה מהר מאיצים אחורה (חדש)
    [SerializeField] float coastKmhPerSec = 4f;     // ירידה איטית כשמשחררים (חיכוך)

    [Header("Steering")]
    [SerializeField] float turnSpeed = 120f;        // מעלות לשנייה (בערך)

    [Header("Collision")]
    [SerializeField] string wallTag = "WALL";       // התגית של הקירות/מכשולים
    [SerializeField] float collisionImpactThreshold = 2f; // מהירות פגיעה מינימלית לאיפוס

    [SerializeField] InputAction move;

    Rigidbody rb;
    float currentSpeedKmh = 0f;

    void Awake()
    {
        move = new InputAction(type: InputActionType.Value, expectedControlType: nameof(Vector2));
        move.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");

        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()  { move.Enable(); }
    void OnDisable() { move.Disable(); }

    void FixedUpdate()
    {
        Vector2 input = move.ReadValue<Vector2>();
        float forwardInput = input.y; // ↑ ↓
        float steerInput = input.x;   // ← →

        // 1) עדכון מהירות (km/h) - תומך גם בנסיעה אחורה
        if (forwardInput > 0.01f)
        {
            if (currentSpeedKmh < 0f)
            {
                // ברקס על נסיעה אחורה (חזרה לכיוון 0)
                currentSpeedKmh += brakeKmhPerSec * Time.fixedDeltaTime;
            }
            else
            {
                // מאיץ קדימה בהדרגה
                currentSpeedKmh += accelKmhPerSec * Time.fixedDeltaTime;
            }
        }
        else if (forwardInput < -0.01f)
        {
            if (currentSpeedKmh > 0f)
            {
                // ברקס על נסיעה קדימה (חזרה לכיוון 0)
                currentSpeedKmh -= brakeKmhPerSec * Time.fixedDeltaTime;
            }
            else
            {
                // ממשיך/מתחיל לנסוע אחורה
                currentSpeedKmh -= reverseAccelKmhPerSec * Time.fixedDeltaTime;
            }
        }
        else
        {
            // לא לוחצים כלום: ירידה איטית לכיוון 0 (חיכוך), משני הצדדים
            if (currentSpeedKmh > 0f)
                currentSpeedKmh = Mathf.Max(0f, currentSpeedKmh - coastKmhPerSec * Time.fixedDeltaTime);
            else if (currentSpeedKmh < 0f)
                currentSpeedKmh = Mathf.Min(0f, currentSpeedKmh + coastKmhPerSec * Time.fixedDeltaTime);
        }

        // גבולות - כעת טווח דו-כיווני
        currentSpeedKmh = Mathf.Clamp(currentSpeedKmh, -maxReverseKmh, maxSpeedKmh);

        // 2) להמיר ל-m/s ולתת מהירות לריג'ידבודי
        float currentSpeedMS = currentSpeedKmh / 3.6f;
        Vector3 vel = transform.forward * currentSpeedMS;
        rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);

        // 3) היגוי רק כשזזים (גם קדימה וגם אחורה, לכן Abs)
        if (Mathf.Abs(currentSpeedKmh) > 0.5f)
        {
            // בנסיעה אחורה ההיגוי הפוך (כמו ברכב אמיתי)
            float direction = currentSpeedKmh >= 0f ? 1f : -1f;
            float turn = steerInput * direction * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
        }
    }

    // --- איפוס מהירות אמיתי בהתנגשות בקיר ---
    void OnCollisionEnter(Collision collision)
    {
        HandleWallHit(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        // למקרה שהרכב "נשען" על הקיר וממשיך ללחוץ ↑/← / →
        HandleWallHit(collision);
    }

    void HandleWallHit(Collision collision)
    {
        if (!collision.collider.CompareTag(wallTag))
            return;

        // מאפסים את המהירות הפנימית של הסקריפט (זה התיקון המרכזי!)
        currentSpeedKmh = 0f;

        // מאפסים גם את הפיזיקה בפועל - מהירות ותנע
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}