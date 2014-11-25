namespace UnityTest
{
    public class IntegrationTestsRunnerSettings : ProjectSettingsBase
    {
        public bool addNewGameObjectUnderSelectedTest;
        public bool blockUIWhenRunning = true;
        public string filterString;
        public bool showAdvancedFilter;
        public bool showFailedTest = true;
        public bool showIgnoredTest = true;
        public bool showNotRunnedTest = true;
        public bool showOptions;
        public bool showSucceededTest = true;
    }
}