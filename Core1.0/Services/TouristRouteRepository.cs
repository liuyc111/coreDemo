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

        public async Task AddTouristRouteasync(TouristRoute touristRoute)
        {
            await _context.AddAsync(touristRoute);
            await Saveasync();
        }

        public async Task<TouristRoutePicture> GetPictureasync(int pictureID)
        {
            return await _context.TouristRoutePictures.Where(o => o.Id.Equals(pictureID)).FirstAsync();
        }

        public async Task<IEnumerable<TouristRoutePicture>> GetPicturesByTouristRouteIDasync(Guid touristRouteID)
        {
            return await _context.TouristRoutePictures.Where(p => p.TouristRouteId.Equals(touristRouteID)).ToListAsync();
        }

        public async Task<TouristRoute> GetTouistRouteasync(Guid touristRouteID)
        {
            return await _context.TouristRoutes.Where(o => o.Id == touristRouteID).FirstAsync();
        }

        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesasync()
        {
            return await _context.TouristRoutes.ToListAsync();
        }

        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesasync(string keywords, string ratingOperator, int? rating)
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
            return await result.ToListAsync();
        }

        public async Task<bool> Saveasync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> TouristRouteExistsasync(Guid touristRouteID)
        {
            return await _context.TouristRoutes.AnyAsync(o => o.Id.Equals(touristRouteID));
        }




    }
}
