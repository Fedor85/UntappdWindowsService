using System.ServiceModel;
using UntappdWindowsService.Extension.Interfaces;
using UntappdWindowsService.Extension.Services;

namespace UntappdWindowsService.Client
{
    public class UntappdWindowsServiceClient(string untappdWCFServiceUrl) : IUntappdWindowsServiceClient
    {
        private ChannelFactory<IClearTempContract> factory;

        public UntappdWindowsServiceClient(IConfigurationService configurationService) : this(configurationService.UntappdWCFServiceUrlFull) { }

        public UntappdWindowsServiceClient(): this( new ConfigurationService().UntappdWCFServiceUrlFull) {}

        public void SetTempFilesByProcessesId(int processeId, string tempFilesPath)
        {
            ChannelFactory<IClearTempContract> channelFactory = GetChannelFactory();
            IClearTempContract contractChannel = channelFactory.CreateChannel();
            IClientChannel clientChannel = contractChannel as IClientChannel;
            clientChannel.Open();

            contractChannel.RegisterProcessesIdByTempFiles(processeId, tempFilesPath);

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
