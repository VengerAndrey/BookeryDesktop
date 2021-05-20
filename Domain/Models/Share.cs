using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Share
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
    }
}