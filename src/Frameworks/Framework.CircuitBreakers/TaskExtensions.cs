﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Framework.CircuitBreaker.Exceptions;

namespace Framework.CircuitBreakers
{
    // The following code is taken from "Crafting a Task.TimeoutAfter Method" post by Joe Hoag
    // http://blogs.msdn.com/b/pfxteam/archive/2011/11/10/10235834.aspx
    public static class TaskExtensions
    {
        public static Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            // Short-circuit #1: infinite timeout or task already completed
            if (task.IsCompleted || (timeout == TimeSpan.MaxValue))
            {
                // Either the task has already completed or timeout will never occur.
                // No proxy necessary.
                return task;
            }

            // tcs.Task will be returned as a proxy to the caller
            TaskCompletionSource<TResult> tcs =
                new TaskCompletionSource<TResult>();

            // Short-circuit #2: zero timeout
            if (timeout == TimeSpan.Zero)
            {
                // We've already timed out.
                tcs.SetException(new CircuitBreakerTimeoutException());
                return tcs.Task;
            }

            // Set up a timer to complete after the specified timeout period
            Timer timer = new Timer(
                state =>
                {
                    // Recover your state information
                    var myTcs = (TaskCompletionSource<TResult>)state;

                    // Fault our proxy with a TimeoutException
                    myTcs.TrySetException(new CircuitBreakerTimeoutException());
                },
                tcs,
                timeout,
                Timeout.InfiniteTimeSpan);

            // Wire up the logic for what happens when source task completes
            task.ContinueWith(
                (antecedent, state) =>
                {
                    // Recover our state data
                    var tuple =
                        (Tuple<Timer, TaskCompletionSource<TResult>>)state;

                    // Cancel the Timer
                    tuple.Item1.Dispose();

                    // Marshal results to proxy
                    MarshalTaskResults(antecedent, tuple.Item2);
                },
                Tuple.Create(timer, tcs),
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);

            return tcs.Task;
        }

        public static Task TimeoutAfter(this Task task, TimeSpan timeout)
        {
            // Short-circuit #1: infinite timeout or task already completed
            if (task.IsCompleted || (timeout == TimeSpan.MaxValue))
            {
                // Either the task has already completed or timeout will never occur.
                // No proxy necessary.
                return task;
            }

            // tcs.Task will be returned as a proxy to the caller
            TaskCompletionSource<VoidTypeStruct> tcs =
                new TaskCompletionSource<VoidTypeStruct>();

            // Short-circuit #2: zero timeout
            if (timeout == TimeSpan.Zero)
            {
                // We've already timed out.
                tcs.SetException(new CircuitBreakerTimeoutException());
                return tcs.Task;
            }

            // Set up a timer to complete after the specified timeout period
            Timer timer = new Timer(
                state =>
                    {
                        // Recover your state information
                        var myTcs = (TaskCompletionSource<VoidTypeStruct>)state;

                        // Fault our proxy with a TimeoutException
                        myTcs.TrySetException(new CircuitBreakerTimeoutException());
                    },
                tcs,
                timeout,
                Timeout.InfiniteTimeSpan);

            // Wire up the logic for what happens when source task completes
            task.ContinueWith(
                (antecedent, state) =>
                    {
                        // Recover our state data
                        var tuple =
                            (Tuple<Timer, TaskCompletionSource<VoidTypeStruct>>)state;

                        // Cancel the Timer
                        tuple.Item1.Dispose();

                        // Marshal results to proxy
                        MarshalTaskResults(antecedent, tuple.Item2);
                    },
                Tuple.Create(timer, tcs),
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);

            return tcs.Task;
        }

        internal struct VoidTypeStruct
        {
        }

        internal static void MarshalTaskResults<TResult>(
            Task source,
            TaskCompletionSource<TResult> proxy)
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
                    Task<TResult> castedSource = source as Task<TResult>;
                    proxy.TrySetResult(
                        castedSource == null
                            ? default(TResult)
                            : // source is a Task
                            castedSource.Result); // source is a Task<TResult>
                    break;
            }
        }
    }
}