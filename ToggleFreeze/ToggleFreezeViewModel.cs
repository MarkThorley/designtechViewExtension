using System;
using Dynamo.Core;
using Dynamo.Extensions;

namespace designtechViewExtension
{
    public class ToggleFreezeViewModel : NotificationObject, IDisposable
    {

        private ReadyParams readyParams;

        public ReadyParams ReadyParamType
        {
            get
            {
                readyParams = getReadyParams();
                return readyParams;
            }
        }

        public ReadyParams getReadyParams()
        {
            return readyParams;
        }

        public ToggleFreezeViewModel(ReadyParams p)
        {
            readyParams = p;
        }

        public void Dispose()
        {
        }
    }
}
