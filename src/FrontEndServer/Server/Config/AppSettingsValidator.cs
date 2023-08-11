namespace FrontEndServer.Server.Config
{
    using Microsoft.Extensions.Options;
    using System.Linq;
    using System.Collections.Generic;

    public class AppSettingsValidator : IValidateOptions<AppSettings>
    {
        // First parameter "name" is used to differentiate validation options by name
        // For example: Development and Production, but in this case it's not needed so just use null
        public ValidateOptionsResult Validate(string? name, AppSettings options)
        {
            var errors = new List<string>();

            // Validate RabbitMQ settings
            if (options.RabbitMQ == null)
            {
                errors.Add("RabbitMQ settings are missing.");
            }
            else
            {
                if (string.IsNullOrEmpty(options.RabbitMQ.HostName))
                {
                    errors.Add("RabbitMQ HostName is missing.");
                }

                if (options.RabbitMQ.RabbitQueues == null || !options.RabbitMQ.RabbitQueues.Any())
                {
                    errors.Add("RabbitMQ queues are missing.");
                }
                else
                {
                    foreach (var queue in options.RabbitMQ.RabbitQueues)
                    {
                        if (string.IsNullOrEmpty(queue.Name))
                        {
                            errors.Add("A RabbitMQ queue name is missing.");
                        }
                    }
                }
            }

            // Validate ConnectionStrings settings
            if (options.ConnectionStrings == null)
            {
                errors.Add("ConnectionStrings settings are missing.");
            }
            else
            {
                if (string.IsNullOrEmpty(options.ConnectionStrings.DefaultConnection))
                {
                    errors.Add("DefaultConnection string is missing.");
                }
            }

            if (errors.Any())
            {
                return ValidateOptionsResult.Fail(errors);
            }

            return ValidateOptionsResult.Success;
        }
    }

}
