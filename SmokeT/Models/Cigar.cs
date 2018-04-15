using System;
using SQLite;

namespace SmokeT.Models
{
    public class Cigar
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int year{ get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public double cost { get; set;}

        public Cigar()
        {
        }
    }
}
