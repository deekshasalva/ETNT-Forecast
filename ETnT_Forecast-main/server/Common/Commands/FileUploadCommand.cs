using System;
using MediatR;

namespace Common.Commands
{
    public class FileUploadCommand : INotification
    {
        public FileUploadCommand(string fileName)
        {
            FileName = fileName;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string FileName { get; set; }
    }
}