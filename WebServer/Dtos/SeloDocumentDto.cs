﻿using WebServer.Models;

namespace WebServer.Dtos
{
    public class SeloDocumentDto
    {
        public Guid Id { get; set; }
        //public Guid? SeloFormId { get; set; }
        public string? KodNaselPunk { get; set; }
        public string? KodOblast { get; set; }
        public string? KodRaiona { get; set; }
        public string Login { get; set; } = string.Empty;
        public int Year { get; set; }
    }
}
