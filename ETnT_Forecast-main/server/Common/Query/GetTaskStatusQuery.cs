using System;
using Common.Models;
using MediatR;

namespace Common.Query
{
    public class GetTaskStatusQuery : IRequest<TaskStatusViewModel>
    {
        public GetTaskStatusQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}