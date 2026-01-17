using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SoundManager Instance { get; set; }
    public AudioSource shootingChannel;
    public AudioClip shootingPistol;
    public AudioClip shootingAk47;
    public AudioSource reloadAk47;
    public AudioSource reloadPistol;
    public AudioSource emptyMagazine;

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
    public void ShootingSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            case Weapon.WeaponModel.Pistol:
                shootingChannel.PlayOneShot(shootingPistol);
                break;
            case Weapon.WeaponModel.Ak47:
                shootingChannel.PlayOneShot(shootingAk47);
                break;
        }


    }
    public void ReloadSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            case Weapon.WeaponModel.Pistol:
                reloadPistol.Play();
                break;
            case Weapon.WeaponModel.Ak47:
                reloadAk47.Play();
                break;
        }
    }
}
