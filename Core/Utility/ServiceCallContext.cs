using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Development.Utility
{
    public sealed class ServiceCallContext<TChannel> : IDisposable {
        readonly ChannelFactory<TChannel> channelFactory;
        readonly TChannel channel;

        public TChannel Channel { get { return channel; } }

        public ServiceCallContext(CookieContainer cookieContainer, EndpointAddress remoteAddress) {
            if(cookieContainer == null)
                throw new ArgumentNullException("cookieContainer");
            if(remoteAddress == null)
                throw new ArgumentNullException("remoteAddress");

            BasicHttpBinding binding = new BasicHttpBinding() { AllowCookies = true };
            channelFactory = new ChannelFactory<TChannel>(binding, remoteAddress);
            channel = channelFactory.CreateChannel();
            //System.ServiceModel.Channels.IHttpCookieContainerManager s = new System.ServiceModel.Channels.IHttpCookieContainerManager();
            //channelFactory.GetProperty<IHttpCookieContainerManager>().CookieContainer = cookieContainer;
            channelFactory.GetProperty<IHttpCookieContainerManager>().CookieContainer = cookieContainer;

        }

        public void Dispose() {
            ((IDisposable)channelFactory).Dispose();
        }
    }
}
