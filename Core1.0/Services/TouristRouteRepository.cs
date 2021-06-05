using Core1._0.Dbcontext;
using Core1._0.IServices;
using Core1._0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.Services
{
    public class TouristRouteRepository : ITouristRouteRepository
    {

        private readonly Appdbcontext _context;

        public TouristRouteRepository(Appdbcontext appdbcontext)
        {
            _context = appdbcontext;
        }

        public void AddTouristRoute(TouristRoute touristRoute)
        {
            _context.Add(touristRoute);
            Save();
        }

        public TouristRoutePicture GetPicture(int pictureID)
        {
            return _context.TouristRoutePictures.Where(o => o.Id.Equals(pictureID)).FirstOrDefault();
        }

        public IEnumerable<TouristRoutePicture> GetPicturesByTouristRouteID(Guid touristRouteID)
        {
            return _context.TouristRoutePictures.Where(p => p.TouristRouteId.Equals(touristRouteID)).ToList();
        }

        public TouristRoute GetTouistRoute(Guid touristRouteID)
        {
            return _context.TouristRoutes.Where(o => o.Id == touristRouteID).FirstOrDefault();
        }

        public IEnumerable<TouristRoute> GetTouristRoutes()
        {
            return _context.TouristRoutes.ToList();
        }

        public IEnumerable<TouristRoute> GetTouristRoutes(string keywords, string ratingOperator, int? rating)
        {
            IQueryable<TouristRoute> result = _context.TouristRoutes.Include(testc => testc.Pictures);
            if (!string.IsNullOrEmpty(keywords))
            {
                result = result.Where(t => t.Title.Contains(keywords));
            }
            if (rating >= 0)
            {
                result = ratingOperator switch
                {
                    "largerThan" => result.Where(t => t.Rating >= rating),
                    "lessThan" => result.Where(t => t.Rating <= rating),
                    _ => result.Where(t => t.Rating.Equals(rating))
                };
            }
            return result.ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool TouristRouteExists(Guid touristRouteID)
        {
            return _context.TouristRoutes.Any(o => o.Id.Equals(touristRouteID));
        }




    }
}
