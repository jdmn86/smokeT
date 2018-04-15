using System;
using System.Collections.Generic;
using System.Linq;
using SmokeT.Models;
using SQLite;
using Xamarin.Forms;

namespace SmokeT.Helpers
{
    public class CigarDatabseController
    {

        static object locker = new object();

        SQLiteConnection database;

        public CigarDatabseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Cigar>();
        }

        public Cigar GetCigar()
        {
            lock (locker)
            {
                if (database.Table<Cigar>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Cigar>().First();
                }
            }
        }

        public List<Cigar> GetCigars()
        {
            lock (locker)
            {
                if (database.Table<Cigar>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Cigar>().ToList();
                }
            }
        }


        public List<Cigar> GetCigarsByDay(DateTime date)
        {
            lock (locker)
            {
                if (database.Table<Cigar>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Cigar>().Where(x => DateTime.Compare(new DateTime(x.year,x.month,x.day), date) == 0).ToList();
                }
            }
        }









        public List<Cigar> GetCigarsByMonth()
        {
            lock (locker)
            {
                if (database.Table<Cigar>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    var lst = database.Table<Cigar>().GroupBy(x => new { x.year, x.month, x.day }).Select(k => new Cigar
                    {
                        year = k.Key.year ,
                        month = k.Key.month,
                        day = k.Key.day,
                        cost = k.Sum(x => x.cost),

                    }).ToList();
                    return lst;
                }
            }
        }









        public int SaveCigar(Cigar cigar){
            lock (locker)
            {
                if (cigar.Id!= 0)
                {
                    database.Update(cigar);
                    return cigar.Id;
                }
                else
                {
                    return database.Insert(cigar);
                }
            }   
        }
    }
}
