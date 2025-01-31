﻿using www.menkind.co.uk.Pages;

namespace www.menkind.co.uk.Tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Product Page")]
    [Obsolete]
    public class ProductPageTests
    {
        private BasePage? _basePage;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [SetUp]
        public void SetUp()
        {
            _basePage = new BasePage(false);
            _basePage.NavigateToUrl(TestData.ProductPageURL);
            _basePage.HandleModals();
        }

        [Test]
        [Category("Smoke")]
        [AllureSubSuite("Add to Cart")]
        public void AddItemToCart_ShouldUpdateCartIcon()
        {
            var productPage = new ProductPageObject();

            Logger.Debug("Starting test: AddItemToCart_ShouldUpdateCartIcon");

            // Step 1: Add item to basket
            productPage.AddToBasket();

            // Step 2: Verify cart icon updates
            Assert.That(productPage.IsCartUpdated(), Is.True, "Cart icon did not update to show 1 item.");

            Logger.Debug("Test passed: Cart icon updated successfully.");
        }

        [TearDown]
        public void TearDown()
        {
            BasePage.QuitDriver();
        }
    }
}
