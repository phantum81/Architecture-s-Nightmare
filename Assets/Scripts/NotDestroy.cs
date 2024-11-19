using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NotDestroy : MonoBehaviour
{
    private static NotDestroy instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
            
        }
    }
    void Start()
    {
       
    }


    public int[] Sol(int[] sequence, int k)
    {
        // ���� k�� �κ� ������ ã��, ���̺��� ���� �� �����Ͽ� ���� ª�� ���� ��ȯ
        Dictionary<int, List<int>> result = Caculate(sequence, k);

        // ������ �����ϴ� �κ� ������ ���� ��� null ����
        if (result == null || result.Count == 0)
        {
            return new int[0];
        }

        // ���� ª�� ���̸� ���� �κ� ������ ����
        List<int> re = result.OrderBy(p => p.Key).First().Value;

        return new int[] { re[0], re[1] };
    }

    public Dictionary<int, List<int>> Caculate(int[] sequence, int k)
    {
        int left = 0, sum = 0;
        Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();

        for (int right = 0; right < sequence.Length; right++)
        {
            sum += sequence[right];

            // sum�� k�� �ʰ��ϸ� left �����͸� �̵��Ͽ� ����
            while (sum > k && left <= right)
            {
                sum -= sequence[left];
                left++;
            }

            // sum�� k�� ���� �� ���̸� Ű�� �Ͽ� ����
            if (sum == k)
            {
                int length = right - left + 1;
                if (!dic.ContainsKey(length))
                {
                    dic[length] = new List<int> { left, right };
                }
            }
        }

        return dic;
    }
}
