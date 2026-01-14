using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SoundManager Instance { get; set; }
    public AudioSource shootingAk47;

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
