using System;
using MediatR;

namespace Common.Commands
{
    public class DeleteForecastCommand : IRequest<bool>
    {
        public DeleteForecastCommand(Guid id, int year)
        {
            Id = id;
            Year = year;
        }

        public Guid Id { get; set; }
        public int Year { get; set; }
    }
}