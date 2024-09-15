using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // ����� namespace �ͧ TextMesh Pro ����Ѻ�����ҹ TMP_Text

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFill;  // ��ҧ�ԧ�֧ UI Image �������ʴ�ᶺ���ʹ
    [SerializeField] private PlayerScript playerScript; // ��ҧ�ԧ�֧ PlayerScript ���ʹ֧����������ǡѺ��ѧ���Ե�ͧ������
    [SerializeField] private RectMask2D _mask; // ���������Ѻ��èѴ��á�ä�ͺ�����ͧ RectMask2D (����ա����ҹ)
    [SerializeField] private TMP_Text _hpIndicator; // ���������Ѻ�ʴ���� HP �ͧ������Ẻ����ѡ������ TextMesh Pro (TMP_Text)

    private float _maxRightMask; // �纤���٧�ش����Ѻ��õ�駤�� RectMask2D (�����ҵ�ͧ���)
    private float _initialRightMask; // �纤��������鹢ͧ RectMask2D (�����ҵ�ͧ���)

    private void Start()
    {
        // ��Ǩ�ͺ����ա�õ�駤�� PlayerScript ��� _hpIndicator ������� ������������ʴ� Error � Console
        if (playerScript == null || _hpIndicator == null)
        {
            Debug.LogError("PlayerScript ���� HP Indicator �����١��ҧ�ԧ!");
            return;
        }

        // ��駤�Ң�ͤ����ͧ _hpIndicator �����ʴ���� HP ������鹢ͧ������
        _hpIndicator.text = $"{playerScript.CurrentHealth}/{playerScript.maxHealth}";
    }

    void Update()
    {
        // ��Ǩ�ͺ����ա�õ�駤�� PlayerScript ��� healthBarFill ������� �������������ش��÷ӧҹ�ͧ�ѧ��ѹ
        if (playerScript == null || healthBarFill == null)
        {
            return;
        }

        // �ӹǳ��� fillValue ����Ҿ�ѧ���Ե�Ѩ�غѹ��ô��¾�ѧ���Ե�٧�ش ���Ƿ�����Ҷ١�ӡѴ������������ҧ 0 �֧ 1
        float fillValue = Mathf.Clamp01((float)playerScript.CurrentHealth / playerScript.maxHealth);

        // ��駤�� fillAmount �ͧ healthBarFill ��觨С�˹���Ҵ����ʴ��ͧᶺ���ʹ
        healthBarFill.fillAmount = fillValue;

        // �Ѿവ��ͤ���� _hpIndicator �����ʴ���� HP �Ѩ�غѹ�ͧ������
        _hpIndicator.text = $"{playerScript.CurrentHealth}/{playerScript.maxHealth}";
    }
}
