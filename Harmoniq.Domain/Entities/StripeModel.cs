using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class StripeModel
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
        public string WebhookSecret { get; set; }
        public string CartWebhookSecret { get; set; }
    }
}