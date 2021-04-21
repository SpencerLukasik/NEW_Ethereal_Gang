using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerConnection : NetworkBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject SpawnPoint1;
    void Start()
    {
        //if (isLocalPlayer == false)
        //    return;
        ServerSpawnMyUnit();
        transform.parent = GameObject.Find("Players").transform;
        transform.parent.GetComponent<PlayerInfo>().increment();
    }

    GameObject myPlayerUnit;
    Rikayon myUnitScripts;
    HealthManager myUnitHealth;

    [Server]
    void ServerSpawnMyUnit()
    {
        myPlayerUnit = Instantiate(PlayerPrefab);
        NetworkServer.Spawn(myPlayerUnit);
        myPlayerUnit.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        myPlayerUnit.transform.position = SpawnPoint1.transform.position;
        myPlayerUnit.transform.parent = transform;
        myUnitHealth = myPlayerUnit.GetComponent<HealthManager>();
        myUnitScripts = myPlayerUnit.GetComponent<Rikayon>();
    }

    [Server]
    public void ServerUpdateAnimation(string animation)
    {
        myUnitScripts.animator.SetTrigger(animation);
    }

    [Server]
    public void ServerSpawn(GameObject tempObject)
    {
        NetworkServer.Spawn(tempObject);
    }

    [Server]
    public void ServerDestroy(GameObject tempObject)
    {
        NetworkServer.Destroy(tempObject);
    }

    [Server]
    public void ServerAddHealth()
    {
        myUnitHealth.maxHealth += 3f;
        myUnitHealth.curHealth += 5f;
        if (myUnitHealth.curHealth > myUnitHealth.maxHealth)
            myUnitHealth.curHealth = myUnitHealth.maxHealth;
        myUnitHealth.UIhealthBar.setMaxHealth(myUnitHealth.maxHealth, myUnitHealth.curHealth);
        RpcShowChanges();
    }

    [Server]
    public void ServerTakeDamage(float damage)
    {
        myUnitHealth.curHealth -= damage;
        myUnitHealth.UIhealthBar.takeDamage(damage);
        RpcShowChanges();
    }

    [Client]
    private void RpcShowChanges()
    {
        myUnitHealth.fade.PopIn();
    }

    [Server]
    public void ServerKillMyUnit()
    {
        ServerUpdateAnimation("Die");
        myUnitScripts.enabled = false;
    }

    [Client]
    public void ClientActivateDeathCam()
    {
        myPlayerUnit.transform.GetChild(4).gameObject.SetActive(false);
        myPlayerUnit.transform.GetChild(5).gameObject.SetActive(false);
        myPlayerUnit.transform.GetChild(6).gameObject.SetActive(true);
    }

    [Server]
    public void ServerKillBean(GameObject bean)
    {
        bean.GetComponent<BeanBehavior>().animator.SetBool("isDead", true);
        bean.GetComponent<BeanBehavior>().alive = false;
    }

    private IEnumerator DelayRun(GameObject obj, float time)
    {
        float i = 0;
        while (i < time)
        {
            i += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        ServerDestroy(obj);
    }

    [Server]
    public void ServerEatBean(GameObject bean)
    {
        bean.GetComponent<BeanBehavior>().animator.SetBool("isEaten", true);
		ServerUpdateAnimation("Eat_Cycle_1");
        StartCoroutine(DelayRun(bean, 2f));
    }

    [Server]
    public void ServerImplant(GameObject bean, GameObject spine)
    {
        spine.transform.parent = bean.transform;
        spine.GetComponent<Spine>().active = false;
    }
}