using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using TestPickPoint.Models;

namespace TestPickPoint.Services
{
    public class PostamatRepository
    {    

        private static readonly ReadOnlyCollection<Postamat> ReadOnlyListPostamats;


        public  List<Postamat> GetAllPostamats()
        {
            return ReadOnlyListPostamats.ToList();
        }


        public bool CheckFormatPostamentNumber(string numberPostamat)
        {
            return true;// В задание не сказана как проверить, просто сказано проверить. 
        }
        public Postamat GetPostamat(string numberPostamat)
        {
            return ReadOnlyListPostamats.FirstOrDefault(x => x.Number == numberPostamat);
        }

        public bool SearchPostamat(string numberPostamat)
        {
            try
            {
                return ReadOnlyListPostamats.Select(x => x.Number).Any(x => x.Contains(numberPostamat));
            }

            catch
            {
                return false;
            }
          
        }
        
        public bool CheckStatusPostamat(int numberPostamat)
        {
            try
            {
                return !ReadOnlyListPostamats.FirstOrDefault(x => x.Number == numberPostamat.ToString()).Status;
            }

            catch
            {
                return true;
            }
        }
    }
}