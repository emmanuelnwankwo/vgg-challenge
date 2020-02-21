using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Dtos
{
    public class ProjectResourceParameters
    {
        const int maxPageSize = 20;
        private int _pageSize = 10;
        public int Offset { get; set; } = 1; // PageNumber
        public int Limit { // PageSize
            get 
            {
                return _pageSize;
            }
            set 
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public string Search { get; set; }
    }
}
