using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ConcurrentStackTest : MonoBehaviour
{
    private const int ThreadCount = 10;
    private const int ItemsPerThread = 10000;
    private const int ExpectedTotal = ThreadCount * ItemsPerThread;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(810, 10, 300, 500), "Test Normal Stack (Unsafe)"))
        {
            TestNormalStack();
        }

        if (GUI.Button(new Rect(810, 520, 300, 500), "Test Concurrent Stack (Safe)"))
        {
            TestConcurrentStack();
        }
    }

    private async void TestNormalStack()
    {
        Stack<int> stack = new Stack<int>();
        Task[] tasks = new Task[ThreadCount];

        Debug.Log($"[Unsafe] Start Push... (Expected: {ExpectedTotal})");

        try
        {
            for (int i = 0; i < ThreadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < ItemsPerThread; j++)
                    {
                        stack.Push(j);
                    }
                });
            }

            await Task.WhenAll(tasks);

            if (stack.Count != ExpectedTotal)
            {
                Debug.LogError($"[Unsafe] Data Lost or Corrupted! Result: {stack.Count} / {ExpectedTotal}");
            }
            else
            {
                Debug.Log($"[Unsafe] Lucky! Result: {stack.Count} / {ExpectedTotal}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[Unsafe] Exception occurred: {e.GetType().Name} - {e.Message}");
        }
    }

    private async void TestConcurrentStack()
    {
        ConcurrentStack<int> cStack = new ConcurrentStack<int>();
        Task[] tasks = new Task[ThreadCount];

        Debug.Log($"[Safe] Start Push... (Expected: {ExpectedTotal})");

        for (int i = 0; i < ThreadCount; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < ItemsPerThread; j++)
                {
                    cStack.Push(j);
                }
            });
        }

        await Task.WhenAll(tasks);

        if (cStack.Count == ExpectedTotal)
        {
            Debug.Log($"[Safe] Success! Result: {cStack.Count} / {ExpectedTotal}");
        }
        else
        {
            Debug.LogError($"[Safe] Something went wrong. Result: {cStack.Count} / {ExpectedTotal}");
        }
    }
}