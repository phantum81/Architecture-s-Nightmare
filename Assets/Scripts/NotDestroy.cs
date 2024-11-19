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
        // 합이 k인 부분 수열을 찾고, 길이별로 사전 순 정렬하여 가장 짧은 수열 반환
        Dictionary<int, List<int>> result = Caculate(sequence, k);

        // 조건을 만족하는 부분 수열이 없을 경우 null 리턴
        if (result == null || result.Count == 0)
        {
            return new int[0];
        }

        // 가장 짧은 길이를 가진 부분 수열을 선택
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

            // sum이 k를 초과하면 left 포인터를 이동하여 줄임
            while (sum > k && left <= right)
            {
                sum -= sequence[left];
                left++;
            }

            // sum이 k와 같을 때 길이를 키로 하여 저장
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
