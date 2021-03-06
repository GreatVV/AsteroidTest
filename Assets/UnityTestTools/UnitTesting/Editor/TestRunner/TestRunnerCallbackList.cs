using System;
using System.Collections.Generic;

namespace UnityTest.UnitTestRunner
{
    public class TestRunnerCallbackList : ITestRunnerCallback
    {
        private readonly List<ITestRunnerCallback> m_CallbackList = new List<ITestRunnerCallback>();

        #region ITestRunnerCallback Members

        public void TestStarted(string fullName)
        {
            foreach (ITestRunnerCallback unitTestRunnerCallback in m_CallbackList)
            {
                unitTestRunnerCallback.TestStarted(fullName);
            }
        }

        public void TestFinished(ITestResult fullName)
        {
            foreach (ITestRunnerCallback unitTestRunnerCallback in m_CallbackList)
            {
                unitTestRunnerCallback.TestFinished(fullName);
            }
        }

        public void RunStarted(string suiteName, int testCount)
        {
            foreach (ITestRunnerCallback unitTestRunnerCallback in m_CallbackList)
            {
                unitTestRunnerCallback.RunStarted(suiteName, testCount);
            }
        }

        public void RunFinished()
        {
            foreach (ITestRunnerCallback unitTestRunnerCallback in m_CallbackList)
            {
                unitTestRunnerCallback.RunFinished();
            }
        }

        public void RunFinishedException(Exception exception)
        {
            foreach (ITestRunnerCallback unitTestRunnerCallback in m_CallbackList)
            {
                unitTestRunnerCallback.RunFinishedException(exception);
            }
        }

        #endregion

        public void Add(ITestRunnerCallback callback)
        {
            m_CallbackList.Add(callback);
        }

        public void Remove(ITestRunnerCallback callback)
        {
            m_CallbackList.Remove(callback);
        }
    }
}