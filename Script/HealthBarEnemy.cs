using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Slider Slider;  // ᶺ��ѧ���Ե����� UI Slider
    public Color Low;  // �բͧᶺ��ѧ���Ե����;�ѧ���Ե���
    public Color High;  // �բͧᶺ��ѧ���Ե����;�ѧ���Ե�٧
    public Vector3 Offset;  // ��������͹�ͧᶺ��ѧ���Ե�ҡ���˹��ѵ��

    // �ѧ��ѹ����Ѻ��駤�Ҿ�ѧ���Ե�ᶺ��ѧ���Ե

    void Start()
    {
        float initialHealth = 100f;  // ��˹���ѧ���Ե�������
        float maxHealth = 100f;  // ��˹���ѧ���Ե�٧�ش
        SetHealth(initialHealth, maxHealth);  // ���¡�ѧ��ѹ���͡�˹���Ҿ�ѧ���Ե
    }
    public void SetHealth(float health, float maxHealth)
    {
        Slider.maxValue = maxHealth;  // ��駤�� maxValue ��͹
        Slider.value = health;  // �ҡ��鹤��µ�駤�� value

        // ��ҧ�ԧ��ѧ Image �ͧ fillRect
        Image fillImage = Slider.fillRect.GetComponent<Image>();

        if (fillImage != null)
        {
            // ����¹�յ����Ҿ�ѧ���Ե
            fillImage.color = Color.Lerp(Low, High, Slider.normalizedValue);
        }

        Color newColor = Color.Lerp(Low, High, Slider.normalizedValue);
        newColor.a = 1.0f;  // �ѧ�Ѻ�������������
        fillImage.color = newColor;
    }


    // �ѧ��ѹ����Ѻ��駤�ҵ��˹觢ͧᶺ��ѧ���Ե
    public void SetPosition(Vector3 newPosition)
    {
        Slider.transform.position = newPosition;
    }

    void Update()
    {
        // ���¡��ѧ��ѹ SetPosition ���͡�˹����˹�ᶺ��ѧ���Ե��ٻẺ screen space
        SetPosition(Camera.main.WorldToScreenPoint(transform.parent.position + Offset));
    }
}
