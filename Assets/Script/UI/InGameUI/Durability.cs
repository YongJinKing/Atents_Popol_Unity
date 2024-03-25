using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Durability : MonoBehaviour
{
    public Image durabilityImage;
    public Image durabilityBgImage;
    public GameObject ArmorDurability;
    public float blinkInterval = 0.5f; // ���������� �����ϴ� ����
    public float maxDurability = 100.0f; // �������� �ִ밪
    public float minDurability = 0.0f;   // �������� �ּҰ�
    public float durabilityDecayRate = 1.0f;

    private Coroutine blinkCoroutine;
    public float curDurability = 100.0f;

    private Color originalColor;

    void Start()
    {
        curDurability = maxDurability; // ������ �ʱⰪ�� �ִ밪���� ����
        originalColor = durabilityImage.color;
        // �ʱ� ����: ������ UI ����
        durabilityBgImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        durabilityImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); // ���İ��� 0���� �����Ͽ� ����
    }

    void Update()
    {
        // �������� 30% ������ �� ������ UI ǥ��
        if (curDurability <= 0.3f * maxDurability)
        {
            if (blinkCoroutine == null)
            {
                // ���������� �����ϴ� �ڷ�ƾ ����
                durabilityBgImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
                blinkCoroutine = StartCoroutine(BlinkRed());
            }
        }
        else
        {
            // ������ ���� �ڷ�ƾ ����
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
                // ������ �ʱ� �������� ����
                durabilityBgImage.color = originalColor;
            }
        }
        // ������ �ð��� ���� ����
        curDurability -= durabilityDecayRate * Time.deltaTime;
        // �������� �ּҰ��� �ִ밪 ���̿� �����ǵ��� ��
        curDurability = Mathf.Clamp(curDurability, minDurability, maxDurability);
        // ������ �̹��� ������Ʈ
        UpdateDurabilityImage();
        Debug.Log("Current Durability: " + curDurability);
    }

    void UpdateDurabilityImage()
    {
        // ������ �̹��� ������Ʈ
        float fillAmount = curDurability / maxDurability; // �������� ���� �̹��� ä��� ���� ���
        durabilityImage.fillAmount = fillAmount; // �̹��� ä��� ���� ����
    }

    IEnumerator BlinkRed()
    {
        Color originalColor = durabilityImage.color;

        while (true)
        {
            // ���������� ����
            durabilityImage.color = Color.red;
            yield return new WaitForSeconds(blinkInterval);
            // ���� �������� ����
            durabilityImage.color = Color.white;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
