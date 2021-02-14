using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.BLL;

namespace Tasks.IntegrationTests.Base
{
    public class BaseIntegrationTest
    {
        public IMapper IMapper { get; private set; }

        public BaseIntegrationTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            IMapper = config.CreateMapper();
        }
    }
}
