using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * �ֲ���������������ƺ͹�����Ϸ�е�ֲ����á�
 */
public class HandManager : MonoBehaviour
{
    // ����ģʽ������ȷ����Ϸ������ֻ��һ��HandManagerʵ��
    public static HandManager Instance { get; private set; }

    // �洢����ֲ��Ԥ������б�
    public List<Plant> plantPrefabList;

    // ��ǰ��ѡ�е�ֲ��Ԥ����ʵ��
    private Plant currentPlant;

    // �ڶ�����ʱ��ʼ������
    private void Awake()
    {
        Instance = this;
    }

    // ÿ֡���£�ʹ��ǰֲ��������ָ��
    private void Update()
    {
        FollowCursor();
    }

    /**
     * �������һ��ֲ�ﵽ��ǰ�����С�
     * @param plantType ֲ�����͡�
     * @return ����ɹ����ֲ�ﷵ��true�����򷵻�false��
     */
    public bool AddPlant(PlantType plantType)
    {
        // ����Ѿ���ֲ����ڣ����������ֲ��
        if (currentPlant != null) return false;

        // ���Ի�ȡ��Ӧֲ�����͵�ֲ��Ԥ����
        Plant plantPrefab = GetPlantPrefab(plantType);
        if (plantPrefab == null)
        {
            // ����޷���ȡֲ��Ԥ���壬���ӡ������Ϣ������false
            print("��Ҫ��ֲ��Ԥ���岻����"); return false;
        }
        // ʵ����ֲ�ﲢ����Ϊ��ǰֲ��
        currentPlant = GameObject.Instantiate(plantPrefab);
        return true;
    }

    /**
     * ����ֲ�����ʹ�ֲ��Ԥ�����б��л�ȡ��Ӧ��ֲ��Ԥ���塣
     * @param plantType ֲ�����͡�
     * @return �ҵ���ֲ��Ԥ���壬���δ�ҵ��򷵻�null��
     */
    private Plant GetPlantPrefab(PlantType plantType)
    {
        foreach(Plant plant in plantPrefabList)
        {
            if (plant.plantType == plantType)
            {
                return plant;
            }
        }
        return null;
    }

    // ʹ��ǰֲ��λ�ø������ָ��
    void FollowCursor()
    {
        // ���û��ѡ�е�ֲ���ִ���κβ���
        if (currentPlant == null) return;

        // �����������������ϵ�е�λ�ã������õ�ǰֲ���λ��
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        currentPlant.transform.position = mouseWorldPosition;

    }

    /**
     * ����ҵ����Ԫ��ʱ���ã�������ָ����Ԫ����ӵ�ǰֲ�
     * @param cell ������ĵ�Ԫ��
     */
    public void OnCellClick(Cell cell)
    {
        // ���û��ѡ�е�ֲ���ִ���κβ���
        if (currentPlant == null) return;

        // �����ڵ�Ԫ�������ֲ��
        bool isSuccess = cell.AddPlant(currentPlant);

        // �����ӳɹ����������ǰֲ���������Ч
        if (isSuccess)
        {
            currentPlant = null;
            AudioManager.Instance.PlayClip(Config.plant);
        }
    }

}
