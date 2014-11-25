using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityTest.UnitTestRunner;

namespace UnityTest
{
    public partial class UnitTestView
    {
        private void UpdateTestInfo(ITestResult result)
        {
            FindTestResult(result.Id).Update(result, false);
        }

        private UnitTestResult FindTestResult(string resultId)
        {
            int idx = m_ResultList.FindIndex(testResult => testResult.Id == resultId);
            if (idx == -1)
            {
                Debug.LogWarning("Id not found for test: " + resultId);
                return null;
            }
            return m_ResultList.ElementAt(idx);
        }

        private void RunTests()
        {
            var filter = new TestFilter();
            string[] categories = GetSelectedCategories();
            if (categories != null && categories.Length > 0)
            {
                filter.categories = categories;
            }
            RunTests(filter);
        }

        private void RunTests(TestFilter filter)
        {
            if (m_Settings.runTestOnANewScene)
            {
                if (m_Settings.autoSaveSceneBeforeRun)
                {
                    EditorApplication.SaveScene();
                }
                if (!EditorApplication.SaveCurrentSceneIfUserWantsTo())
                {
                    return;
                }
            }

            string currentScene = null;
            int undoGroup = -1;
            if (m_Settings.runTestOnANewScene)
            {
                currentScene = OpenNewScene();
            }
            else
            {
                undoGroup = RegisterUndo();
            }

            StartTestRun(filter, new TestRunnerEventListener(UpdateTestInfo));

            if (m_Settings.runTestOnANewScene)
            {
                LoadPreviousScene(currentScene);
            }
            else
            {
                PerformUndo(undoGroup);
            }
        }

        private string OpenNewScene()
        {
            string currentScene = EditorApplication.currentScene;
            if (m_Settings.runTestOnANewScene)
            {
                EditorApplication.NewScene();
            }
            return currentScene;
        }

        private void LoadPreviousScene(string currentScene)
        {
            if (!string.IsNullOrEmpty(currentScene))
            {
                EditorApplication.OpenScene(currentScene);
            }
            else
            {
                EditorApplication.NewScene();
            }

            if (Event.current != null)
            {
                GUIUtility.ExitGUI();
            }
        }

        public void StartTestRun(TestFilter filter, ITestRunnerCallback eventListener)
        {
            var callbackList = new TestRunnerCallbackList();
            if (eventListener != null)
            {
                callbackList.Add(eventListener);
            }
            k_TestEngine.RunTests(filter, callbackList);
        }

        private static int RegisterUndo()
        {
#if UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
            Undo.RegisterSceneUndo("UnitTestRunSceneSave");
            return -1;
#else
            return Undo.GetCurrentGroup();
#endif
        }

        private static void PerformUndo(int undoGroup)
        {
            EditorUtility.DisplayProgressBar("Undo", "Reverting changes to the scene", 0);
            DateTime undoStartTime = DateTime.Now;
#if UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2
            Undo.PerformUndo();
#else
            Undo.RevertAllDownToGroup(undoGroup);
#endif
            if ((DateTime.Now - undoStartTime).Seconds > 1)
            {
                Debug.LogWarning(
                                 "Undo after unit test run took " + (DateTime.Now - undoStartTime).Seconds +
                                 " seconds. Consider running unit tests on a new scene for better performance.");
            }
            EditorUtility.ClearProgressBar();
        }

        [MenuItem("Unity Test Tools/Unit Test Runner %#&u")]
        public static void ShowWindow()
        {
            GetWindow(typeof (UnitTestView)).Show();
        }

        #region Nested type: TestRunnerEventListener

        public class TestRunnerEventListener : ITestRunnerCallback
        {
            private readonly Action<ITestResult> m_UpdateCallback;

            public TestRunnerEventListener(Action<ITestResult> updateCallback)
            {
                m_UpdateCallback = updateCallback;
            }

            #region ITestRunnerCallback Members

            public void TestStarted(string fullName)
            {
                EditorUtility.DisplayProgressBar("Unit Tests Runner", fullName, 1);
            }

            public void TestFinished(ITestResult result)
            {
                m_UpdateCallback(result);
            }

            public void RunStarted(string suiteName, int testCount)
            {}

            public void RunFinished()
            {
                EditorUtility.ClearProgressBar();
            }

            public void RunFinishedException(Exception exception)
            {
                RunFinished();
            }

            #endregion
        }

        #endregion
    }

    public class TestFilter
    {
        public static TestFilter Empty = new TestFilter();
        public string[] categories;
        public string[] names;
        public object[] objects;
    }
}