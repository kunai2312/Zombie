using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager Instance { get; set; }
    public TextMeshProUGUI ammoDisplay;

    private void Awake()
    {
        //kt đã object  chưa và kt object phải của mình kh
        // tránh việc 2 object
        if (Instance != null && Instance != this)
        {
            //phát hiện 2 object thì hủy cái tạo sau
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
