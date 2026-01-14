using UnityEngine;

public class GobalRefences : MonoBehaviour
{
    //static: Thuộc về class, không thuộc riêng object nào
    // có thẻ gọi bất kì script nào : GobalRefences.Instance
    public static GobalRefences Instance { get; set; }
    public GameObject bulletImpactEffectPrefab;

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
