using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image checkmark;
    [SerializeField] private Image background;

    [SerializeField] private float colorChangeSpeed = 5f;
    [SerializeField] private float positionChangeSpeed = 5f;

    [Header("Checkmark Points")]
    [SerializeField] private Transform pointCheckmarkOn;
    [SerializeField] private Transform pointCheckmarkOff;

    [Header("Background Colors")]
    [SerializeField] private Color colorBackgroundOn = Color.green;
    [SerializeField] private Color colorBackgroundOff = Color.red;

    private const string KEY_ALARM_ENABLE = "ALARM_CLOCK";


    private Vector3 targetPosition;
    private Color targetColor;

    private void OnEnable()
    {
        bool isEnabled = PlayerPrefs.GetInt(KEY_ALARM_ENABLE, 0) == 1;
        SetToggleState(isEnabled);
        toggle.isOn = isEnabled;
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        SetToggleState(isOn);
        PlayerPrefs.SetInt(KEY_ALARM_ENABLE, isOn ? 1 : 0);
    }

    private void SetToggleState(bool isOn)
    {
        targetColor = isOn ? colorBackgroundOn : colorBackgroundOff;
        targetPosition = isOn ? pointCheckmarkOn.position : pointCheckmarkOff.position;
    }

    private void Update()
    {       
        background.color = Color.Lerp(background.color, targetColor, colorChangeSpeed * Time.deltaTime);
        checkmark.transform.position = Vector3.Lerp(checkmark.transform.position, targetPosition, positionChangeSpeed * Time.deltaTime);           
    }
}