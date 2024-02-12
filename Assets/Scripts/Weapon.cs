using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 子弹的预制体
    public GameObject prefabBullet;

    // 几种武器的CD时间长度
    public float pistolFireCD = 0.2f;
    public float shotgunFireCD = 0.5f;
    public float rifleCD = 0.1f;

    // 上次开火时间
    float lastFireTime;

    // 当前使用哪种武器
    public int curGun { get; private set; }     // 0 手枪，  1 散弹枪   2 自动步枪

    // 开火函数，由角色脚本调用
    // keyDown代表这一帧按下了开火键，keyPressed代表开火键正在持续按下
    // 这样区分是为了实现 手枪 和 自动步枪 的不同手感
    public void Fire(bool keyDown, bool keyPressed, UnityEngine.Color color)
    {
        // 根据当前武器，调用对应的开火函数
        switch (curGun)
        {
            case 0:
                if (keyDown)
                {
                    PistolFire(color);
                }
                break;
            case 1:
                if (keyDown)
                {
                    shotgunFire(color);
                }
                break;
            case 2:
                if (keyPressed)
                {
                    RifleFire(color);
                }
                break;
        }
    }

    // 更换武器
    public int Change(int weaponAvailable)
    {
        curGun += 1;
        if (curGun == weaponAvailable + 1)
        {
            curGun = 0;
        }
        return curGun;
    }

    // 手枪射击专用函数
    public void PistolFire(UnityEngine.Color color)
    {
        if (lastFireTime + pistolFireCD > Time.time)
        {
            return;
        }
        lastFireTime = Time.time;

        GameObject bullet = Instantiate(prefabBullet, null);
        bullet.transform.position = transform.position + transform.forward * 1.0f;
        bullet.transform.forward = transform.forward;

        // Change the color of the bullet
        MeshRenderer renderer = bullet.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }

    // 自动步枪射击专用函数
    public void RifleFire(UnityEngine.Color color)
    {
        if (lastFireTime + rifleCD > Time.time)
        {
            return;
        }
        lastFireTime = Time.time;

        GameObject bullet = Instantiate(prefabBullet, null);
        bullet.transform.position = transform.position + transform.forward * 1.0f;
        bullet.transform.forward = transform.forward;
        // Change the color of the bullet
        MeshRenderer renderer = bullet.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }

    // 散弹枪射击专用函数
    public void shotgunFire(UnityEngine.Color color)
    {
        if (lastFireTime + shotgunFireCD > Time.time)
        {
            return;
        }
        lastFireTime = Time.time;

        // 创建5颗子弹，相互间隔10度，分布于前方扇形区域
        for (int i = -2; i <= 2; i++)
        {
            GameObject bullet = Instantiate(prefabBullet, null);
            Vector3 dir = Quaternion.Euler(0, i * 10, 0) * transform.forward;

            bullet.transform.position = transform.position + dir * 1.0f;
            bullet.transform.forward = dir;
            MeshRenderer renderer = bullet.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
            Bullet b = bullet.GetComponent<Bullet>();
            b.lifeTime = 0.3f;
        }

    }
}
