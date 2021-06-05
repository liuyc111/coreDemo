using Core1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.IServices
{
    public interface ITouristRouteRepository
    {
        Task<IEnumerable<TouristRoute>> GetTouristRoutesasync();
        Task<TouristRoute> GetTouistRouteasync(Guid touristRouteID);

        Task<bool> TouristRouteExistsasync(Guid touristRouteID);

        Task<IEnumerable<TouristRoutePicture>> GetPicturesByTouristRouteIDasync(Guid touristRouteID);

        Task<TouristRoutePicture> GetPictureasync(int pictureID);

        Task AddTouristRouteasync(TouristRoute touristRoute);
        Task<IEnumerable<TouristRoute>> GetTouristRoutesasync(string keywords, string ratingOperator, int? rating);
        Task<bool> Saveasync();
    }
}
