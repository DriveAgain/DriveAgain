using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    [Header("Speed (km/h)")]
    [SerializeField] float maxSpeedKmh = 50f;      // מהירות מותרת במדריך
    [SerializeField] float accelKmhPerSec = 12f;   // כמה מהר מאיצים כשמחזיקים ↑
    [SerializeField] float brakeKmhPerSec = 25f;   // כמה מהר בולמים כשמחזיקים ↓
    [SerializeField] float coastKmhPerSec = 4f;    // ירידה איטית כשמשחררים (חיכוך)

    [Header("Steering")]
    [SerializeField] float turnSpeed = 120f;       // מעלות לשנייה (בערך)

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

        // 1) עדכון מהירות (km/h)
        if (forwardInput > 0.01f)
        {
            // מאיץ בהדרגה
            currentSpeedKmh += accelKmhPerSec * Time.fixedDeltaTime;
        }
        else if (forwardInput < -0.01f)
        {
            // ברקס בהדרגה (מוריד מהירות)
            currentSpeedKmh -= brakeKmhPerSec * Time.fixedDeltaTime;
        }
        else
        {
            // לא לוחצים כלום: ירידה איטית
            if (currentSpeedKmh > 0f)
                currentSpeedKmh -= coastKmhPerSec * Time.fixedDeltaTime;
        }

        // גבולות
        currentSpeedKmh = Mathf.Clamp(currentSpeedKmh, 0f, maxSpeedKmh);

        // 2) להמיר ל-m/s ולתת מהירות לריג’ידבודי
        float currentSpeedMS = currentSpeedKmh / 3.6f;
        Vector3 vel = transform.forward * currentSpeedMS;
        rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);

        // 3) היגוי רק כשזזים
        if (currentSpeedKmh > 0.5f)
        {
            float turn = steerInput * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
        }
    }
}
