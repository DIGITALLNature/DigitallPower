// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using Microsoft.Xrm.Sdk;

namespace dgt.power.common.tests.Logic;

internal sealed class SdkTestTransport : TransportBindingElement
{
    internal int Requests { get; private set; }

    public override string Scheme => "https";

    public override BindingElement Clone() => this;

    public override T? GetProperty<T>(BindingContext context) where T : class =>
        context.GetInnerProperty<T>();

    public override bool CanBuildChannelFactory<TChannel>(BindingContext context) =>
        typeof(TChannel) == typeof(IRequestChannel);

    public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context) =>
        (IChannelFactory<TChannel>)(object)new Factory(this);

    private sealed class Factory(SdkTestTransport transport) : ChannelFactoryBase<IRequestChannel>
    {
        protected override IRequestChannel OnCreateChannel(EndpointAddress address, Uri via) =>
            new Channel(this, transport, address, via);

        protected override void OnOpen(TimeSpan timeout) { }
        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state) =>
            Complete(null, callback, state);
        protected override void OnEndOpen(IAsyncResult result) { }
    }

    private sealed class Channel(ChannelManagerBase manager, SdkTestTransport transport,
        EndpointAddress address, Uri via) : ChannelBase(manager), IRequestChannel
    {
        public EndpointAddress RemoteAddress => address;
        public Uri Via => via;

        public Message Request(Message message) => Request(message, DefaultSendTimeout);

        public Message Request(Message message, TimeSpan timeout)
        {
            transport.Requests++;
            return Message.CreateMessage(message.Version,
                "http://schemas.microsoft.com/xrm/2011/Contracts/Services/IOrganizationService/ExecuteResponse",
                new ResponseBody());
        }

        public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state) =>
            BeginRequest(message, DefaultSendTimeout, callback, state);
        public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state) =>
            Complete(Request(message, timeout), callback, state);
        public Message EndRequest(IAsyncResult result) => ((Task<Message>)result).GetAwaiter().GetResult();
        protected override void OnAbort() { }
        protected override void OnClose(TimeSpan timeout) { }
        protected override void OnOpen(TimeSpan timeout) { }
        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state) =>
            Complete(null, callback, state);
        protected override void OnEndClose(IAsyncResult result) { }
        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state) =>
            Complete(null, callback, state);
        protected override void OnEndOpen(IAsyncResult result) { }
    }

    private sealed class ResponseBody() : BodyWriter(isBuffered: true)
    {
        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            const string serviceNamespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services";
            writer.WriteStartElement("ExecuteResponse", serviceNamespace);
            new DataContractSerializer(typeof(OrganizationResponse), "ExecuteResult", serviceNamespace)
                .WriteObject(writer, new OrganizationResponse());
            writer.WriteEndElement();
        }
    }

    private static Task<Message> Complete(Message? message, AsyncCallback callback, object state)
    {
        var completion = new TaskCompletionSource<Message>(state);
        completion.SetResult(message!);
        callback?.Invoke(completion.Task);
        return completion.Task;
    }
}
