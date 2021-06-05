using Core1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.IServices
{
    public interface ITouristRouteRepository
    {
        IEnumerable<TouristRoute> GetTouristRoutes();
        TouristRoute GetTouistRoute(Guid touristRouteID);

        bool TouristRouteExists(Guid touristRouteID);

        IEnumerable<TouristRoutePicture> GetPicturesByTouristRouteID(Guid touristRouteID);

        TouristRoutePicture GetPicture(int pictureID);

        void AddTouristRoute(TouristRoute touristRoute);
        IEnumerable<TouristRoute> GetTouristRoutes(string keywords, string ratingOperator, int? rating);
        bool Save();
    }
}
