using UnityEngine;

/**
 * 表示向日葵类，继承自Plant类。向日葵能够产生阳光。
 */
public class Sunflower : Plant
{
    public float produceDuration = 5; // 向日葵产生阳光的周期，单位为秒
    private float produceTimer = 0; // 用于追踪向日葵产生阳光的计时器
    private Animator anim; // 向日葵的动画控制器，用于控制动画状态

    public GameObject sunPrefab; // 阳光粒子系统的预设对象，即产生阳光时的特效

    public float jumpMinDistance = 0.3f; // 阳光跳跃的最小距离
    public float jumpMaxDistance = 2; // 阳光跳跃的最大距离

    // 在对象初始化时调用，用于获取向日葵的动画控制器
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /**
     * 当向日葵处于激活状态时，每帧调用此方法。用于控制阳光的产生。
     */
    protected override void EnableUpdate()
    {
        produceTimer += Time.deltaTime;

        // 当产生阳光的计时器超过设定周期时，触发产生阳光的动画
        if (produceTimer > produceDuration)
        {
            produceTimer = 0;
            anim.SetTrigger("IsGlowing");
        }
    }

    /**
     * 用于触发向日葵产生阳光的效果。
     */
    public void ProduceSun()
    { 
        // 在向日葵位置处实例化阳光粒子系统对象
        GameObject go = GameObject.Instantiate(sunPrefab, transform.position, Quaternion.identity);

        // 随机生成阳光跳跃的距离
        float distance = Random.Range(jumpMinDistance, jumpMaxDistance);
        // 随机决定阳光跳跃的方向（正面或反面）
        distance = Random.Range(0, 2) < 1 ? -distance : distance;
        // 计算阳光跳跃后的最终位置
        Vector3 position = transform.position;
        position.x += distance;

        // 调整阳光对象的位置，使其跳出向日葵，并播放跳跃动画
        go.GetComponent<Sun>().JumpTo(position);
    }
}
