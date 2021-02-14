using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tasks.BLL;

namespace Tasks.UnitTests.Base
{
    public class BaseUnitTest
    {
        public IMapper IMapper { get; private set; }

        public BaseUnitTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            IMapper = config.CreateMapper();
        }
    }
}
