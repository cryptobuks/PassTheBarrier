﻿using SQLite;

namespace PassTheBarier.Core.Data.Entities
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }
    }
}