﻿// Copyright 2007-2016 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Testes
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using GreenPipes;


    public abstract class BusTest :
        IBusTest
    {
        readonly IBusControl _busControl;
        ISendEndpoint _endpoint;

        protected BusTest(IBusControl busControl)
        {
            _busControl = busControl;
        }

        protected abstract Uri InputQueueAddress { get; }

        public async Task Start()
        {
            await _busControl.StartAsync().ConfigureAwait(false);

            _endpoint = await _busControl.GetSendEndpoint(InputQueueAddress).ConfigureAwait(false);
        }

        public async Task Stop()
        {
            await _busControl.StopAsync().ConfigureAwait(false);
        }

        ConnectHandle ISendObserverConnector.ConnectSendObserver(ISendObserver observer)
        {
            if (_endpoint == null)
                throw new InvalidOperationException("The test must be started before use");

            return _endpoint.ConnectSendObserver(observer);
        }

        Task ISendEndpoint.Send<T>(T message, CancellationToken cancellationToken)
        {
            if (_endpoint == null)
                throw new InvalidOperationException("The test must be started before use");

            return _endpoint.Send(message, cancellationToken);
        }

        Task ISendEndpoint.Send<T>(T message, IPipe<SendContext<T>> pipe, CancellationToken cancellationToken)
        {
            if (_endpoint == null)
                throw new InvalidOperationException("The test must be started before use");

            return _endpoint.Send(message, pipe, cancellationToken);
        }

        Task ISendEndpoint.Send<T>(T message, IPipe<SendContext> pipe, CancellationToken cancellationToken)
        {
            if (_endpoint == null)
                throw new InvalidOperationException("The test must be started before use");

            return _endpoint.Send(message, pipe, cancellationToken);
        }

        Task ISendEndpoint.Send(object message, CancellationToken cancellationToken)
        {
            if (_endpoint == null)
                throw new InvalidOperationException("The test must be started before use");

            return _endpoint.Send(message, cancellationToken);
        }

        Task ISendEndpoint.Send(object message, Type messageType, CancellationToken cancellationToken)
        {
            if (_endpoint == null)
                throw new InvalidOperationException("The test must be started before use");

            return _endpoint.Send(message, messageType, cancellationToken);
        }

        Task ISendEndpoint.Send(object message, IPipe<SendContext> pipe, CancellationToken cancellationToken)
        {
            if (_endpoint == null)
                throw new InvalidOperationException("The test must be started before use");

            return _endpoint.Send(message, pipe, cancellationToken);
        }

        Task ISendEndpoint.Send(object message, Type messageType, IPipe<SendContext> pipe, CancellationToken cancellationToken)
        {
            if (_endpoint == null)
                throw new InvalidOperationException("The test must be started before use");

            return _endpoint.Send(message, messageType, pipe, cancellationToken);
        }

        Task ISendEndpoint.Send<T>(object values, CancellationToken cancellationToken)
        {
            if (_endpoint == null)
                throw new InvalidOperationException("The test must be started before use");

            return _endpoint.Send<T>(values, cancellationToken);
        }

        Task ISendEndpoint.Send<T>(object values, IPipe<SendContext<T>> pipe, CancellationToken cancellationToken)
        {
            if (_endpoint == null)
                throw new InvalidOperationException("The test must be started before use");

            return _endpoint.Send(values, pipe, cancellationToken);
        }

        Task ISendEndpoint.Send<T>(object values, IPipe<SendContext> pipe, CancellationToken cancellationToken)
        {
            if (_endpoint == null)
                throw new InvalidOperationException("The test must be started before use");

            return _endpoint.Send<T>(values, pipe, cancellationToken);
        }
    }
}