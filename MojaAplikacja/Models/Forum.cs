﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojaAplikacja.Models
{
    public class Forum
    {
        public string Title { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}