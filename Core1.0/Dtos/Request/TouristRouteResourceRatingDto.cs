using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core1._0.Dtos.Request
{
    public class TouristRouteResourceRatingDto
    {
        //title检索条件
        public string KewWord { get; set; }


        public string RatingOperator { get; set; }

        public int? RatingValue { get; set; }

        private string _ratingValue;
        public string Rating
        {
            get { return _ratingValue; }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Regex regex
                        = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
                    var match = regex.Match(value);
                    if (match.Success)
                    {
                        RatingOperator = match.Groups[1].Value;
                        RatingValue = Int32.Parse(match.Groups[2].Value);
                    }
                }
                _ratingValue = value;
            }

        }
    }
}
