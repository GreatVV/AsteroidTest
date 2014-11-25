namespace UnityTest
{
    public class UnitTestsRunnerSettings : ProjectSettingsBase
    {
        public bool autoSaveSceneBeforeRun;
        public int categoriesMask;
        public bool filtersFoldout;
        public bool horizontalSplit = true;
        public bool optionsFoldout;
        public bool runOnRecompilation;
        public bool runTestOnANewScene;

        public bool showFailed = true;
        public bool showIgnored = true;
        public bool showNotRun = true;
        public bool showSucceeded = true;
        public string testFilter = "";
    }
}