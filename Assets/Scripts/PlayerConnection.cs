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
        CmdSpawnMyUnit();
        transform.parent = GameObject.Find("Players").transform;
        transform.parent.GetComponent<PlayerInfo>().increment();
    }

    GameObject myPlayerUnit;
    HealthManager myUnitHealth;

    [Server]
    void CmdSpawnMyUnit()
    {
        myPlayerUnit = Instantiate(PlayerPrefab);
        NetworkServer.Spawn(myPlayerUnit);
        myPlayerUnit.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        myPlayerUnit.transform.position = SpawnPoint1.transform.position;
        myPlayerUnit.transform.parent = transform;
        myUnitHealth = myPlayerUnit.GetComponent<HealthManager>();
    }

    [Server]
    public void ServerSpawnSpine(GameObject tempObject)
    {
        NetworkServer.Spawn(tempObject);
    }

    [Server]
    public void ServerDestroySpine(GameObject tempObject)
    {
        NetworkServer.Destroy(tempObject);
    }

    [Server]
    public void CmdAddHealth()
    {
        myUnitHealth.maxHealth += 3f;
        myUnitHealth.curHealth += 5f;
        if (myUnitHealth.curHealth > myUnitHealth.maxHealth)
            myUnitHealth.curHealth = myUnitHealth.maxHealth;
        myUnitHealth.UIhealthBar.setMaxHealth(myUnitHealth.maxHealth, myUnitHealth.curHealth);
        RpcShowChanges();
    }

    [Server]
    public void CmdTakeDamage(float damage)
    {
        myUnitHealth.curHealth -= damage;
        myUnitHealth.UIhealthBar.takeDamage(damage);
        RpcShowChanges();
    }

    [ClientRpc]
    private void RpcShowChanges()
    {
        myUnitHealth.fade.PopIn();
    }

    [Server]
    public void CmdKillMyUnit()
    {
        myPlayerUnit.GetComponent<Rikayon>().animator.SetTrigger("Die");
        myPlayerUnit.GetComponent<Rikayon>().enabled = false;
    }

    [ClientRpc]
    public void RpcActivateDeathCam()
    {
        myPlayerUnit.transform.GetChild(6).gameObject.SetActive(true);
    }

    [Server]
    public void CmdKillBean(GameObject bean)
    {
        bean.GetComponent<BeanBehavior>().animator.SetBool("isDead", true);
        bean.GetComponent<BeanBehavior>().alive = false;
    }

    [Server]
    public void CmdImplant(GameObject bean, GameObject spine)
    {
        spine.transform.parent = bean.transform;
        spine.GetComponent<Spine>().active = false;
    }
}