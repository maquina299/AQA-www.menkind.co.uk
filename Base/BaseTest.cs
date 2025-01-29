namespace www.menkind.co.uk.Tests
{
    public class BaseTest
    {
        protected static IWebDriver? _driver;
        public WebDriverWait? _wait;
        public void NavigateToUrl(string url) => _driver?.Navigate().GoToUrl(url);


        protected bool _isDriverInitialized = false;

        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected virtual ChromeOptions GetDriverOptions()
        {
            ChromeOptions options = new();
            options.AddArgument("--start-maximized");

            // Disable images by default (override in HomePageTests)
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);

            return options;
        }
        [SetUp]
        public void InitializeDriver()
        {
            if (!_isDriverInitialized)
            {
                _driver = new ChromeDriver(GetDriverOptions());
                _isDriverInitialized = true;
                _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5)); // Initialize _wait

            }
        }


        [TearDown]
        public void QuitDriver()
        {
            if (_driver != null)
        {
            _driver.Quit();
            _driver.Dispose();
            _driver = null;
                _isDriverInitialized = false;
        }
        }
    }
}
