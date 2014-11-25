using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityTest
{
    public class GroupByExecutionMethodRenderer : AssertionListRenderer<CheckMethod>
    {
        protected override IEnumerable<IGrouping<CheckMethod, AssertionComponent>> GroupResult
            (IEnumerable<AssertionComponent> assertionComponents)
        {
            IEnumerable<CheckMethod> enumVals = Enum.GetValues(typeof (CheckMethod)).Cast<CheckMethod>();
            var pairs = new List<CheckFunctionAssertionPair>();

            foreach (CheckMethod checkMethod in enumVals)
            {
                IEnumerable<AssertionComponent> components =
                    assertionComponents.Where(c => (c.checkMethods & checkMethod) == checkMethod);
                IEnumerable<CheckFunctionAssertionPair> componentPairs =
                    components.Select(
                                      a => new CheckFunctionAssertionPair
                                           {
                                               checkMethod = checkMethod,
                                               assertionComponent = a
                                           });
                pairs.AddRange(componentPairs);
            }
            return pairs.GroupBy(pair => pair.checkMethod, pair => pair.assertionComponent);
        }

        #region Nested type: CheckFunctionAssertionPair

        private class CheckFunctionAssertionPair
        {
            public AssertionComponent assertionComponent;
            public CheckMethod checkMethod;
        }

        #endregion
    }
}