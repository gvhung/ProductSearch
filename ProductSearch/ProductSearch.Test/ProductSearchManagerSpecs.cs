﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductSearch.DataAccess.Repository;
using Rhino.Mocks;

namespace ProductSearch.Test
{
    [TestClass]
    public class ProductSearchManagerSpecs
    {
        [TestInitialize()]
        public void MyTestInitialize()
        {
            
        }

        [TestMethod]
        public void When_initializing_a_search_the_previous_search_should_be_cancelled()
        {
            var repo1 = MockRepository.GenerateMock<IProductSearchRepository>();
            var repo2 = MockRepository.GenerateMock<IProductSearchRepository>();
            var repo3 = MockRepository.GenerateMock<IProductSearchRepository>();



        }

        [TestMethod]
        public void When_the_most_recent_search_completes_it_should_post_results_and_previous_search_ignored()
        {

        }
    }
}
