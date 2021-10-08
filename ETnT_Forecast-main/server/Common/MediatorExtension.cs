using System.Threading;
using Hangfire;
using MediatR;

namespace Common
{
    public static class MediatorExtension
    {
        public static void Enqueue(this IMediator mediator, INotification notification)
        {
            var client = new BackgroundJobClient();
            client.Enqueue<IMediator>(m => m.Publish(notification, new CancellationToken()));
        }
    }
}