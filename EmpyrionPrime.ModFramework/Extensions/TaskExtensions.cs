using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmpyrionPrime.ModFramework.Extensions
{
    internal static class TaskExtensions
    {
        // From: https://devblogs.microsoft.com/pfxteam/crafting-a-task-timeoutafter-method/
        public static Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, int milliseconds)
        {
            if (task.IsCompleted || (milliseconds == Timeout.Infinite))
                return task;

            var tcs = new TaskCompletionSource<TResult>();

            if(milliseconds == 0)
            {
                tcs.SetException(new TimeoutException($"Waited 0ms"));
                return tcs.Task;
            }

            var timer = new Timer(state =>
            {
                var taskCompletionSource = (TaskCompletionSource<TResult>)state;

                taskCompletionSource.TrySetException(new TimeoutException($"Waited {milliseconds}ms"));
            }, tcs, milliseconds, Timeout.Infinite);

            task.ContinueWith((antecedent, state) =>
            {
                var tuple = (Tuple<Timer, TaskCompletionSource<TResult>>)state;
                tuple.Item1.Dispose();

                MarshalTaskResults(antecedent, tuple.Item2);
            }, 
            Tuple.Create(timer, tcs), 
            CancellationToken.None, 
            TaskContinuationOptions.ExecuteSynchronously, 
            TaskScheduler.Default);

            return tcs.Task;
        }

        public static Task TimeoutAfter(this Task task, int milliseconds)
        {
            return TimeoutAfter<VoidTypeStruct>(task as Task<VoidTypeStruct>, milliseconds);
        }

        private struct VoidTypeStruct { }

        private static void MarshalTaskResults<TResult>(Task source, TaskCompletionSource<TResult> proxy)
        {
            switch (source.Status)
            {
                case TaskStatus.Faulted:
                    proxy.TrySetException(source.Exception);
                    break;
                case TaskStatus.Canceled:
                    proxy.TrySetCanceled();
                    break;
                case TaskStatus.RanToCompletion:
                    if (source is Task<TResult> castedSource)
                        proxy.TrySetResult(castedSource.Result);
                    else
                        proxy.TrySetResult(default);
                    break;
            }
        }
    }
}
