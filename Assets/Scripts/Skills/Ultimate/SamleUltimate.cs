using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class SamleUltimate : MonoBehaviour
{
    public delegate void EffectHandler();
    public event EffectHandler EffectNotify;

    public abstract int TotalCooldown { get; set; }
    protected abstract void Effect(); // для предмета Comet
    public  int NowCooldown { get; set; }

    protected Slider slider;

    private Button button;

    protected virtual void Start()
    {
        button = gameObject.GetComponent<Button>();
        slider = gameObject.transform.GetChild(0).GetComponent<Slider>();
    }
    //Метод для кнопки
    public void ActivateUltimate()
    {
        EffectNotify?.Invoke();
        button.interactable = false;
        NowCooldown = TotalCooldown;
        Effect();
        StartCoroutine(CooldownVisualizer());
    }
    // Метод для вообзобновления перезарядки после сейва
    public void ActivateUltimate(int nowCooldown)
    {
        Start();
        button.interactable = false;
        NowCooldown = nowCooldown;
        StartCoroutine(CooldownVisualizer());
    }

    public void ReduceCooldown(int howMuch)
    {
        if (NowCooldown > 0)
        {
            NowCooldown -= howMuch;
            RenderSlider();
        }
    }

    private void RenderSlider()
    {
        slider.value = (float)NowCooldown / (float)TotalCooldown * 100;
    }

    IEnumerator CooldownVisualizer()
    {
        while (NowCooldown > 0)
        {
            RenderSlider();
            NowCooldown--;

            yield return new WaitForSeconds(1f);
        }
        button.interactable = true;
        yield break;
    }
}
