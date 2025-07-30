using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StyleUIManager : MonoBehaviour
{
    public Slider styleBar;
    public TextMeshProUGUI styleRankText;

    void Update()
    {
        if (ScoreManager.Instance == null) return;

        styleBar.value = ScoreManager.Instance.style;
        styleRankText.text = ScoreManager.Instance.GetStyleRank();
    }
}
