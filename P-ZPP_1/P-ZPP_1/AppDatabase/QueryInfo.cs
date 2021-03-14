﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    public class QueryInfo
    {
        [Key]
        public int Id { get; set; }
        public string querry { get; set; }
        public DateTime date { get; set; }

        public QueryInfo(string querry, DateTime date)
        {
            this.querry = querry;
            this.date = date;
        }
    }
}