﻿/*******************************************************************************
 *
 *  Copyright (c) 2014 Carlos Campo <carlos@campo.com.co>
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 * 
 *******************************************************************************/
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GF.SimSig.Gateway.Stomp.Helpers
{
	/// <summary>
	/// Class to execute tasks serially.
	/// </summary>
    internal class SerialTaskExecuter<T> : ITaskExecuter<T> where T : class 
	{
        private readonly LinkedList<Func<Task>> _queue;

        public SerialTaskExecuter()
        {
            _queue = new LinkedList<Func<Task>>();
        }

        public Task<T> Execute(Func<Task<T>> task, CancellationToken cancellationToken)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return Execute(task as Func<Task>, cancellationToken) as Task<T>;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public Task Execute(Func<Task> task, CancellationToken cancellationToken)
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            
            Func<Task> nextTask =
                () =>
                    Task.Run(task, cancellationToken).ContinueWith(
                        awaitedTask =>
                        {
                            tcs.TrySet(awaitedTask);
                            Monitor.Enter(_queue);
                            _queue.RemoveFirst();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                            Func<Task> next = _queue.FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                            Monitor.Exit(_queue);
                            if(next != null) _ = next();
                        });


            Monitor.Enter(_queue);
            _queue.AddLast(nextTask);
            bool start = _queue.Count == 1;
            Monitor.Exit(_queue);
            if (start) nextTask();

            return tcs.Task;
        }
    }

    /// <summary>
    /// Class to execute tasks serially.
    /// </summary>
    internal class SerialTaskExecuter : SerialTaskExecuter<object>
    {

    }

    /// <summary>
    /// TaskCompletionSource extensions.
    /// </summary>
    internal static class TaskCompletionSourceExtensions
    {
        public static void TrySet<T>(this TaskCompletionSource<T> tcs, Task task) where T : class 
        {
            if (task.IsFaulted)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                tcs.TrySetException(task.Exception.InnerExceptions);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            else if (task.IsCanceled)
            {
                tcs.TrySetCanceled();
            }
            else
            {
#pragma warning disable CS8604 // Possible null reference argument.
                tcs.TrySetResult(task is not Task<T> taskWithResult ? null : taskWithResult.Result);
#pragma warning restore CS8604 // Possible null reference argument.
            }
        }
    }
}
