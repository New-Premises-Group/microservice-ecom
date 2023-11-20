using IW.Authentication;
using Microsoft.Extensions.Options;

namespace IW.Configurations
{
    public class RabbitMqOptionSetup : IConfigureOptions<RabbitMqOptions>
    {
        private const string SectionName = "RabbitMq";

        private readonly IConfiguration _configuration;

        public RabbitMqOptionSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(RabbitMqOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}