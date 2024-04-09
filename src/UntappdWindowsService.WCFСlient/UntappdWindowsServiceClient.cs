using System.ServiceModel;
using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Extension.Services;

namespace UntappdWindowsService.Сlient
{
    public class UntappdWindowsServiceClient(string untappdWCFServiceUrl) : IUntappdWindowsServiceClient
    {
        private ChannelFactory<IClearTempContract> factory;

        public UntappdWindowsServiceClient(): this( new ConfigurationService().UntappdWCFServiceUrlFull) {}

        public void SetTempFilesByProcessesId(int processeId, string tempFilesPath)
        {
            ChannelFactory<IClearTempContract> channelFactory = GetChannelFactory();
            IClearTempContract channel = channelFactory.CreateChannel();
            IClientChannel clientChannel = channel as IClientChannel;
            clientChannel.Open();

            channel.RegisterProcessesIdByTempFiles(processeId, tempFilesPath);

            clientChannel.Close();
        }

        private ChannelFactory<IClearTempContract> GetChannelFactory()
        {
            if (factory == null)
            {
                factory = new(new BasicHttpBinding(), new EndpointAddress(untappdWCFServiceUrl));
                factory.Open();
            }
            return factory;
        }
    }
}
