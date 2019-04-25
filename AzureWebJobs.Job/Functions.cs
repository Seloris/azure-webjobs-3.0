using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace AzureWebJobs.Job
{
    class Functions
    {
        public async Task DoThingsAsync([QueueTrigger("queue")] string message)
        {
            // TODO
            await Task.FromResult(message);
        }
    }
}
