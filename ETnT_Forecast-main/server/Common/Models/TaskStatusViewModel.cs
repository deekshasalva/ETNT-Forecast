using System;

namespace Common.Models
{
    public class TaskStatusViewModel
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string FileName { get; set; }
        public string Errors { get; set; }
    }
}