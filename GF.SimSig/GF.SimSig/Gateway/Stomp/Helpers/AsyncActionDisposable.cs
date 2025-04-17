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
using System.Threading;
using System.Threading.Tasks;

namespace GF.SimSig.Gateway.Stomp.Helpers
{
    /// <summary>
	/// IAsyncDisposable implementation that calls a custom action when it is disposed.
	/// </summary>
    internal class AsyncActionDisposable : IAsyncDisposable
    {
        private readonly Func<CancellationToken, Task> _action;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="action">Action to be invoked when this instance is disposed.</param>
        public AsyncActionDisposable(Func<CancellationToken, Task> action)
        {
            _action = action;
        }
        
        public void Dispose()
        {
            DisposeAsync(CancellationToken.None).Wait();
        }

        public Task DisposeAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => _action(cancellationToken), cancellationToken);
        }
    }
}
