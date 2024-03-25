using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Durability : MonoBehaviour
{
    public Image durabilityImage;
    public Image durabilityBgImage;
    public GameObject ArmorDurability;
    public float blinkInterval = 0.5f; // 빨간색으로 점멸하는 간격
    public float maxDurability = 100.0f; // 내구도의 최대값
    public float minDurability = 0.0f;   // 내구도의 최소값
    public float durabilityDecayRate = 1.0f;

    private Coroutine blinkCoroutine;
    public float curDurability = 100.0f;

    private Color originalColor;

    void Start()
    {
        curDurability = maxDurability; // 내구도 초기값을 최대값으로 설정
        originalColor = durabilityImage.color;
        // 초기 설정: 내구도 UI 숨김
        durabilityBgImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        durabilityImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); // 알파값을 0으로 설정하여 숨김
    }

    void Update()
    {
        // 내구도가 30% 이하일 때 내구도 UI 표시
        if (curDurability <= 0.3f * maxDurability)
        {
            if (blinkCoroutine == null)
            {
                // 빨간색으로 점멸하는 코루틴 시작
                durabilityBgImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
                blinkCoroutine = StartCoroutine(BlinkRed());
            }
        }
        else
        {
            // 빨간색 점멸 코루틴 종료
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
                // 빨간색 초기 색상으로 변경
                durabilityBgImage.color = originalColor;
            }
        }
        // 내구도 시간에 따라 감소
        curDurability -= durabilityDecayRate * Time.deltaTime;
        // 내구도가 최소값과 최대값 사이에 유지되도록 함
        curDurability = Mathf.Clamp(curDurability, minDurability, maxDurability);
        // 내구도 이미지 업데이트
        UpdateDurabilityImage();
        Debug.Log("Current Durability: " + curDurability);
    }

    void UpdateDurabilityImage()
    {
        // 내구도 이미지 업데이트
        float fillAmount = curDurability / maxDurability; // 내구도에 따른 이미지 채우기 정도 계산
        durabilityImage.fillAmount = fillAmount; // 이미지 채우기 정도 설정
    }

    IEnumerator BlinkRed()
    {
        Color originalColor = durabilityImage.color;

        while (true)
        {
            // 빨간색으로 변경
            durabilityImage.color = Color.red;
            yield return new WaitForSeconds(blinkInterval);
            // 원래 색상으로 변경
            durabilityImage.color = Color.white;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
