using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour
{
    public int ItemType;
    Player playerScript;
    public AudioClip TriggerItemSFX;
    public GameObject NoHitEffectUI;

    private void Update()
    {
        transform.position += Vector3.down * 3 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.SFXPlay("ItemSFX", TriggerItemSFX);

            playerScript = collision.GetComponent<Player>();

            transform.position = new Vector3(1000, 1000, 100);

            switch(ItemType)
            {
                case 0: // Upgrade
                    StartCoroutine(Player.instance.ShowMaxLevelAction());

                    if(Player.PlayerLevel >= 4)
                    {
                        print("Max");
                        DataManager.CurScore += 50;
                        Destroy(gameObject);
                        return;
                    }

                    Player.PlayerLevel += 1;
                    Destroy(gameObject);

                    break;
                case 1: // NoHit
                    StartCoroutine(NoHitAction());
                    break;
                case 2: // AddHp
                    playerScript.HP.value += 10;
                    Destroy(gameObject);
                    break;
                case 3: // AddEngine
                    playerScript.Engine.value = playerScript.Engine.maxValue;
                    Destroy(gameObject);
                    break;
                case 4: // 


                    break;
            }
        }
    }

    IEnumerator NoHitAction()
    {
        NoHitEffectUI = GameObject.Find("Canvas").transform.Find("NoHitEffect").gameObject;

        print("公利");
        NoHitEffectUI.SetActive(true);
        playerScript.gameObject.layer = 31;
        yield return new WaitForSeconds(2f);
        playerScript.gameObject.layer = 6;
        print("公利 秦力");
        NoHitEffectUI.SetActive(false);
        Destroy(gameObject);
    }

    

}
