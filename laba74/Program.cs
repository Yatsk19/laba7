using System;
using System.Collections.Generic;
using System.Linq;

public class TaskScheduler<TTask, TPriority>
{
    private SortedList<TPriority, Queue<TTask>> taskQueue = new SortedList<TPriority, Queue<TTask>>();

    public void AddTask(TTask task, TPriority priority)
    {
        if (!taskQueue.ContainsKey(priority))
        {
            taskQueue[priority] = new Queue<TTask>();
        }
        taskQueue[priority].Enqueue(task);
    }

    public TTask ExecuteNext(TaskExecution<TTask> executionDelegate)
    {
        if (taskQueue.Count == 0)
        {
            throw new InvalidOperationException("No tasks to execute.");
        }

        TPriority highestPriority = taskQueue.Keys.First();
        Queue<TTask> highestPriorityQueue = taskQueue[highestPriority];

        if (highestPriorityQueue.Count == 0)
        {
            taskQueue.Remove(highestPriority);
            return ExecuteNext(executionDelegate);
        }

        TTask task = highestPriorityQueue.Dequeue();
        executionDelegate(task);
        return task;
    }
}

public delegate void TaskExecution<TTask>(TTask task);
